namespace TA_ArnoldClark.Tests
{
    using System;

    using TA_ArnoldClark.Calculator;

    using Xunit;

    public class CarLoanCalculatorTests
    {
        [Fact]
        public void TestValidScheduleForOneYear()
        {
            // Arrange
            CarLoanCalculator calculator = new CarLoanCalculator(10000, FinanceOption.OneYear, 1500, DateTime.Today);

            // Act
            var payments = calculator.CalculatePayments();

            // Assert
            Assert.Equal(12, payments.Count);
            Assert.Equal(new decimal(796.33), payments[0].Amount);
            Assert.Equal(new decimal(708.33), payments[1].Amount);
            //could assert the rest of the payments
            Assert.Equal(new decimal(728.33), payments[11].Amount);
        }

        [Fact]
        public void TestValidScheduleForTwoYears()
        {
            // Arrange
            CarLoanCalculator calculator = new CarLoanCalculator(10000, FinanceOption.TwoYears, 1500, DateTime.Today);

            // Act
            var payments = calculator.CalculatePayments();

            // Assert
            Assert.Equal(24, payments.Count);
            Assert.Equal(new decimal(442.17), payments[0].Amount);
            Assert.Equal(new decimal(354.17), payments[1].Amount);
            //could assert the rest of the payments
            Assert.Equal(new decimal(374.17), payments[23].Amount);
        }

        [Fact]
        public void TestValidScheduleForThreeYears()
        {
            // Arrange
            CarLoanCalculator calculator = new CarLoanCalculator(10000, FinanceOption.ThreeYears, 1500, DateTime.Today);

            // Act
            var payments = calculator.CalculatePayments();

            // Assert
            Assert.Equal(36, payments.Count);
            Assert.Equal(new decimal(324.11), payments[0].Amount);
            Assert.Equal(new decimal(236.11), payments[1].Amount);
            //could assert the rest of the payments
            Assert.Equal(new decimal(256.11), payments[35].Amount);
        }


        [Fact]
        public void TestWhenMinimumDepotIsNotMet()
        {
            // Arrange
            CarLoanCalculator calculator = new CarLoanCalculator(10000, FinanceOption.ThreeYears, 500, DateTime.Today);

            // Act
            Exception exception = Assert.Throws<Exception>(() => calculator.CalculatePayments());

            // Assert
            Assert.Equal("Min deposit not met", exception.Message);
        }
    }
}
