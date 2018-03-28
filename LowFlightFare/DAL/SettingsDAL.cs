using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LowFlightFare.DbContexts;
using LowFlightFare.Models;

namespace LowFlightFare.DAL
{
    public class SettingsDAL : DAL
    {
        public SettingsDAL(LowFlightFareDbContext dbContext) : base(dbContext) { }

        public void UpdateSettings(Settings settings)
        {
            _DbContext.Settings.First().LocalizationID = settings.LocalizationID;
        }

        //Overriden methods

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