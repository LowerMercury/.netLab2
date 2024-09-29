using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Description: Retrieves and alters data in the database (the txt file)
 * Name: Dominick Hagedorn
 * Date:9/17/2024
 * Bugs: None Known.
 * Reflection: This was a pretty easy class to implement
 */
namespace Lab1
{
    public class Database : IDatabase
    {
        private List<Airport> airports = new List<Airport>();
        private String airportsFile = "C:\\Users\\Dominick\\Desktop\\College\\Semester 3\\CS341\\Labs\\Lab2\\airports.txt"; // fix

        /**
         * loads all airports from the txt file on startup
         */
        public Database()
        {
            LoadAirports();
        }

        /**
         * deletes an airport with a given id
         */
        public AirportDeletionError DeleteAirport(string id)
        {
            if (airports.Remove(SelectAirport(id))) // if successfully removed
            {
                StoreAirportsToFile(); // store changes
                return AirportDeletionError.NoError;
            }
            return AirportDeletionError.FailedToDeleteError; // dont change file
        }

        /**
         * adds an airport to the collection
         */
        public AirportAddError InsertAirport(Airport airport)
        {
            if (airport == null) // if a valid airport was passed
            {
                return AirportAddError.NoAirportPased;
            }
            airports.Add(airport); // add airport to collection

            StoreAirportsToFile(); // save changes
            return AirportAddError.NoError;
        }

        /**
         * finds and returns an airport based on its id
         */
        public Airport SelectAirport(string id)
        {
            foreach (Airport airport in airports) //  iterate through stored airports
            {
                if (airport.Id.Equals(id)) // match found
                {
                    return airport; // return desired airport
                }
            }

            return null; // airport not found
        }

        public List<Airport> SelectAllAirports()
        {
            return airports;
        }

        /**
         * update the properties inside of a given airport
         * 
         */
        public AirportEditError UpdateAirport(Airport airport)
        {
            if (airport == null || SelectAirport(airport.Id) == null) // if airport is not present in data base
            {
                return AirportEditError.IdNotPresent; // dont update collection
            }
            Airport previousAirport = SelectAirport(airport.Id); // get airport
            previousAirport.Copy(airport); // copy over new data
            StoreAirportsToFile(); // store changes
            return AirportEditError.NoError;
        }

        /**
         * loads airports from a txt file
         */
        public bool LoadAirports()
        {
            String[] lines = File.ReadAllLines(airportsFile); // read in stored data
            foreach(String line in lines)
            {
                String[] airportProperties = line.Split(" "); // split up airport properties
                if(airportProperties.Length == 4) // makes sure all properties are present
                {
                    airports.Add(new Airport(airportProperties[0], airportProperties[1], DateTime.Parse(airportProperties[2]), Int32.Parse(airportProperties[3]))); // add airport to collection
                }
                else
                {
                    return false; // airport entry malformed
                }
            }
            return true; // succesfully loaded
        }

        /**
         * stores the airports into a text file
         */
        public void StoreAirportsToFile()
        {
            
            String[] lines = new string[airports.Count]; // to store airport data
            int i = 0;
            foreach (Airport airport in airports) // iterate through all airports
            {
                lines[i++] = $"{airport.Id} {airport.City} {airport.DateVisited:MM/dd/yyyy} {airport.Rating}"; // split up properties into correct format
            }
            File.WriteAllLines(airportsFile, lines); // write to txt file
            
        }
    }
}
