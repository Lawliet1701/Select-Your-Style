using ChooseYourStyle.Helpers;
using ChooseYourStyle.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;
using ChooseYourStyle.Infrastructure;

namespace ChooseYourStyle.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login)
        {
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["Login"] = login ;
            System.Web.HttpContext.Current.Application.UnLock();
            return RedirectToAction("Main");
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] //to prevent incorrect work by click prev/next page in browser
        public ActionResult Main()
        {
            Debug.WriteLine("INSE");
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["ImageHelper"] = new ImageHelper();
            System.Web.HttpContext.Current.Application.UnLock();
            return View();
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Result()
        {
            bool completed = false;

            if (System.Web.HttpContext.Current.Application["Completed"] != null)
            {
                completed = (bool)System.Web.HttpContext.Current.Application["Completed"];
            }

            if (completed)
            {
                ImageHelper imageHelper = (ImageHelper)System.Web.HttpContext.Current.Application["ImageHelper"];

                Dictionary<string, int> nonZeroStyles = new Dictionary<string, int>();

                Summary summary = new Summary()
                {
                    Login = (string)System.Web.HttpContext.Current.Application["Login"],
                    MainStyle = imageHelper.getMaxStyle(),
                    Styles = imageHelper.getAllStyleCounters().Where(p => p.Value > 0)
                               .OrderByDescending(p => p.Value)
                               .ToDictionary(p => p.Key, p => p.Value),
                };

                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["Completed"] = false;
                System.Web.HttpContext.Current.Application.UnLock();

                return View(summary);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Results()
        {

            List<Result> resultList;

            using (DatabaseContext db = new DatabaseContext())
            {
                var results = db.Results.Include(r => r.Style);
                resultList = results.ToList<Result>();

            }
            return View(resultList);
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]  
        [AjaxOnly]
        public ActionResult GetImages()
        {
            ImageHelper imageHelper = (ImageHelper)System.Web.HttpContext.Current.Application["ImageHelper"];

            string selectedStyle = this.Request.QueryString["selectedStyle"];

            imageHelper.IncreaseStyleCounter(selectedStyle);

            imageHelper.step++;

            List<Image> list = imageHelper.getRandomImages();

            return Json(new { imageHelper.step, Images = list }, JsonRequestBehavior.AllowGet);
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        [AjaxOnly]
        public ActionResult CheckResult()
         {
            ImageHelper imageHelper = (ImageHelper)System.Web.HttpContext.Current.Application["ImageHelper"];

            string selectedStyle = this.Request.QueryString["selectedStyle"];
            imageHelper.IncreaseStyleCounter(selectedStyle);

            if (imageHelper.checkAllSelections())       // if true -> go to step 11
             {
                imageHelper.step++;
                List<Image> list = imageHelper.getLastImages();
                return Json(new { imageHelper.step, Images = list }, JsonRequestBehavior.AllowGet);
            }
             else                                       // if false -> save result in DB and go to result page
             {
                 ResultHelper rh = new ResultHelper();
                 rh.saveResult(imageHelper.getMaxStyleId());

                 System.Web.HttpContext.Current.Application["Completed"] = true;

                 return Json(new {Action = "/Home/Result"}, JsonRequestBehavior.AllowGet);
            }
    
    }

    }
}