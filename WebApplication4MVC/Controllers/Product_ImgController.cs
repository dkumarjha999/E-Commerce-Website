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
    public class Product_ImgController : Controller
    {
         public SqlConnection con;
        
         public Product_ImgController()
            {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
              con = new SqlConnection(constring);
              }
        // GET: Product_Img

        public ActionResult Index()
        {
            Product_Info_Detail_Handler obj = new Product_Info_Detail_Handler();
            List<Product_Info_Detail> list = obj.GetItemList();
            List<SelectListItem> oList = new List<SelectListItem>();
            foreach (var item in list)
            {
                oList.Add(new SelectListItem()
                {
                    Text = item.ProductName.ToString(),
                    Value = item.Id.ToString()
                });
            }

            ProductImg oModel = new ProductImg();
            oModel.list_Product_Info_Detail = oList;

            return View(oModel);

           
        }

        // save: Product_Img
        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> file, ProductImg prodImg)
        {
            foreach (var n in file)
            {
                if (n != null && n.ContentLength > 0)
                {
                   
                    string imgname = Path.GetFileName(n.FileName).ToString();
                    string imgext = Path.GetExtension(imgname);
                    var rondom = Guid.NewGuid() + imgname;

                    if (imgext == ".jpg" || imgext == ".PNG" || imgext == ".jfif")
                    {
                        string imgpath = "/ProductImg/"+rondom;
                       


                        string querry = "insert into Product_Img (ProductId,ImgPath,Active)values('" + prodImg.ProductId + "','" + imgpath + "','true')";
                        SqlCommand cmd = new SqlCommand(querry, con);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        n.SaveAs(Server.MapPath(imgpath));
                        con.Close();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //public ActionResult displaypic()
        //{

        //    ProductImg picture = new ProductImg();
        //    picture.ImgPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/images/"), filename);

        //    return View(picture);
        //}

        public ActionResult Products(int Subid)
        {
            List<ProductImg> iList = new List<ProductImg>();
            string query = @"select top 1 with ties img.ProductId ,ImgPath,Id, prod.CategoryName,prod.SubCatId,prod.SubCategoryName, prod.ProductName, prod.Cost, 
                         prod.Discount, MfgDate, ExpDate from Product_Img img
                          left outer join 
                          (SELECT a.Name as CategoryName, b.Name as SubCategoryName,b.Id As SubCatId,c.Id AS ProductId, c.ProductName,d.Cost,
                            d.Discount,d.MfgDate,d.ExpDate from Product_Info_Detail c
                          	join Product_sub_cat  b on b.Id=c.Product_sub_cat_id 
                          	join Product_cat a on a.Id=b.ProductCatId 
                          	join Product_Availability_Detail d on c.Id=d.ProductId
                           ) prod on img.ProductId = prod.ProductId
						   where prod.SubCatId="+Subid+" order by row_number() over (partition by img.ProductId order by Convert(Date,ExpDate) asc)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new ProductImg
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    ProductName = (dr["ProductName"]).ToString(),
                    ImgPath = (dr["ImgPath"]).ToString(),
                    Cost = Convert.ToDecimal(dr["Cost"]),
                    MfgDate = (dr["MfgDate"]).ToString(),
                    ExpDate = (dr["ExpDate"]).ToString(),

                });
            }
            return View(iList);
        }

        public ActionResult Displaypic()
        {
            List<ProductImg> iList = new List<ProductImg>();
            string query = @"select top 1 with ties img.ProductId ,ImgPath,Id, prod.CategoryName, prod.SubCategoryName, prod.ProductName, prod.Cost, 
                         prod.Discount, MfgDate, ExpDate from Product_Img img
                          left outer join 
                          (SELECT a.Name as CategoryName, b.Name as SubCategoryName,c.Id AS ProductId, c.ProductName,d.Cost,
                            d.Discount,d.MfgDate,d.ExpDate from Product_Info_Detail c
                          	join Product_sub_cat  b on b.Id=c.Product_sub_cat_id 
                          	join Product_cat a on a.Id=b.ProductCatId 
                          	join Product_Availability_Detail d on c.Id=d.ProductId
                           ) prod on img.ProductId = prod.ProductId
                           order by row_number() over (partition by img.ProductId order by Convert(Date,ExpDate) asc)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new ProductImg
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName=(dr["ProductName"]).ToString(),
                        ImgPath = (dr["ImgPath"]).ToString(),
                        Cost = Convert.ToDecimal(dr["Cost"]),
                        MfgDate = (dr["MfgDate"]).ToString(),
                        ExpDate = (dr["ExpDate"]).ToString(),
                    
                    });
            }
            return View(iList);
        }


        // 2. ********** Insert Item **********

        public ActionResult ProductDetails(int ProdId)
        {
            List<ProductImg> iList = new List<ProductImg>();

            string query = @"select a.Name as CategoryName, b.Name as SubCategoryName,c.ProductName,c.Description,d.Cost,d.Discount,d.MfgDate,d.ExpDate,d.ProductAvlId,
                            e.ProductId,e.ImgPath,e.Id,f.Name  as BrandName,g.VendorName as ven_name,g.VendorId
                            from Product_Info_Detail c 
                            join Product_sub_cat  b on b.Id=c.Product_sub_cat_id 
                            join Product_cat a on a.Id=b.ProductCatId 
                            join Product_Availability_Detail d on c.Id=d.ProductId
                            join Product_Brand f on f.Id= d.BrandId
							join Product_Availability h on h.Id=d.ProductAvlId									 
							join Vendor_Info g on g.VendorId=h.VendorId
                            join Product_Img e on c.Id=e.ProductId where e.ProductId='" + ProdId + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new ProductImg     
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName=(dr["ProductName"]).ToString(),
                        Description = (dr["Description"]).ToString(),
                        CategoryName = (dr["CategoryName"]).ToString(),
                        SubCategoryName = (dr["SubCategoryName"]).ToString(),
                        VendorId = Convert.ToInt32(dr["VendorId"]),
                        Ven_name=(dr["Ven_name"]).ToString(),
                        BrandName = (dr["BrandName"]).ToString(),
                        ImgPath = (dr["ImgPath"]).ToString(),
                        Cost = Convert.ToDecimal(dr["Cost"]),
                        Discount = Convert.ToDecimal(dr["Discount"]),
                        MfgDate = (dr["MfgDate"]).ToString(),
                        ExpDate = (dr["ExpDate"]).ToString(),                 
                    });

         
            }
            return View(iList);

        }

        public ActionResult Review(int Uid)
        {

            //string query = "INSERT INTO UserReview(UserName,Email,Review,ProductId,RevDate)VALUES('" + UserName + "','" + Email + "','" + Review + "','" + ProductId + "', GetDate(). ToString())";
            //   SqlCommand cmd = new SqlCommand(query, con);
            //    con.Open();
            //    int i = cmd.ExecuteNonQuery();
            //    con.Close();                          
        

           
       return RedirectToAction("Displaypic");
        }


        //// 3. ********** Update Item Details **********
        //public bool UpdateItem(Product_sub_cat iList)
        //{
        //    //int cnt = 1;
        //    // bool lsDuplicate = false;
        //    connection();

        //    //string query = "SELECT * FROM Product_sub_cat Where Code = '" + iList.Code + "'";
        //    //con.Open();
        //    //SqlCommand cmd = new SqlCommand(query, con);
        //    //SqlDataReader rdr = cmd.ExecuteReader();
        //    //    if (rdr.Read())
        //    //    {
        //    //        cnt += 1;
        //    //    }
        //    //   con.Close();

        //    //    if (cnt>1)
        //    //    {
        //    //       // lsDuplicate = true;
        //    //        return false;
        //    //    }
        //    //  else
        //    //   {
        //    bool Status = true;
        //    int ModifyBy = 1;
        //    // DateTime ModifyBydate = DateTime.Now;
        //    string query = "UPDATE Product_sub_cat SET ProductCatId='" + iList.ProductCatId + "', Name = '" + iList.Name + "', Description = '" + iList.Description + "', SortName = '" + iList.SortName + "', Code = '" + iList.Code + "', Status = '" + Status + "', Modifyby = '" + ModifyBy + "', ModifyBydate = GetDate() WHERE Id = " + iList.Id;
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();

        //    if (i >= 1)
        //        return true;
        //    else
        //        return false;

        //    //}

        //}

        //// 4. ********** Delete Item **********
        //public bool DeleteItem(int Id)
        //{
        //    connection();
        //    string query = "DELETE FROM Product_sub_cat WHERE Id = " + Id;
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();

        //    if (i >= 1)
        //        return true;
        //    else
        //        return false;
        //}

    }
}