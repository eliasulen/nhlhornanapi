using Microsoft.AspNetCore.Mvc;
using nhlhornanapi.Model;
using nhlhornanapi.Util;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace nhlhornanapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Connected");
        }
    }
}