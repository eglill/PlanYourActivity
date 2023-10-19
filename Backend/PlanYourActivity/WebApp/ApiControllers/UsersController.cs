using System.Net;
using System.Net.Mime;
using App.Contracts.BLL;
using Asp.Versioning;
using AutoMapper;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;
using Public.DTO.v1.Identity;

namespace WebApp.ApiControllers;

/// <summary>
/// User Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly AppUserMapper _mapper;
    
    /// <summary>
    /// User Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public UsersController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new AppUserMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>users</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<AppUser>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var data = await _bll.UserService.AllAsync();
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
}