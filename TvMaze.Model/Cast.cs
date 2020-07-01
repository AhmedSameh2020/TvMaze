using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Model
{
    public class CastJson
    {
        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
