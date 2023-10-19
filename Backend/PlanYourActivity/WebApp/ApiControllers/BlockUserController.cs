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
/// BlockUser Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BlockUserController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly AppUserMapper _mapper;
    
    /// <summary>
    /// BlockUser Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public BlockUserController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new AppUserMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all blocked users by blocker id
    /// </summary>
    /// <returns>Users</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<AppUser>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetBlockedUsers()
    {
        var data = await _bll.BlockedUserService.AllAsync(User.GetUserId());
        if (data == null)
        {
            return new List<AppUser>();
        }
        var res = data.Select(c => _mapper.Map(c.BlockedAppUser)!).ToList();
        return res;
    }
    
    /// <summary>
    /// Unblock user by id
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost("{userId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BlockUser(Guid userId)
    {
        if (userId == User.GetUserId())
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Can't block yourself"
            });
        }
        var data = await _bll.BlockedUserService.BlockUser(userId, User.GetUserId());
        await _bll.SaveChangesAsync();
        
        if (data)
        {
            return Ok();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "User already blocked/failed to block"
        });
    }
    
    /// <summary>
    /// Unblock user by id
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpDelete("{userId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UnblockUser(Guid userId)
    {
        var data = await _bll.BlockedUserService.RemoveAsync(userId, User.GetUserId());
        await _bll.SaveChangesAsync();
        if (data)
        {
            return NoContent();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "User already unblocked/failed to unblock"
        });
    }
}