using LowFlightFare.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LowFlightFare.DAL
{
    public abstract class DAL
    {
        protected DAL(LowFlightFareDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        protected LowFlightFareDbContext _DbContext { get; set; }

        // Public methods - (not abstract and virtual) //

        /// <summary>
        /// Get all rows of T entity from database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>() where T : class => _DbContext.Set<T>().ToList();

        /// <summary>
        /// Method just call DbContext.SaveChanges()
        /// </summary>
        /// <returns></returns>
        public int UpdateDatabase() => _DbContext.SaveChanges();

        /// <summary>
        /// Method just call DbContext.SaveChangesAsync()
        /// </summary>
        /// <returns></returns>
        public Task<int> UpdateDatabaseAsync() => _DbContext.SaveChangesAsync();

        // Abstract methods//

        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetByID<T>(int id) where T : class;

        public abstract void CreateEntity<T>(T entity) where T : class;

        // Virtual methods (override if you wish) //
        //Some of the DB entities don't have some of the columns, 
        //that's why some methods are virtual so that classes that inherite from OntoDAL doesn't ahave to implement every method.

        /// <summary>
        /// Get entity by name <para/>
        /// NOTE: Not implemented ! (Override if you wish get entity by name)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual T GetByName<T>(string name) where T : class { return null; }

    }
}