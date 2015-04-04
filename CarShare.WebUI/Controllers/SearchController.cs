using CarShare.Domain.Abstract;
using CarShare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShare.WebUI.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        ICarRepository carRepo;

        public SearchController(ICarRepository carRep)
        {
            this.carRepo = carRep;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            //Car car = carRepo.GetCarByID(carID);
            return View(carRepo.GetCarAndAssociatedDetails(id));
        }

    }
}
