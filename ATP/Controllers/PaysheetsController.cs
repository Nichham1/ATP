using ATP.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ATP.Controllers
{
    public class PaysheetsController : Controller
    {
        private APTDatabaseEntities db = new APTDatabaseEntities();

        // GET: Paysheets
        public ActionResult Index()
        {
            var paysheets = db.Paysheets.Include(p => p.Customer).Include(p => p.Destination).Include(p => p.Driver).Include(p => p.Rate);
            return View(paysheets.ToList());
        }

        // GET: Paysheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paysheet paysheet = db.Paysheets.Find(id);
            if (paysheet == null)
            {
                return HttpNotFound();
            }
            return View(paysheet);
        }

        // GET: Paysheets/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "Id", "FirstName");
            ViewBag.DestinationID = new SelectList(db.Destinations, "Id", "Name");
            ViewBag.DriversID = new SelectList(db.Drivers, "Id", "FirstName");
            ViewBag.RateID = new SelectList(db.Rates, "Id", "Name");
            return View();
        }

        // POST: Paysheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerID,Chit_num,TruckLic_num,Job_num,DestinationID,Date_of_delivery,RateID,DriversID")] Paysheet paysheet)
        {
            if (ModelState.IsValid)
            {
                db.Paysheets.Add(paysheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "Id", "FirstName", paysheet.CustomerID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "Id", "Name", paysheet.DestinationID);
            ViewBag.DriversID = new SelectList(db.Drivers, "Id", "FirstName", paysheet.DriversID);
            ViewBag.RateID = new SelectList(db.Rates, "Id", "Name", paysheet.RateID);
            return View(paysheet);
        }

        // GET: Paysheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paysheet paysheet = db.Paysheets.Find(id);
            if (paysheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "Id", "FirstName", paysheet.CustomerID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "Id", "Name", paysheet.DestinationID);
            ViewBag.DriversID = new SelectList(db.Drivers, "Id", "FirstName", paysheet.DriversID);
            ViewBag.RateID = new SelectList(db.Rates, "Id", "Name", paysheet.RateID);
            return View(paysheet);
        }

        // POST: Paysheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerID,Chit_num,TruckLic_num,Job_num,DestinationID,Date_of_delivery,RateID,DriversID")] Paysheet paysheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paysheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "Id", "FirstName", paysheet.CustomerID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "Id", "Name", paysheet.DestinationID);
            ViewBag.DriversID = new SelectList(db.Drivers, "Id", "FirstName", paysheet.DriversID);
            ViewBag.RateID = new SelectList(db.Rates, "Id", "Name", paysheet.RateID);
            return View(paysheet);
        }

        // GET: Paysheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paysheet paysheet = db.Paysheets.Find(id);
            if (paysheet == null)
            {
                return HttpNotFound();
            }
            return View(paysheet);
        }

        // POST: Paysheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paysheet paysheet = db.Paysheets.Find(id);
            db.Paysheets.Remove(paysheet);
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
    }
}
