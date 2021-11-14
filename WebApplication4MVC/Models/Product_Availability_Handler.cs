using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace WebApplication4MVC.Models
{
    public class Product_Availability_Handler
    {

         public SqlConnection con;
         public Product_Availability_Handler()
        {
             string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

         public List<Product_Availability> GetItemList()
        {

            List<Product_Availability> iList = new List<Product_Availability>();

            string query = @"SELECT a.*,b.VendorName
                            FROM Product_Availability a
                            JOIN Vendor_Info b ON a.VendorId = b.VendorId";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Product_Availability

                {
                    Id = Convert.ToInt32(dr["Id"]),
                    VendorId = Convert.ToInt32(dr["VendorId"]),
                    AvailabilityDate = Convert.ToDateTime(dr["AvailabilityDate"]),
                    Description =(dr["Description"]).ToString(),
                    Ven_name = dr["VendorName"].ToString()
                });
            }
            return iList;
        }


         // 2. ********** Insert Item **********
         public int InsertItem(Product_Availability iList)
         {
             int id = 0;
             con.Open();

             string query = "INSERT INTO Product_Availability(VendorId,AvailabilityDate,Description)VALUES('" + iList.VendorId + "','" + iList.AvailabilityDate + "','" + iList.Description + "')";
             SqlCommand cmd = new SqlCommand(query, con);

             int i = cmd.ExecuteNonQuery();
             con.Close();

             if (i > 0)
             {
                 con.Open();
                 query = "select Top 1 Id from Product_Availability order by Id desc";
                 cmd = new SqlCommand(query, con);
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {
                     id = Convert.ToInt32(dr["Id"].ToString());
                 }
             }

             return id;
         }





        // 3. ********** Update Item Details **********
        public int UpdateItem(Product_Availability iList ,int avid)
        {

            string query = "UPDATE Product_Availability SET VendorId = '" + iList.VendorId + "', AvailabilityDate = '" + iList.AvailabilityDate + "', Description = '" + iList.Description + "' WHERE Id = " + avid;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >0)
                { return avid; }
                else
                { return 0; }

         

        }

        // 4. ********** Delete Item **********
         public int DeleteItem(int Id)
          {
          
            string query = "DELETE FROM Product_Availability  WHERE Id = " + Id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            { return Id; }
            else
            { return Id; }
             
         }


    }


 }
