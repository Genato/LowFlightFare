using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.BusinessLogic
{
    public abstract class BusinessLogic
    { 
        // Virtual methods //

        public virtual int CreateEntity<T>(T entity) where T : class { throw new NotImplementedException(); }

        public virtual int SaveEntity<T>(T entity) where T : class { throw new NotImplementedException(); }
    }
}