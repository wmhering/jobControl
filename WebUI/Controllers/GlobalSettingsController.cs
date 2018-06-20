using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobControl.WebUI.Controllers
{
    public class ConfigurationController : Controller
    {

        public ConfigurationController()
        {
        }

        [HttpGet("globalSetting")]
        public IActionResult Get()
        {
            return View("EditView");
        }
    }
}