using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ContactosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contactos
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Index()
        {
            return View(db.Contactos.ToList());
        }

        // GET: Contactos/Details/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            int val = Int32.Parse(contactos.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Detalles = clientes.Nombre;
            if (contactos == null)
            {
                return HttpNotFound();
            }
            return View(contactos);
        }

        // GET: Contactos/Create
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Create()
        {
            var clientes = db.Clientes.ToList();

            var viewModel = new Contactos { Cliente = clientes };

            return View(viewModel);
        }

        // POST: Contactos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Create(Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                db.Contactos.Add(contactos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactos);
        }

        // GET: Contactos/Edit/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            int val= Int32.Parse(contactos.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Nombre = clientes.Nombre;
            if (contactos == null)
            {
                return HttpNotFound();
            }
            ViewBag.viewModel = GetClientes();
            return View(contactos);
        }

        // POST: Contactos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Edit([Bind(Include = "Id,IdCliente,Nombre,Apellido,PhoneNumber,Email,Puesto")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactos);
        }

        // GET: Contactos/Delete/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            int val = Int32.Parse(contactos.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Eliminar = clientes.Nombre;
            if (contactos == null)
            {
                return HttpNotFound();
            }
            return View(contactos);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactos contactos = db.Contactos.Find(id);
            db.Contactos.Remove(contactos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public List<Cliente> GetClientes()
        {
            var res = db.Clientes.ToList<Cliente>();
            return res;
        }
    }
}
