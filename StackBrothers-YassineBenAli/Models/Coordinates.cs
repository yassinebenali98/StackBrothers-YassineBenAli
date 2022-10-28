using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace StackBrothers_YassineBenAli.Models

{

    public partial class Geo
    {
        [JsonProperty("resourceSets")]
        public ResourceSet[] ResourceSets { get; set; }

    }
    public partial class ResourceSet
    {
      

        [JsonProperty("resources")]
        public Resource[] Resources { get; set; }
    }

    public partial class Resource
    {
       
        [JsonProperty("point")]
        public Point Point { get; set; }

        
       

        [JsonProperty("results")]
        public Result[] Results { get; set; }

    }

    public partial class Point
    {
       [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }



        public partial class Geoo
        {
                     

            [JsonProperty("resourceSets")]
            public ResourceSet[] ResourceSets { get; set; }

            [JsonProperty("statusDescription")]
            public string StatusDescription { get; set; }

        }

      

      
        public partial class Result
        {
       


            [JsonProperty("travelDistance")]
            public double TravelDistance { get; set; }
   
        }
}

