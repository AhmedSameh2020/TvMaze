using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Model
{
    public class Show
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cast")]
        public List<Person> Cast { get; set; }
    }
}
