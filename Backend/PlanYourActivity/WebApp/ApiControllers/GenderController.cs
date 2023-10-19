using System.Net;
using System.Net.Mime;
using App.Contracts.BLL;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers;

/// <summary>
/// Gender Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly GenderMapper _mapper;

    /// <summary>
    /// Gender Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public GenderController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new GenderMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all genders
    /// </summary>
    /// <returns>All genders</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Gender>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Gender>>> GetGenders()
    {
        var data = await
            _bll.GenderService.AllAsync();

        var res = data
            .Select(e => _mapper.Map(e)!)
            .ToList();

        return res;
    }
    
    // GET: api/Gender/5
    /// <summary>
    /// Get Gender by GenderId
    /// </summary>
    /// <param name="id">GenderId</param>
    /// <returns>Gender with given id</returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Gender), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Public.DTO.v1.Gender>> GetGenders(Guid id)
    {
        var gender = await _bll.GenderService.FindAsync(id);

        if (gender == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.NotFound,
                Error = "No gender with given id"
            });
        }

        var res = _mapper.Map(gender)!;

        return res;
    }

    // POST: api/Gender
    /// <summary>
    /// Add new gender
    /// </summary>
    /// <param name="gender">Gender data</param>
    /// <returns>Added gender with updated id</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Gender), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Public.DTO.v1.Gender>> PostGender(Public.DTO.v1.Gender gender)
    {
        // TODO check if role is admin
        var bllGender = _mapper.Map(gender);
        if (bllGender != null)
        {
            var res = _mapper.Map(_bll.GenderService.Add(bllGender));
            await _bll.SaveChangesAsync();

            return Created("GetGender", res);
        }

        return NoContent();
    }
}