using DocterSpot.Models;
using DocterSpot.Respository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DocterSpot.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Home page for all (Admin,Doctor,Patient,User)
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AboutUs()
        { 
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// Signup for user's given in the ui based field value's will stored in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(UserModel model)
        {
            try
            {
                UserRepo repo = new UserRepo();
                bool flag = repo.InsertRecord(model);
                if (flag)
                {
                    return RedirectToAction("SignIn", "Home");
                }
                else
                {
                    ViewBag.Message = "Email already taken";
                }
                return View();
            }
            catch(Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View(); 
            }
        }

        public ActionResult SignIn()
        {
            ModelState.Clear();
            return View();
        }


        /// <summary>
        /// Sign for all (Admin,Doctor,Patient)
        /// Based on the email & password matches bidirect to respective page
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View page</returns>
       [HttpPost]
        public ActionResult SignIn(UserModel model)
        {
            try
            {
                int result;
                int id;
                string name;
                UserRepo repo = new UserRepo();
                bool flag = repo.CheckSignIn(model, out result, out id, out name);
                if (flag)
                {
                    if (result == 1)
                    {
                        return RedirectToAction("Home", "Patient", new { id = Session["Id"], name = Session["FirstName"] });
                    }
                    else if (result == 2)
                    {
                        return RedirectToAction("Home", "Doctor", new { id = Session["Id"], name = Session["FirstName"] });
                    }
                    else if (result == 3)
                    {
                        return RedirectToAction("HomeAdmin", "Admin", new { id = Session["Id"], name = Session["FirstName"] });
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid email and password";
                    return View();
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
        /// Forgot password 
        /// </summary>
        /// <returns> Bool -- T or F --</returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(UserModel usermodel)
        {
            UserRepo userRepo = new UserRepo();
            bool flag = userRepo.UpdatePassword(usermodel);
            if(flag)
            {
                return RedirectToAction("SignIn", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid email and name";
            }
            return View();
        }

        // Get: Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // Clear session data
            return RedirectToAction("Home", "Home");
        }


        // GET: Contacts/Create
        public ActionResult Contact()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        public ActionResult Contact(ContactModel contactModel)
        {
            try
            {
                UserRepo contactRepository = new UserRepo();
                bool flag = contactRepository.InsertRemarks(contactModel);
                if (flag)
                {
                    ViewBag.Message = "Thanks for your comments";
                }
                return View();
            }
            catch (Exception exception) 
            {
                ErrorLog.LogError(exception);
                return View();
            }
        }


    }
}