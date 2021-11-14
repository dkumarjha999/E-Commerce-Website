using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4MVC.Models
{
    public class Product_Availability
    {


        [Display(Name = "Id")]
        [Required(ErrorMessage = " Id cant be null")]
        public int Id { get; set; }


        [Display(Name = "Vendor Name")]
        [Required(ErrorMessage = "Vendor Name(Id) cant be null")]
        public int VendorId { get; set; }
        public string Ven_name{ get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Provide  Description")]
        public string Description { get; set; }

        [Display(Name = "AvailabilityDate")]
        [Required(ErrorMessage = "AvailabilityDate cant be null")]
        public DateTime AvailabilityDate { get; set; }


       
 
    }
}