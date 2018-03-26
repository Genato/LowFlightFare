using LowFlightFare.DAL;
using LowFlightFare.Models;
using LowFlightFare.Models.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        ///Public methods///
        
        //Http
        /// <summary>
        /// Make Http GET request to Amadeus
        /// </summary>
        /// <param name="searchParameters"></param>
        public SearchResults HttRequestToAmadeusAPI(SearchParameters searchParameters)
        {
            PrepareSearchParametersForHttpRequest(searchParameters);
            ConstructUrl();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_AmadeusApiUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                Response flightResults = JsonConvert.DeserializeObject<Response>(json);
            }

            //TODO
            // Parse response to SearchResults

            return new SearchResults();
        }

        private void PrepareSearchParametersForHttpRequest(SearchParameters searchParameters)
        {
            _Currency = _CurrencyDAL.GetByID<Currency>(searchParameters.CurrencyID).CurrencyCode;
            _From_IATA_CODE = searchParameters.From_IATA_code;
            _To_IATA_CODE = searchParameters.To_IATA_code;
            _PassangerNumber = searchParameters.PassangerNumber > 0 ? searchParameters.PassangerNumber.ToString() : "0";
            _Depart = searchParameters.Depart.ToString("yyyy-MM-ddTHH:mm");
            _Return = searchParameters.Return.ToString("yyyy-MM-ddTHH:mm");
        }

        private void ConstructUrl()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append(_AmadeusApiUrl);
            builder.Append("apikey=" + _AmadeusApikey);
            builder.Append("&origin=" + _From_IATA_CODE);
            builder.Append("&destination=" + _To_IATA_CODE);
            builder.Append("&arrive_by=" + _Depart);
            builder.Append("&currency=" + _Currency);

            if (_Return.Contains("0001-01-01T00:00") == false)
                builder.Append("&return_by=" + _Return);

            if (_PassangerNumber != ("0"))
                builder.Append("&adults=" + _PassangerNumber);

            _AmadeusApiUrl = builder.ToString();
        }

        //Airport_IATA_Code methods
        public Airport_IATA_Code GetAirportByIATACode(string IATA_code) => _Airport_IATA_CodesDAL.GetByIATACode(IATA_code);
        public List<Airport_IATA_Code> GetAirportByTownName(string townName) => _Airport_IATA_CodesDAL.GetByTownName(townName);

        //SearchResults methods
        /// <summary>
        /// Get list of SearchResults by SearchParametersID
        /// </summary>
        /// <param name="searchParameterID"></param>
        /// <returns></returns>
        public List<SearchResults> GetSearchResultsBySearchParameterID(int searchParameterID) => _SearchResultsDAL.GetBySearchParametersID(searchParameterID);

        //SearchParameters methods
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

        ///Public members///

        /// <summary>
        /// List of currencies.
        /// </summary>
        public List<Currency> Currencies { get { return _CurrencyDAL.GetAll<Currency>(); } }

        //Private members
        private CurrencyDAL _CurrencyDAL { get; set; }
        private Airport_IATA_CodesDAL _Airport_IATA_CodesDAL { get; set; }
        private SearchParametersDAL _SearchParametersDAL { get; set; }
        private SearchResultsDAL _SearchResultsDAL { get; set; }

        //Private - Amadeus members
        //private string _AmadeusApiUrl { get { return ConfigurationManager.AppSettings["AMADEUS_API_URL"]; } set {; } }
        private string _AmadeusApiUrl { get; set; } = ConfigurationManager.AppSettings["AMADEUS_API_URL"];
        private string _AmadeusApikey { get { return ConfigurationManager.AppSettings["AMADEUS_API_KEY"]; } }
        public string _Currency { get; set; }
        public string _Depart { get; set; }
        public string _Return { get; set; }
        public string _From_IATA_CODE { get; set; }
        public string _To_IATA_CODE { get; set; }
        public string _PassangerNumber { get; set; }
    }
}