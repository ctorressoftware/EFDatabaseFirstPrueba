using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsandoEF.Models;
using UsandoEF.Models.ViewModels;

namespace UsandoEF.Controllers
{
    public class PersonaController : Controller
    {
        public ActionResult Index()
        {
            PruebaEFEntities db = new PruebaEFEntities();

            List<Models.ViewModels.Persona> personas = new List<Models.ViewModels.Persona>();

            foreach (var item in db.Persona)
            {
                Models.ViewModels.Persona persona = new Models.ViewModels.Persona
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Sexo = item.Sexo,
                    IdCargo = item.IdCargo
                };

                personas.Add(persona);
            }

            /*personas = (from persona in db.Persona
                        select new Models.ViewModels.Persona
                        {
                            Id = persona.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Sexo = persona.Sexo,
                            IdCargo = persona.IdCargo
                        }
                        ).ToList();*/

            return View(personas);
        }

        public ActionResult Create()
        {
            PruebaEFEntities db = new PruebaEFEntities();

            List<Models.ViewModels.Cargo> cargos = new List<Models.ViewModels.Cargo>();

            cargos = (from cargo in db.Cargo
                      select new Models.ViewModels.Cargo
                      {
                          Id = cargo.Id,
                          Descripcion = cargo.Descripcion

                      }).ToList();

            List<SelectListItem> items = cargos.ConvertAll (cargo =>
            {
                return new SelectListItem()
                {
                    Text = cargo.Descripcion.ToString(),
                    Value = cargo.Id.ToString()
                };
            });

            ViewBag.cargos = items;

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                db.Persona.Add(new Models.Persona
                {
                     Nombre = collection["Nombre"].ToString(),
                     Apellido = collection["Apellido"].ToString(),
                     Sexo = collection["Sexo"].ToString(),
                     IdCargo = int.Parse(collection["Cargo"].ToString())
                });

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //TODO
        public ActionResult Edit(int Id)
        {
            Models.ViewModels.Cargo cargo = new Models.ViewModels.Cargo();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                cargo.Id = db.Cargo.Find(Id).Id;
                cargo.Descripcion = db.Cargo.Find(Id).Descripcion;
            }

            return View(cargo);
        }

        [HttpPost] //TODO
        public ActionResult Edit(int Id, FormCollection collection)
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

            return RedirectToAction("GetAll");
        }

        public ActionResult Delete(int Id)
        {
            Models.ViewModels.Persona persona = new Models.ViewModels.Persona();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                persona.Id = db.Persona.Find(Id).Id;
                persona.Nombre = db.Persona.Find(Id).Nombre;
                persona.Apellido = db.Persona.Find(Id).Apellido;
                persona.Sexo = db.Persona.Find(Id).Sexo;
                persona.IdCargo = db.Persona.Find(Id).IdCargo;
            }

            return View(persona);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                db.Persona.Remove(db.Persona.Find(id));
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            Models.ViewModels.Persona persona = new Models.ViewModels.Persona();

            using (PruebaEFEntities db = new PruebaEFEntities())
            {
                persona.Id = db.Persona.Find(Id).Id;
                persona.Nombre = db.Persona.Find(Id).Nombre;
                persona.Apellido = db.Persona.Find(Id).Apellido;
                persona.Sexo = db.Persona.Find(Id).Sexo;
                persona.IdCargo = db.Persona.Find(Id).IdCargo;
            }

            return View(persona);
        }
    }
}