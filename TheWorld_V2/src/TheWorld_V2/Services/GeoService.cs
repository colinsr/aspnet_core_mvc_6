using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace TheWorld_V2.Services
{
    public class GeoService
    {
        private readonly ILogger<GeoService> _logger;

        public GeoService(ILogger<GeoService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup(string location)
        {
            var result = new CoordServiceResult { Success = false, Message = "Undetermined failure during lookup" };

            var encodedName = WebUtility.UrlEncode(location);
            var bingKey = Startup.Configuration["AppSettings:BingKey"];
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={bingKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);

            // Read out the results
            // Fragile, might need to change if the Bing API changes
            var bingResult = JObject.Parse(json);
            var resources = bingResult["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{location}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{location}' as a location";
                }
                else
                {
                    var coordinates = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)  coordinates[0];
                    result.Longitude = (double) coordinates[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}