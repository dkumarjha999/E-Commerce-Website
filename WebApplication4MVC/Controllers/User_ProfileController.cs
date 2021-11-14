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
    public class User_ProfileController : Controller
    {
        // GET: User_Profile        
        User_Profile_Handler ItemHandler;

        public User_ProfileController()
        {
            ItemHandler = new User_Profile_Handler();
        }

        public ActionResult Index()
        {

            List<User_Profile> list = ItemHandler.GetItemList();

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
        public ActionResult UserReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserReg(User_Profile iList)       
         {         
            if (ModelState.IsValid)
            {
                if (ItemHandler.InsertItem(iList))
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "User Created Successfully..";
                    return RedirectToAction("DisplayPic", "Product_Img");
                }
                else
                {
                    ModelState.Clear();
                    TempData["SaveMsg"] = "Mobile Number Already exist  CanNot be same !! Try again ";
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PasswordRecovery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordRecovery(User_Profile iList)
        {
            if (ItemHandler.GetPassword(iList))
                {
                 TempData["SaveMsg"] = "Your Password Has Been Sent To Your Registered Email And Mobile No";       
                 return RedirectToAction("Index");
                }
                else
                {
                    
                    TempData["SaveMsg"] = "Mobile Number And Email Not Valid";            
                    return RedirectToAction("Index");
                }           
        }

         [HttpGet]
        public ActionResult UserLogin()
        {
           
            return View();
        }

         [HttpPost]
         public ActionResult UserLogin(User_Profile iList)
         {
             if (ItemHandler.Login(iList))
             {
                 TempData["SaveMsg"] = "##Logged In ##";
                 return RedirectToAction("DisplayPic", "Product_Img");
             }
             else
             {

                 TempData["SaveMsg"] = " !!Something Went Wrong !!";
                 return RedirectToAction("UserLogin");
             }
         }
      
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    City_Name_Handler obj = new City_Name_Handler();
        //    List<City_Name> list = obj.GetItemList();
        //    List<SelectListItem> oList = new List<SelectListItem>();
        //    foreach (var item in list)
        //    {
        //        oList.Add(new SelectListItem()
        //        {
        //            Text = item.Name.ToString(),
        //            Value = item.Id.ToString()
        //        });
        //    }

        //    User_Profile oModel = new User_Profile();          
        //    oModel = ItemHandler.GetItemList().Find(itemmodel => itemmodel.Uid == id);
        //    oModel.list_City_Name = oList;

        //    return View(oModel);
        //}
        //[HttpPost]
        //public ActionResult Edit(int id, User_Profile iList)
        //{
        //    if (ItemHandler.UpdateItem(iList))
        //    {

        //        TempData["UpdateMsg"] = "Updated Successfully..";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {

        //        TempData["UpdateMsg"] = " Some Felids are Empty !! Try again ";
        //        return RedirectToAction("Index");
        //    }
        //}



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

             return RedirectToAction("Index");
         }

        //public ActionResult Details(int id)
        //{
        //    return View(ItemHandler.GetItemList().Find(itemmodel => itemmodel.Uid == id));
        //}

        //public JsonResult GetValues(int stateId)
        //{
        //    List<string> res = new List<string>();
        //    res = ItemHandler.getStateCountry(stateId);
        //    //Code to get state name and country name from database by city id
        //    return Json(res);
        //}


        public ActionResult PageLock()
        {
            User_Profile oModel = new User_Profile();
            return View();
        }

    }
}