using LowFlightFare.DAL;
using LowFlightFare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.BusinessLogic
{
    public class SettingsLogic : BusinessLogic
    {
        public SettingsLogic(LocaleDAL localeDAL, SettingsDAL settingsDAL)
        {
            _LocaleDAL = localeDAL;
            _SettingsDAL = settingsDAL;
        }

        public int UpdateSettings(Settings settings)
        {
            _SettingsDAL.UpdateSettings(settings);

            return _SettingsDAL.UpdateDatabase();
        }

        /// <summary>
        /// Set localization
        /// </summary>
        /// <param name="userID">/param>
        public void SetLocalizationForCurrentUser(int localeID)
        {
            HttpContext.Current.Request.RequestContext.RouteData.Values["lang"] = _LocaleDAL.GetByID<Locale>(localeID)._Localization;
        }

        public List<Settings> Settings { get { return _SettingsDAL.GetAll<Settings>(); } }
        public List<Locale> Locales { get { return _LocaleDAL.GetAll<Locale>(); } }

        private LocaleDAL _LocaleDAL { get; set; }
        private SettingsDAL _SettingsDAL { get; set; }
    }
}