namespace BusBoard
{
    public class BusStopWithBusses
    {
        public string naptanId;
        public List<Bus> nextBusses;

        public BusStopWithBusses(string id, List<Bus> busses)
        {
            this.naptanId = id;
            this.nextBusses = busses;
        }

    }
}