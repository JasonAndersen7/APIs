using TempleInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API
{
    public class TemplesDataStore
    {
        public static TemplesDataStore Current { get; } = new TemplesDataStore();
        public List<TempleDTO> Temples { get; set; }

        public TemplesDataStore()
        {
            // init dummy data
            Temples = new List<TempleDTO>()
            {
                new TempleDTO()
                {
                     Id = 1,
                     Name = "New York temple",
                     Description = "The one with that big park.",
                     PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto() {
                             Id = 1,
                             Name = "Central Park",
                             Description = "The most visited urban park in the United States." },
                          new PointOfInterestDto() {
                             Id = 2,
                             Name = "Empire State Building",
                             Description = "A 102-story skyscraper located in Midtown Manhattan." },
                     }
                },
                new TempleDTO()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto() {
                             Id = 3,
                             Name = "Cathedral of Our Lady",
                             Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
                          new PointOfInterestDto() {
                             Id = 4,
                             Name = "Antwerp Central Station",
                             Description = "The the finest example of railway architecture in Belgium." },
                     }
                },
                new TempleDTO()
                {
                    Id= 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto() {
                             Id = 5,
                             Name = "Eiffel Tower",
                             Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                          new PointOfInterestDto() {
                             Id = 6,
                             Name = "The Louvre",
                             Description = "The world's largest museum." },
                     }
                }
            };

        }
    }
}
