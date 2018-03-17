using LowFlightFare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.ViewModels
{
    public class SearchFlightsViewModel
    {
        public SearchParameters SearchParameters { get; set; }
        public SearchResults SearchResults { get; set; }
        public List<Currency> Currency { get; set; }
    }
}