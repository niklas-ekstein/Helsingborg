using System;
using System.ComponentModel.DataAnnotations;

//Oklart om [Required] behövs här..

namespace Helsingborg.Models
{
    public class DataCustomer
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Plats av inköp")]
        public string PlaceOfPurchase { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Belopp ink moms")]
        public decimal AmountIncludingVAT { get; set; }
        [Required]
        [Display(Name = "Moms")]
        public decimal VAT { get; set; }
        [Required]
        [Display(Name = "Anledning")]
        public string Reason { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Medlemmar")]
        public string Members { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Kommentar")]
        public string Comment { get; set; } = string.Empty;
    }
}

