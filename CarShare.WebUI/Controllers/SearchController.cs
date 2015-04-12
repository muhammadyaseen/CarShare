using CarShare.Domain.Abstract;
using CarShare.Domain.Entities;
using CarShare.Domain.ViewEntities;
using CarShare.WebUI.Models;
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

        [ChildActionOnly]
        public PartialViewResult RenderSearchForm()
        {
            SearchForm form = new SearchForm();
            form.Makes = carRepo.GetAllMakes().Select(m => new SelectListItem { Text = m.MakeName, Value = m.MakeID.ToString() });

            form.Models = carRepo.GetAllModels().Select(m => new SelectListItem { Text = m.ModelName, Value = m.ModelID.ToString() }); 

            return PartialView(form);
        }

    }
}
