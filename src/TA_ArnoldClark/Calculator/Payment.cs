namespace TA_ArnoldClark.Calculator
{
    using System;

    public class Payment
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Comments { get; set; }

        public decimal OutstandingAmount { get; set; }
    }
}