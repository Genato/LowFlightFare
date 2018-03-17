using LowFlightFare.BusinessLogic;
using LowFlightFare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LowFlightFare.Controllers
{
    public class SearchFlightsController : Controller
    {
        public SearchFlightsController(SearchFlightsLogic searchFlightsLogic)
        {
            _SearchFlightsLogic = searchFlightsLogic;
        }

        [HttpGet]
        public ActionResult SearchFlights()
        {
            SearchFlightsViewModel searchFlightsViewModel = new SearchFlightsViewModel();
            searchFlightsViewModel.Currency = _SearchFlightsLogic.Currencies;

            return View(searchFlightsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchFlights(SearchFlightsViewModel searchFlightsViewModel)
        {

            return View(searchFlightsViewModel);
        }

        [HttpGet]
        public JsonResult SearchAirportByIATAcode(string IATAcode)
        {


            return Json("HELLO", JsonRequestBehavior.AllowGet);
        }

        //Private members
        private SearchFlightsLogic _SearchFlightsLogic { get; set; }
    }
}