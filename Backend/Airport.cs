using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Description: A class to represent an airport
 * Name: Dominick Hagedorn
 * Date:9/17/2024
 * Bugs: None Known.
 * Reflection: this was a very simple class to implement
 */
namespace Lab1
{
    public class Airport
    {
        private String id;
        private String city;
        private DateTime dateVisited;
        private int rating;

        public string Id 
        { 
            get => id;
            set
            {
                id = value;
                return;
            }
        }

        public string City 
        { 
            get => city;
            set
            {
                city = value;
                return;
            }
        }

        public DateTime DateVisited 
        { 
            get => dateVisited;
            set
            {
                dateVisited = value;
            }
        }

        public int Rating 
        { 
            get => rating;
            set
            {                
                rating = value;
                return;
            }
        }

        /**
         * creates a new airport with given qualities
         */
        public Airport(String id, String city, DateTime dateVisited, int rating)
        {
            this.Id = id;
            this.City = city;
            this.DateVisited = dateVisited;
            this.Rating = rating;
        }

        /**
         * creates the default airport, which is appleton
         */
        public Airport()
        {
            this.Rating = 5;
            this.City = "Appleton";
            this.Id = "KATW";
            this.DateVisited = DateTime.Now;
        }

        /**
         * checks if two airports are equal
         */
        public override bool Equals(Object? obj)
        {
            if (obj == null) return false; // if object doesnt exist
            Airport other = obj as Airport; // store obj as an airport
            if(this.Id == other.Id) // check if they have the same id
            {
                return true; // they are equal
            }
            return false; // they are not equal
        }

        /**
         * copys the data from one airport to another
         */
        public void Copy(Airport airport)
        {
            this.Id = airport.Id;
            this.City = airport.City;
            this.DateVisited = airport.DateVisited;
            this.Rating = airport.Rating;
        }

        /**
         * String representation of an airport
         */
        public override string ToString()
        {
            return $"{Id} - {City}, {DateVisited:M/d/yyyy}, {Rating}";
        }
    }
}
