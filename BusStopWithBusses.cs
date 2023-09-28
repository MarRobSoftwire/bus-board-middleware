namespace BusBoard
{
    public class BusStopWithBusses
    {
        public string naptanId;
        public List<Bus> nextBusses;

        public BusStopWithBusses(string id, List<Bus> busses)
        {
            naptanId = id;
            nextBusses = busses;
        }

    }
}