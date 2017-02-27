using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChooseYourStyle.Models
{
    public class Style
    {

        public int StyleId { get; set; }

        [Required]
        public string Title { get; set; }

    }
}