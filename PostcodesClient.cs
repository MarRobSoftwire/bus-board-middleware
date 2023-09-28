using RestSharp;
using Newtonsoft.Json;

namespace BusBoard
{
    public class PostCodeClient
    {
        private readonly RestClient client;
        
        public PostCodeClient()
        {
            client = new RestClient(
                new RestClientOptions("https://api.postcodes.io/postcodes")
            );
        }

        public Location getLocation(string PostCode) {
            var request = new RestRequest(PostCode);
            var response = client.Get(request);
            return JsonConvert.DeserializeObject<LocationReturn>(response.Content).Result;
        }

        private class LocationReturn {
            public Location Result { get; set; }
        }
    }
}