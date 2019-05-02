namespace TA_ArnoldClark.Pages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using TA_ArnoldClark.Calculator;

    [BindProperties(SupportsGet = true)]
    public class IndexModel : PageModel
    {
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal VehiclePrice { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal Deposit { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; } = DateTime.Today;

        public FinanceOption Term { get; set; }

        public IActionResult OnGetPartial()
        {
            CarLoanCalculator calculator = new CarLoanCalculator(this.VehiclePrice, this.Term, this.Deposit, this.DeliveryDate);
            List<Payment> results = new List<Payment>();

            // This is a little smelly, for a fully blown application, I would take the time to either create a custom ValidationException or have
            // some sort of generic results that included validation errors (to avoid exception driven development) and then create a filter that 
            // handled them 
            if (this.ModelState.IsValid)
            {
                try
                {
                    results = calculator.CalculatePayments();
                }
                catch (Exception e)
                {
                   this.ModelState.AddModelError(string.Empty, e.Message);
                }
                
            }

            return new PartialViewResult
                       {
                           ViewName = "_LoanSchedule",
                           ViewData = new ViewDataDictionary<List<PaymentViewModel>>(this.ViewData, results.Select(PaymentViewModel.Create).ToList())
                       };
        }
    }

    public class PaymentViewModel
    {
        public string Date { get; set; }

        public string Amount { get; set; }

        public string Comments { get; set; }

        public string OutstandingAmount { get; set; }

        public static PaymentViewModel Create(Payment payment)
        {
            return new PaymentViewModel()
                       {
                           Date = payment.Date.ToLongDateString(),
                           Amount = $"£{payment.Amount:N}",
                           OutstandingAmount = $"£{payment.OutstandingAmount:N}",
                           Comments = payment.Comments,
                       };
        }
    }
}
