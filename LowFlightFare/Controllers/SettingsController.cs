using LowFlightFare.BusinessLogic;
using LowFlightFare.Models;
using LowFlightFare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LowFlightFare.Controllers
{
    public class SettingsController : LowFlightFareBaseController
    {
        public SettingsController(SettingsLogic settingsLogic)
        {
            _SettingsLogic = settingsLogic;
        }

        [HttpGet]
        public ActionResult Settings()
        {
            SettingsViewModel settingsViewModel = new SettingsViewModel()
            {
                _Localization = _SettingsLogic.Locales,
                SelectedLocale = _SettingsLogic.Settings.First().LocalizationID
            };

            return View(settingsViewModel);
        }

        [HttpPost]
        public ActionResult Settings(SettingsViewModel settingsViewModel)
        {
            Settings settings = new Settings()
            {
                LocalizationID = settingsViewModel.SelectedLocale
            };

            _SettingsLogic.UpdateSettings(settings);
            _SettingsLogic.SetLocalizationForCurrentUser(settingsViewModel.SelectedLocale);

            return RedirectToAction("Settings");
        }

        private SettingsLogic _SettingsLogic { get; set; }
    }
}