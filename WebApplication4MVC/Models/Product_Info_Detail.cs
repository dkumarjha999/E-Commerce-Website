using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4MVC.Models
{
    public class Product_Info_Detail
    {

          [Display(Name = "Main Info Detail Id")]
         public int Id { get; set; }

         [ForeignKey("Id")]
         [Display(Name = "ProductCat")]
         [Required(ErrorMessage = "provide a Product Category Name")]
         public int Product_cat_id { get; set; }
         public string CategoryName { get; set; }

          [ForeignKey("Id")]
        [Display(Name = "ProductSubCat")]
        [Required(ErrorMessage = "provide a Product sub Category Name")]
        public int Product_sub_cat_id { get; set; }
        public string SubCategoryName { get; set; }

        
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "provide a Product info detail  Category Unique Code")]
        [Display(Name = "Code")]
        public string Product_Code{ get; set; }

        [Display(Name = "BarCode")]
        public string Bar_Code{ get; set; }


        [Display(Name = "Status")]
        public bool Status { get; set; }


        [Display(Name = "ModifyBy")]
        public int Modifyby { get; set; }


        [Display(Name = "ModifyByDate")]
        public DateTime Modifybydate { get; set; }


        [Required(ErrorMessage = "Please Fill  Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        public IEnumerable<SelectListItem> list_Product_cat { get; set; }
        public IEnumerable<SelectListItem> list_Product_sub_cat { get; set; }
    }
}