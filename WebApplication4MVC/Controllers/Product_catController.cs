using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication4MVC.Models;

namespace WebApplication4MVC.Controllers
{
    public class Product_catController : Controller
    {
        Product_cat_Handler ItemHandler;

        public Product_catController()
        {
            ItemHandler = new Product_cat_Handler();
        }


        // GET: Product_cat
        public ActionResult Index()
        {
            List<Product_cat> list = ItemHandler.GetItemList();

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
                return View();
            }
            [HttpPost]
           public ActionResult Create(HttpPostedFileBase file, Product_cat iList)
            {

                if (file != null && file.ContentLength > 0)
                {

                    string imgname = Path.GetFileName(file.FileName).ToString();
                    string imgext = Path.GetExtension(imgname);
                    var rondom = Guid.NewGuid() + imgname;

                    if (imgext == ".jpg" || imgext == ".PNG" || imgext == ".jfif" || imgext == ".jpeg")
                    {
                        string imgpath = "/CategoryImg/" + rondom;

                        if (ItemHandler.InsertItem(imgpath, iList))
                        {
                            file.SaveAs(Server.MapPath(imgpath));
                            ModelState.Clear();
                            TempData["SaveMsg"] = "Saved Successfully..";
                           
                        }
                     }
                 }
                else
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "Code Already exist  CanNot be same !! Try again ";                    
                }
                return RedirectToAction("Index");
            }

        

           [HttpGet]
            public ActionResult Edit(int id)
            {
                return View(ItemHandler.GetItemList().Find(itemmodel => itemmodel.Id ==id));
            }
            [HttpPost]
            public ActionResult Edit(int id, Product_cat iList)
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
                if(ItemHandler.DeleteItem(id))
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


    }
}


 
        
       
 