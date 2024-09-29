
namespace Lab1
{
    public interface IBusinessLogic
    {
        public List<Airport> Airports { get;}

        public AirportInputError AddAirport(String id, String city, DateTime dateVisited, int rating);
        public AirportDeletionError DeleteAirport(String id);
        public AirportEditError EditAirport(String id, String city, DateTime dateVisited, int rating);
        public Airport FindAirport(String id);
        public List<Airport> GetAirports();
    }
}
