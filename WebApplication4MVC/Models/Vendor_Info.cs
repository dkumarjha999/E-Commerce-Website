using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication4MVC.Models
{
    public class Vendor_Info
    {

       
        [Display(Name = "Vendor Id")]
        [Required(ErrorMessage = "Vendor Id cant be null")]
        public int VendorId { get; set; }

        [Display(Name = "Vendor Name")]
        [Required(ErrorMessage = "provide A Vendor Name")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]  
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "provide a Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }


        public bool MobileVerification { get; set; }


        public bool EmailVerification { get; set; }

    
        [Display(Name = "Country")]
        [Required(ErrorMessage = "select  Vendor Country")]
        public int Country { get; set; }
        public string CountryName{ get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "select  Vendor State")]
        public int State { get; set; }
        public string StateName { get; set; }
       
     
        [Display(Name = "City")]
        [Required(ErrorMessage = "select  Vendor City")]
        public int City { get; set; }
        public string CityName { get; set; }

        [Display(Name = "PinCode")]
        [Required(ErrorMessage = "Provide  Vendor PinCode")]
        public string PinCode { get; set; }

        [Display(Name = "GSTIN No")]
        [Required(ErrorMessage = "Provide  Vendor GSTIN")]
        public string GSTIN { get; set; }

        [Display(Name = "PanCardNumber")]
        [Required(ErrorMessage = "Provide PanCardNumber")]
        public string PanCardNumber { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "provide Address")]
        public string Address { get; set; }

        public List<SelectListItem> list_City_Name { get; set; }


    }
}