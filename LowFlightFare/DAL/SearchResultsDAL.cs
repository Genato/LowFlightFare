using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LowFlightFare.DbContexts;
using LowFlightFare.Models;

namespace LowFlightFare.DAL
{
    public class SearchResultsDAL : DAL
    {
        public SearchResultsDAL(LowFlightFareDbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Get SearchResult by SearchParameterID
        /// </summary>
        /// <param name="searchParametersID"></param>
        /// <returns></returns>
        public List<SearchResults> GetBySearchParametersID(int searchParametersID)
        {
            return (from result in _DbContext.SearchResults
                    where result.SearchParametersID == searchParametersID
                    select result).ToList();
        }

        public void CreateEntities(List<SearchResults> listOfResults)
        {
            _DbContext.SearchResults.AddRange(listOfResults);
        }

        public override void CreateEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public override T GetByID<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}