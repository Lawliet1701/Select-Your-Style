using ChooseYourStyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseYourStyle.Helpers
{
    public class ResultHelper
    {
        public void SaveResult(int styleId)
        {
            Result result = new Result()
            {
                Login = System.Web.HttpContext.Current.Application["Login"] as string,
                Date = DateTime.Now,
                StyleId = styleId + 1,
            };

            using (var dbContext = new DatabaseContext())
            {
                dbContext.Results.Add(result);
                dbContext.SaveChanges();
            }

        }
    }
}