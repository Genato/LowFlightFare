using LowFlightFare.DAL;
using LowFlightFare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}