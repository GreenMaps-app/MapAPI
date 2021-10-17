using System;

#nullable disable

namespace MapAPI.Models
{
    // Defines the model to be equivalent to the table
    // Automatically created through scanning the database table
    public partial class MapLocationDatum
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string IpAddress { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Message { get; set; }
        public bool Resolved { get; set; }
        public string Severity { get; set; }    // NOTE THAT severity ONLY ALLOWS "Low", "Medium", "High"
    }
}
