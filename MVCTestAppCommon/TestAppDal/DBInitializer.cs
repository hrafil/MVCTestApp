using MVCTestAppCommon.TestAppDal.Models;
using System.Collections.Generic;

namespace MVCTestAppCommon.TestAppDal
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseAlways<MVCTestAppContext>
    {
        protected override void Seed(MVCTestAppContext context)
        {
            var invoices = new List<Invoice>
            {
                new Invoice{ IsPaid=false, Email="vyvojar1@gmail.com", BillerFullName="Rudolf Jelínek" },
                new Invoice{ IsPaid=false, Email="vyvojar2@gmail.com", BillerFullName="Bernard Pivovarník" },
                new Invoice{ IsPaid=true, Email="manager@gmail.com", BillerFullName="Zdeněk Hruška" },
            };
            invoices.ForEach(s => context.Invoices.Add(s));
            context.SaveChanges();
        }
    }
}
