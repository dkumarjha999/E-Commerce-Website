using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4MVC.Models;

namespace WebApplication4MVC.ViewModels
{
    public class ProductAvailablityViewModel
    {
        public IEnumerable<Product_Availability_Detail> list_Product_Availability_Detail { get; set; }
        public Product_Availability_Detail Product_Availability_Detail { get; set; }
        public IEnumerable<Product_Availability> list_Product_Availability { get; set; }
        public Product_Availability Product_Availability { get; set; }

        public List<SelectListItem> list_Vendor_Info { get; set; }
        public List<SelectListItem> list_Product_Info_Detail { get; set; }
        public List<SelectListItem> list_Product_Brand { get; set; }
    }
}