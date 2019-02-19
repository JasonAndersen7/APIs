using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempleInfo.API.Entities;

namespace TempleInfo.API
{
    public static class TempleInfoContextExt
    {
  
        public static void EnsureSeedDataForContext( this TempleInfoContext context)
        {
            if (context.Temples.Any())
            {
                return;
            }

            var temples = new List<Temple>()
            {
                new Temple()
                {
                   
                     Name = "Salt Lake City temple",
                     Description = "The one with big spires.",
                     PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                            
                             Name = "Salt Lake",
                             Description = "As stated, a very salty lake." },
                          new PointOfInterest() {
                           
                             Name = "Visitor Center",
                             Description = "Missionaries from all around the world serve here. " },
                     }
                },
                new Temple()
                {
                    
                    Name = "Washington D.C.",
                    Description = "Very dramatic temple with with views along the highway.",
                    PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                            
                             Name = "Parking is easy to find",
                             Description = "First major temple east of the Mississipi." },
                          new PointOfInterest() {
                           
                             Name = "Temple Gardens",
                             Description = "The the finest example of flowers" },
                     }
                },
                new Temple()
                {
                   
                    Name = "Paris",
                    Description = "Temple in France.",
                    PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                          
                             Name = "Eiffel Tower",
                             Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                          new PointOfInterest() {
                            
                             Name = "The Louvre",
                             Description = "The world's largest museum." },
                     }
                }

            };

            context.Temples.AddRange(temples);
            context.SaveChanges();

        }
    }
}
