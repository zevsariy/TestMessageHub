using System;

namespace TestMessageHub.Models
{
    /// <summary>
    /// Companies names
    /// </summary>
    public static class Companies
    {
        /// <summary>
        /// Adidas company name
        /// </summary>
        public const string Adidas = "ADIDAS";

        /// <summary>
        /// Nike company name
        /// </summary>
        public const string Nike = "NIKE";

        /// <summary>
        /// Puma company name
        /// </summary>
        public const string Puma = "PUMA";

        public static void Validate(string companyName)
        {
            switch (companyName.ToUpper())
            {
                case Adidas:
                case Nike:
                case Puma:
                    return;
                default:
                    throw new Exception(string.Format("Unknown company name: {0}", companyName));
            };
        }
    }
}
