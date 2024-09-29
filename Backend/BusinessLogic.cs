


using System.Security.Cryptography;

/**
 * Description: Connects the inputs in the user interface to the operations on the data base
 * Name: Dominick Hagedorn
 * Date:9/17/2024
 * Bugs: None Known.
 * Reflection: This class was also pretty easy, most of the work happened on the other classes
 */
namespace Lab1
{
    public class BusinessLogic : IBusinessLogic
    {
        private IDatabase dataBase = new Database();
        public List<Airport> Airports { get => dataBase.SelectAllAirports();}

        /**
         * create a new airport inside the data base with given qualities
         */
        public AirportInputError AddAirport(string id, string city, DateTime dateVisited, int rating = 1)
        {
            // Check if ID length is not 3 or 4 characters
            if (id.Length < 3 || id.Length > 4)
                return AirportInputError.InvalidIDLength;

            // Check if the airport already exists
            if (dataBase.SelectAirport(id) != null)
                return AirportInputError.DuplicateAirportId;

            // Check if the city name is too long
            if (city.Length > 25)
                return AirportInputError.InvalidCityLength;

            // Validate the date
            if (dateVisited == default)
                return AirportInputError.InvalidDate;

            // Validate the rating
            if (rating < 1 || rating > 5 || rating == null)
                return AirportInputError.RatingOutOfBounds;

            // If all validations pass, insert the new airport
            dataBase.InsertAirport(new Airport(id, city, dateVisited, rating));
            return AirportInputError.NoError;
        }
        
        /**
         * deletes a given airport based on an id
         */
        public AirportDeletionError DeleteAirport(string id)
        {
            if(FindAirport(id) == null)
            {
                return AirportDeletionError.AirportNotFound;
            }
            return dataBase.DeleteAirport(id);
        }

        /**
         * updates an airport with new given properties
         */
        public AirportEditError EditAirport(string id, string city, DateTime dateVisited, int rating)
        {
            if(dataBase.SelectAirport(id) == null) // if airport is not present
            {
                return AirportEditError.IdNotPresent;
            }
            AirportInputError errorCode = AddAirport(id, city, dateVisited, rating);
            if(errorCode == AirportInputError.NoError || errorCode == AirportInputError.DuplicateAirportId)
            {
                return dataBase.UpdateAirport(new Airport(id, city, dateVisited, rating)); // update existing airport
            }
            switch (errorCode)
            {
                case  AirportInputError.DBAdditionError:
                    return AirportEditError.DBAdditionError;
                case AirportInputError.InvalidDate:
                    return AirportEditError.InvalidDate;
                case AirportInputError.InvalidCityLength:
                    return AirportEditError.InvalidCityLength;
                case AirportInputError.InvalidIDLength:
                    return AirportEditError.InvalidIDLength;
                default:
                    return AirportEditError.Error;
            }  
        }
        
        /**
         * returns the airport with the given id
         */
        public Airport FindAirport(string id)
        {
            return dataBase.SelectAirport(id);
        }

        /**
         * returns the entire list of airports
         */
        public List<Airport> GetAirports()
        {
            return Airports;
        }
    }
}
