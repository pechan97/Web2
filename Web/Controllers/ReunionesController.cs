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
    public class ReunionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reuniones
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Index()
        {
            return View(db.Reuniones.ToList());
        }

        // GET: Reuniones/Details/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reuniones reuniones = db.Reuniones.Find(id);
            string val1 = reuniones.IdUsuario;
            ApplicationUser users = db.Users.Find(val1);
            ViewBag.Detalle1 = users.Name;
            int val = Int32.Parse(reuniones.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Detalle2 = clientes.Nombre;
            if (reuniones == null)
            {
                return HttpNotFound();
            }
            return View(reuniones);
        }

        // GET: Reuniones/Create
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Create()
        {
            var clientes = db.Clientes.ToList();
            var users = db.Users.ToList();
            var viewModel = new Reuniones { Cliente = clientes, User = users };
            return View(viewModel);
        }

        // POST: Reuniones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Create([Bind(Include = "Id,IdCliente,IdUsuario,Nombre,Fecha,Virtual")] Reuniones reuniones)
        {
            if (ModelState.IsValid)
            {
                db.Reuniones.Add(reuniones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reuniones);
        }

        // GET: Reuniones/Edit/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reuniones reuniones = db.Reuniones.Find(id);
            string val1 = reuniones.IdUsuario;
            ApplicationUser users = db.Users.Find(val1);
            ViewBag.Name = users.Name;
            int val = Int32.Parse(reuniones.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Nombre = clientes.Nombre;
            ViewBag.ListaClientes = GetClientes();
            ViewBag.ListaUsuarios = GetListUser();
            if (reuniones == null)
            {
                return HttpNotFound();
            }
            return View(reuniones);
        }

        // POST: Reuniones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Edit([Bind(Include = "Id,IdCliente,IdUsuario,Nombre,Fecha,Virtual")] Reuniones reuniones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reuniones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reuniones);
        }

        // GET: Reuniones/Delete/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reuniones reuniones = db.Reuniones.Find(id);
            string val1 = reuniones.IdUsuario;
            ApplicationUser users = db.Users.Find(val1);
            ViewBag.Delete1 = users.Name;
            int val = Int32.Parse(reuniones.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Delete2 = clientes.Nombre;
            if (reuniones == null)
            {
                return HttpNotFound();
            }
            return View(reuniones);
        }

        // POST: Reuniones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reuniones reuniones = db.Reuniones.Find(id);
            db.Reuniones.Remove(reuniones);
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
        public List<ApplicationUser> GetListUser()
        {
            var res = db.Users.ToList<ApplicationUser>();
            return res;
        }
    }
}
