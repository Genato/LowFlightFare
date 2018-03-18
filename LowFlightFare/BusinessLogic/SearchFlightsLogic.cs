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
        public SearchFlightsLogic(CurrencyDAL currenciesDAL, Airport_IATA_CodesDAL airport_IATA_CodesDAL)
        {
            _CurrencyDAL = currenciesDAL;
            _Airport_IATA_CodesDAL = airport_IATA_CodesDAL;
        }

        //Public methods

        public Airport_IATA_Code GetAirportByIATACode(string IATA_code) => _Airport_IATA_CodesDAL.GetByIATACode(IATA_code);

        public List<Airport_IATA_Code> GetAirportByTownName(string townName) => _Airport_IATA_CodesDAL.GetByTownName(townName);

        //Public members

        /// <summary>
        /// Get all currenicies
        /// </summary>
        public List<Currency> Currencies { get { return _CurrencyDAL.GetAll<Currency>(); } }

        //Private members
        private CurrencyDAL _CurrencyDAL { get; set; }
        private Airport_IATA_CodesDAL _Airport_IATA_CodesDAL { get; set; }
    }
}