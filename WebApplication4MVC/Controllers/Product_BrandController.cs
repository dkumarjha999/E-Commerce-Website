using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4MVC.Models;


namespace WebApplication4MVC.Controllers
{
    public class Product_BrandController : Controller
    {
       Product_Brand_Handler ItemHandler;

       public Product_BrandController()
        {
            ItemHandler = new Product_Brand_Handler();
        }


        // GET: Product_cat
        public ActionResult Index()
        {
            List<Product_Brand> list = ItemHandler.GetItemList();

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
           public ActionResult Create(Product_Brand iList)
            {
                    if(ModelState.IsValid)
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
                            TempData["SaveMsg"] = "product Brand Already exist  CanNot be same !! Try again ";
                            return RedirectToAction("Index");
                        }

                    }

                    return View();

            }


         [HttpGet]
            public ActionResult Edit(int id)
            {
                return View(ItemHandler.GetItemList().Find(itemmodel => itemmodel.Id ==id));
            }
            [HttpPost]
         public ActionResult Edit(int id, Product_Brand iList)
            {
                if (ItemHandler.UpdateItem(iList))
                {

                    TempData["UpdateMsg"] = "Saved Successfully..";
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["UpdateMsg"] = "Product Brand Already exist  CanNot be same !! Try again ";
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