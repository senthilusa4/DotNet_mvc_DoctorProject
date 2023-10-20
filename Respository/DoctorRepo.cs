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
    public class DoctorRepo
    {
        private SqlConnection con;

        private void Connectivity()
        {
            string connect1 = ConfigurationManager.ConnectionStrings["connect"].ToString();
            con = new SqlConnection(connect1);
        }

        /// <summary>
        /// if click profile that respective id based details should show in the edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DoctorModel> Fetch(int id)
        {
            try
            {
                Connectivity();
                con.Open();
                List<DoctorModel> doctorList = new List<DoctorModel>();
                SqlCommand command = new SqlCommand("ReadDoctorDetails", con);
                command.Parameters.AddWithValue("@Id", id);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    doctorList.Add(

                        new DoctorModel
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            FirstName = Convert.ToString(r["FirstName"]),
                            LastName = Convert.ToString(r["LastName"]),
                            Email = Convert.ToString(r["Email"]),
                            Password = Decrypt(Convert.ToString(r["Password"]))
                        }
                    );
                }
                return doctorList;
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
        public bool Update(DoctorModel obj ,int id)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("EditDoctorDetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                command.Parameters.AddWithValue("@LastName", obj.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);
                command.Parameters.AddWithValue("@Age", obj.Age);
                command.Parameters.AddWithValue("@Gender", obj.Gender);
                command.Parameters.AddWithValue("@DoctorId", obj.DoctorId);
                command.Parameters.AddWithValue("@Specialization", obj.Specialization);
                command.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                command.Parameters.AddWithValue("@Address", obj.Address);
                command.Parameters.AddWithValue("@State", obj.State);
                command.Parameters.AddWithValue("@City", obj.City);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@Password", Encrypt(obj.Password));
                command.Parameters.AddWithValue("@Photo", obj.Photo);
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
        /// Based on the doctorId who all are booking the doctor appointment that respective patient details display
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of patient details</returns>
        public List<PatientModel> FetchPatient(int id)
        {
            try
            {
                Connectivity();
                con.Open();
                List<PatientModel> PatientList = new List<PatientModel>();
                SqlCommand command = new SqlCommand("GetPatientDetails", con);
                command.Parameters.AddWithValue("@DoctorId", id);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    PatientList.Add(

                        new PatientModel
                        {
                            PatientName = Convert.ToString(r["PatientName"]),
                            PatientId = Convert.ToInt32(r["PatientId"]),
                            Issue = Convert.ToString(r["Issue"]),
                            VisitDate = Convert.ToDateTime(r["VisitDate"]),
                            VisitTime = Convert.ToString(r["VisitTime"]),
                            Status = Convert.ToInt32(r["Status"])
                        }
                    );
                }
                return PatientList;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Based on the doctor's conformation status 
        /// That status will update in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns>bool -- update status execute means T (or) F --</returns>
        public bool ModifyStatus(int id,int status) 
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("ModifyStatus", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PatientId", id);
                command.Parameters.AddWithValue("@Status", status);
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
        /// Already given conformation patient view page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of conformation patient details</returns>
        public List<PatientModel> ConfirmPatientDetails(int id)
        {
            try
            {
                Connectivity();
                con.Open();
                List<PatientModel> PatientList = new List<PatientModel>();
                SqlCommand command = new SqlCommand("ConfirmAppointment", con);
                command.Parameters.AddWithValue("@DoctorId", id);
                command.Parameters.AddWithValue("@Status", 1);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    PatientList.Add(

                        new PatientModel
                        {
                            PatientName = Convert.ToString(r["PatientName"]),
                            PatientId = Convert.ToInt32(r["PatientId"]),
                            Issue = Convert.ToString(r["Issue"]),
                            VisitDate = Convert.ToDateTime(r["VisitDate"]),
                            VisitTime = Convert.ToString(r["VisitTime"]),
                            Status = Convert.ToInt32(r["Status"])
                        }
                    );
                }
                return PatientList;
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