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
        public SearchFlightsLogic(CurrencyDAL currenciesDAL)
        {
            _CurrencyDAL = currenciesDAL;
        }

        //Public methods


        //Public members
        public List<Currency> Currencies { get { return _CurrencyDAL.GetAll<Currency>(); } }

        //Private members
        private CurrencyDAL _CurrencyDAL { get; set; }
    }
}