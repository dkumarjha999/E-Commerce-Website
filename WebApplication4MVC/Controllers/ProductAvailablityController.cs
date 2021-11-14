using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4MVC.Models;
using WebApplication4MVC.ViewModels;
using Newtonsoft.Json;


namespace WebApplication4MVC.Controllers
{
    public class ProductAvailablityController : Controller
    {
        Product_Availability_Handler masterHandler = new Product_Availability_Handler();
        Product_Availability_Detail_Handler detailHandler = new Product_Availability_Detail_Handler();

        Vendor_Info_Handler venHandler = new Vendor_Info_Handler();
        Product_Info_Detail_Handler prodHandler = new Product_Info_Detail_Handler();
        Product_Brand_Handler branHandler = new Product_Brand_Handler();


        // GET: ProductAvailablity
        public ActionResult Index()
        {
            ProductAvailablityViewModel vwModel = new ProductAvailablityViewModel();
            vwModel.list_Product_Availability = masterHandler.GetItemList();
            return View(vwModel);

        }

        public ActionResult ProductDetails(int nid)
        {
            ProductAvailablityViewModel vwModel = new ProductAvailablityViewModel();
            vwModel.list_Product_Availability_Detail = detailHandler.GetItemList(nid);
            return View(vwModel);
        }


        public ActionResult Getitem()
        {
            ProductAvailablityViewModel oModel = new ProductAvailablityViewModel();
            // getting list of vendor
            List<Vendor_Info> list1 = venHandler.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list1)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.VendorName.ToString(),
                    Value = item.VendorId.ToString()
                });
            }
            oModel.list_Vendor_Info = oList;

            // getting list of products

            List<Product_Info_Detail> list2 = prodHandler.GetItemList();
            List<SelectListItem> oList2 = new List<SelectListItem>();
            foreach (var item in list2)
            {
                oList2.Add(new SelectListItem()
                {
                    Text = item.ProductName.ToString(),
                    Value = item.Id.ToString()
                });
            }
            oModel.list_Product_Info_Detail = oList2;


            // getting list of Product Brands

            List<Product_Brand> list3 = branHandler.GetItemList();
            List<SelectListItem> oList3 = new List<SelectListItem>();
            foreach (var item in list3)
            {
                oList3.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
            oModel.list_Product_Brand = oList3;



            return View(oModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return (Getitem());
        }

        [HttpPost]
        public ActionResult Create(string master, string detail)
        {

            if (!string.IsNullOrEmpty(master) && !string.IsNullOrEmpty(detail))
            {
                var masterModel = JsonConvert.DeserializeObject<Product_Availability>(master);
                List<Product_Availability_Detail> detailModel = (List<Product_Availability_Detail>)JsonConvert.DeserializeObject(detail, typeof(List<Product_Availability_Detail>));

                if (masterModel != null)
                {
                    //Save Master Data  and //Get Master Id
                    if (detailModel.Count > 0)
                    {
                        int id = masterHandler.InsertItem(masterModel);
                        if (id != 0)
                        {
                            //Save Detail Data
                            foreach (var item in detailModel)
                            {
                                Product_Availability_Detail odetail = new Product_Availability_Detail();
                                detailHandler.InsertItem(item, id);
                            }
                        }
                    }


                }
               // return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            //return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductAvailablityViewModel vwModel = new ProductAvailablityViewModel();

            List<Vendor_Info> list1 = venHandler.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list1)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.VendorName.ToString(),
                    Value = item.VendorId.ToString()
                });
            }
            vwModel.list_Vendor_Info = oList;

            // getting list of products

            List<Product_Info_Detail> list2 = prodHandler.GetItemList();
            List<SelectListItem> oList2 = new List<SelectListItem>();
            foreach (var item in list2)
            {
                oList2.Add(new SelectListItem()
                {
                    Text = item.ProductName.ToString(),
                    Value = item.Id.ToString()
                });
            }
            vwModel.list_Product_Info_Detail = oList2;


            // getting list of Product Brands

            List<Product_Brand> list3 = branHandler.GetItemList();
            List<SelectListItem> oList3 = new List<SelectListItem>();
            foreach (var item in list3)
            {
                oList3.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
            vwModel.list_Product_Brand = oList3;


            Product_Availability pr_av = new Product_Availability();
            pr_av = masterHandler.GetItemList().Find(itemmodel => itemmodel.Id == id);
            // var pr_avl = new List<Product_Availability> { pr_av };
            List<Product_Availability_Detail> list_Product_Availability_Detail = detailHandler.GetItemList(id);
            vwModel.list_Product_Availability_Detail = list_Product_Availability_Detail;
            vwModel.Product_Availability = pr_av;

            return View(vwModel);
        }


        [HttpPost]
        public ActionResult Edit(string master, string detail, int avid)
        {

            if (!string.IsNullOrEmpty(master) && !string.IsNullOrEmpty(detail))
            {
                var masterModel = JsonConvert.DeserializeObject<Product_Availability>(master);
                List<Product_Availability_Detail> detailModel = (List<Product_Availability_Detail>)JsonConvert.DeserializeObject(detail, typeof(List<Product_Availability_Detail>));

                if (masterModel != null)
                {
                    //Save Master Data  and //Get Master Id
                    if (detailModel.Count > 0)
                    {
                        int id = masterHandler.UpdateItem(masterModel, avid);
                        if (id != 0)
                        {
                            //Save Detail Data
                            foreach (var item in detailModel)
                            {
                                //Product_Availability_Detail odetail = new Product_Availability_Detail();
                                detailHandler.DeleteItem(id);
                            }

                            foreach (var item in detailModel)
                            {
                                //Product_Availability_Detail odetail = new Product_Availability_Detail();
                                detailHandler.InsertItem(item, id);
                            }
                        }
                    }


                }
              
            }
            return RedirectToAction("Index");
            //return View();
        }


        public ActionResult delete(int id)
        {
            bool x = detailHandler.DeleteItem(id);

            if (x)
            { masterHandler.DeleteItem(id); }

            return RedirectToAction("Index");
        }
    }
}