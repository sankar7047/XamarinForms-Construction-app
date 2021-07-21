using System;
using System.IO;
using Newtonsoft.Json;

namespace MartinPulgarConstructions.Models
{
    public class Photo
    {
        public string ImageFormat { get; set; }
        public string ImageData { get; set; }

        [JsonIgnore]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public Stream ImageStream { get; set; }
    }
}
