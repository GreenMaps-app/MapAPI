using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MapAPI.Models
{
    public class DatapointContext : DbContext
    {
        public DatapointContext(DbContextOptions<DatapointContext> options) : base(options)
        {
        }

        public DbSet<Datapoint> Datapoints { get; set; }


    }
}
