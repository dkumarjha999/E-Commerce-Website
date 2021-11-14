using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4MVC.Models;
 
namespace WebApplication4MVC.Controllers
{
    public class Product_Info_DetailController : Controller
    {
         Product_Info_Detail_Handler ItemHandler;

         public Product_Info_DetailController()
        {
            ItemHandler = new Product_Info_Detail_Handler();
        }

        public ActionResult Index()
        {

            List<Product_Info_Detail> list = ItemHandler.GetItemList();

            if (TempData["DeleteMsg"] != null)
            {
                ViewBag.Msg = TempData["DeleteMsg"].ToString();
                TempData["DeleteMsg"] = null;
            }

            if (TempData["SaveMsg"] != null)
            {
                ViewBag.Msg = TempData["SaveMsg"].ToString();
                TempData["SaveMsg"] = null;
            }

            if (TempData["UpdateMsg"] != null)
            {
                ViewBag.Msg = TempData["UpdateMsg"].ToString();
                TempData["UpdateMsg"] = null;
            }

            return View(list);
        }



        [HttpGet]
        public ActionResult Create()
        {

            Product_cat_Handler obj = new Product_cat_Handler();
            List<Product_cat> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }

            Product_Info_Detail oModel = new Product_Info_Detail();
            oModel.list_Product_cat= oList;


            Product_sub_cat_Handler obj1 = new Product_sub_cat_Handler();
            List<Product_sub_cat> list1 = obj1.GetItemList();
            List<SelectListItem> oList1 = new List<SelectListItem>();
            foreach (var item in list1)
            {
                oList1.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
            //Product_Info_Detail oModel1 = new Product_Info_Detail();
            oModel.list_Product_sub_cat = oList1;


            return View(oModel);
        }


        [HttpPost]

        public ActionResult Create(Product_Info_Detail iList)
        {
            Product_cat_Handler obj = new Product_cat_Handler();
            List<Product_cat> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }

            Product_Info_Detail oModel = new Product_Info_Detail();
            oModel.list_Product_cat = oList;


            Product_sub_cat_Handler obj1 = new Product_sub_cat_Handler();
            List<Product_sub_cat> list1 = obj1.GetItemList();
            List<SelectListItem> oList1 = new List<SelectListItem>();
            foreach (var item in list1)
            {
                oList1.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
            //Product_Info_Detail oModel1 = new Product_Info_Detail();
            oModel.list_Product_sub_cat = oList1;

            if (ModelState.IsValid)
            {

                if (ItemHandler.InsertItem(iList))
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "Saved Successfully..";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "Something went wrong check form field  !! Try again ";
                    return RedirectToAction("Index");
                }


            }

            return View(oModel);

        }





        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product_cat_Handler obj = new Product_cat_Handler();
            List<Product_cat> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
       
            Product_sub_cat_Handler obj1 = new Product_sub_cat_Handler();
            List<Product_sub_cat> list1 = obj1.GetItemList();
            List<SelectListItem> oList1 = new List<SelectListItem>();
            foreach (var item in list1)
            {
                oList1.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
            Product_Info_Detail oModel = new Product_Info_Detail();         
           oModel=ItemHandler.GetItemList().Find(itemmodel => itemmodel.Id == id);  // phle data then dropdown list bhejna hai nhi to error 
           oModel.list_Product_sub_cat = oList1;
           oModel.list_Product_cat = oList;

           return View(oModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product_Info_Detail iList)
        {


            if (ItemHandler.UpdateItem(iList))
            {

                TempData["UpdateMsg"] = "Saved Successfully..";
                return RedirectToAction("Index");
            }
            else
            {

                TempData["UpdateMsg"] = "Code Already exist  CanNot be same !! Try again ";
                return RedirectToAction("Index");
            }



            //try
            //{
            //    ItemHandler.UpdateItem(iList);
            //    TempData["UpdateMsg"] = "Updated Successfully..";
            //    return RedirectToAction("Index");
            //}
            //catch { return View(); }
        }



        public ActionResult Delete(int id)
        {
            try
            {
                if (ItemHandler.DeleteItem(id))
                {
                    TempData["DeleteMsg"] = "Delete Successfully..";
                    return RedirectToAction("Index");
                }
            }
            catch { return View(); }

            return View();
        }

        public ActionResult Details(int id)
        {
            return View(ItemHandler.GetItemList().Find(itemmodel => itemmodel.Id == id));
        }

        public JsonResult getProductSubCat(int prod_id)
        {
            List<Product_sub_cat> res = new List<Product_sub_cat>();
            res = ItemHandler.getProductSubCat(prod_id);

            List<SelectListItem> listSubCat = new List<SelectListItem>();
            foreach (var item in res)
            {
                listSubCat.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            //Code to get state name and country name from database by city id
            return Json(listSubCat);
        }


    }
}