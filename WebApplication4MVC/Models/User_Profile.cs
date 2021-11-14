using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication4MVC.Models
{
    public class User_Profile
    {
         [Key]
         public int Uid { get;set;}
 
         [Required (ErrorMessage="please Enter User Name")]
         [Display(Name = "UserName")]
         public string UserName { get; set; }


         public string Address { get; set; }


         [Display(Name = "Country")]       
         public int Country { get; set; }
         public string CountryName { get; set; }

         [Display(Name = "State")]       
         public int State { get; set; }
         public string StateName { get; set; }


         [Display(Name = "City")]       
         public int City { get; set; }
         public string CityName { get; set; }

         [Required(ErrorMessage = "You must provide a phone number")]
         [Display(Name = "Home Phone")]
         [DataType(DataType.PhoneNumber)]
         [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
         public string PhoneNo { get; set; }

         //[Required(ErrorMessage = "You must provide a phone number")]
         //[Display(Name = "Home Phone")]
         //[DataType(DataType.PhoneNumber)]
         //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Your PhoneNo And Confirm PhoneNo Not Matched")]
         //[System.Web.Mvc.CompareAttribute("PhoneNo")]
         //public string ConfPhoneNo { get; set; }

        
         [DataType(DataType.EmailAddress)]
         [EmailAddress]
         [Required(ErrorMessage = "please Enter Your Email Address")]  
         public string Email { get; set; }

       
         //[DataType(DataType.EmailAddress)]
         //[EmailAddress]
         //[Required(ErrorMessage = "Your Email And Confirm Email Not Matched")]
         //[System.Web.Mvc.CompareAttribute("Email")]
         //public string ConfEmail { get; set; }

         [Required(ErrorMessage = "please Provide RegStatus")]
         [Display(Name = "RegStatus")]
         public bool RegStatus { get; set; }

        [DataType(DataType.Password)]
         [Required(ErrorMessage = "please Provide User Password")]
         [Display(Name = "UserPassword")]
         public string UserPassword { get; set; }

        [DataType(DataType.Password)]
         [Required(ErrorMessage = "Your Password And Confirm Password Not Matched")]
         [System.Web.Mvc.CompareAttribute("UserPassword")]
         public string ConfUserPassword { get; set; }

         public string Img { get; set; }

         public List<SelectListItem> list_City_Name { get; set; }

    }
}