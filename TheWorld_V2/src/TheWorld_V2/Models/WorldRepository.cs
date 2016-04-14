using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace TheWorld_V2.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("couldn't look up the thing", ex.Message);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                        .Include(t => t.Stops)
                        .OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("couldn't look up the thing", ex.Message);
                return null;
            }
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            return _context.Trips.Where(t => t.Name == tripName && t.UserName == userName)
                .Include(t => t.Stops).FirstOrDefault();
        }

        public void AddStop(string tripName, Stop newStop, string userName)
        {
            var theTrip = GetTripByName(tripName, userName);
            newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            theTrip.Stops.Add(newStop);

            _context.Add(newStop);
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            return GetAllTripsWithStops().Where(t => t.UserName == name).ToList();
        }
    }
}