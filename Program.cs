using BusBoard;
class App
{
    static void Main()
    {
        var busController = new BusStopClient();
        var postCodeController = new PostCodeClient();
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/next-busses/stop-id/{id}", async (string id) => await busController.getNextBusses(id)
        );
        app.MapGet("/next-busses/stop-id/{id}/maximum-count/{count}", async (string id, int count) => await busController.getNextBusses(id, count)
        );


        app.Run();
    }
}

