using LowFlightFare.DAL;
using LowFlightFare.Models;
using LowFlightFare.Models.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace LowFlightFare.BusinessLogic
{
    public class SearchFlightsLogic : BusinessLogic
    {
        public SearchFlightsLogic(CurrencyDAL currenciesDAL, Airport_IATA_CodesDAL airport_IATA_CodesDAL, SearchParametersDAL searchParametersDAL, SearchResultsDAL searchResultsDAL)
        {
            _CurrencyDAL = currenciesDAL;
            _Airport_IATA_CodesDAL = airport_IATA_CodesDAL;
            _SearchParametersDAL = searchParametersDAL;
            _SearchResultsDAL = searchResultsDAL;
        }

        #region Amadeus Http request - methods

        /// <summary>
        /// Make Http GET request to Amadeus
        /// </summary>
        /// <param name="searchParameters"></param>
        public List<SearchResults> HttRequestToAmadeusAPI(SearchParameters searchParameters)
        {
            PrepareSearchParametersForHttpRequest(searchParameters);
            ConstructUrl();

            Response flightResults = new Response();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_AmadeusApiUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    flightResults = JsonConvert.DeserializeObject<Response>(json);

                    return PrepareSearchResultsForView(flightResults);
                }
            }
            catch (WebException webExc)
            {
                using (var stream = webExc.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {

                    Debug.WriteLine(reader.ReadToEnd());
                }
            }

            // If WebException occure empty list of Search results 
            return new List<SearchResults>(0);
        }

        private void PrepareSearchParametersForHttpRequest(SearchParameters searchParameters)
        {
            _Currency = _CurrencyDAL.GetByID<Currency>(searchParameters.CurrencyID).CurrencyCode;
            _From_IATA_CODE = searchParameters.From_IATA_code;
            _To_IATA_CODE = searchParameters.To_IATA_code;
            _PassangerNumber = searchParameters.PassangerNumber > 0 ? searchParameters.PassangerNumber.ToString() : "0";
            _Depart = searchParameters.Depart.ToString("yyyy-MM-dd");
            _Return = searchParameters.Return.ToString("yyyy-MM-dd");
        }

        private void ConstructUrl()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append(_AmadeusApiUrl);
            builder.Append("apikey=" + _AmadeusApikey);
            builder.Append("&origin=" + _From_IATA_CODE);
            builder.Append("&destination=" + _To_IATA_CODE);
            builder.Append("&departure_date=" + _Depart);
            builder.Append("&currency=" + _Currency);

            if (_Return.Contains("0001-01-01T00:00") == false)
                builder.Append("&return_date=" + _Return);

            if (_PassangerNumber != ("0"))
                builder.Append("&adults=" + _PassangerNumber);

            _AmadeusApiUrl = builder.ToString();
        }

        private List<SearchResults> PrepareSearchResultsForView(Response response)
        {
            List<SearchResults> listOfResults = new List<SearchResults>(response.results.Count);

            foreach (var result in response.results)
            {
                SearchResults searchResults = new SearchResults();
                searchResults.Currency = response.currency;
                searchResults.Price = result.fare.total_price;
                searchResults.PassangerNumber = (int)(Convert.ToDouble(result.fare.total_price) / Convert.ToDouble(result.fare.price_per_adult.total_fare));

                foreach (var itinerary in result.itineraries)
                {
                    //Odlazni let
                    searchResults.Depart = itinerary.outbound.flights.First().departs_at;
                    searchResults.From_Outbound = itinerary.outbound.flights.First().origin.airport;
                    searchResults.To_Outbound = itinerary.outbound.flights.Last().destination.airport;
                    searchResults.OutboundInterchanges = itinerary.outbound.flights.Count;

                    //Povratni let
                    searchResults.Return = itinerary.inbound.flights.First().departs_at;
                    searchResults.From_Inbound = itinerary.inbound.flights.First().origin.airport;
                    searchResults.To_Inbound = itinerary.inbound.flights.Last().destination.airport;
                    searchResults.ReturnInterchanges = itinerary.inbound.flights.Count;
                }

                listOfResults.Add(searchResults);
            }

            return listOfResults;
        }

        #endregion

        #region Airport_IATA_Code - methods

        /// <summary>
        /// Get Airport by IATA code.
        /// </summary>
        /// <param name="IATA_code"></param>
        /// <returns></returns>
        public Airport_IATA_Code GetAirportByIATACode(string IATA_code) => _Airport_IATA_CodesDAL.GetByIATACode(IATA_code);

        /// <summary>
        /// Get Airport by townname.
        /// </summary>
        /// <param name="townName"></param>
        /// <returns></returns>
        public List<Airport_IATA_Code> GetAirportByTownName(string townName) => _Airport_IATA_CodesDAL.GetByTownName(townName);

        #endregion

        #region SearchResults - methods

        /// <summary>
        /// Get list of SearchResults by SearchParametersID
        /// </summary>
        /// <param name="searchParameterID"></param>
        /// <returns></returns>
        public List<SearchResults> GetSearchResultsBySearchParameterID(int searchParameterID) => _SearchResultsDAL.GetBySearchParametersID(searchParameterID);

        /// <summary>
        /// Create range of SearchResult entities
        /// </summary>
        /// <param name="listOdResults"></param>
        /// <returns></returns>
        public int SaveListOfSearchResults(List<SearchResults> listOdResults)
        {
            _SearchResultsDAL.CreateEntities(listOdResults);

            return _SearchResultsDAL.UpdateDatabase();
        }

        public void LinkSearchParametersToSearchResults(List<SearchResults> searchResults, int searchParametersID)
        {
            foreach (var item in searchResults)
            {
                item.SearchParametersID = searchParametersID;
            }
        }

        #endregion

        #region SearchParameters - methods

        /// <summary>
        /// Check if SearchParameters exists by SearchParametersID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool SearchParametersExists(int ID) => _SearchParametersDAL.GetByID<SearchParameters>(ID) == null ? false : true;

        /// <summary>
        /// Check if SearchParameters exists by all parameters except ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool SearchParametersExists(SearchParameters searchParameters) => _SearchParametersDAL.GetByParametersExceptID(searchParameters) == null ? false : true;

        /// <summary>
        /// Get SearchParameters by all parameters except ID
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public SearchParameters GetSearchParametersByParameters(SearchParameters searchParameters) => _SearchParametersDAL.GetByParametersExceptID(searchParameters);

        /// <summary>
        /// Create SearchParamters entity
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public int SaveSearchParameters(SearchParameters searchParameters)
        {
            _SearchParametersDAL.CreateEntity<SearchParameters>(searchParameters);

            return _SearchParametersDAL.UpdateDatabase();
        }

        #endregion

        #region Public members

        /// <summary>
        /// List of currencies.
        /// </summary>
        public List<Currency> Currencies { get { return _CurrencyDAL.GetAll<Currency>(); } }

        #endregion

        #region Private members

        //DALs
        private CurrencyDAL _CurrencyDAL { get; set; }
        private Airport_IATA_CodesDAL _Airport_IATA_CodesDAL { get; set; }
        private SearchParametersDAL _SearchParametersDAL { get; set; }
        private SearchResultsDAL _SearchResultsDAL { get; set; }

        //Amadeus
        private string _AmadeusApiUrl { get; set; } = ConfigurationManager.AppSettings["AMADEUS_API_URL"];
        private string _AmadeusApikey { get { return ConfigurationManager.AppSettings["AMADEUS_API_KEY"]; } }
        public string _Currency { get; set; }
        public string _Depart { get; set; }
        public string _Return { get; set; }
        public string _From_IATA_CODE { get; set; }
        public string _To_IATA_CODE { get; set; }
        public string _PassangerNumber { get; set; }

        #endregion
    }
}