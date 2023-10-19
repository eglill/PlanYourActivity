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
/// Activity Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ActivityController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ActivityMapper _mapper;
    
    /// <summary>
    /// Activity Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public ActivityController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new ActivityMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all activities
    /// </summary>
    /// <returns>Activities</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Activity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Activity>>> GetActivities()
    {
        var data = await _bll.ActivityService.AllAsync();
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
}