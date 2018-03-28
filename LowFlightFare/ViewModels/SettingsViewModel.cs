using LowFlightFare.Localization;
using LowFlightFare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LowFlightFare.ViewModels
{
    public class SettingsViewModel
    {
        [Display(Name = nameof(Labels.SelectLanguage), ResourceType = typeof(Labels))]
        public List<Locale> _Localization { get; set; }

        public int SelectedLocale { get; set; }

    }
}