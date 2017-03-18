using ChooseYourStyle.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ChooseYourStyle.Helpers
{
    public class ImageHelper
    {
        private List<string> styles = new List<string>()
        {
            "American", "Asian", "Classic", "Country", "Eclectic", "Industrial", "Maritime", "Mediterranean",
            "Modern", "Retro", "Rustic", "Scandinavian", "Tropical"
        };

        private List<int> selectedImages = new List<int>() { 0 };

        private Dictionary<string, int> counterStyle;

        private string[] maxStyles = new string[2];

        private  Random random = new Random();

        public int step { get; set; } = 0;

        public  readonly int MAX_STEPS = 10;


        public ImageHelper()
        {
            counterStyle = styles.ToDictionary(x => x, x => 0);
         }

        //get 2 images with random styles
        public List<Image> GetRandomImages()
        {
            List<Image> list = new List<Image>();

            int firstStyle = -1;

            for (int i = 0; i < 2; i++)
            {
                Image image = new Image();
                int rStyle = 0;
                int rImage = 0;

                // check duplicate styles (this step) and images (all steps)
                while (firstStyle == rStyle || selectedImages.Contains(rStyle * 10 + rImage))
                {
                    rStyle = random.Next(styles.Count);
                    rImage = random.Next(MAX_STEPS) + 1;
                }

                selectedImages.Add(rStyle * 10 + rImage);
                firstStyle = rStyle;

                image.style = styles[rStyle];
                image.path = "/Content/images/styles/" + image.style + "/" + image.style.ToLower() + rImage + ".jpg";

                list.Add(image);
            }

            return list;
        }

        //get 2 images with certain styles on additional step
        public List<Image> GetLastImages()
        {
            List<Image> list = new List<Image>();

            for (int i = 0; i < 2; i++)
            {
                Image image = new Image();

                int rStyle = styles.IndexOf(maxStyles[i]);

                int rImage = 0;

                do
                {
                    rImage = random.Next(10) + 1;
                } while (selectedImages.Contains(rStyle * 10 + rImage));

                image.style = styles[rStyle];
                image.path = "/Content/images/styles/" + image.style + "/" + image.style.ToLower() + rImage + ".jpg";

                list.Add(image);

            }

            return list;

        }

        public void IncreaseStyleCounter(string style)
        {
            if (style != "")
            {
                counterStyle[style]++;
            }
        }

        //search max style (or styles)
        public bool CheckAllSelections()
        {
            int max = -1;
            bool equalMaxValue = false;

            foreach (KeyValuePair<string, int> kvp in counterStyle)
            {

                if (kvp.Value > max)
                {
                    max = kvp.Value;
                    maxStyles[0] = kvp.Key;
                    equalMaxValue = false;
                }
                else if (kvp.Value == max)
                {
                    maxStyles[1] = kvp.Key;
                    equalMaxValue = true;
                }
            }

            return equalMaxValue;
        }

        public int GetMaxStyleId()
        {
            return GetIdStyle(maxStyles[0]);

        }

        public string GetMaxStyle()
        {
            return maxStyles[0];
        }

        public int GetIdStyle(string style)
        {
            return styles.IndexOf(style);
        }

        public Dictionary<string, int> GetAllStyleCounters()
        {
            return counterStyle;
        }

    }
}