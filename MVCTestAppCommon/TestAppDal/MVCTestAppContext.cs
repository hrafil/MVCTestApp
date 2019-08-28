using MVCTestAppCommon.TestAppDal.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVCTestAppCommon.TestAppDal
{
    public class MVCTestAppContext : DbContext
    {
        public MVCTestAppContext() : base("MVCTestAppContext") { }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
