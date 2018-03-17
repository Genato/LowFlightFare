using LowFlightFare.DbContexts;
using LowFlightFare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.DAL
{
    public class CurrencyDAL : DAL
    {
        public CurrencyDAL(LowFlightFareDbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Add UserSettings to DbSet. (Call DbSet.SaveChanges() to insert it into database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public override void CreateEntity<T>(T entity)
        {
            _DbContext.Currencies.Add((Currency)Convert.ChangeType(entity, typeof(Currency)));
        }

        /// <summary>
        /// Get UserSettings by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T GetByID<T>(int id)
        {
            Currency currencies = _DbContext.Currencies.Find(id);

            return (T)Convert.ChangeType(currencies, typeof(Currency));
        }
    }
}