using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChooseYourStyle.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {

        protected override void Seed(DatabaseContext context)
        {
            context.Styles.Add(new Style() { StyleId = 1, Title = "American"});
            context.Styles.Add(new Style() { StyleId = 2, Title = "Asian" });
            context.Styles.Add(new Style() { StyleId = 3, Title = "Classic" });
            context.Styles.Add(new Style() { StyleId = 4, Title = "Country" });
            context.Styles.Add(new Style() { StyleId = 5, Title = "Eclectic" });
            context.Styles.Add(new Style() { StyleId = 6, Title = "Industrial" });
            context.Styles.Add(new Style() { StyleId = 7, Title = "Maritime" });
            context.Styles.Add(new Style() { StyleId = 8, Title = "Mediterranean" });
            context.Styles.Add(new Style() { StyleId = 9, Title = "Modern" });
            context.Styles.Add(new Style() { StyleId = 10, Title = "Retro" });
            context.Styles.Add(new Style() { StyleId = 11, Title = "Rustic" });
            context.Styles.Add(new Style() { StyleId = 12, Title = "Scandinavian" });
            context.Styles.Add(new Style() { StyleId = 13, Title = "Tropical" });


            for (int i = 0; i < 10; i++)
            {
                context.Results.Add(new Result() { ResultId = 1, Login = "User"+i, Date = DateTime.Now, StyleId = (i%13+1) });
            }

            base.Seed(context);
        }
    }
}