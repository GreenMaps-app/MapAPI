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

        public List<MapLocationDatum> GetAll()
        {
            if (db != null)
            {
                List<MapLocationDatum> datapoints = new List<MapLocationDatum>();

                var result = from o in db.MapLocationData
                             orderby o.Id descending
                             select o;

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

                    datapoints.Add(datapoint);
                }

                return datapoints;
            }

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
                db.MapLocationData.Add(entity);
            }
        }
        public void Delete(int id)
        {
            MapLocationDatum datapoint = db.MapLocationData.Find(id);
            db.MapLocationData.Remove(datapoint);
        }
    }
}
