using TempleInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Services
{
   public interface ITempleInfoRepository
    {

        bool TempleExists(int templeId);

        IEnumerable<Temple> GetTemples();

        Temple GetTemple(int templeId, bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForTemple(int templeId);

        PointOfInterest GetPointOfInterestForTemple(int templeId, int pointOfInterestId);

        void AddPointOfInterest(int templeId, PointOfInterest poi);

        void DeletePointOfInterest(PointOfInterest poi);

        bool Save();

    }
}
