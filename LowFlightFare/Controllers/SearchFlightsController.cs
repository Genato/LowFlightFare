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
    public class SearchFlightsController : Controller
    {
        public SearchFlightsController(SearchFlightsLogic searchFlightsLogic)
        {
            _SearchFlightsLogic = searchFlightsLogic;
        }

        [HttpGet]
        public ActionResult SearchFlights()
        {
            SearchFlightsViewModel searchFlightsViewModel = new SearchFlightsViewModel()
            {
                Currency = _SearchFlightsLogic.Currencies
            };

            return View(searchFlightsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchFlights(SearchFlightsViewModel searchFlightsViewModel)
        {

            return View(searchFlightsViewModel);
        }

        [HttpGet]
        public JsonResult SearchAirportByTownName(string townName)
        {
            List<Airport_IATA_Code> airport_IATA_Code = _SearchFlightsLogic.GetAirportByTownName(townName);

            //If there is no airport with this IATA code send message in model
            if (airport_IATA_Code.Count == 0)
            {
                airport_IATA_Code.Add(new Airport_IATA_Code()
                                        {
                                            IATA_code = "There is no Airport in this town",
                                            AirportName = "",
                                            TownName = ""
                                        });
            }

            return Json(airport_IATA_Code, JsonRequestBehavior.AllowGet);
        }

        //Private members
        private SearchFlightsLogic _SearchFlightsLogic { get; set; }
    }
}