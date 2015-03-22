using CarShare.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShare.Domain.Entities;

namespace CarShare.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        IUserRepository userRepo;

        public HomeController(IUserRepository uRepo)
        {
            this.userRepo = uRepo;
        }

        public ActionResult Index()
        {
            User u = userRepo.GetUserByID(1);

            return View(u);
        }

        public ActionResult Login()
        {
            User u = userRepo.Authenticate("john.d@email.com", "test");

            return View(u);
        }
    }
}
