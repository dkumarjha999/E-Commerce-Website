using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4MVC.Models
{
    public class Product_cat
    {
         
         [Display(Name="Product_cat_Id")]
         public int Id { get; set; }
 
          [Required(ErrorMessage="provide a Product Category Name")]
          [Display(Name = "Name")]
         public string Name { get; set; }

           [Required(ErrorMessage = "provide  Product Category Description")]
           [Display(Name = "Description")]
         public string Description { get; set; }



           [Display(Name = "SortName")]
         public string SortName { get; set; }


           [Required(ErrorMessage = "provide a Product Category Unique Code")]
           [Display(Name = "Code")]
         public string Code { get; set; }

         [Required(ErrorMessage = "provide a Product Category Image")]
           [Display(Name = "CategoryImage")]
           public string Img { get; set; }

           [Display(Name = "Status")]
         public bool Status { get; set; }


           [Display(Name = "ModifyBy")]
         public int Modifyby { get; set; }


           [Display(Name = "ModifyByDate")]
          public DateTime Modifybydate { get; set; }

    }
}