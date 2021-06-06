using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AsyncTests.Models
{
    public class Country
    {
        public string Name { get; set; }
        [JsonPropertyName("alpha2Code")]
        public string Code { get; set; }
        public string Capital { get; set; }
    }
}
