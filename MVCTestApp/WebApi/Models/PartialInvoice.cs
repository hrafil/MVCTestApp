using MVCTestAppCommon.TestAppDal.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCTestApp.WebApi.Models
{
    /// <summary>
    /// Třída pro využití v InvoiceApi - PATCH metoda
    /// </summary>
    //TODO: Soudržnost s modelem Invoice by šla vyřešit rozhraním IInvoice, které obě implementují (v případě změny se rozšíří rozhraní)
    public class PartialInvoice
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(5)]
        public string BillerFullName { get; set; }

        public bool IsAtLeastOnePropertySet()
        {
            return !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(BillerFullName);
        }

        public Invoice CopyToInvoice(Invoice invoice)
        {
            if (!string.IsNullOrEmpty(Email))
                invoice.Email = Email;
            if (!string.IsNullOrEmpty(BillerFullName))
                invoice.BillerFullName = BillerFullName;
            return invoice;
        }
    }
}