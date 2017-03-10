using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace MvcNinjaApp.Controllers
{
    public class NinjasController : Controller
    {
        //private NinjaContext db = new NinjaContext();
        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        // GET: Ninjas
        public ActionResult Index()
        {
            //var ninjas = db.Ninjas.Include(n => n.Clan);
            var ninjas = _repo.GetNinjaWithClan();
            return View(ninjas.ToList());
        }

        // GET: Ninjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Ninja ninja = db.Ninjas.Find(id);
            var ninja = _repo.GetNinjaWithEquipmentAndClan(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        // GET: Ninjas/Create
        public ActionResult Create()
        {
            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName");
            return View();
        }

        // POST: Ninjas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveNewNinja(ninja);
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // GET: Ninjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = _repo.GetNinjaWithEquipment(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // POST: Ninjas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveUpdatedNinja(ninja);
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // GET: Ninjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ninja ninja = _repo.GetNinjaById(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        // POST: Ninjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteNinja(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
