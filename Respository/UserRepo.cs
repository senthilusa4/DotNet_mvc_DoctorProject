using DocterSpot.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DocterSpot.Respository
{
    public class UserRepo
    {

        private SqlConnection con;

        private void Connectivity()
        {
            string connect1 = ConfigurationManager.ConnectionStrings["connect"].ToString();
            con = new SqlConnection(connect1);
        }

        /// <summary>
        /// inserting the registration details to the table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool --insert means T otherwise F--</returns>
        public bool InsertRecord(UserModel obj)
        {
            try
            {
                Connectivity();
                SqlCommand command1 = new SqlCommand("CheckEmail", con);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@Email", obj.Email);
                con.Open();
                SqlDataReader reader = command1.ExecuteReader();

                if (reader.Read() == true)
                {
                    return false;
                }
                else
                {
                    con.Close();
                    SqlCommand command = new SqlCommand("UserRegister", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Role", 1);
                    command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    command.Parameters.AddWithValue("@LastName", obj.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);
                    command.Parameters.AddWithValue("@Age", obj.Age);
                    command.Parameters.AddWithValue("@Gender", obj.Gender);
                    command.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                    command.Parameters.AddWithValue("@Address", obj.Address);
                    command.Parameters.AddWithValue("@City", obj.City);
                    command.Parameters.AddWithValue("@State", obj.State);
                    command.Parameters.AddWithValue("@Email", obj.Email);
                    command.Parameters.AddWithValue("@Password", Encrypt(obj.Password));
                    con.Open();
                    int i = command.ExecuteNonQuery();

                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Based on given in the ui email & password matches record. Reader will read the entire row
        /// That object based get the Id and Role 
        ///     # Id for adding session 
        ///     # Role for Bidirect to page( based on email& password matches)  
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="result"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckSignIn(UserModel userModel,out int result,out int id,out string name)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("ReadSignInDetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", userModel.Email);
                command.Parameters.AddWithValue("@Password", Encrypt(userModel.Password));
                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["Role"];
                    id = (int)reader["Id"];
                    name = (string)reader["FirstName"];
                    HttpContext.Current.Session["Id"] = id;
                    HttpContext.Current.Session["FirstName"] = name;

                    FormsAuthentication.SetAuthCookie(userModel.Email,true);
                    return true;
                }
                else
                {
                    result = 0;
                    id = 0;
                    name = "!!!";
                    return false;
                }
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Forgot password firstname and email maches means password will change the respective user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>return true or false</returns>
        public bool UpdatePassword(UserModel userModel)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("ForgotPassword", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                command.Parameters.AddWithValue("@Email", userModel.Email);
                command.Parameters.AddWithValue("@Password", Encrypt(userModel.Password));
                con.Open();
                int i = command.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Inserting the remarks or query to the contact table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>return bool true or fasle </returns>
        public bool InsertRemarks(ContactModel obj)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("InsertRemarks", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                command.Parameters.AddWithValue("@Remarks", obj.Remarks);

                con.Open();
                int i = command.ExecuteNonQuery();
                if (i == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                con.Close();
            }

        }


        /// <summary>
        /// Normal text to converting the encrypted text
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns>string - encrypted text </returns>
        private static string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

    }

}