using System;

namespace TestMessageHub.Models
{
    /// <summary>
    /// Date range model
    /// </summary>
    public class DateRange
    {
        /// <summary>
        /// DateTime from (start of range)
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// DateTime to (end of range)
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Check if dateTime in range
        /// </summary>
        /// <param name="dateTime">DateTime for check</param>
        /// <returns>Bool</returns>
        public bool IsDateInRange(DateTime dateTime)
        {
            return (dateTime >= From && dateTime <= To);
        }
    }
}
