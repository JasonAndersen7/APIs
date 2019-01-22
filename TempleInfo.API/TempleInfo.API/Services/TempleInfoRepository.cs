using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempleInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace TempleInfo.API.Services
{
    public class TempleInfoRepository : ITempleInfoRepository
    {

        private TempleInfoContext _ctx;

        public TempleInfoRepository(TempleInfoContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPointOfInterest(int templeId, PointOfInterest poi)
        {
            var templeToReturn = GetTemple(templeId, false);
            templeToReturn.PointsOfInterest.Add(poi);
       
        
      
        }

        public void DeletePointOfInterest(PointOfInterest poi)
        {
            _ctx.PointsofInterest.Remove(poi);
        }


        public bool TempleExists(int templeId)
        {
            return _ctx.Temples.Any(c => c.Id == templeId);
        }
               
        public IEnumerable<Temple> GetTemples()
        {
           return  _ctx.Temples.OrderBy(c => c.Name).ToList();
        }

        public Temple GetTemple(int templeId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _ctx.Temples.Include(c => c.PointsOfInterest).Where(c => c.Id == templeId).FirstOrDefault();

            }
           
             return   _ctx.Temples.Where(c => c.Id == templeId).FirstOrDefault();
           
        }

        public PointOfInterest GetPointOfInterestForTemple(int templeId, int pointOfInterestId)
        {
            return _ctx.PointsofInterest.Where(p => p.templeId == templeId && p.Id == pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForTemple(int templeId)
        {
            return _ctx.PointsofInterest.Where(p => p.templeId == templeId).ToList();
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
