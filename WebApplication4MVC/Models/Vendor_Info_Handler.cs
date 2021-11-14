using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace WebApplication4MVC.Models
{
    public class Vendor_Info_Handler
    {
        public SqlConnection con;
        public Vendor_Info_Handler()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

        public List<Vendor_Info> GetItemList()
        {
            List<Vendor_Info> iList = new List<Vendor_Info>();

            string query = @"select a.*,b.Name as CityName,c.Name as StateName,d.Name as CountryName from Vendor_Info a join City_Name b ON a.City=b.Id join State_Name c ON a.State=c.Id join Country_Name d ON a.Country=d.Id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Vendor_Info
                {
                    VendorName = dr["VendorName"].ToString(),
                    Mobile = dr["Mobile"].ToString(),
                    Email = dr["Email"].ToString(),
                    MobileVerification = Convert.ToBoolean(dr["MobileVerification"].ToString()),
                    EmailVerification = Convert.ToBoolean(dr["EmailVerification"].ToString()),
                    Country = Convert.ToInt32(dr["Country"]),                 
                    State = Convert.ToInt32(dr["State"]),                    
                    City = Convert.ToInt32(dr["City"]),                  
                    PinCode = dr["PinCode"].ToString(),
                    GSTIN = dr["GSTIN"].ToString(),
                    PanCardNumber = dr["PanCardNumber"].ToString(),
                    VendorId = Convert.ToInt32(dr["VendorId"]),
                    Address = dr["Address"].ToString(),
                    CountryName = (dr["CountryName"]).ToString(),
                    StateName = (dr["StateName"]).ToString(),
                    CityName = (dr["CityName"]).ToString()
                });
            }
            return iList;
        }


        // 2. ********** Insert Item **********
        public bool InsertItem(Vendor_Info iList)
        {
            bool lsDuplicate = false;

            string query = "SELECT * FROM Vendor_Info Where GSTIN = '" + iList.GSTIN + "'";
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
                bool MobileVerification = true;
                bool EmailVerification = true;
                //int ModifyBy = 1;
                //DateTime ModifyBydate = DateTime.Now;

                query = @"INSERT INTO Vendor_Info(VendorName,Mobile,Email,MobileVerification,EmailVerification
                            ,Country,State,City,PinCode,GSTIN,PanCardNumber,Address)
                            VALUES
                            ('" + iList.VendorName + "','" + iList.Mobile + "','" + iList.Email + "','" + MobileVerification + "','" + EmailVerification + "' ," + iList.Country + "," + iList.State + ",'" + iList.City + "','" + iList.PinCode + "','" + iList.GSTIN + "','" + iList.PanCardNumber + "','" + iList.Address + "')";
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
        public bool UpdateItem(Vendor_Info iList)
        {

            int cnt = 1;
            // bool lsDuplicate = false;
            string query = "SELECT * FROM Vendor_Info Where GSTIN = '" + iList.GSTIN + "'";
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
                bool MobileVerification = false;
                bool EmailVerification = false;
                // DateTime ModifyBydate = DateTime.Now;
                query = "UPDATE Vendor_Info SET VendorName = '" + iList.VendorName + "', Mobile = '" + iList.Mobile + "', MobileVerification = '" + MobileVerification + "', EmailVerification = '" + EmailVerification + "', Country = " + iList.Country + ", State = " + iList.State + ", City = " + iList.City + ", PinCode = '" + iList.PinCode + "', GSTIN = '" + iList.GSTIN + "', PanCardNumber = '" + iList.PanCardNumber + "', Address = Address WHERE VendorId = " + iList.VendorId;
                cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
                else
                    return false;

            }


        }

        // 4. ********** Delete Item **********
        public bool DeleteItem(int Id)
        {

            string query = "DELETE FROM Vendor_Info WHERE VendorId = " + Id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<string> getStateCountry(int Id)
        {
            List<string> res = new List<string>();
            string query = @"select a.StateId, (Select Name from State_Name b where b.Id = a.StateId) as StateName,a.CountryId,
            (Select Name from Country_Name c where c.Id = a.CountryId) as CountryName
            from City_Name a
            where a.Id = " + Id + "";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                res.Add(rdr["StateId"].ToString());
                res.Add(rdr["StateName"].ToString());
                res.Add(rdr["CountryId"].ToString());
                res.Add(rdr["CountryName"].ToString());
            }
            con.Close();
            return (res);
        }


    }
}

