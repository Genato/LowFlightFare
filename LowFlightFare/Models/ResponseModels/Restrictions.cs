using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class Restrictions
    {
        public bool refundable { get; set; }
        public bool change_penalties { get; set; }
    }
}