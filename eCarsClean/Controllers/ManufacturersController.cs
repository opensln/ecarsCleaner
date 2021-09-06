using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCarsClean.Models;
using eCarsClean.ViewModels;
using System.Data.Entity;
using System.Net;

namespace eCarsClean.Controllers
{
    public class ManufacturersController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: and Display the Manufacturers
        public ActionResult Index()
        {
            var ManufacturerList = _context.Manufacturers;

            return View(ManufacturerList);
        }

        public ActionResult ManufacturerDetails(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturerObj = _context.Manufacturers.Find(Id);
            if (manufacturerObj == null)
            {
                return HttpNotFound();
            }
            return View(manufacturerObj);
        }



        // GET: manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManufacturerId,ManufacturerName,AddressLine1,City,Postcode,Tel,Email,WebSite")] Manufacturer manufacturerObj)
        {
            if (ModelState.IsValid)
            {
                _context.Manufacturers.Add(manufacturerObj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturerObj);
        }

        // GET: manufacturers/Edit/5
        [Authorize]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturerObj = _context.Manufacturers.Find(Id);
            if (manufacturerObj == null)
            {
                return HttpNotFound();
            }
            return View(manufacturerObj);
        }

        // POST: manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ManufacturerName,AddressLine1,City,Postcode,Tel,Email,WebSite")] Manufacturer manufacturerObj)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(manufacturerObj).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturerObj);
        }

        // GET: manufacturers/Delete/5
        [Authorize]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturers = _context.Manufacturers.Find(Id);
            if (manufacturers == null)
            {
                return HttpNotFound();
            }
            return View(manufacturers);
        }

        // POST: manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            Manufacturer manufacturerObj = _context.Manufacturers.Find(Id);
            _context.Manufacturers.Remove(manufacturerObj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }

  
}