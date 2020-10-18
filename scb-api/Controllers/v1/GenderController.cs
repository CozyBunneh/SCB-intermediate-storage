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
  /// Gender api endpoint
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  [Route("api/v1.0/Region/{regionId}/[controller]")]
  public class GenderController : ControllerBase
  {
    private readonly ILogger<GenderController> _logger;
    private readonly ScbDbContext _context;
    private readonly DbSet<Gender> _genderDbSet;
    private readonly DbSet<NewBorn> _newBornDbSet;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public GenderController(ILogger<GenderController> logger, ScbDbContext context)
    {
      _logger = logger;
      _context = context;
      _genderDbSet = _context.Set<Gender>();
      _newBornDbSet = _context.Set<NewBorn>();
    }

    /// <summary>
    /// Get all genders with their id and name
    /// </summary>
    /// <param name="regionId">Region id</param>
    /// <returns></returns>
    /// <response code="200">List of genders returned</response>
    /// <response code="404">Genders could not be found</response>
    [HttpGet]
    [ProducesResponseType(typeof(GenderV1[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> GetAll([FromRoute] string regionId)
    {
      if (string.IsNullOrWhiteSpace(regionId))
      {
        return BadRequest();
      }

      var genders = await _genderDbSet.ToListAsync();
      if (genders == null || genders.Count == 0)
      {
        return NotFound();
      }

      return Ok(genders.OfType<Gender>().Select(g => GenderV1.Translate(g)));
    }

    /// <summary>
    /// Gets a specific gender by id
    /// </summary>
    /// <param name="regionId">Region id</param>
    /// <param name="id">Gender id</param>
    /// <returns></returns>
    /// <response code="200">Gender returned</response>
    /// <response code="404">Gender could not be found for that id</response>
    /// <response code="400">Badly formed request</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenderV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> Get([FromRoute] string regionId, int id)
    {
      if (id == 0)
      {
        return BadRequest();
      }

      var gender = await _genderDbSet.FindAsync(id);
      var newBornIds = await _newBornDbSet.Where(n => n.Region.Id == regionId && n.Gender.Id == id).Select(n => n.Year).ToListAsync();
      if (gender == null || newBornIds == null || newBornIds.Count == 0)
      {
        return NotFound(gender);
      }

      return Ok(GenderV1.Translate(gender, newBornIds));
    }
  }
}