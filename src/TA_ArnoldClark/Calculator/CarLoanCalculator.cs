namespace TA_ArnoldClark.Calculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CarLoanCalculator
    {
        private decimal vehicalPrice;

        private FinanceOption financeOption;

        private decimal deposit;

        private DateTime deliveryDate;

        public CarLoanCalculator(decimal vehicalPrice, FinanceOption financeOption, decimal deposit, DateTime deliveryDate)
        {
            this.vehicalPrice = vehicalPrice;
            this.financeOption = financeOption;
            this.deposit = deposit;
            this.deliveryDate = deliveryDate;
        }

        public List<Payment> CalculatePayments()
        {
            List<Payment> payments = new List<Payment>();
            this.EnsureHasMinimumDeposit();
            var paymentsDates = this.deliveryDate.ListFirstMonthOfEachMonth((int)this.financeOption);
            decimal monthlyPayment = this.CalculateMontlyPayment();

            foreach (var date in paymentsDates)
            {
                Payment payment = new Payment();
                payment.Amount = monthlyPayment;
                payment.Date = date;
                payments.Add(payment);
            }
            
            this.AddArrangementFee(payments);
            this.AddCompletionFee(payments);
            this.AddOutstandingAmount(payments);
            return payments;
        }

        private void EnsureHasMinimumDeposit()
        {
            decimal depositPercent = (this.deposit / this.vehicalPrice) * 100;

            if (depositPercent < LoanRules.MinimumDepositPercent)
            {
                throw new Exception("Min deposit not met");
            }
        }

        private decimal CalculateMontlyPayment()
        {
            decimal outstanding = this.vehicalPrice - this.deposit;

            return decimal.Round(outstanding / (int)this.financeOption, 2, MidpointRounding.AwayFromZero);
        }

        private void AddArrangementFee(List<Payment> payments)
        {
            Payment firstPayment = payments.First();
            firstPayment.Amount = firstPayment.Amount + LoanRules.ArrangementFee;
            firstPayment.Comments = $"Includes Arrangement Fee payment of £{LoanRules.ArrangementFee}";
        }

        private void AddCompletionFee(List<Payment> payments)
        {
            Payment lastPayment = payments.Last();
            lastPayment.Amount = lastPayment.Amount + LoanRules.CompletionFee;
            lastPayment.Comments = $"Includes Completion fee payment of £{LoanRules.CompletionFee}";
        }

        private void AddOutstandingAmount(List<Payment> payments)
        {
            decimal outstanding = (this.vehicalPrice + LoanRules.CompletionFee + LoanRules.ArrangementFee) - this.deposit;

            foreach (Payment payment in payments)
            {
                outstanding = outstanding - payment.Amount;
                payment.OutstandingAmount = outstanding;
            }
        }
    }
}