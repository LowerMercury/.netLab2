using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public interface IDatabase
    {
        public List<Airport> SelectAllAirports();
        public Airport SelectAirport(String id);
        public AirportAddError InsertAirport(Airport airport);
        public AirportDeletionError DeleteAirport(String id);
        public AirportEditError UpdateAirport(Airport airport);

    }
}
