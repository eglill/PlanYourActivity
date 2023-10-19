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

namespace WebApp.ApiControllers;

/// <summary>
/// Group Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GroupController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly GroupMapper _mapper;
    
    /// <summary>
    /// Group Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public GroupController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new GroupMapper(autoMapper);
    }
    
    /// <summary>
    /// Get all public groups, where user meets requirements and can join
    /// </summary>
    /// <returns>Public groups</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Group>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Group>>> GetGroups()
    {
        var data = await _bll.GroupService.AllAsync(User.GetUserId());
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
    
    /// <summary>
    /// Check if user is group admin
    /// </summary>
    /// <returns>Boolean</returns>
    [HttpGet("Admin/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> GetGroups(Guid groupId)
    {
        var res = await _bll.GroupService.IsUserAdmin(groupId, User.GetUserId());
        return res;
    }
    
    /// <summary>
    /// Get group data. Must be in group.                     
    /// </summary>
    /// <returns>Group data</returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<GroupWithData>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Public.DTO.v1.GroupWithData>> GetGroup(Guid id)
    {
        var data = await _bll.GroupService.FindGroupAsync(id, User.GetUserId());
        
        if (data == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.NotFound,
                Error = "No gender with given id or no access to group"
            });
        }

        return _mapper.MapToGroupWithData(data);
    }
    
    /// <summary>
    /// Get groups that have invited user to join.                  
    /// </summary>
    /// <returns>Group list</returns>
    [HttpGet("Invite")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Group>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Group>>> GetGroupInvitations()
    {
        var data = await _bll.GroupService.FindInvites(User.GetUserId());
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
    
    /// <summary>
    /// Get groups that user have joined.                 
    /// </summary>
    /// <returns>Group list</returns>
    [HttpGet("User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Group>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Group>>> GetUserGroups()
    {
        var data = await _bll.GroupService.UserGroups(User.GetUserId());
        var res = data.Select(c => _mapper.Map(c)!).ToList();
        return res;
    }
    
    /// <summary>
    /// Get all users in group                
    /// </summary>
    /// <returns>User list</returns>
    [HttpGet("Users/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Identity.AppUser>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Identity.AppUser>>> GetAllUsersInGroup(Guid groupId)
    {
        var data = await _bll.GroupService.UsersInGroup(groupId, User.GetUserId());
        if (data == null)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "User not in group!"
            });
        }
        var res = data.Select(c => _mapper.MapAppUser(c)).ToList();
        return res;
    }
    
    /// <summary>
    /// Create new group                 
    /// </summary>
    /// <returns>Plain group without includes</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Group>), StatusCodes.Status201Created)]
    public async Task<ActionResult<Public.DTO.v1.Group>> PostGroup(AddGroup group)
    {
        var bllGroup = _mapper.MapToAddGroup(group);
        bllGroup.AdminId = User.GetUserId();
        var result = _mapper.Map(_bll.GroupService.Add(bllGroup));
        await _bll.SaveChangesAsync();
        return CreatedAtAction("GetGroup", new {id = result!.Id}, result);
    }
    
    /// <summary>
    /// Join Group                
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost("Join/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> JoinGroup(Guid groupId)
    {
        var joined = await _bll.GroupService.JoinGroup(groupId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (joined)
        {
            return Ok();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "User doesn't meet requirements"
        });
    }
    
    /// <summary>
    /// Leave Group and delete upcoming events of this group from personal events list
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost("Leave/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LeaveGroup(Guid groupId)
    {
        var leaved = await _bll.GroupService.LeaveGroup(groupId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (leaved)
        {
            return Ok();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "Leaving failed"
        });
    }
    
    /// <summary>
    /// Kick user from group. Kicker must be admin in that group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost("Admin/Kick")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> KickUser(GroupAdminAction kickUser)
    {
        var kicked = await _bll.GroupService.KickUser(kickUser.GroupId, kickUser.UserId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (kicked)
        {
            return Ok();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "Kicking failed"
        });
    }
    
    /// <summary>
    /// Invite user to group. Inviter must be admin in that group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost("Admin/Invite")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> InviteUser(GroupAdminAction inviteUser)
    {
        var invited = await _bll.GroupService.InviteUser(inviteUser.GroupId, inviteUser.UserId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (invited)
        {
            return Ok();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "Invitation failed"
        });
    }
    
    /// <summary>
    /// Update group. Updater must be admin in that group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPut("Admin/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGroup(Guid groupId, AddGroup group)
    {
        var groupBll = _mapper.MapToAddGroup(group);
        groupBll.Id = groupId;
        groupBll.AdminId = User.GetUserId();
        var updated = await _bll.GroupService.Update(groupBll, User.GetUserId());
        await _bll.SaveChangesAsync();
        
        if (updated)
        {
            return NoContent();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "Updating failed"
        });
    }
    
    /// <summary>
    /// Delete group. Deleter must be admin in that group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpDelete("Admin/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        var deleted = await _bll.GroupService.Delete(groupId, User.GetUserId());
        await _bll.SaveChangesAsync();
        
        if (deleted)
        {
            return NoContent();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "Deleting failed"
        });
    }
}