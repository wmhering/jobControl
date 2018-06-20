using Microsoft.AspNetCore.Mvc;

using JobControl.Dal;

namespace JobControl.WebUI.Controllers
{
    public class JobsController : Controller
    {
        JobRepository _Repository;

        public JobsController(JobRepository reposirory)
        {
            _Repository = reposirory;
        }

        [HttpGet("~/jobs")]
        public IActionResult Get()
        {
            return View("ListView", _Repository.Fetch());
        }

        [HttpGet("~/jobs/{key}")]
        public IActionResult Get(int key)
        {
            return View("EditView", _Repository.Fetch(key));
        }

        [HttpGet("~/jobs/new")]
        public IActionResult GetNew()
        {
            return View("EditView", 0);
        }
    }
}