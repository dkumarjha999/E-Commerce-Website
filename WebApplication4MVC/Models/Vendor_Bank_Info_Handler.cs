using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace WebApplication4MVC.Models
{
    public class Vendor_Bank_Info_Handler
    {

     public SqlConnection con;
     public Vendor_Bank_Info_Handler()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** get  Item **********

     public List<Vendor_Bank_Info> GetItemList()
        {
            List<Vendor_Bank_Info> iList = new List<Vendor_Bank_Info>();

            string query = @"select a.*,b.VendorName from Vendor_Bank_Info a join Vendor_Info b ON a.VendorId=b.VendorId";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();


            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new Vendor_Bank_Info
                {
                     Id = Convert.ToInt32(dr["Id"]),
                    VendorId = Convert.ToInt32(dr["VendorId"]),
                    VendorName=(dr["VendorName"]).ToString(),
                    BankName = dr["BankName"].ToString(), 
                    Branch = dr["Branch"].ToString(),
                    IFSCCode = dr["IFSCCode"].ToString(),
                    AccountNo = dr["AccountNo"].ToString(),
                    
                });
            }
            return iList;
        }


        // 2. ********** Insert Item **********
     public bool InsertItem(Vendor_Bank_Info iList)
        {
            bool lsDuplicate = false;

            string query = "SELECT * FROM Vendor_Bank_Info Where AccountNo = '" + iList.AccountNo + "'";
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
                //bool MobileVerification = true;
                //bool EmailVerification = true;
                ////int ModifyBy = 1;
                ////DateTime ModifyBydate = DateTime.Now;

                query = @"INSERT INTO Vendor_Bank_Info(VendorId,BankName,Branch,IFSCCode,AccountNo)
                            VALUES
                            (" + iList.VendorId + ",'" + iList.BankName + "','" + iList.Branch + "','" + iList.IFSCCode + "','" +iList.AccountNo + "')";
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
     public bool UpdateItem(Vendor_Bank_Info iList)
        {

            int cnt = 1;
            // bool lsDuplicate = false;
            string query = "SELECT * FROM Vendor_Bank_Info Where AccountNo = '" + iList.AccountNo + "'";
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
            
                //// DateTime ModifyBydate = DateTime.Now;
                query = "UPDATE Vendor_Bank_Info SET VendorId = '" + iList.VendorId + "',BankName = '" + iList.BankName + "', Branch = '" + iList.Branch + "', IFSCCode = '" + iList.IFSCCode + "', AccountNo = '" + iList.AccountNo + "' WHERE Id = " + iList.Id;
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

            string query = "DELETE FROM Vendor_Bank_Info WHERE Id = " + Id;
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

