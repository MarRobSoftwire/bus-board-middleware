using BusBoard;
using Newtonsoft.Json;

class App
{
    static void Main()
    {
        var busController = new BusStopClient();
        var postCodeController = new PostCodeClient();
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet(
            "/next-busses/stop-id/{id}",
            async (string id) =>
                await busController.getNextBusses(id)
        );
        app.MapGet(
            "/next-busses/stop-id/{id}/maximum-count/{count}",
            async (
                string id,
                int count
            ) => 
                await busController.getNextBusses(id, count)
        );
        app.MapGet("/next-busses/post-code/{postCode}", async (string postCode) => {
            Location location = postCodeController.getLocation(postCode);
            List<BusStop> busStops = await busController.getNearestBusStops(location);

            var allNextBusses = 
                new List<Task<List<Bus>>>(
                    busStops.Select(
                        (stop) => busController.getNextBusses(stop.NaptanId)
                    )
                );
            var completedTasks = await Task.WhenAll(allNextBusses.ToArray());
            
            return JsonConvert
                .SerializeObject(
                    completedTasks.Select(
                        (bussesTask, index) => 
                            new BusStopWithBusses(busStops[index].NaptanId, bussesTask)
                    )
                );
            }
        );

        app.Run();
    }
}

