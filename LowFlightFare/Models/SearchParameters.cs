using LowFlightFare.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models
{
    public class SearchParameters
    {
        public int ID { get; set; }

        [Display(Name = nameof(Labels.Origin), ResourceType = typeof(Labels))]
        public string From_IATA_code { get; set; }

        [Display(Name = nameof(Labels.Destination), ResourceType = typeof(Labels))]
        public string To_IATA_code { get; set; }

        [Display(Name = nameof(Labels.DepartureDate), ResourceType = typeof(Labels))]
        public DateTime Depart { get; set; }

        [Display(Name = nameof(Labels.ReturnDate), ResourceType = typeof(Labels))]
        public DateTime Return { get; set; }

        [Display(Name = nameof(Labels.PassangerNumber), ResourceType = typeof(Labels))]
        public int PassangerNumber { get; set; }

        [Display(Name = nameof(Labels.Currency), ResourceType = typeof(Labels))]
        public int CurrencyID { get; set; }
    }
}