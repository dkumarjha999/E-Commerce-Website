using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

using System.Web.Mvc;
using WebApplication4MVC.Models;

namespace WebApplication4MVC.Controllers
{
    public class Vendor_Bank_InfoController : Controller
    {
        // GET:  Vendor_Bank_Info
        Vendor_Bank_Info_Handler ItemHandler;

        public Vendor_Bank_InfoController()
        {
            ItemHandler = new Vendor_Bank_Info_Handler();
        }

        public ActionResult Index()
        {

            List<Vendor_Bank_Info> list = ItemHandler.GetItemList();

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
            Vendor_Info_Handler obj = new Vendor_Info_Handler();
            List<Vendor_Info> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.VendorName.ToString(),
                    Value = item.VendorId.ToString()
                });
            }

            Vendor_Bank_Info oModel = new Vendor_Bank_Info();
            oModel.list_Vendor_Info = oList;
            return View(oModel);
        }


        [HttpPost]
        public ActionResult Create(Vendor_Bank_Info iList)
        {
            Vendor_Info_Handler obj = new Vendor_Info_Handler();
            List<Vendor_Info> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.VendorName.ToString(),
                    Value = item.VendorId.ToString()
                });
            }

            Vendor_Bank_Info oModel = new Vendor_Bank_Info();
            oModel.list_Vendor_Info = oList;

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
                    TempData["SaveMsg"] = "AccountNo Already exist  CanNot be same !! Try again ";
                    return RedirectToAction("Index");
                }


            }

            return View(oModel);

        }





        [HttpGet]
        public ActionResult Edit(int id)
        {

            Vendor_Info_Handler obj = new Vendor_Info_Handler();
            List<Vendor_Info> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.VendorName.ToString(),
                    Value = item.VendorId.ToString()
                });
            }

            Vendor_Bank_Info oModel = new Vendor_Bank_Info();
            oModel = ItemHandler.GetItemList().Find(itemmodel => itemmodel.Id == id);
            oModel.list_Vendor_Info= oList;

            return View(oModel);
        }
        [HttpPost]
        public ActionResult Edit(int id, Vendor_Bank_Info iList)
        {
            if (ItemHandler.UpdateItem(iList))
            {

                TempData["UpdateMsg"] = "Saved Successfully..";
                return RedirectToAction("Index");
            }
            else
            {

                TempData["UpdateMsg"] = "AccountNo Already exist  CanNot be same !! Try again ";
                return RedirectToAction("Index");
            }
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


    }

}