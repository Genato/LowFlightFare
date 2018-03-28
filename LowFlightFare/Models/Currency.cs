using LowFlightFare.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models
{
    public class Currency
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        [Display(Name = nameof(Labels.Currency), ResourceType = typeof(Labels))]
        public string CurrencyCode { get; set; }
    }
}