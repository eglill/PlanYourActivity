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
/// Colour Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ColourController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ColourMapper _mapper;
    
    /// <summary>
    /// Colour Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public ColourController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new ColourMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all colours
    /// </summary>
    /// <returns>Colours</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Colour>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Colour>>> GetColours()
    {
        var data = await _bll.ColourService.AllAsync();
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
}