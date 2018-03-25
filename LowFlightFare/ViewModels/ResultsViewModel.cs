using LowFlightFare.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.ViewModels
{
    public class ResultsViewModel
    {
        //public List<SearchResults> SearchResults { get; set; }
        public IPagedList<SearchResults> SearchResults { get; set; }
    }
}