using LowFlightFare.BusinessLogic;
using LowFlightFare.Models;
using LowFlightFare.ViewModels;
using PagedList;
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
            ResultsViewModel resultsViewModel = new ResultsViewModel();

            //If "SearchParameters" exists get "SearchResults" by "SearchParameters" and show it on view-"Results" 
            if (_SearchFlightsLogic.SearchParametersExists(searchFlightsViewModel.SearchParameters))
            {
                resultsViewModel.SearchResults = _SearchFlightsLogic.GetSearchResultsBySearchParameterID(_SearchFlightsLogic.GetSearchParametersByParameters(searchFlightsViewModel.SearchParameters).ID)
                                                                    .OrderBy(x => x.Depart)
                                                                    .ToPagedList(1, 5);

            }

            //Http request to Amadeus
            List<SearchResults> listOfResults = _SearchFlightsLogic.HttRequestToAmadeusAPI(searchFlightsViewModel.SearchParameters);

            if(listOfResults.Count > 0)
            {
                _SearchFlightsLogic.SaveSearchParameters(searchFlightsViewModel.SearchParameters);
                _SearchFlightsLogic.LinkSearchParametersToSearchResults(listOfResults, _SearchFlightsLogic.GetSearchParametersByParameters(searchFlightsViewModel.SearchParameters).ID);
                _SearchFlightsLogic.SaveListOfSearchResults(listOfResults);
                resultsViewModel.SearchResults = listOfResults.OrderBy(x => x.ID).ToPagedList(1, 3);
            }

            return View("Results", resultsViewModel);
        }

        [HttpGet]
        public ActionResult Results(int searchParametersID, int pageNumber = 1, int pageSize = 3)
        {
            ResultsViewModel searchFlightsViewModel = new ResultsViewModel()
            {
                SearchResults = _SearchFlightsLogic.GetSearchResultsBySearchParameterID(searchParametersID).OrderBy(x => x.ID).ToPagedList(pageNumber, pageSize)
            };

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