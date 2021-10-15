using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapAPI.Models;

namespace MapAPI.Models.Repository
{
    public interface IMapLocationRepository
    {
        List<MapLocationDatum> GetAll();
        MapLocationDatum Get(int id);
        void Add(MapLocationDatum entity);
        //void Update(MapLocationDatum entity);
        void Delete(int id);
    }
}
