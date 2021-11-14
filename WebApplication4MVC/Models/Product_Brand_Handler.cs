using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebApplication4MVC.Models
{
    public class Product_Brand_Handler
    {
        public SqlConnection con;
        public Product_Brand_Handler()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

        public List<Product_Brand> GetItemList()
        {

            List<Product_Brand> iList = new List<Product_Brand>();

            string query = "SELECT * FROM Product_Brand";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Product_Brand
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Description = Convert.ToString(dr["Description"]),
                    SortName = Convert.ToString(dr["SortName"]),
                    Code = Convert.ToString(dr["Code"]),
                    Status = Convert.ToBoolean(dr["Status"]),
                    Modifyby=Convert.ToInt32(dr["Modifyby"]),
                    Modifybydate = Convert.ToDateTime(dr["Modifybydate"]),
                });
            }
            return iList;
        }


        // 2. ********** Insert Item **********
        public bool InsertItem(Product_Brand iList)
        {
            bool lsDuplicate = false;



            string query = "SELECT * FROM Product_Brand Where Name = '" + iList.Name + "'";
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

                query = "INSERT INTO Product_Brand(Name,Description,SortName,Code,Status,Modifyby,Modifybydate)VALUES('" + iList.Name + "','" + iList.Description + "','" + iList.SortName + "','" + iList.Code + "','" + status + "' ,'" + ModifyBy + "', GetDate())";
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
        public bool UpdateItem(Product_Brand iList)
        {

            int cnt = 1;
            // bool lsDuplicate = false;


            string query = "SELECT * FROM Product_Brand Where Name = '" + iList.Name + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                cnt += 1;
            }
            con.Close();

            if (cnt > 1)
            {
                // lsDuplicate = true;
                return false;
            }
            else
            {
                bool Status = true;
                int ModifyBy = 1;
                // DateTime ModifyBydate = DateTime.Now;
                query = "UPDATE Product_Brand SET Name = '" + iList.Name + "', Description = '" + iList.Description + "', SortName = '" + iList.SortName + "', Code = '" + iList.Code + "', Status = '" + Status + "', Modifyby = '" + ModifyBy + "', ModifyBydate = GetDate() WHERE Id = " + iList.Id;
                cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
                else
                    return false;

            }

            // connection();
            // bool Status = true;
            // int ModifyBy = 1;
            //// DateTime ModifyBydate = DateTime.Now;
            // string query = "UPDATE Product_cat SET Name = '" + iList.Name + "', Description = '" + iList.Description + "', SortName = '" + iList.SortName + "', Code = '" + iList.Code + "', Status = '" + Status + "', Modifyby = '" + ModifyBy + "', ModifyBydate = GetDate() WHERE Id = " + iList.Id;
            // SqlCommand cmd = new SqlCommand(query, con);
            // con.Open();
            // int i = cmd.ExecuteNonQuery();
            // con.Close();

            // if (i >= 1)
            //     return true;
            // else
            //     return false;
        }

        // 4. ********** Delete Item **********
        public bool DeleteItem(int Id)
        {

            string query = "DELETE FROM Product_Brand WHERE Id = " + Id;
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