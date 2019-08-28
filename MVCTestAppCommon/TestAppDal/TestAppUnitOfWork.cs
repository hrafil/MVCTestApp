using MVCTestAppCommon.TestAppDal.Repository;
using System;

namespace MVCTestAppCommon.TestAppDal
{
    public class TestAppUnitOfWork : IDisposable
    {
        private readonly MVCTestAppContext context = new MVCTestAppContext();

        private InvoiceRepository invoiceRepository;

        public InvoiceRepository InvoiceRepository
        {
            get
            {
                return invoiceRepository ?? (invoiceRepository = new InvoiceRepository(context));
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
