using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication4MVC.Models
{
    public class Product_Brand
    {

         [Required(ErrorMessage = " Product_Brand Id")]
         [Display(Name = "Product_Brand_Id")]
         public int Id { get; set; }


         [Required(ErrorMessage = "provide Product Brand Name")]
         [Display(Name = "Brand Name")]
         public string Name { get; set; }


         [Required(ErrorMessage = "provide Product Brand Description")]
         [Display(Name = "Description")]
         public string Description { get; set; }


       
         [Display(Name = "SortName")]
         public string SortName { get; set; }


         [Required(ErrorMessage = "provide Product Brand Code")]
         [Display(Name = "Code")]
         public string Code { get; set; }


         [Required(ErrorMessage = "provide Product Brand Status")]
         [Display(Name = "Status")]
         public bool Status { get; set; }


         [Required(ErrorMessage = "Product Brand Modifyby")]
         [Display(Name = "Modifyby")]
         public int Modifyby { get; set; }


         [Required(ErrorMessage = "Modifybydate")]
         [Display(Name = "Modifybydate")]
         public DateTime Modifybydate { get; set; }
     
        

    }
}