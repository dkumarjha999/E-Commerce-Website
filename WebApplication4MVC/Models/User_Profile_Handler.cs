using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace WebApplication4MVC.Models
{
    public class User_Profile_Handler
    {

        public SqlConnection con;
        public User_Profile_Handler()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

        public List<User_Profile> GetItemList()
        {
            List<User_Profile> iList = new List<User_Profile>();

            string query = @"select * from User_Profile";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new User_Profile
                {
                    Uid = Convert.ToInt32(dr["Uid"]),
                    UserName = dr["UserName"].ToString(),
                    PhoneNo = dr["PhoneNo"].ToString(),
                    Email = dr["Email"].ToString(),                                    
                    //UserPassword = dr["UserPassword"].ToString(),                   
                    //Country = Convert.ToInt32(dr["Country"]),
                    //State = Convert.ToInt32(dr["State"]),
                    //City = Convert.ToInt32(dr["City"]),
                    //Address = dr["Address"].ToString(),
                    //CountryName = (dr["CountryName"]).ToString(),
                    //StateName = (dr["StateName"]).ToString(),
                    //CityName = (dr["CityName"]).ToString(),
                    //Img = dr["Img"].ToString(),
                    RegStatus = Convert.ToBoolean(dr["RegStatus"].ToString())
                });
            }
            return iList;
        }


        // 2. ********** Insert Item **********
        public bool InsertItem(User_Profile iList)
        {
            bool lsDuplicate = false;

            string query = "SELECT * FROM User_Profile Where PhoneNo = '" + iList.PhoneNo + "'";
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
                       
                //int ModifyBy = 1;
                //DateTime ModifyBydate = DateTime.Now;

                query = @"INSERT INTO User_Profile(UserName,PhoneNo,Email,UserPassword,RegStatus)
                            VALUES
                            ('" + iList.UserName + "','" + iList.PhoneNo + "','" + iList.Email + "','" + iList.UserPassword + "','"+iList.RegStatus+"')";
                cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
            }

            return false;

        }


        public bool GetPassword(User_Profile iList)
        {
            bool IsMatched = false;

            string query = "SELECT * FROM User_Profile Where PhoneNo = '" + iList.PhoneNo + "' and Email='" + iList.Email+ "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                IsMatched = true;
            }
            con.Close();
            return IsMatched;
        }


        public bool Login(User_Profile iList)
        {
            bool IsMatched = false;

            string query1 = "SELECT * FROM User_Profile Where PhoneNo = '" + iList.PhoneNo + "' and UserPassword='" + iList.UserPassword + "'";       
            con.Open();
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataReader rdr1 = cmd1.ExecuteReader();           
            if (rdr1.Read())
            {
                IsMatched = true;
            }
            con.Close();
            //string query2 = "SELECT * FROM User_Profile Where Email = '" + iList.Email + "' and UserPassword='" + iList.UserPassword + "'";
            //con.Open();
            //SqlCommand cmd2 = new SqlCommand(query2, con);
            //SqlDataReader rdr2 = cmd2.ExecuteReader();       
            //if (rdr2.Read())
            //{
            //    IsMatched = true;
            //}
            //con.Close();
            return IsMatched;
        }



       //3. ********** Update Item Details **********
        //public bool UpdateItem(User_Profile iList)
        //{
        //    bool IsMatched = false;
        //    string query1 = "SELECT * FROM User_Profile Where PhoneNo = '" + iList.PhoneNo + "' and UserPassword='" + iList.UserPassword + "'";
        //    con.Open();
        //    SqlCommand cmd1 = new SqlCommand(query1, con);
        //    SqlDataReader rdr1 = cmd1.ExecuteReader();
        //   bool x=if(rdr1.Read())
        //   {IsMatched=true;}
        //        string query2 = "UPDATE User_Profile SET Img = '" + iList.Img + "', Country = " + iList.Country + ", State = " + iList.State + ", City = " + iList.City + ", Address = '" + iList.Address + "' WHERE Uid = " + iList.Uid;
        //        SqlCommand cmd = new SqlCommand(query2, con);
        //        con.Open();
        //        int i = cmd.ExecuteNonQuery();
        //        con.Close();

        //        if (i >= 1)
        //        { return true; }
        //        else
        //        { return false; }

                
        //    }

            
           



        //}

        // 4. ********** Delete Item **********
        public bool DeleteItem(int Id)
        {

            string query = "DELETE FROM User_Profile WHERE Uid = " + Id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

//        public List<string> getStateCountry(int Id)
//        {
//            List<string> res = new List<string>();
//            string query = @"select a.StateId, (Select Name from State_Name b where b.Id = a.StateId) as StateName,a.CountryId,
//            (Select Name from Country_Name c where c.Id = a.CountryId) as CountryName
//            from City_Name a
//            where a.Id = " + Id + "";

//            SqlCommand cmd = new SqlCommand(query, con);
//            con.Open();
//            SqlDataReader rdr = cmd.ExecuteReader();
//            while (rdr.Read())
//            {
//                res.Add(rdr["StateId"].ToString());
//                res.Add(rdr["StateName"].ToString());
//                res.Add(rdr["CountryId"].ToString());
//                res.Add(rdr["CountryName"].ToString());
//            }
//            con.Close();
//            return (res);
//        }

    }
}