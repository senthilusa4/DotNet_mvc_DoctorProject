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

namespace DocterSpot.Respository
{
    public class AdminRepo
    {
        private SqlConnection con;

        private void Connectivity()
        {
            string connect1 = ConfigurationManager.ConnectionStrings["connect"].ToString();
            con = new SqlConnection(connect1);
        }

        /// <summary>
        /// View / Read all doctor details  
        /// </summary>
        /// <returns>List --Doctor's list--</returns>
        public List<DoctorModel> DoctorDetails()
        {
            try
            {
                Connectivity();
                con.Open();
                List<DoctorModel> doctorList = new List<DoctorModel>();
                SqlCommand command = new SqlCommand("ReadDoctorForUser", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Role", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    if (
                        r["Id"] != DBNull.Value &&
                        r["FirstName"] != DBNull.Value &&
                        r["LastName"] != DBNull.Value &&
                        r["DateOfBirth"] != DBNull.Value &&
                        r["Age"] != DBNull.Value &&
                        r["Gender"] != DBNull.Value &&
                        r["DoctorId"] != DBNull.Value &&
                        r["Specialization"] != DBNull.Value &&
                        r["MobileNumber"] != DBNull.Value &&
                        r["Address"] != DBNull.Value &&
                        r["State"] != DBNull.Value &&
                        r["City"] != DBNull.Value &&
                        r["Email"] != DBNull.Value &&
                        r["Password"] != DBNull.Value &&
                        r["Photo"] != DBNull.Value
                    )
                    {
                        doctorList.Add(

                            new DoctorModel
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                FirstName = Convert.ToString(r["FirstName"]),
                                LastName = Convert.ToString(r["LastName"]),
                                DateOfBirth = Convert.ToDateTime(r["DateOfBirth"]),
                                Age = Convert.ToInt32(r["Age"]),
                                Gender = Convert.ToString(r["Gender"]),
                                DoctorId = Convert.ToString(r["DoctorId"]),
                                Specialization = Convert.ToString(r["Specialization"]),
                                MobileNumber = Convert.ToString(r["MobileNumber"]),
                                Address = Convert.ToString(r["Address"]),
                                State = Convert.ToString(r["State"]),
                                City = Convert.ToString(r["City"]),
                                Email = Convert.ToString(r["Email"]),
                                Password = Convert.ToString(r["Password"]),
                                Photo = (byte[])(r["Photo"])
                            }
                        );
                    }
                }

                return doctorList;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Add new doctor by the admin 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Bool -true-false-</returns>
        public bool AddDoctor(ManageDoctorModel obj)
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
                    SqlCommand command = new SqlCommand("AddDocter", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Role", 2);
                    command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    command.Parameters.AddWithValue("@LastName", obj.LastName);
                    command.Parameters.AddWithValue("@Email", obj.Email);
                    command.Parameters.AddWithValue("@Password",Encrypt(obj.Password));

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
        /// Delete or remove doctor record 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bool -true-false- </returns>
        public bool Delete(int id)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("DeleteDoctor", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                con.Open();
                int i = command.ExecuteNonQuery();
                con.Close();

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
        /// Display all the admin details
        /// </summary>
        /// <returns></returns>
        public List<AdminModel> AdminDetails()
        {
            try
            {
                Connectivity();
                con.Open();
                List<AdminModel> AdminList = new List<AdminModel>();
                SqlCommand command = new SqlCommand("ReadAdmin", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Role", 3);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    AdminList.Add(

                        new AdminModel
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            FirstName = Convert.ToString(r["FirstName"]),
                            LastName = Convert.ToString(r["LastName"]),
                            Email = Convert.ToString(r["Email"]),
                            Password = Decrypt(Convert.ToString(r["Password"]))
                        }
                    );
                }

                return AdminList;
            }
            finally 
            {
                con.Close(); 
            }    
        }

        /// <summary>
        /// Register admin by the admin & adding to the register table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> Bool true/false </returns>
        public bool AddAdmin(AdminModel obj)
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
                    SqlCommand command = new SqlCommand("AddAdmin", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Role", 3);
                    command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    command.Parameters.AddWithValue("@LastName", obj.LastName);
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
        /// if click profile that respective id based details should show in the edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AdminModel> Fetch(int id)
        {
            try
            {
                Connectivity();
                con.Open();
                List<AdminModel> AdminList = new List<AdminModel>();
                SqlCommand command = new SqlCommand("ReadAdminDetails", con);
                command.Parameters.AddWithValue("@Id", id);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    AdminList.Add(

                        new AdminModel
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            FirstName = Convert.ToString(r["FirstName"]),
                            LastName = Convert.ToString(r["LastName"]),
                            Email = Convert.ToString(r["Email"]),
                            Password = Decrypt(Convert.ToString(r["Password"]))
                        }
                    );
                }

                return AdminList;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// That fetching details can update or modify. Then click save button the details can change
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns> bool </returns>
        public bool Update(AdminModel obj ,int id)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("UpdateAdmin", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                command.Parameters.AddWithValue("@LastName", obj.LastName);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@Password", Encrypt(obj.Password));
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
        /// Delete admin details by the admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bool true/false </returns>
        public bool DeleteAdminDetail(int id,int AdminId)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("DeleteAdmin", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                if (id != AdminId)
                {
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
        /// Display all the user details 
        /// </summary>
        /// <returns>List of user details</returns>
        public List<UserModel> UserDetails()
        {
            try
            {
                Connectivity();
                con.Open();
                List<UserModel> UserList = new List<UserModel>();
                SqlCommand command = new SqlCommand("ReadUser", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Role", 1);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    UserList.Add(

                        new UserModel
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            FirstName = Convert.ToString(r["FirstName"]),
                            LastName = Convert.ToString(r["LastName"]),
                            DateOfBirth = Convert.ToDateTime(r["DateOfBirth"]),
                            Age = Convert.ToInt32(r["Age"]),
                            Gender = Convert.ToString(r["Gender"]),
                            MobileNumber = Convert.ToString(r["MobileNumber"]),
                            Email = Convert.ToString(r["Email"]),
                            Password = Convert.ToString(r["Password"])
                        }
                    );
                }

                return UserList;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// List of remarks or query fill in the contact form that details will show the admin module
        /// </summary>
        /// <returns>List of query filled user details </returns>
        public List<ContactModel> FeedbackDetails()
        {
            try {

                Connectivity();
                con.Open();
                List<ContactModel> ContactList = new List<ContactModel>();
                SqlCommand command = new SqlCommand("RemarkDetails", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    ContactList.Add(

                        new ContactModel
                        {
                            Name = Convert.ToString(r["Name"]),
                            Email = Convert.ToString(r["Email"]),
                            MobileNumber = Convert.ToString(r["MobileNumber"]),
                            Remarks = Convert.ToString(r["Remarks"])
                        }
                    );
                }
                return ContactList;
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

        /// <summary>
        /// Encryped text to converting the normal text
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns>string - normal text </returns>
        private static string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}