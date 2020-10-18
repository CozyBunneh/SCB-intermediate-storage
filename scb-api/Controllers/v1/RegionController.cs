using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using scb_api.Models;
using scb_api.Models.DTOs.api.v1;
using scb_api.Models.Entities;

namespace scb_api.Controllers.v1
{
  /// <summary>
  /// Region api endpoint
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  [Route("api/v1.0/[controller]")]
  public class RegionController : ControllerBase
  {
    private readonly ILogger<RegionController> _logger;
    private readonly ScbDbContext _context;
    private readonly DbSet<Region> _regionDbSet;
    private readonly DbSet<Gender> _genderDbSet;
    private readonly DbSet<NewBorn> _newBornDbSet;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public RegionController(ILogger<RegionController> logger, ScbDbContext context)
    {
      _logger = logger;
      _context = context;
      _regionDbSet = _context.Set<Region>();
      _genderDbSet = _context.Set<Gender>();
      _newBornDbSet = _context.Set<NewBorn>();
    }

    /// <summary>
    /// Get all regions with their id and name
    /// </summary>
    /// <returns></returns>
    /// <response code="200">List of regions returned</response>
    /// <response code="404">Regions could not be found</response>
    [HttpGet]
    [ProducesResponseType(typeof(RegionV1[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> GetAll()
    {
      var regions = await _regionDbSet.ToListAsync();

      if (regions == null || regions.Count == 0)
      {
        return NotFound();
      }

      return Ok(regions.OfType<Region>().Select(r => RegionV1.Translate(r)));
    }

    /// <summary>
    /// Gets a specific region by id
    /// </summary>
    /// <param name="id">Region id</param>
    /// <returns></returns>
    /// <response code="200">Region returned</response>
    /// <response code="404">Region could not be found for that id</response>
    /// <response code="400">Badly formed request</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RegionV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> Get(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        return BadRequest();
      }

      var region = await _regionDbSet.FindAsync(id);
      var genderIds = await _genderDbSet.Select(g => g.Id).ToListAsync();
      if (region == null || genderIds == null || genderIds.Count == 0)
      {
        return NotFound(region);
      }

      return Ok(RegionV1.Translate(region, genderIds));
    }

    /// <summary>
    /// Gets a specific region by id
    /// </summary>
    /// <param name="id">Region id</param>
    /// <returns></returns>
    /// <response code="200">Region returned</response>
    /// <response code="404">Region could not be found for that id</response>
    /// <response code="400">Badly formed request</response>
    [HttpGet("Full/{id}")]
    [ProducesResponseType(typeof(RegionFullV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> GetFull(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        return BadRequest();
      }

      var region = await _regionDbSet.FindAsync(id);
      if (region == null)
      {
        return NotFound(region);
      }

      var genders = await _genderDbSet.ToListAsync();
      if (genders == null || genders.Count == 0)
      {
        return NotFound(genders);
      }

      var newBorns = await _newBornDbSet.Where(n => n.Region.Id == region.Id).ToListAsync();
      if (newBorns == null || newBorns.Count == 0)
      {
        return NotFound(newBorns);
      }

      var newBornsV1 = newBorns.Select(n => NewBornV1.Translate(n));
      var gendersV1 = genders.Select(g => GenderFullV1.Translate(g, newBorns.Where(n => n.Gender.Id == g.Id).Select(n => NewBornV1.Translate(n)).ToList().OrderBy(n => n.Year)));
      return Ok(RegionFullV1.Translate(region, gendersV1));
    }
  }
}