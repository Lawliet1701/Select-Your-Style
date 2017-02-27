using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseYourStyle.Models
{
    public class Summary
    {
        public string Login { get; set; }
        public string MainStyle { get; set; }
        public Dictionary<string, int> Styles { get; set; }

    }
}