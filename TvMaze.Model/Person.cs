using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Model
{
    public class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
    }
}
