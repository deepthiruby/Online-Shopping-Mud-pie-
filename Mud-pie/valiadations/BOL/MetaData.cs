using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace valiadations.Models
{
    public class MetaData
    {


        public int ContactID { get; set; }
        [Display(Name = "*Your Name")]
        [Required(ErrorMessage = "Please Enter Your Name")]
        [StringLength(50)]
        [RegularExpression(@"([a-zA-Z\d]+[\w\d]*|)[a-zA-Z]+[\w\d.]*", ErrorMessage = "Invalid username")]
        public string Name { get; set; }



        [Display(Name = "*Your Email Address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
       ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }




        [Display(Name = "Your Phone Number")]

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Please Enter the Message")]
        [StringLength(150)]
        [RegularExpression(@"^(.|\s)*[a - zA - Z]+(.|\s)*$", ErrorMessage = "Please Enter the Message")]
        [Display(Name = "*Your Message")]
        public string Message { get; set; }

    }
}