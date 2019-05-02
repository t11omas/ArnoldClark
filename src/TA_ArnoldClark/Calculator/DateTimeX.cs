namespace TA_ArnoldClark.Calculator
{
    using System;
    using System.Collections.Generic;

    public static class DateTimeX
    {
        public static List<DateTime> ListFirstMonthOfEachMonth(this DateTime deliveryDate, int term)
        {
            List<DateTime> result = new List<DateTime>();
            DateTime startDate = new DateTime(deliveryDate.Year, deliveryDate.Month + 1, 1);
            for (int mth = 0; mth < term; mth++)
            {
                DateTime dt = startDate.AddMonths(mth);
                while (dt.DayOfWeek != DayOfWeek.Monday)
                {
                    dt = dt.AddDays(1);
                }

                result.Add(dt);
            }

            return result;
        }
    }
}