using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace WebApplication4MVC.Models
{
    public class City_Name_Handler
    {
        public SqlConnection con;
        public City_Name_Handler()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

        public List<City_Name> GetItemList()
        {
            List<City_Name> iList = new List<City_Name>();

            string query = "SELECT * FROM City_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new City_Name
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString()
                });
            }
            return iList;
        }
    }
}

