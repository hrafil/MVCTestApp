using MVCTestAppCommon.TestAppDal;
using MVCTestAppCommon.TestAppDal.Models;
using System.Data;
using System.Net;
using System.Web.Mvc;

namespace MVCTestApp.Controllers.Mvc
{
    public class InvoiceController : Controller
    {
        // TODO: Some IoC would be nice
        private TestAppUnitOfWork TestAppUnitOfWork { get; } = new TestAppUnitOfWork();

        // TODO: Some async would be nice
        public ActionResult Index()
        {
            return View(TestAppUnitOfWork.InvoiceRepository.Get());
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Create new invoice";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email, BillerFullName")]Invoice invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TestAppUnitOfWork.InvoiceRepository.Insert(invoice);
                    TestAppUnitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }

        public ActionResult Edit(int? invoiceID)
        {
            if (invoiceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invoice = TestAppUnitOfWork.InvoiceRepository.GetByID(invoiceID);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TestAppUnitOfWork.InvoiceRepository.Update(invoice);
                    TestAppUnitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(invoice);
        }

        public ActionResult Delete(int? invoiceID)
        {
            if (invoiceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invoice = TestAppUnitOfWork.InvoiceRepository.GetByID(invoiceID);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Invoice invoice)
        {
            TestAppUnitOfWork.InvoiceRepository.Delete(invoice);
            TestAppUnitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}