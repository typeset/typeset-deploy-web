using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Typeset.Deploy.Web.Models.Deploy
{
    public class AppHarbor
    {
        [Required]
        [Display(Name = "build url")]
        public string BuildUrl { get; set; }
    }
}