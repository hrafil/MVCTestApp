using MVCTestApp.WebApi.Filters;
using MVCTestApp.WebApi.Models;
using MVCTestAppCommon.TestAppDal;
using MVCTestAppCommon.TestAppDal.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCTestApp.Controllers.Api
{
    public class InvoiceController : ApiController
    {
        private TestAppUnitOfWork TestAppUnitOfWork { get; } = new TestAppUnitOfWork();

        // GET: api/invoice/getunpayed/
        [ActionName("getunpayed")]
        [CustomAuthorization]
        public IEnumerable<Invoice> GetUnpayed()
        {
            return TestAppUnitOfWork.InvoiceRepository.Get(o => !o.IsPaid);
        }

        // POST: api/invoice/payinvoice/5
        [ActionName("payinvoice")]
        [HttpPost]
        [CustomAuthorization]
        public HttpResponseMessage PayInvoice(int? id)
        {
            if (id == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            var invoice = TestAppUnitOfWork.InvoiceRepository.GetByID(id);
            if (invoice == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            if (invoice.IsPaid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = "Invoice was already payed" });

            invoice.IsPaid = true;
            TestAppUnitOfWork.Save();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // PATCH: api/invoice/5
        [HttpPatch]
        [Route("api/invoice/{id:int}")]
        [CustomAuthorization]
        public HttpResponseMessage PartialUpdate(int? id, [FromBody] PartialInvoice partialInvoice)
        {
            if (id == null || partialInvoice == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            var invoice = TestAppUnitOfWork.InvoiceRepository.GetByID(id);
            if (invoice == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (partialInvoice.IsAtLeastOnePropertySet())
            {
                partialInvoice.CopyToInvoice(invoice);
                TestAppUnitOfWork.InvoiceRepository.Update(invoice);
                TestAppUnitOfWork.Save();
                return Request.CreateResponse(HttpStatusCode.OK, invoice);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
