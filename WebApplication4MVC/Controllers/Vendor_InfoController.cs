using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;

using System.Web.Mvc;
using WebApplication4MVC.Models;

namespace WebApplication4MVC.Controllers
{
    public class Vendor_InfoController : Controller
    {
        // GET: Vendor_Info
        Vendor_Info_Handler ItemHandler;

        public Vendor_InfoController()
        {
            ItemHandler = new Vendor_Info_Handler();
        }

        public ActionResult Index()
        {

            List<Vendor_Info> list = ItemHandler.GetItemList();

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
            City_Name_Handler obj = new City_Name_Handler();
            List<City_Name> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
              {
                  Text = item.Name.ToString(),
                  Value = item.Id.ToString()
              });
            }

            Vendor_Info oModel = new Vendor_Info();
            oModel.list_City_Name = oList;
            return View(oModel);
        }


        [HttpPost]
        public ActionResult Create(Vendor_Info iList)
        {
            City_Name_Handler obj = new City_Name_Handler();
            List<City_Name> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }

            iList.list_City_Name = oList;

            if (ModelState.IsValid)
            {
                if (ItemHandler.InsertItem(iList))
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "Vendor Info Created Successfully..";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "Vendor Info GSTIN Already exist  CanNot be same !! Try again ";
                    return RedirectToAction("Index");
                }


            }

            return View();

        }





        [HttpGet]
        public ActionResult Edit(int id)
        {
            City_Name_Handler obj = new City_Name_Handler();
            List<City_Name> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }

            Vendor_Info oModel = new Vendor_Info();          
            oModel = ItemHandler.GetItemList().Find(itemmodel => itemmodel.VendorId == id);
            oModel.list_City_Name = oList;

            return View(oModel);
        }
        [HttpPost]
        public ActionResult Edit(int id, Vendor_Info iList)
        {
            if (ItemHandler.UpdateItem(iList))
            {

                TempData["UpdateMsg"] = "Updated Successfully..";
                return RedirectToAction("Index");
            }
            else
            {

                TempData["UpdateMsg"] = "GSTIN Already exist  CanNot be same !! Try again ";
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
            return View(ItemHandler.GetItemList().Find(itemmodel => itemmodel.VendorId == id));
        }

        public JsonResult GetValues(int stateId)
        {
            List<string> res = new List<string>();
            res = ItemHandler.getStateCountry(stateId);
            //Code to get state name and country name from database by city id
            return Json(res);
        }


    }
}