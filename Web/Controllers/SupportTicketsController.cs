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
    public class SupportTicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupportTickets
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Index()
        {
            return View(db.SupportTickets.ToList());
        }

        // GET: SupportTickets/Details/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTickets supportTickets = db.SupportTickets.Find(id);
            int val = Int32.Parse(supportTickets.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Detalles = clientes.Nombre;
            if (supportTickets == null)
            {
                return HttpNotFound();
            }
            return View(supportTickets);
        }

        // GET: SupportTickets/Create
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Create()
        {
            ViewBag.MiListado = ObtenerListado();
            var clientes = db.Clientes.ToList();

            var viewModel = new SupportTickets { Cliente = clientes };
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Nombre = User.Identity.Name;
            }

            return View(viewModel);
        }

        // POST: SupportTickets/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Create([Bind(Include = "Id,IdCliente,Usuario,Titulo,Detalle,Estado")] SupportTickets supportTickets)
        {
            if (ModelState.IsValid)
            {
                db.SupportTickets.Add(supportTickets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supportTickets);
        }

        // GET: SupportTickets/Edit/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTickets supportTickets = db.SupportTickets.Find(id);
            int val = Int32.Parse(supportTickets.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Edit = clientes.Nombre;
            ViewBag.ListaClientes = GetClientes();
            ViewBag.MiListado = ObtenerListado();
            if (supportTickets == null)
            {
                return HttpNotFound();
            }
            return View(supportTickets);
        }

        // POST: SupportTickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Edit([Bind(Include = "Id,IdCliente,Usuario,Titulo,Detalle,Estado")] SupportTickets supportTickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportTickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supportTickets);
        }

        // GET: SupportTickets/Delete/5
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTickets supportTickets = db.SupportTickets.Find(id);
            int val = Int32.Parse(supportTickets.IdCliente);
            Cliente clientes = db.Clientes.Find(val);
            ViewBag.Delete = clientes.Nombre;
            if (supportTickets == null)
            {
                return HttpNotFound();
            }
            return View(supportTickets);
        }

        // POST: SupportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportTickets supportTickets = db.SupportTickets.Find(id);
            db.SupportTickets.Remove(supportTickets);
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
        public List<SelectListItem> ObtenerListado()
        {
            return new List<SelectListItem>(){
                new SelectListItem()
                {
                    Text = "Abierto"
                },
                new SelectListItem()
                {
                    Text = "En Proceso"
                },
                new SelectListItem()
                {
                    Text = "En Espera"
                },
                new SelectListItem()
                {
                    Text = "Finalizado"
                },

            };
        }
        public List<Cliente> GetClientes()
        {
            var res = db.Clientes.ToList<Cliente>();
            return res;
        }
    }
}
