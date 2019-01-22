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
                   
                     Name = "New York temple",
                     Description = "The one with that big park.",
                     PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                            
                             Name = "Central Park",
                             Description = "The most visited urban park in the United States." },
                          new PointOfInterest() {
                           
                             Name = "Empire State Building",
                             Description = "A 102-story skyscraper located in Midtown Manhattan." },
                     }
                },
                new Temple()
                {
                    
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                            
                             Name = "Cathedral of Our Lady",
                             Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
                          new PointOfInterest() {
                           
                             Name = "Antwerp Central Station",
                             Description = "The the finest example of railway architecture in Belgium." },
                     }
                },
                new Temple()
                {
                   
                    Name = "Paris",
                    Description = "The one with that big tower.",
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
