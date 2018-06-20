using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using JobControl.Bll;

namespace JobControl.Web.Controllers.Api
{
    /// <summary>
    /// Controller that handles api request to manage global settings.</summary>
    /// <remarks>
    /// Exceptions thrown from methods returning <see cref="IActionResult"/> in this controller should be handled by the
    /// <see cref="ApiExceptionHandlerMiddleware"/>.</remarks>
    [Route("api/globalSettings")]
    public class GlobalSettingsController : Controller
    {
        ILogger _Logger;
        IGlobalSettingsRepository _Repository;

        public GlobalSettingsController(IGlobalSettingsRepository repository, ILoggerFactory loggerFactory)
        {
            _Logger = loggerFactory.CreateLogger(GetType());
            _Repository = repository;
        }

        /// <summary>
        /// Handles GET request to api/globalSettings.</summary>
        /// <returns>
        /// An <see cref="IActionResult"/> representing an OK status code and a <see cref="GlobalSettingsEditor"/>.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _Repository.Fetch();
            return Ok(result);
        }

        /// <summary>
        /// Handles PUT request to api/globalSettings.</summary>
        /// <param name="data">
        /// An <see cref="GlobalSettingsEditor"/> that is deserialized from the request body.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing an OK status code and a GlobalSettingsEditor.</returns>
        /// <remarks>
        /// <para>If the body contains invalid data then this method will return a Bad Request status code and a list of <see cref="ModelError"/>s.</para>
        /// <para>If another user has changed the data since it was recieved then this method will return a Conflict status code and a GlobalSettingsEditor
        /// with the other user's changes.</para>
        /// </remarks>
        [HttpPut]
        public IActionResult Put([FromBody]GlobalSettingsEditor data)
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