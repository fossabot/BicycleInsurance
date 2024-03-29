// Code generated by Microsoft (R) AutoRest Code Generator 0.12.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Raci.B2C.Bicycle.ClientApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    /// <summary>
    /// </summary>
    public partial class BicyclePolicyDetailDTO
    {
        /// <summary>
        /// Initializes a new instance of the BicyclePolicyDetailDTO class.
        /// </summary>
        public BicyclePolicyDetailDTO() { }

        /// <summary>
        /// Initializes a new instance of the BicyclePolicyDetailDTO class.
        /// </summary>
        public BicyclePolicyDetailDTO(string make = default(string), string model = default(string), string year = default(string), string type = default(string))
        {
            Make = make;
            Model = model;
            Year = year;
            Type = type;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "make")]
        public string Make { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
