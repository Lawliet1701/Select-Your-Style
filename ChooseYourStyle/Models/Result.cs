using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChooseYourStyle.Models
{
    public class Result
    {

        public int ResultId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Login { get; set; }

        public DateTime Date { get; set; }

        public int StyleId { get; set; }

        public virtual Style Style { get; set; }

    }
}