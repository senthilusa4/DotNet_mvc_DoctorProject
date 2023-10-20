using DocterSpot.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DocterSpot.Respository
{
    public class PatientRepository
    {
        private SqlConnection con;

        private void Connectivity()
        {
            string connect1 = ConfigurationManager.ConnectionStrings["connect"].ToString();
            con = new SqlConnection(connect1);
        }


        /// <summary>
        /// Displaying the doctors details
        /// </summary>
        /// <returns>List of doctors details</returns>
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
                        r["Specialization"] != DBNull.Value &
                        r["Photo"] != DBNull.Value
                    )
                        doctorList.Add(

                        new DoctorModel
                        {
                            Id = Convert.ToInt32(r["ID"]),
                            FirstName = Convert.ToString(r["FirstName"]),
                            Specialization = Convert.ToString(r["Specialization"]),
                            Photo = (byte[])(r["Photo"])
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
        /// List the specialization based doctor etails to the page
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns>List of doctor's</returns>
        public List<DoctorModel> DoctorDetails(string specialization)
        {
            try
            {
                Connectivity();
                con.Open();
                List<DoctorModel> doctorList = new List<DoctorModel>();
                SqlCommand command = new SqlCommand("FilterDoctorDetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Specialization", specialization);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    if (
                        r["Id"] != DBNull.Value &&
                        r["FirstName"] != DBNull.Value &&
                        r["Specialization"] != DBNull.Value &
                        r["Photo"] != DBNull.Value
                    )
                        doctorList.Add(

                        new DoctorModel
                        {
                            Id = Convert.ToInt32(r["ID"]),
                            FirstName = Convert.ToString(r["FirstName"]),
                            Specialization = Convert.ToString(r["Specialization"]),
                            Photo = (byte[])(r["Photo"])
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
        /// Appointment conformation 
        /// User giving time or date doctor busy means the data not insert
        /// Otherwise doctor available means only the data insert
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <param name="doctorId"></param>
        /// <returns> bool -- if available T not means F --</returns>
        public bool PatientDetails(PatientModel obj,int id,int doctorId,string name)
        {
            try
            {
                Connectivity();
                SqlCommand command = new SqlCommand("DoctorAvailability", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DoctorId", doctorId);
                command.Parameters.AddWithValue("@VisitDate", obj.VisitDate);
                command.Parameters.AddWithValue("@VisitTime", obj.VisitTime);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (true == reader.Read())
                {
                    return false;
                }
                else
                {
                    con.Close();
                    SqlCommand command1 = new SqlCommand("InsertPatient", con);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@Id", id);
                    command1.Parameters.AddWithValue("@PatientName", obj.PatientName);
                    command1.Parameters.AddWithValue("@DoctorId", doctorId);
                    command1.Parameters.AddWithValue("@Issue", obj.Issue);
                    command1.Parameters.AddWithValue("@VisitDate", obj.VisitDate);
                    command1.Parameters.AddWithValue("@VisitTime", obj.VisitTime);
                    command1.Parameters.AddWithValue("@Status", 0);

                    con.Open();
                    int i = command1.ExecuteNonQuery();

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
        /// View page for patient, Notification for conformation appointment to the patient 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PatientModel> AppointmentStatus(int id)
        {
            try
            {
                Connectivity();
                con.Open();
                List<PatientModel> PatientList = new List<PatientModel>();
                SqlCommand command = new SqlCommand("ViewPatientDetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
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
    }
}