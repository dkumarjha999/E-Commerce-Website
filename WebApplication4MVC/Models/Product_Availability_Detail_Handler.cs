using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace WebApplication4MVC.Models
{
    public class Product_Availability_Detail_Handler
    {
         public SqlConnection con;
         public Product_Availability_Detail_Handler()
        {
             string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

         public List<Product_Availability_Detail> GetItemList(int nId)
        {
            List<Product_Availability_Detail> iList = new List<Product_Availability_Detail>();

            string query = @"SELECT a.*,b.Name as BrandName,c.ProductName 
                        FROM Product_Availability_Detail a JOIN Product_Brand b ON a.BrandId = b.Id
                               join Product_Info_Detail c ON a.ProductId=c.Id where a.ProductAvlId=" + nId + "";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Product_Availability_Detail

                {
                    Id = Convert.ToInt32(dr["Id"]),
                    ProductAvlId = Convert.ToInt32(dr["ProductAvlId"]),
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    BrandId = Convert.ToInt32(dr["BrandId"]),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    Cost = Convert.ToDecimal(dr["Cost"]),
                    Discount = Convert.ToDecimal(dr["Discount"]),
                    MfgDate = (dr["MfgDate"]).ToString(),
                    ExpDate =(dr["ExpDate"]).ToString(),
                    BestBefore = (dr["BestBefore"]).ToString(),
                    ProductName = dr["ProductName"].ToString(),
                    BrandName = dr["BrandName"].ToString()
                 
                });
            }
            return iList;
        }


         public bool InsertItem(Product_Availability_Detail iList, int id)
             {
                 string query = "INSERT INTO Product_Availability_Detail(ProductAvlId,ProductId,BrandId,Quantity,Cost,Discount,MfgDate,ExpDate,BestBefore)VALUES('" + id + "','" + iList.ProductId + "','" + iList.BrandId + "','" + iList.Quantity + "','" + iList.Cost + "','" + iList.Discount + "','" + iList.MfgDate + "','" + iList.ExpDate + "','"+ iList.BestBefore +"' )";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 0)
                { return true; }

                else { return false; }
          }







         //// 3. ********** Update Item Details **********
         //public bool UpdateItem(Product_Availability_Detail iList,int id)
         //{

         //    string query = "UPDATE Product_Availability_Detail SET ProductId = '" + iList.ProductId + "', 'BrandId = '" + iList.BrandId + "',' Quantity = '" + iList.Quantity + "', ' Cost = '" + iList.Cost + "', 'Discount = '" + iList.Discount + "', ' MfgDate = '" + iList.MfgDate + "',' ExpDate = '" + iList.ExpDate + "',' BestBefore= '" + iList.BestBefore + "'  WHERE  iList.ProductAvlId  = id ";
         //    SqlCommand cmd = new SqlCommand(query, con);
         //    con.Open();
         //    int i = cmd.ExecuteNonQuery();
         //    con.Close();

         //    if (i >= 1)
         //        return true;
         //    else
         //        return false;


         //}

         // 4. ********** Delete Item **********
         public bool DeleteItem(int Id)
         {

             string query = "DELETE FROM Product_Availability_Detail WHERE ProductAvlId = " + Id;
             SqlCommand cmd = new SqlCommand(query, con);
             con.Open();
             int i = cmd.ExecuteNonQuery();
             con.Close();

             if (i >= 1)
                 return true;
             else
                 return false;
         }


    }

}