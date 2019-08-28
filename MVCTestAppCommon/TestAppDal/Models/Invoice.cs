using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTestAppCommon.TestAppDal.Models
{
    public class Invoice
    {
        //TODO: Validations
        //TODO: ViewModel would be nice (do not put everything in model)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Invoice ID")]
        public long InvoiceID { get; set; }

        public bool IsPaid { get; set; }

        [Display]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Biller")]
        [Required]
        [MinLength(5)]
        public string BillerFullName { get; set; }

        //TODO: Some message to load from config would be nice - hardcoded is ugly
        [NotMapped]
        [Display(Name = "Paid")]
        public string IsPaidFormatted { get { return IsPaid ? "paid" : "not paid"; }}
    }
}
