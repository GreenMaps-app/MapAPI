using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapAPI.Models.Repository
{
    public class MapLocationRepository : IMapLocationRepository
    {
        private hackathon_dbContext db;

        public MapLocationRepository(hackathon_dbContext dbContext)
        {
            this.db = dbContext;
        }

        /**
         * Get all datapoints inside the database table
         */
        public List<MapLocationDatum> GetAll()
        {
            if (db != null)
            {
                List<MapLocationDatum> datapoints = new List<MapLocationDatum>();

                /**
                 * Equivalent SQL query
                 * SELECT * FROM db.MapLocationData ORDER BY Id DESC;
                 */
                var result = from o in db.MapLocationData
                             orderby o.Id descending
                             select o;

                /**
                 * Intialize new datapoint object for each result and add to the list
                 */
                foreach (var r in result)
                {
                    MapLocationDatum datapoint = new MapLocationDatum();
                    datapoint.Id = r.Id;
                    datapoint.DateCreated = r.DateCreated;
                    datapoint.IpAddress = r.IpAddress;
                    datapoint.Title = r.Title;
                    datapoint.Latitude = r.Latitude;
                    datapoint.Longitude = r.Longitude;
                    datapoint.Message = r.Message;
                    datapoint.Resolved = r.Resolved;

                    datapoints.Add(datapoint);
                }

                return datapoints;
            }

            // If database is null, return null
            return null;
        }
        public MapLocationDatum Get(int id)
        {
            if (db != null)
            {
                return db.MapLocationData.Find(id);
            }

            return null;
        }
        public void Add(MapLocationDatum entity)
        {
            if (db != null)
            {
                // Must save changes after making a change to the database
                db.MapLocationData.Add(entity);
                db.SaveChanges();
            }
        }

        public void UpdateStatus(int id, bool status)
        {
            if (db != null)
            {
                // Must save changes after making a change to the database
                
                MapLocationDatum datapoint = db.MapLocationData.Find(id);
                datapoint.Resolved = status;
                db.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            if (db != null)
            {
                // Must save changes after making a change to the database
                MapLocationDatum datapoint = db.MapLocationData.Find(id);
                db.MapLocationData.Remove(datapoint);
                db.SaveChanges();
            }
        }
    }
}
