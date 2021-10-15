﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MapAPI.Models
{
    public partial class MapLocationDatum
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string IpAddress { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Message { get; set; }
    }
}
