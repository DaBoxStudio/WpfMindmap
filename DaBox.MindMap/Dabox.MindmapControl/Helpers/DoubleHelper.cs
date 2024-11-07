using System.Globalization;

namespace Dabox.MindmapControl.Helpers
{
    public static class DoubleHelper
    {
        #region [ Fields ]

        /// <summary>
        /// The decimal format information
        /// </summary>
        private static NumberFormatInfo SQLDecimalFormatInfo = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
        };

        private static CultureInfo SQLCultureInfo = new CultureInfo("en-US")
        {
            NumberFormat = SQLDecimalFormatInfo
        };

        /// <summary>
        /// The decimal format information
        /// </summary>
        private static string SQLDateTimeFormatInfo = "yyyy-MM-dd HH:mm:ss"; // ODBC canonical  format

        #endregion

        /// <summary>
        /// Converts to parsingstring.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToParsingString(this double value)
        {
            return string.Format(SQLCultureInfo, "{0:0.########################}", value);
        }
    }
}
