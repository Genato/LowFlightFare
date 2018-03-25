using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LowFlightFare.DbContexts;
using LowFlightFare.Models;

namespace LowFlightFare.DAL
{
    public class SearchParametersDAL : DAL
    {
        public SearchParametersDAL(LowFlightFareDbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Get SearchParameters by all parameters except ID
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public SearchParameters GetByParametersExceptID(SearchParameters searchParameters)
        {
            return (from parameters in _DbContext.SearchParameters
                    where parameters.CurrencyID == searchParameters.CurrencyID
                            && parameters.Depart == searchParameters.Depart
                            && parameters.Return == searchParameters.Return
                            && parameters.From_IATA_code == searchParameters.From_IATA_code
                            && parameters.To_IATA_code == searchParameters.To_IATA_code
                            && parameters.PassangerNumber == searchParameters.PassangerNumber
                    select parameters).FirstOrDefault();
        }

        /// <summary>
        /// Create new SearchParameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override void CreateEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get SearchParameters by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T GetByID<T>(int id)
        {
            SearchParameters currencies = _DbContext.SearchParameters.Find(id);
            
            return (T)Convert.ChangeType(currencies, typeof(SearchParameters));
        }
    }
}