using RestSharp;
using Newtonsoft.Json;

namespace BusBoard
{
    public class BusStopClient
    {
        public BusStopClient()
        {
            var options = new RestClientOptions("https://api.tfl.gov.uk/StopPoint") {
            };
            client = new RestClient(options);
        }
        
        private readonly RestClient client;

        public async Task<List<Bus>> getNextBusses(string StopId, int maxCount = 5)
        {
            var request = new RestRequest(StopId + "/Arrivals");
            var response = await client.GetAsync(request);
            var busses = JsonConvert
                .DeserializeObject<List<Bus>>(response.Content);
            return busses
                .OrderBy((bus) => bus.TimeToStation)
                .Take(maxCount)
                .ToList();
        }

        public async Task<List<BusStop>> getNearestBusStops(Location location, int maxCount = 2) 
        {
            var request = new RestRequest($"?lat={location.Latitude}&lon={location.Longitude}&stopTypes=NaptanPublicBusCoachTram");
            var response = await client.GetAsync(request);
            var stopPoints = JsonConvert
                .DeserializeObject<BusStopReturn>(response.Content)
                .StopPoints;
            return stopPoints
                .Take(maxCount)
                .ToList();
        }

        internal Task<List<BusStop>> getNearestBusStops(HttpContext stopId)
        {
            throw new NotImplementedException();
        }

        private class BusStopReturn
        {
            public List<BusStop> StopPoints { get; set; }
        }
    }
}