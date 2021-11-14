using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication4MVC.Models
{
    public class Product_Availability_Detail
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Availability Id cant be null")]
        public int Id { get; set; }

        [Display(Name = "ProductAvlId")]
        [Required(ErrorMessage = "ProductAvlId cant be null")]
        public int ProductAvlId { get; set; }

        [Display(Name = "ProductName")]
        [Required(ErrorMessage = "ProductId cant be empty")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Display(Name = "BrandName")]
        [Required(ErrorMessage = "BrandName cant be empty")]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

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


    }
}