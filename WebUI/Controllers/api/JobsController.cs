using System;
using System.Linq;
using System.Threading;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using JobControl.Bll;

namespace JobControl.Web.Controllers.Api
{
    public class JobsController : Controller
    {
        ILogger _Logger;
        IJobRepository _Repository;

        public JobsController(IJobRepository repository, ILoggerFactory loggerFactory)
        {
            _Logger = loggerFactory.CreateLogger(GetType());
            _Repository = repository;
        }

        /// <summary>
        /// Handles request to api/jobs</summary>
        [HttpGet("/api/jobs")]
        public ActionResult Get()
        {
            var result = _Repository.Fetch();
            return Ok(result);
        }

        [HttpGet("/api/jobs/{id:int}")]
        public ActionResult Get(int id)
        {
            var result = _Repository.Fetch(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("/api/jobs/new")]
        public ActionResult GetNew()
        {
            var result = _Repository.Create();
            return Ok(result);
        }

        [HttpDelete("/api/jobs/{id:int}")]
        public ActionResult Delete(int id, [FromQuery]byte[] concurrency)
        {
            var result = _Repository.Delete(id, concurrency);
            if (result.ConcurrencyError)
                return StatusCode(409, result.Data); // Conflict
            return Ok();
        }

        [HttpPost("/api/jobs")]
        public ActionResult Post([FromBody]JobEditor data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            var result = _Repository.Save(data);
            if (result.ConcurrencyError)
                return StatusCode(409, result.Data); // Conflict
            return CreatedAtAction("Put", new { id = result.Data.Key}, result.Data);
        }

        [HttpPut("/api/jobs/{id:int}")]
        public ActionResult Put(int id, [FromBody]JobEditor data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            var result = _Repository.Save(data);
            if (result.ConcurrencyError)
                return StatusCode(409, result.Data); // Conflict
            return Ok(result.Data);
        }
    }
}