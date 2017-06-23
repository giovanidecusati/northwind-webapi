using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace NorthWind.WebApi.Controllers
{
    [Route("services")]
    public class ServiceController : Controller
    {
        static string _version;

        static ServiceController()
        {
            _version = Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        [HttpGet]
        [Route("version")]
        [AllowAnonymous]
        public IActionResult Version()
        {
            return Ok(new { version = _version });
        }

        [HttpGet]
        [Route("servername")]
        [AllowAnonymous]
        public IActionResult ServerName()
        {
            return Ok(new { server = Environment.MachineName });
        }
    }
}
