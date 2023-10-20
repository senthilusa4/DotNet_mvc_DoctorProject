using DocterSpot.Models;
using DocterSpot.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DocterSpot.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult HomeAdmin()
        {
            return View();
        }

        /// <summary>
        /// List of doctor details will show to the admin
        /// </summary>
        /// <returns>List</returns>
        // GET: Doctor Details
        [Authorize]
        public ActionResult DoctorDetails()
        {
            try
            {
                AdminRepo AdminRepo = new AdminRepo();
                List<DoctorModel> doctorList = AdminRepo.DoctorDetails();
                return View(doctorList);
            }
            catch(Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }


        // GET : Create or Add docters
        [Authorize]
        public ActionResult CreateDoctor()
        {
            return View();
        }

        /// <summary>
        /// Added doctor details to the table if already given email is there means
        /// Preventing message sending the admin
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // Create new doctor details
        [HttpPost]
        [Authorize]
        public ActionResult CreateDoctor(ManageDoctorModel obj)
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                bool flag = repo.AddDoctor(obj);
                if (flag)
                {
                    ViewBag.Message1 = "Doctor details added successfully.";
                }
                else
                {
                    ViewBag.Message2 = "Email already taken";
                }
                return View();
            }

            catch(Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Id based delete the respective doctor details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Delete docter record
        [Authorize]
        public ActionResult DeleteDocter(int id)
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                repo.Delete(id);
                return RedirectToAction("DoctorDetails");
            }
            catch(Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Admin entry or register based admindetails will show in the list view
        /// </summary>
        /// <returns>List view</returns>
        // GET: Admin Details
        [Authorize]
        public ActionResult AdminDetails()
        {
            try
            {
                AdminRepo adminRepo = new AdminRepo();
                List<AdminModel> AdminList = adminRepo.AdminDetails();
                return View(AdminList);
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }


        // GET : Create or Add Admin
        [Authorize]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        /// <summary>
        /// insering the admin details to the register table
        /// if given email already in the table means sending preventing message to the user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // Create new Admin details 
        [HttpPost]
        [Authorize]
        public ActionResult CreateAdmin(AdminModel obj)
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                bool flag = repo.AddAdmin(obj);
                if (flag)
                {
                    ViewBag.Message1 = "Admin details added successfully.";
                }
                else
                {
                    ViewBag.Message2 = "Email already taken";
                }
                return View();

            }

            catch(Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Fetching the already given field value's
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        // GET: Admin/Edit
        [Authorize]
        public ActionResult EditAdminProfile(int id)
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                return View(model: repo.Fetch(id).Find(e => e.Id == id));
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Updating the admin profile based on the session based getting the id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>View</returns>
        // POST: Admin/Edit
        [HttpPost]
        [Authorize]
        public ActionResult EditAdminProfile(AdminModel model, int id)
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                bool flag = repo.Update(model, id);
                if (flag)
                {
                    ViewBag.Message = "Admin details updated successfully";
                }
                return View();
            }
            catch (Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }



        /// <summary>
        /// Delete/remove admin record in a existing table 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Admin details View page </returns>
        [Authorize]
        public ActionResult DeleteAdmin(int id,int AdminId)
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                bool flag = repo.DeleteAdminDetail(id,AdminId);
                if (flag==true)
                {
                    return RedirectToAction("AdminDetails");
                }
                else
                {
                    ViewBag.Message = "You can't delete your own profile";
                    return RedirectToAction("AdminDetails");
                }
            }
            catch (Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// View details from the contact form query to admin
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult FeedBack()
        {
            try
            {
                AdminRepo repo = new AdminRepo();
                List<ContactModel> list = repo.FeedbackDetails();
                return View(list);
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }


        // GET: User Details
        [Authorize]
        public ActionResult UserDetails()
        {
            try
            {
                AdminRepo adminRepo = new AdminRepo();
                List<UserModel> UserList = adminRepo.UserDetails();
                return View(UserList);
            }
            catch(Exception exception)
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }


    }
}
