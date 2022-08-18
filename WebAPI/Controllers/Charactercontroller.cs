using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Charactercontroller : ControllerBase
    {
        private static Charater knight = new Charater();
        public IActionResult Get()
        {
            return Ok(knight);
        }
    }
}