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

            return View("Index", null);
        }

        [HttpPost]
        public ActionResult Search(SearchForm form)
        {
            IEnumerable<DetailsView> results = carRepo.GetSearchResults(
                form.Keyword, 
                form.SelectedModelID,
                form.SelectedMakeID,
                form.SelectedLocation
            );
            
            return View("Index", results);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            ViewBag.LoggedInID = ((UserSessionData)Session["UserData"]).UserID;
            DetailsView dv = carRepo.GetCarAndAssociatedDetails(id);
            dv.DetailsRequested = false;

            return View(dv);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ReqDetails(RequestForm form)
        {
            ViewBag.LoggedInID = ((UserSessionData)Session["UserData"]).UserID;
            DetailsView dv = carRepo.GetCarAndAssociatedDetails(form.CarID);
            

            // Add your code here
            
            dv.DetailsRequested = true;

            return View("Details" , dv);
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
