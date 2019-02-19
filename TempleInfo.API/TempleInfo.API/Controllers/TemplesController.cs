using AutoMapper;
using TempleInfo.API.Models;
using TempleInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Controllers
{
    [Route("api/temples")]
    public class TemplesController : Controller
    {
        private ILogger<TemplesController> _logger;
        private IMailService _mailService;


        private ITempleInfoRepository _templeInfoRepository;
        public TemplesController(ILogger<TemplesController> logger, IMailService mailService, ITempleInfoRepository templeInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _templeInfoRepository = templeInfoRepository;

        }

        [HttpGet()]
        public IActionResult Gettemples()
        {

            var temples = _templeInfoRepository.GetTemples();

            var results = Mapper.Map<IEnumerable<TempleOnlyDTO>>(temples);

            return Ok(results);
           // return Ok(templesDataStore.Current.temples);
        }

        [HttpGet("{id}")]
        public IActionResult GetTemple(int id, bool include = false)
        {
            // find temple

            var templeToReturn = _templeInfoRepository.GetTemple(id, include);

            if (templeToReturn == null)
            {
                _logger.LogInformation($"temple with templeID {id} was not found");
                return NotFound();
            }

            if (include)
            {
                #region commented out
                //var templeResult = new templeDTO()
                //{
                //    Id = templeToReturn.Id
                //    ,
                //    Name = templeToReturn.Name
                //    ,
                //    Description = templeToReturn.Description
                //};

                //foreach(var poi in templeToReturn.PointsOfInterest)
                //{
                //    templeResult.PointsOfInterest.Add(
                //        new PointOfInterestDto()
                //        {
                //            Id = poi.Id,
                //            Name = poi.Name,
                //            Description = poi.Description
                //        });
                //}
                #endregion

                var templeResult = Mapper.Map<TempleDTO>(templeToReturn);

                return Ok(templeResult);
            }


            var templeOnlyResult = Mapper.Map<TempleOnlyDTO>(templeToReturn);
            _mailService.Send($"temple {id} requested", "A GET call was issued");
            // return Ok(templeToReturn);
            return Ok(templeOnlyResult);
        }



    }
}
