using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebApplication4MVC.Models
{
   public class Product_Info_Detail_Handler
    {
      public SqlConnection con;
      public Product_Info_Detail_Handler()
        {
             string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

        public List<Product_Info_Detail> GetItemList()
        {
            
            List<Product_Info_Detail> iList = new List<Product_Info_Detail>();

            string query = @"SELECT dbo.Product_cat.Name as CategoryName, dbo.Product_sub_cat.Name AS SubCategoryName, dbo.Product_Info_Detail.ProductName, 
            dbo.Product_Info_Detail.Product_cat_id, 
            dbo.Product_Info_Detail.Product_sub_cat_id,
            dbo.Product_Info_Detail.Product_Code, 
            dbo.Product_Info_Detail.Bar_Code, 
            dbo.Product_Info_Detail.Status,
            dbo.Product_Info_Detail.Modifyby, 
            dbo.Product_Info_Detail.Description,
            dbo.Product_Info_Detail.Modifybydate, dbo.Product_Info_Detail.Id FROM   dbo.Product_Info_Detail 
            INNER JOIN dbo.Product_cat ON dbo.Product_Info_Detail.Product_cat_id = dbo.Product_cat.Id 
            INNER JOIN  dbo.Product_sub_cat ON dbo.Product_Info_Detail.Product_sub_cat_id = dbo.Product_sub_cat.Id 
            AND dbo.Product_cat.Id = dbo.Product_sub_cat.ProductCatId";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Product_Info_Detail

                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Product_cat_id = Convert.ToInt32(dr["Product_cat_id"]),
                    Product_sub_cat_id = Convert.ToInt32(dr["Product_sub_cat_id"]),
                    CategoryName = Convert.ToString(dr["CategoryName"]),
                    SubCategoryName = Convert.ToString(dr["SubCategoryName"]),
                    ProductName = Convert.ToString(dr["ProductName"]),
                    Product_Code = Convert.ToString(dr["Product_Code"]),
                    Bar_Code = Convert.ToString(dr["Bar_Code"]),
                    Description = Convert.ToString(dr["Description"]),
              
                    //    Status = Convert.ToBoolean(dr["Status"]),
                    //Modifyby=Convert.ToInt32(dr["Modifyby"]),
                    //  Modifybydate = Convert.ToDateTime(dr["Modifybydate"]),
                });
            }
            return iList;
        }


        // 2. ********** Insert Item **********
        public bool InsertItem(Product_Info_Detail iList)
        {
            bool lsDuplicate = false;

            

            string query = "SELECT * FROM Product_Info_Detail Where Bar_Code = '" + iList.Bar_Code + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read() || iList.Product_sub_cat_id==0)
            {
                lsDuplicate = true;
            }
            con.Close();

            if (lsDuplicate == true)
            {
                return false;
            }
            else
            {
                bool status = true;
                int ModifyBy = 1;
                //DateTime ModifyBydate = DateTime.Now;

                query = "INSERT INTO Product_Info_Detail(Product_cat_id,Product_sub_cat_id,ProductName,Product_Code,Bar_Code,Description,Status,Modifyby,Modifybydate)VALUES('" + iList.Product_cat_id + "','" + iList.Product_sub_cat_id + "','" + iList.ProductName + "','" + iList.Product_Code + "','" + iList.Bar_Code + "','"+iList.Description+"','" + status + "' ,'" + ModifyBy + "', GetDate())";
                cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
            }

            return false;

        }

        // 3. ********** Update Item Details **********
        public bool UpdateItem(Product_Info_Detail iList)
        {
           // int cnt = 1;
            // bool lsDuplicate = false;
      

         //   //string query = "SELECT * FROM Product_Info_Detail Where Bar_Code= '" + iList.Bar_Code + "'";
         //   con.Open();
         //cmd = new SqlCommand(query, con);
         //   //SqlDataReader rdr = cmd.ExecuteReader();
            //if (rdr.Read())
            //{
            //    cnt += 1;
            //}
            //con.Close();

            //if (cnt > 1)
            //{
            //    // lsDuplicate = true;
            //    return false;
            //}


           // else
           // {
                bool Status = true;
                int ModifyBy = 1;
                // DateTime ModifyBydate = DateTime.Now;
                string query = "UPDATE Product_Info_Detail SET Product_cat_id = '" + iList.Product_cat_id + "', Description = '" + iList.Description + "',Product_sub_cat_id = '" + iList.Product_sub_cat_id + "', ProductName = '" + iList.ProductName + "', Product_Code = '" + iList.Product_Code + "', Bar_Code = '" + iList.Bar_Code + "', Status = '" + Status + "', Modifyby = '" + ModifyBy + "', ModifyBydate = GetDate() WHERE Id = " + iList.Id;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
                else
                    return false;

            //}

        }

        // 4. ********** Delete Item **********
        public bool DeleteItem(int Id)
        {
          
            string query = "DELETE FROM Product_Info_Detail WHERE Id = " + Id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }



        public List<Product_sub_cat> getProductSubCat(int prod_id)
        {
            List<Product_sub_cat> res = new List<Product_sub_cat>();

            string query = @"select a.Id, a.Name 
                                        from Product_sub_cat a
                        join Product_cat b on a.ProductCatId = b.Id
                         Where b.Id = " + prod_id + "";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Product_sub_cat omodel = new Product_sub_cat();
                omodel.Id = Convert.ToInt32(rdr["Id"].ToString());
                omodel.Name = rdr["Name"].ToString();
                res.Add(omodel);
            }
            con.Close();
            return (res);
        }





    }
}