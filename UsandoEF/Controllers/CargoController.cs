using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsandoEF.Models;

namespace UsandoEF.Controllers
{
    public class CargoController : Controller
    {
        public ActionResult Index()
        {
            List<Models.ViewModels.Cargo> cargos = new List<Models.ViewModels.Cargo>();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                cargos = (from cargo in db.Cargo
                          select new Models.ViewModels.Cargo
                          {
                              Id = cargo.Id,
                              Descripcion = cargo.Descripcion

                          }).ToList();
            }
           
            return View(cargos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                db.Cargo.Add(new Cargo
                {
                    Descripcion = collection["Descripcion"].ToString()
                });

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit (int Id)
        {
            Models.ViewModels.Cargo cargo = new Models.ViewModels.Cargo();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                cargo.Id = db.Cargo.Find(Id).Id;
                cargo.Descripcion = db.Cargo.Find(Id).Descripcion;
            }

            return View(cargo);
        }

        [HttpPost]
        public ActionResult Edit (int Id, FormCollection collection)
        {
            Models.Cargo cargo = new Models.Cargo();

            cargo.Id = Id;
            cargo.Descripcion = collection["Descripcion"].ToString();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                db.Cargo.Attach(cargo);

                db.Entry(cargo).Property("Descripcion").IsModified = true;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            Models.ViewModels.Cargo cargo = new Models.ViewModels.Cargo();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                cargo.Id = db.Cargo.Find(Id).Id;   
                cargo.Descripcion = db.Cargo.Find(Id).Descripcion;   
            }

            return View(cargo);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                db.Cargo.Remove(db.Cargo.Find(id));
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            Models.ViewModels.Cargo cargo = new Models.ViewModels.Cargo();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                cargo.Id = db.Cargo.Find(Id).Id;
                cargo.Descripcion = db.Cargo.Find(Id).Descripcion;
            }

            return View(cargo);
        }
    }
}