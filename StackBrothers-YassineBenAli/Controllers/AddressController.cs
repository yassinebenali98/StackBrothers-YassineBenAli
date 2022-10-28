using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackBrothers_YassineBenAli.Data;
using StackBrothers_YassineBenAli.Models;
using System;
using System.IO;
using System.Net;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace StackBrothers_YassineBenAli.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();


        private DataContext _addressContext;
        public AddressController(DataContext addressContext)
        {
            _addressContext = addressContext;
        }

        [HttpGet("addresses")]
        public ActionResult<List<Address>> Getstrings([FromQuery] string? filter, [FromQuery] string? orderBy, [FromQuery] string? order)
        {
            var adrs = _addressContext.Addresses.ToList();
            if (!String.IsNullOrEmpty(filter))
            {
                foreach (var ad in adrs.ToList())
                {
                    if (!ad.ToString().Contains(filter))
                    {
                        Console.WriteLine(ad);
                        adrs.Remove(ad);
                    }

                }
            }


            var sortBy = new Dictionary<string, Func<IEnumerable<Address>, IEnumerable<Address>>>()
                    {
                 { "Street", ls => ls.OrderBy(l => l.Street) },
                 { "HouseNumber", ls => ls.OrderBy(l => l.HouseNumber) },
                 { "City", ls => ls.OrderBy(l => l.City) },
                 { "ZipCode", ls => ls.OrderBy(l => l.ZipCode) },
                 { "Country", ls => ls.OrderBy(l => l.Country) },
                };
            if (!String.IsNullOrEmpty(orderBy) && order == "a")
            {
                return sortBy[orderBy](adrs).ToList();
            };
            if (!String.IsNullOrEmpty(orderBy) && order == "d")
            {
                return sortBy[orderBy](adrs).Reverse().ToList();
            }; 
            return adrs.ToList();

        }




        [HttpGet("{id}")]
        public ActionResult<Address> GetstringById(int id)
        {
            return _addressContext.Addresses.FirstOrDefault(address => address.Id == id);
        }

        [HttpPost("")]
        public Address Poststring(Address address)
        {
            _addressContext.Addresses.Add(address);
            _addressContext.SaveChanges();
            return address;
        }

        [HttpPut("{id}")]
        public ActionResult<Address> Putstring(int id, Address address)
        {
            var oldAddress = _addressContext.Addresses.FirstOrDefault(address => address.Id == id);
            oldAddress.Street = address.Street;
            oldAddress.HouseNumber = address.HouseNumber;
            oldAddress.ZipCode = address.ZipCode;
            oldAddress.City = address.City;
            oldAddress.Country = address.Country;
            _addressContext.SaveChanges();
            return oldAddress;
        }

        [HttpDelete("{id}")]
        public ActionResult<int> DeletestringById(int id)
        {
            _addressContext.Addresses.Remove(_addressContext.Addresses.FirstOrDefault(Address => Address.Id == id));
            _addressContext.SaveChanges();
            return id;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("coords")]
        public async Task<Geo> getCords(int id)
        {
            var adr = _addressContext.Addresses.FirstOrDefault(address => address.Id == id);

            var coords = new Geo();
            try
            {
                var url = "https://dev.virtualearth.net/REST/v1/Locations?" +
                "CountryRegion=" + adr.Country +
                "&adminDistrict=" +
                "&locality=" + adr.City +
                "&postalCode=" + adr.ZipCode +
                "&addressLine=" + adr.HouseNumber + "%" + adr.Street + "%" + adr.City + "%" + adr.ZipCode +
                "&key=AlomCUY2ezekP4zldzPJ10J0t0l-mUGkgWXiskdjkwTqFgxUfqzh7PlzLYAzeqh5";

                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var jsonString = response.Content.ReadAsStringAsync().Result;
                coords = JsonConvert.DeserializeObject<Geo>(jsonString);
                return coords;
                Console.WriteLine(coords.ResourceSets[0].Resources[0].Point.Coordinates[0]);
                Console.WriteLine(coords.ResourceSets[0].Resources[0].Point.Coordinates[1]);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }


        [HttpGet("distance")]
        public async Task<double> getDistance([FromQuery] int id1, [FromQuery] int id2)
        {
            Geo coords1 = await getCords(id1);
            Geo coords2 = await getCords(id2);

            var x1 = coords1.ResourceSets[0].Resources[0].Point.Coordinates[0].ToString().Replace(',', '.');
            var y1 = coords1.ResourceSets[0].Resources[0].Point.Coordinates[1].ToString().Replace(',', '.');

            var x2 = coords2.ResourceSets[0].Resources[0].Point.Coordinates[0].ToString().Replace(',', '.');
            var y2 = coords2.ResourceSets[0].Resources[0].Point.Coordinates[1].ToString().Replace(',', '.');
            var distanceJson = new Geoo();
            try
            {
                var url = "https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix" +
                    "?origins=" + x1 +
                    "," +
                    y1 +
                    "5&destinations=" +
                    x2+
                    "," +
                    y2 +
                    "&travelMode=driving&key=AlomCUY2ezekP4zldzPJ10J0t0l-mUGkgWXiskdjkwTqFgxUfqzh7PlzLYAzeqh5";

                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var jsonString = response.Content.ReadAsStringAsync().Result;
                distanceJson = JsonConvert.DeserializeObject<Geoo>(jsonString);

                Console.WriteLine(distanceJson.ResourceSets[0].Resources[0].Results[0].TravelDistance);
                return distanceJson.ResourceSets[0].Resources[0].Results[0].TravelDistance;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return 0;
            }
        }
    }
}

