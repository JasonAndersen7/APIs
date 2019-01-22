using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Entities
{
    public class TempleInfoContext: DbContext
    {
        public DbSet<Temple> Temples { get; set; }

        public DbSet<PointOfInterest> PointsofInterest { get; set; }

        public TempleInfoContext(DbContextOptions<TempleInfoContext> options)
            :base (options)
        {
            Database.EnsureCreated();
            Console.Write(Database.GenerateCreateScript());

        }

    }
}
