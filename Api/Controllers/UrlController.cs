﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Url_Shortener.Data.DTO;
using Url_Shortener.Services.UrlService;

namespace Url_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlOperations;

        public UrlController(IUrlService urlOperations)
        {
            _urlOperations = urlOperations;
        }

        [HttpPost("LongToShort")]
        public async Task<IActionResult> LongToShort([FromBody] LongRequest request)
        {
            var result = await _urlOperations.LongToShortUrlAsync(request);

            return Ok(result);
        }

        [HttpPost("ShortToLong")]
        public async Task<IActionResult> ShortToLong([FromBody] ShortRequest request)
        {
            var result = await _urlOperations.ShortToLongUrlAsync(request);

            if (string.IsNullOrEmpty(result))
                return BadRequest();

            return Ok(result);
        }
    }
}
