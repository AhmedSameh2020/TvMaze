using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvMaze.Model;
using TvMaze.Services.Interfaces;

namespace TvMaze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public ITvScraperService TvScraperService { get; set; }

        public ShowController(ITvScraperService tvScraperService)
        {
            TvScraperService = tvScraperService;
        }
        // GET api/Show/get?page=1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int page)
        {
            return Ok(await TvScraperService.GetShowsByPage(page));
        }
    }

}
