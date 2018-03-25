using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LowFlightFare.DbContexts;
using LowFlightFare.Models;

namespace LowFlightFare.DAL
{
    public class Airport_IATA_CodesDAL : DAL
    {
        public Airport_IATA_CodesDAL(LowFlightFareDbContext dbContext) : base(dbContext) { }

        public Airport_IATA_Code GetByIATACode(string IATA_code)
        {
            Airport_IATA_Code airport_IATA_Code = (from airport in _DbContext.AirportIATACodes
                                                   where airport.IATA_code == IATA_code
                                                   select airport).FirstOrDefault();

            return airport_IATA_Code;
        }

        public List<Airport_IATA_Code> GetByTownName(string townName)
        {
            List<Airport_IATA_Code> listOfAirports = (from airport in _DbContext.AirportIATACodes
                                                      where airport.TownName.Contains(townName) && airport.IATA_code != null
                                                      select airport).ToList();

            return listOfAirports;
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