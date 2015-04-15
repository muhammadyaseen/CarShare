using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Net;
using CarShare.WebUI.Models;
using CarShare.Domain.Entities;
using CarShare.Domain.Abstract;

namespace CarShare.WebUI.Controllers
{
    public class CarController : Controller
    {
        ICarRepository carRepo;


         public CarController(ICarRepository carRep)
        {
            this.carRepo = carRep;
        }

        // GET: MyContacts
        public ActionResult Index()
        {
            return View();
        }



        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    string query = "SELECT Title, Capacity  from [addcar] where CarID = " + id + ";";
        //    DataSet ds = CarShare.Domain.Concrete.SQLUserRepository.SendDataSet(query);

        //    if (ds == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Car contact = new Car();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        contact = new Car { CarID = Convert.ToInt32(dr["CarID"]), Title = Convert.ToString(dr["Title"]), Capacity = Convert.ToString(dr["Capacity"]) };
        //    }

        //    return View(contact);
        //}

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public ActionResult Create(Car c)
        {

           if (ModelState.IsValid)
           {
               if (carRepo.Save(c)) return RedirectToAction("Profile","Home"); 
           }

           return View(c);

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Car c = carRepo.GetCarByID(id);

            return View(c);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Title,Capacity")] Car c)
        {

            if (ModelState.IsValid)
            {
                if (carRepo.Update(c)) return RedirectToAction("Profile", "Home");
            }

            return View(c);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Car c = carRepo.GetCarByID(id);

            return View(c);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carRepo.Delete(id);

            return RedirectToAction("Profile", "Home");
        }
    }
}


