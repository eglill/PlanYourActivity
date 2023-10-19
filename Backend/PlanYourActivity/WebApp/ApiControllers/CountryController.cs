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
/// Country Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CountryController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly CountryMapper _mapper;
    
    /// <summary>
    /// Country Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public CountryController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new CountryMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all countries
    /// </summary>
    /// <returns>Countries</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Country>>> GetCountries()
    {
        var data = await _bll.CountryService.AllAsync();
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
}