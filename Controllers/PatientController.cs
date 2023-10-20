using DocterSpot.Models;
using DocterSpot.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace DocterSpot.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        [Authorize]
        public ActionResult Home()
        { 
            return View();
        }

        [Authorize]
        public ActionResult OurServices()
        {
            return View();
        }

        [Authorize]
        public ActionResult DoctorDetails(string specialization)
        { 
            try{
                PatientRepository patient = new PatientRepository();
                if (!string.IsNullOrEmpty(specialization))
                {
                    List<DoctorModel> doctorList = patient.DoctorDetails(specialization);
                    return View(doctorList);
                }
                else
                {
                    List<DoctorModel> Doctor = patient.DoctorDetails();
                    ModelState.Clear();
                    return View(Doctor);
                }
            }
            catch (Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        [Authorize]
        // GET: Patient
        public ActionResult PatientForm(int id,int doctorId,string name)
        {
            try
            {
                PatientModel patient = new PatientModel();
                patient.PatientName = name;
                return View(patient);
            }
            catch (Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult PatientForm(PatientModel patientModel, int id, int doctorId, string name)
        {
            try
            {
                PatientRepository patientRepository = new PatientRepository();
                bool flag = patientRepository.PatientDetails(patientModel, id, doctorId, name);
                if (flag)
                {
                    ViewBag.Message = "Thanks for visiting us Your appointment will be in the queue" +
                        " wait for the doctor confirmation";
                }
                else
                {
                    ViewBag.Message = "Doctor not Available (or) busy";
                }
                return View();
            }
            catch (Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        [Authorize]
        public ActionResult AppointmentStatus(int id) 
        {
            try
            {
                PatientRepository patientRepo = new PatientRepository();
                List<PatientModel> patientList = patientRepo.AppointmentStatus(id);
                return View(patientList);
            }
            catch (Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

    }
}
