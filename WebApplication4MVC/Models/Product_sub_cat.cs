using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication4MVC.Models
{
    public class Product_sub_cat
    {

        [Display(Name = "SubCatId")]
        public int Id { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "select  Product Category Name")]
        public int ProductCatId { get; set; }
        public string CategoryName { get; set; }

       

        [Required(ErrorMessage = "provide a Product Sub Category Name")]
        [Display(Name = "Sub Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "provide  Product Sub Category Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }



        [Display(Name = "SortName")]
        public string SortName { get; set; }


        [Required(ErrorMessage = "provide a Product Sub Category Unique Code")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "provide a Product SubCategory Image")]
        [Display(Name = "SubCategoryImage")]
        public string SubImg { get; set; }


        [Display(Name = "Status")]
        public bool Status { get; set; }


        [Display(Name = "ModifyBy")]
        public int Modifyby { get; set; }


        [Display(Name = "ModifyByDate")]
        public DateTime Modifybydate { get; set; }


        public IEnumerable<SelectListItem> list_Product_cat { get; set; }

    }
}