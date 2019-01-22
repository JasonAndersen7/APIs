using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempleInfo.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TempleInfo.API.Controllers
{
    public class DummyController : Controller
    {

        private TempleInfoContext _ctx;

        public DummyController(TempleInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }

    }
}
