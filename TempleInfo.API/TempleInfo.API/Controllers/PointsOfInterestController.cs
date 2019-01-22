using AutoMapper;
using TempleInfo.API.Entities;
using TempleInfo.API.Models;
using TempleInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Controllers
{

    [Route("api/temples")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;

        private ITempleInfoRepository _templeInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService service, ITempleInfoRepository templeInfoRepository)
        {

            _logger = logger;
            _mailService = service;
            _templeInfoRepository = templeInfoRepository;
        }

        [HttpGet("{templeId}/PointsOfInterest")]
        public IActionResult GetPointsOfInterest(int templeId)
        {
            // var result = templesDataStore.Current.temples.FirstOrDefault(x => x.Id == templeId);
            var result = _templeInfoRepository.GetPointsOfInterestForTemple(templeId);
            if (result == null)
            {
                _logger.LogInformation($"temple with templeID {templeId} was not found");
                   return NotFound();
            }

            var pointsOfInterest = AutoMapper.Mapper.Map<IEnumerable<PointOfInterestDto>>(result);


            return Ok(pointsOfInterest);
        }


        [HttpGet("{templeId}/pointsOfInterest/{pId}", Name ="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int templeId, int pId)
        {
            //first try to find the temple
            //  var temple = templesDataStore.Current.temples.FirstOrDefault(x => x.Id == templeId);
            var temple = _templeInfoRepository.GetPointOfInterestForTemple(templeId,pId);
            if (temple == null)
            {
                _logger.LogInformation($"temple with templeID {templeId} and point of interest {pId} was not found");
                return NotFound();
            }

            //var pointOfInterest = temple.PointsOfInterest.FirstOrDefault(p => p.Id == pId);
            //if (pointOfInterest == null)
            //{
            //    return  NotFound();
            //}

            var pointOfInterestResult = Mapper.Map<PointOfInterest>(temple);

            return new OkObjectResult(pointOfInterestResult);

        }

        [HttpPost("{templeId}/pointsOfInterest")]
        public IActionResult CreatePointOfInterest(int templeId, [FromBody] PointOfInterestUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var temple = templesDataStore.Current.temples.FirstOrDefault(c => c.Id == templeId);
           // var temple = _templeInfoRepository.Gettemple(templeId, true);
           

            if (!_templeInfoRepository.TempleExists(templeId))
            {
                return NotFound();
            }



            //for demo only
            //var maxPointsId = templesDataStore.Current.temples.SelectMany(c => c.PointsOfInterest).Max(m => m.Id);
            //var maxPointsId = temple.PointsOfInterest.Max(m => m.Id);

            //var finalPointOfInterest = new PointOfInterest()
            //{
            //    Id = ++maxPointsId,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description

            //};

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _templeInfoRepository.AddPointOfInterest(templeId, finalPointOfInterest);

            if (!_templeInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while processing your request");

            }

           // temple.PointsOfInterest.Add(finalPointOfInterest);

             return CreatedAtRoute("GetPointOfInterest", new
            { templeId = templeId, pId = finalPointOfInterest.Id }, 
            finalPointOfInterest);
        }


   
        [HttpPut("{templeId}/pointsOfInterest/{pId}")]
        public IActionResult UpdatePointOfInterest(int templeId, int pId, [FromBody] PointOfInterestUpdateDto pointOfInterestUpdate)
        {

            if (pointOfInterestUpdate == null)
            {
                return BadRequest();
            }

            if (pointOfInterestUpdate.Description == pointOfInterestUpdate.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //first try to find the temple
            TempleDTO temple = Gettemple(templeId);
            if (temple == null)
            {
                _logger.LogInformation($"temple with templeID {templeId} was not found");
                return NotFound();
            }

            //next find the point of interest
            PointOfInterestDto pointOfInterestStore = GetPointOfInterest(pId, temple);
            if (pointOfInterestStore == null)
            {
                return NotFound();
            }

            pointOfInterestStore.Name = pointOfInterestUpdate.Name;
            pointOfInterestStore.Description = pointOfInterestUpdate.Description;

            return NoContent();
        }


        [HttpPatch("{templeId}/pointsOfInterest/{pId}")]
        public IActionResult PatchPointOfInterest(int templeId, int pId, [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDoc)
        {

            if (patchDoc == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            ////first try to find the temple
            //templeDTO temple = Gettemple(templeId);
            //if (temple == null)
            //{
            //    return NotFound();
            //}

            if(!_templeInfoRepository.TempleExists(templeId))
            {
                return NotFound();
            }

            //next find the point of interest
            // PointOfInterestDto pointOfInterestStore = GetPointOfInterest(pId, temple);
            //if (pointOfInterestStore == null)
            //{
            //    return NotFound();
            //}

            var pointOfInterestEntity = _templeInfoRepository.GetPointOfInterestForTemple(templeId, pId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestUpdateDto>(pointOfInterestEntity);
                   //new PointOfInterestUpdateDto()
                   //{
                   //    Name = pointOfInterestStore.Name,
                   //    Description = pointOfInterestStore.Description
                   //};

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //pointOfInterestStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestStore.Description = pointOfInterestToPatch.Description;

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            return NoContent();
        }

        [HttpDelete("{templeId}/pointsOfInterest/{pId}")]
        public IActionResult DeletePointOfInterest(int templeId, int pId)
        {
            //first try to find the temple
           // templeDTO temple = Gettemple(templeId);
          //  if (temple == null)
          if(!_templeInfoRepository.TempleExists(templeId))
            {
                _logger.LogInformation($"temple with templeID {templeId} was not found");
                return NotFound();
            }

            //PointOfInterestDto pointOfInterest = GetPointOfInterest(pId, temple);
            // if (pointOfInterest == null)

            var pointOfInterestEntity = _templeInfoRepository.GetPointOfInterestForTemple(templeId, pId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // temple.PointsOfInterest.Remove(pointOfInterest);

            _templeInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if (!_templeInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while processing your request");

            }

            _mailService.Send("Point of interest deleted.",
                $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted");
            return NoContent();

        }


        #region private methods

        private static TempleDTO Gettemple(int templeId)
        {
            return TemplesDataStore.Current.Temples.FirstOrDefault(x => x.Id == templeId);
        }


        private static PointOfInterestDto GetPointOfInterest(int pId, TempleDTO temple)
        {
            return temple.PointsOfInterest.FirstOrDefault(p => p.Id == pId);
        }
        #endregion
    }
}
