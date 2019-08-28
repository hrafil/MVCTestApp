using MVCTestAppCommon.TestAppDal.Models;

namespace MVCTestAppCommon.TestAppDal.Repository
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public InvoiceRepository(MVCTestAppContext context) : base(context) { }
    }
}
