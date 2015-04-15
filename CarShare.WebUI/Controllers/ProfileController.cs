using CarShare.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShare.Domain.Entities;
using CarShare.WebUI.Models;
using System.Web.Security;
using System.IO;

namespace CarShare.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        IUserRepository userRepo;

        public ProfileController(IUserRepository uRepo)
        {
            this.userRepo = uRepo;
        }

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User u, HttpPostedFileBase Image)
        {
            if (u.Password != u.PasswordAgain) ModelState.AddModelError("PasswordAgain", "Passwords don't match!");

            if (ModelState.IsValid)
            {
                //save file to File System
                //string relativePath = "~/Content/Uploads/" + Path.GetFileName(image.FileName);
                
                u.Status = "ACTIVE"; //default value
                
                if (Image != null) //user supplied no profile photo
                {
                    string relativePath = "~/Uploads/" + Path.GetFileName(Image.FileName);
                    string physicalPath = Server.MapPath(relativePath);

                    Image.SaveAs(physicalPath);

                    u.Image = Url.Content(relativePath);
                }
                else
                {
                    u.Image = Url.Content("~/Uploads/Default.png"); //user hasn't supplied an image, we will use default
                }
                if (userRepo.Save(u) == 1)
                {
                    return RedirectToAction("SignIn"); //successful insertion
                }
                else
                    return View("Error");
               
            }
            else //there was a form validation or other error.
            {
                return View(u);
            }
        }



        [HttpGet]
        public ActionResult SignIn()
        {
            return View( new SignInViewModel());
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel m)
        {
            if (ModelState.IsValid)
            {
                User u = userRepo.Authenticate(m.Email, m.Password);
                bool isAuthenticated = Membership.ValidateUser(m.Email, m.Password);

                if (u != null && isAuthenticated)
                {
                    UserSessionData sessionData = new UserSessionData()
                    {
                        UserID = u.UserID,
                        Name = u.Name,
                    };

                    System.Web.HttpContext.Current.Session["UserData"] = sessionData;
                    Session.Timeout = 10;


                    FormsAuthentication.SetAuthCookie(m.Email, true);

                    /*FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        u.Email,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        u.UserID.ToString()
                    );

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
                    */

                    return RedirectToAction("Home"); 
                }
                else
                {
                    ModelState.AddModelError("SignIn","Email or Password Incorrect");
                }
            }
            
            return View(m);
        }

        [Authorize]
        public ActionResult Home()
        {
            UserSessionData loggedInUser = (UserSessionData)System.Web.HttpContext.Current.Session["UserData"];

            User u = userRepo.GetUserByID(((UserSessionData)System.Web.HttpContext.Current.Session["UserData"]).UserID);

            return View(u); 
            
        }

    }
}
