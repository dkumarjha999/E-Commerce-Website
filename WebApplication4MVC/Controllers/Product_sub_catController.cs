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
    public class Product_sub_catController : Controller
    {
         public SqlConnection con;
   
        Product_sub_cat_Handler ItemHandler;

        public Product_sub_catController()
        {
             string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
            ItemHandler = new Product_sub_cat_Handler();
        }


        // GET: Product_sub_cat
        public ActionResult Index()
        {

            List<Product_sub_cat> list = ItemHandler.GetItemList();

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

            Product_sub_cat oModel = new Product_sub_cat();
            oModel.list_Product_cat = oList;

            return View(oModel);
        }


        public ActionResult SubCatDetail(int catid)
        {
            List<Product_sub_cat> iList = new List<Product_sub_cat>();

            string query = @"select a.*,b.Name as CategoryName from Product_sub_cat a join Product_cat b ON a.ProductCatId=b.Id and a.ProductCatId='" + catid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Product_sub_cat
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    ProductCatId = Convert.ToInt32(dr["ProductCatId"]),
                    CategoryName = (dr["CategoryName"]).ToString(),
                    Name = Convert.ToString(dr["Name"]),
                    Description = Convert.ToString(dr["Description"]),
                    SortName = Convert.ToString(dr["SortName"]),
                    Code = Convert.ToString(dr["Code"]),
                    SubImg = Convert.ToString(dr["SubImg"]),
                    //        Status = Convert.ToBoolean(dr["Status"]),
                    //Modifyby=Convert.ToInt32(dr["Modifyby"]),
                    //  Modifybydate = Convert.ToDateTime(dr["Modifybydate"]),
                });
            }

            return View(iList);
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Product_sub_cat iList)
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

            Product_sub_cat oModel = new Product_sub_cat();
            oModel.list_Product_cat = oList;


            if (file != null && file.ContentLength > 0)
            {

                string imgname = Path.GetFileName(file.FileName).ToString();
                string imgext = Path.GetExtension(imgname);
                var rondom = Guid.NewGuid() + imgname;

                if (imgext == ".jpg" || imgext == ".PNG" || imgext == ".jfif" || imgext == ".jpeg")
                {
                    string imgpath = "/SubCatImg/" + rondom;

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




        //   return View(oModel);

        //}





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

            Product_sub_cat oModel = new Product_sub_cat();
            oModel = ItemHandler.GetItemList().Find(itemmodel => itemmodel.Id == id);
            oModel.list_Product_cat = oList;

            return View(oModel);

        }
        [HttpPost]
        public ActionResult Edit(int id, Product_sub_cat iList)
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

    }
}

