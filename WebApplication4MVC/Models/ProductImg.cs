using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4MVC.Models
{
    public class ProductImg
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Active Status")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "ProductName")]
        public int ProductId { get; set; }
        public string ProductName{ get; set; }

        [Display(Name="ImageName")]
        public string ImgPath { get; set; }


        [Display(Name = "ProductAvlId")]
        [Required(ErrorMessage = "ProductAvlId cant be null")]
        public int ProductAvlId { get; set; }

         [Display(Name = "BrandName")]
        [Required(ErrorMessage = "BrandName cant be empty")]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        [Display(Name = "CategoryName")]
        [Required(ErrorMessage = "CategoryName cant be empty")]      
        public string CategoryName { get; set; }

        [Display(Name = "SubCategoryName")]
        [Required(ErrorMessage = "SubCategoryName cant be empty")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity cant be null")]
        public int Quantity { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost cant be null")]
        public decimal Cost { get; set; }

        [Display(Name = "Discount")]
        [Required(ErrorMessage = "provide Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "MfgDate")]
        [Required(ErrorMessage = "MfgDate cant be empty")]
        public string MfgDate { get; set; }

        [Display(Name = "ExpDate")]
        [Required(ErrorMessage = "ExpDate cant be empty")]
        public string ExpDate { get; set; }

        [Display(Name = "BestBefore")]
        [Required(ErrorMessage = "BestBefore cant be empty")]
        public string BestBefore { get; set; }

  
        [Display(Name = "Vendor Name")]
        [Required(ErrorMessage = "Vendor Name(Id) cant be null")]
        public int VendorId { get; set; }
        public string Ven_name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Provide  Description")]
        public string Description { get; set; }

        [Display(Name = "AvailabilityDate")]
        [Required(ErrorMessage = "AvailabilityDate cant be null")]
        public DateTime AvailabilityDate { get; set; }




        public List<SelectListItem> list_Product_Info_Detail { get; set; }
        public List<SelectListItem> list_Product_Availability_Detail { get; set; }
        public List<SelectListItem> list_Product_Availability { get; set; }
      

    }

}