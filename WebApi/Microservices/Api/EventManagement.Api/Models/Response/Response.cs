using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EventManagement.Api.RestModels.Response
{
    public class Response<T>
    {
        //public DateTime RequestAt { get; set; }
        //public DateTime ResponseAt { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<T> Data { get; set; }
        //public string Message { get; set; }

    }
}
