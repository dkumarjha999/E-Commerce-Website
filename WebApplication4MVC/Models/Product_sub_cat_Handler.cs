using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace WebApplication4MVC.Models
{
    public class Product_sub_cat_Handler
    {

        private SqlConnection con;
        public Product_sub_cat_Handler()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

        public List<Product_sub_cat> GetItemList()
        {
         
            List<Product_sub_cat> iList = new List<Product_sub_cat>();

            string query = @"select a.*,b.Name as CategoryName from Product_sub_cat a join Product_cat b ON a.ProductCatId=b.Id "; 
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
                    CategoryName=(dr["CategoryName"]).ToString(),
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
            return iList;
        }


        // 2. ********** Insert Item **********
        public bool InsertItem(string file,Product_sub_cat iList)
        {
            bool lsDuplicate = false;



            string query = "SELECT * FROM Product_cat Where Code = '" + iList.Code + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
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

                query = "INSERT INTO Product_sub_cat(ProductCatId,Name,Description,SortName,Code,SubImg,Status,Modifyby,Modifybydate)VALUES('" + iList.ProductCatId + "','" + iList.Name + "','" + iList.Description + "','" + iList.SortName + "','" + iList.Code + "','" + file + "' ,'" + status + "' ,'" + ModifyBy + "', GetDate())";
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
        public bool UpdateItem(Product_sub_cat iList)
        {
            //int cnt = 1;
           // bool lsDuplicate = false;
            

            //string query = "SELECT * FROM Product_sub_cat Where Code = '" + iList.Code + "'";
            //con.Open();
            //SqlCommand cmd = new SqlCommand(query, con);
            //SqlDataReader rdr = cmd.ExecuteReader();
            //    if (rdr.Read())
            //    {
            //        cnt += 1;
            //    }
            //   con.Close();

            //    if (cnt>1)
            //    {
            //       // lsDuplicate = true;
            //        return false;
            //    }
            //  else
            //   {
                bool Status = true;
                int ModifyBy = 1;
                // DateTime ModifyBydate = DateTime.Now;
                string query = "UPDATE Product_sub_cat SET ProductCatId='"+iList.ProductCatId+"', Name = '" + iList.Name + "', Description = '" + iList.Description + "', SortName = '" + iList.SortName + "', Code = '" + iList.Code + "', Status = '" + Status + "', Modifyby = '" + ModifyBy + "', ModifyBydate = GetDate() WHERE Id = " + iList.Id;
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
       
            string query = "DELETE FROM Product_sub_cat WHERE Id = " + Id;
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