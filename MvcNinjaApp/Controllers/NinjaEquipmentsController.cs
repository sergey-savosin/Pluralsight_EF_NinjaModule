﻿using System;
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
    public class NinjaEquipmentsController : Controller
    {
        //private NinjaContext db = new NinjaContext();
        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        // GET: NinjaEquipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NinjaEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,Type,DateCreated,DateModified")] NinjaEquipment ninjaEquipment)
        {
            int ninjaId;
            if (!int.TryParse(Request.Form["NinjaId"], out ninjaId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _repo.SaveNewEquipment(ninjaEquipment, ninjaId);
            return RedirectToAction("Edit", "Ninjas", new { id = ninjaId });
        }

        // GET: NinjaEquipments/Edit/5
        public ActionResult Edit(int? id, int ninjaId, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.NinjaId = ninjaId;
            ViewBag.NinjaName = name;
            var ninjaEquipment = _repo.GetEquipmentById(id.Value);
            if (ninjaEquipment == null)
            {
                return HttpNotFound();
            }
            return View(ninjaEquipment);
        }

        // POST: NinjaEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,Type,DateCreated,DateModified")] NinjaEquipment ninjaEquipment)
        {
            int ninjaId;
            if (!int.TryParse(Request.Form["NinjaId"], out ninjaId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _repo.SaveUpdatedEquipment(ninjaEquipment, ninjaId);
            return RedirectToAction("Edit", "Ninjas", new { id = ninjaId });
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
