using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCarsClean.Models;
using eCarsClean.ViewModels;
using System.Data.Entity;
using System.IO;
using System.Net;

namespace eCarsClean.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: and Display the CarsList
        public ActionResult Index() 
        {
            var viewModel = new CarsViewModel
            {
                CarList = _context.Cars.Include(c => c.Manufacturer)
            };

            return View(viewModel);
        }
        //-------------------------------------------------------------------------------------------------
        public ActionResult _CarRowPartial(int? Id)
        {
            var singleCarForThisRow = _context.Cars.Find(Id);
            return PartialView(singleCarForThisRow);
        }
        //-------------------------------------------------------------------------------------------------
        public ActionResult _CarMobilePartial(int? Id)
        {
            var singleCarForThisRow = _context.Cars.Find(Id);
            return PartialView(singleCarForThisRow);
        }
        //-------------------------------------------------------------------------------------------------
        //used by Index view to update likes when likes button is clicked
        [HttpPost]
        public ActionResult Like(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car carObj = _context.Cars.Find(Id );
            if (carObj == null)
            {
                return HttpNotFound();
            }

            int currentLikes = (int)carObj.Likes;
            carObj.Likes = currentLikes + 1;

            if (ModelState.IsValid)
            {
                _context.Entry(carObj).State = EntityState.Modified;
                _context.SaveChanges();
            }

            carObj = _context.Cars.Find(Id);
            return PartialView("_CarRowPartial", carObj);
        }
        //-------------------------------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult DisLike(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car carObj = _context.Cars.Find(Id);

            if (carObj == null)
            {
                return HttpNotFound();
            }


            int currentLikes = (int)carObj.Likes;

            if (currentLikes == 0)
            {
                carObj = _context.Cars.Find(Id);
                return PartialView("_CarRowPartial", carObj);
            }
            carObj.Likes = currentLikes - 1;

            if (ModelState.IsValid)
            {
                _context.Entry(carObj).State = EntityState.Modified;
                _context.SaveChanges();
            }

            carObj = _context.Cars.Find(Id);
            return PartialView("_CarRowPartial", carObj);
        }
        //-----------------Mobile View Actions---------------------------------------
        //used by Index view to update likes when likes button is clicked
     
        public ActionResult CarDetails(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car carObj = _context.Cars.Find(Id);
            if (carObj == null)
            {
                return HttpNotFound();
            }
            return View(carObj);
        }
        public ActionResult CreateCarEntry()
        {
            ViewBag.ManufacturerId = new SelectList(_context.Manufacturers, "Id", "manufacturerName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCarEntry([Bind(Include = "Id,ManufacturerId,CarModel,Price,MilesPerCharge,Image,Video,Likes,ImageFile")] Car carObj)
        {
            if (carObj.Image == null)
            {
                carObj.Image = "~/Content/images/default1.png";
            }

            if (ModelState.IsValid)
            {
                //Upload the file wthout the extension

                string imagename = Path.GetFileNameWithoutExtension(carObj.ImageFile.FileName);
                string extension = Path.GetExtension(carObj.ImageFile.FileName);
                imagename = imagename + extension;
                carObj.Image = imagename;

                imagename = Path.Combine(Server.MapPath("~/Content/images/"), imagename);
                carObj.ImageFile.SaveAs(imagename);
                //Add newCar to database


                _context.Cars.Add(carObj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManufacturerId = new SelectList(_context.Manufacturers, "Id", "manufacturerName", carObj.ManufacturerId);
            return View(carObj);
        }

        // GET: carDetails/Edit/5
        [Authorize]
        public ActionResult EditCarEntry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car carObj = _context.Cars.Find(id);
            if (carObj == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerId = new SelectList(_context.Manufacturers, "Id", "manufacturerName", carObj.ManufacturerId);
            return View(carObj);
        }

        // POST: carDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarEntry([Bind(Include = "Id,ManufacturerId,CarModel,Price,MilesPerCharge,Image,Video,Likes")] Car carObj)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(carObj).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerId = new SelectList(_context.Manufacturers, "Id", "manufacturerName", carObj.ManufacturerId);
            return View(carObj);
        }

        // GET: carDetails/Delete/5
        [Authorize]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car carObj = _context.Cars.Find(Id);
            if (carObj == null)
            {
                return HttpNotFound();
            }
            return View(carObj);
        }

        // POST: carDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            Car carObj = _context.Cars.Find(Id);
            _context.Cars.Remove(carObj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //-----------------------------------------------------------------------

        public ActionResult MilesPerCharge()
        {
            var carObj = _context.Cars.Include(c => c.Manufacturer);
            return View(carObj);
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