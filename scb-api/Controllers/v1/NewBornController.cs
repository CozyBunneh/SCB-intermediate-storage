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
  /// New born entries for regions api endpoint
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  [Route("api/v1.0/Region/{regionId}/Gender/{genderId}/[controller]")]
  public class NewBornController : ControllerBase
  {
    private readonly ILogger<NewBornController> _logger;
    private readonly ScbDbContext _context;
    private readonly DbSet<NewBorn> _newBornDbSet;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public NewBornController(ILogger<NewBornController> logger, ScbDbContext context)
    {
      _logger = logger;
      _context = context;
      _newBornDbSet = _context.Set<NewBorn>();
    }

    /// <summary>
    /// Get all new born entries for a specific region
    /// </summary>
    /// <param name="regionId">Region id</param>
    /// <param name="genderId">Gender id</param>
    /// <returns></returns>
    /// <response code="200">List of new born entries returned</response>
    /// <response code="404">New born entries could not be found for that region id</response>
    /// <response code="400">Badly formed request</response>
    [HttpGet]
    [ProducesResponseType(typeof(NewBornV1[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> GetAll([FromRoute] string regionId, [FromRoute] int genderId)
    {
      if (string.IsNullOrWhiteSpace(regionId) || genderId == 0)
      {
        return BadRequest();
      }

      var newBorns = await _newBornDbSet.Where(n => n.Region.Id == regionId && n.Gender.Id == genderId).ToListAsync();
      if (newBorns == null || newBorns.Count == 0)
      {
        return NotFound();
      }

      return Ok(newBorns.OfType<NewBorn>().Select(NewBornV1.Translate).OrderBy(n => n.Year));
    }

    /// <summary>
    /// Gets a specific new born entity by id
    /// </summary>
    /// <param name="regionId">Region id</param>
    /// <param name="genderId">Gender id</param>
    /// <param name="year">New born entity year</param>
    /// <returns></returns>
    /// <response code="200">New born entity returned</response>
    /// <response code="404">New born entity could not be found for that id</response>
    /// <response code="400">Badly formed request</response>
    [HttpGet("{year}")]
    [ProducesResponseType(typeof(NewBornV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> Get([FromRoute] string regionId, [FromRoute] int genderId, int year)
    {
      if (string.IsNullOrWhiteSpace(regionId) || genderId == 0 || year == 0)
      {
        return BadRequest();
      }

      var newBorn = await _newBornDbSet.Where(n => n.Year == year).FirstAsync();
      if (newBorn == null)
      {
        return NotFound(newBorn);
      }

      return Ok(NewBornV1.Translate(newBorn));
    }
  }
}