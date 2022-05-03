using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudApp.DataModel
{
    public class UserMasterModel:CommonModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression("^([a-zA-Z0-9 .&'-_]+)$", ErrorMessage = "Invalid Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression("^([0-9]+)$", ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        public string Mobile { get; set; }
    }
}