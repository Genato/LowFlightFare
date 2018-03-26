using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class Response
    {
        public string currency { get; set; }
        public List<Result> results { get; set; }
    }
}