using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageLog.Models;

namespace GarageLog.Data
{
    public class GarageLogContext : DbContext
    {
        public GarageLogContext (DbContextOptions<GarageLogContext> options)
            : base(options)
        {
        }

        public DbSet<GarageLog.Models.User> User { get; set; } = default!;
        public DbSet<GarageLog.Models.Vehcile> Vehcile { get; set; } = default!;
        public DbSet<GarageLog.Models.Maintenance> Maintenance { get; set; } = default!;
    }
}
