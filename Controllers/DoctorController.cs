using DocterSpot.Models;
using DocterSpot.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocterSpot.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        [Authorize]
        public ActionResult Home()
        {
            return View();
        }

        [Authorize]
        public ActionResult PatientQueue(int id)
        {
            try
            {
                DoctorRepo repo = new DoctorRepo();
                List<PatientModel> ConfirmList = repo.ConfirmPatientDetails(id);
                return View(ConfirmList);
            }
            catch(Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        [Authorize]
        public ActionResult PatientRequest(int id)
        {
            try
            {
                DoctorRepo repo = new DoctorRepo();
                List<PatientModel> PatientList = repo.FetchPatient(id);
                return View(PatientList);
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View(); 
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult PatientRequest(int itemId, int status)
        {
            try
            {
                DoctorRepo repo = new DoctorRepo();
                bool flag = repo.ModifyStatus(itemId, status);
                return View();
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }


        // GET: Docter/Edit
        [Authorize]
        public ActionResult EditDoctorProfile(int id)
        {
            try
            {
                DoctorRepo repo = new DoctorRepo();
                return View(model: repo.Fetch(id).Find(e => e.Id == id));
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        // POST: Docter/Edit
        [HttpPost]
        [Authorize]
        public ActionResult EditDoctorProfile(DoctorModel model, int id)
        {
            try
            {
                DoctorRepo repo = new DoctorRepo();
                bool flag = repo.Update(model, id);
                if (flag)
                {
                    ViewBag.Message = "Doctor details updated successfully";
                }
                return View();
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

    }
}
