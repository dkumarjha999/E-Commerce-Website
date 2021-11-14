using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4MVC.Models
{
    public class Vendor_Bank_Info
    {

        [Display(Name = "Vendor Bank Id")]
        [Required(ErrorMessage = "Vendor bank  Id cant be null")]
        public int Id { get; set; }

         [ForeignKey("VendorId")]
         [Display(Name = "Vendor Name")]
         [Required(ErrorMessage = "please select Vendor Name")]
          public int VendorId { get; set; }
         public string VendorName { get; set; }



        [Display(Name = "Bank Name")]
        [Required(ErrorMessage = "select  Vendor BankName")]
          public string BankName { get; set; }


        [Display(Name = "IFSCCode")]
        [Required(ErrorMessage = "Provide Vendor Bank IFSCCode")]
         public string IFSCCode { get; set; }


        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Provide Bank Branch")]
          public string Branch { get; set; }


        [Display(Name = "AccountNo")]
        [Required(ErrorMessage = "Provide Bank AccountNo")]
          public string AccountNo { get; set; }


        public IEnumerable<SelectListItem> list_Vendor_Info { get; set; }

    }
}
