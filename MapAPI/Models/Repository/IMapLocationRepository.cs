﻿using System.Collections.Generic;

namespace MapAPI.Models.Repository
{
    /**
     * Interface to be implemented by MapLocationRepository
     */
    public interface IMapLocationRepository
    {
        List<MapLocationDatum> GetAll();
        MapLocationDatum Get(int id);
        int Add (MapLocationDatum entity);
        void UpdateStatus (int id, bool status);
        void Delete(int id);
    }
}
