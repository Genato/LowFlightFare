using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LowFlightFare.DbContexts;
using LowFlightFare.Models;

namespace LowFlightFare.DAL
{
    public class LocaleDAL : DAL
    {
        public LocaleDAL(LowFlightFareDbContext dbContext) : base(dbContext) { }

        public override void CreateEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public override T GetByID<T>(int id)
        {
            Locale userSettings = _DbContext.Locale.Find(id);

            return (T)Convert.ChangeType(userSettings, typeof(Locale));
        }
    }
}