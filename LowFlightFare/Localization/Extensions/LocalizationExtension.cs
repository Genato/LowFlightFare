using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LowFlightFare.Localization.Extensions
{
    /// <summary>
    /// Set localization.
    /// </summary>
    public class LocalizationExtension
    {
        /// <summary>
        /// Apply locale to current thread for current user
        /// </summary>
        /// <param name="lang">locale name</param>
        public static void SetLang()
        {
            // Get locale from route values
            string lang = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["lang"];

            // If we haven't found appropriate culture - seet default locale then
            if (lang.Contains("lang"))
                lang = ConfigurationManager.AppSettings["DEFAULT_LANGUAGE"];

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
        }

        public static string GetWebErrorMsg(string message)
        {
            if (message.Contains("The origin should be a three letter IATA location code"))
                return ErrorMsg.OriginIATACode;
            else if (message.Contains("The destination should be a three letter IATA location code"))
                return ErrorMsg.DestinationIATACode;
            else if (message.Contains("No itinerary found for requested segment"))
                return ErrorMsg.NoItinerary;
            else if (message.Contains("Departure date should be after today's date"))
                return ErrorMsg.DepartureDate;
            else if (message.Contains("Return date should be after departure date"))
                return ErrorMsg.ReturnDate;
            else if (message.Contains("When specified, the number of adults should be an integer between 0 and 9"))
                return ErrorMsg.Passangers;

            return "";
        }
    }
}