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
/// Event Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EventController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly EventMapper _mapper;
    
    /// <summary>
    /// Event Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public EventController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new EventMapper(autoMapper);
    }

    /// <summary>
    /// Get all public events where user meets requirements
    /// </summary>
    /// <returns>All public events</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<GroupEvent>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.GroupEvent>>> GetPublicEvents(EventFilter? eventFilter)
    {
        var data = await
            _bll.EventService.AllPublicAsync(_mapper.MapEventFilter(eventFilter), User.GetUserId());
        
        var res = data
            .Select(e => _mapper.Map(e)!)
            .ToList();
        
        return res;
    }

    /// <summary>
    /// Get user event by event id
    /// </summary>
    /// <returns>Users event</returns>
    [HttpGet("User/{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<UserEvent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Public.DTO.v1.UserEvent>> GetUserEvent(Guid id)
    {
        var data = await _bll.EventService.FindUserEventAsync(id, User.GetUserId());
        
        if (data == null)
        {
            return NotFound();
        }
        
        return _mapper.MapUserEvent(data);
    }

    /// <summary>
    /// Get group event by event id. User must be in group.
    /// </summary>
    /// <returns>Group event</returns>
    [HttpGet("Group/{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<GroupEvent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Public.DTO.v1.GroupEvent>> GetGroupEvent(Guid id)
    {
        var data = await _bll.EventService.FindGroupEventAsync(id, User.GetUserId());
    
        if (data == null)
        {
            return NotFound();
        }
    
        return _mapper.Map(data)!;
    }

    /// <summary>
    /// Get all group events by a group id. User must be in this group.
    /// </summary>
    /// <returns>All group events, if user not in group, then 400 - Bad Request</returns>
    [HttpGet("Group/All/{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<GroupEvent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.GroupEvent>>> GetGroupEvents(Guid groupId)
    {
        var data = await
            _bll.EventService.AllGroupEventsAsync(groupId, User.GetUserId());
    
        if (data == null)
        {
            return BadRequest();
        }
        
        var res = data
            .Select(e => _mapper.Map(e)!)
            .ToList();
        
        return res;
    }

    /// <summary>
    /// Get all user events
    /// </summary>
    /// <returns>All user events</returns>
    [HttpGet("User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<UserEvent>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.UserEvent>>> GetUserEvents()
    {
        var data = await
            _bll.EventService.AllUserEventsAsync(User.GetUserId());
        
        var res = data
            .Select(e => _mapper.MapUserEvent(e)!)
            .ToList();
        
        return res;
    }
    
    /// <summary>
    /// Add group event to user event list. User must be in group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost("User/{eventId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Public.DTO.v1.UserEvent>> AddGroupEventToEvents(Guid eventId)
    {
        var added = await _bll.EventService.AddGroupEventToEvents(eventId, User.GetUserId());
        await _bll.SaveChangesAsync();
        if (added)
        {
            return Ok();
        }
        return BadRequest(new RestApiErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            Error = "Adding failed"
        });
    }

    /// <summary>
    /// Add new user event
    /// </summary>
    /// <returns>Plain Event without includes</returns>
    [HttpPost("User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<UserEvent>), StatusCodes.Status201Created)]
    public async Task<ActionResult<Public.DTO.v1.UserEvent>> PostUserEvent(AddUserEvent userEvent)
    {
        var bllEvent = _mapper.MapEventWithUser(userEvent);
        bllEvent.AppUserId = User.GetUserId();
        var result = _mapper.MapUserEvent(_bll.EventService.AddUserEvent(bllEvent, User.GetUserId())!);
        await _bll.SaveChangesAsync();
        return CreatedAtAction("GetUserEvent", new {id = result!.Id}, result);
    }
    
    /// <summary>
    /// Add new group event if user is group admin
    /// </summary>
    /// <returns>Plain Event without includes</returns>
    [HttpPost("Group")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<GroupEvent>), StatusCodes.Status201Created)]
    public async Task<ActionResult<Public.DTO.v1.GroupEvent>> PostGroupEvent(AddGroupEvent groupEvent)
    {
        var bllEvent = _mapper.MapEventWithGroup(groupEvent);
        var result = _mapper.Map(await _bll.EventService.AddGroupEvent(bllEvent, User.GetUserId()));
        await _bll.SaveChangesAsync();
        return CreatedAtAction("GetGroupEvent", new {id = result!.Id}, result);
    }
    
    /// <summary>
    /// Update user event
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPut("User/{eventId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateUserEvent(Guid eventId, AddUserEvent userEvent)
    {
        var userEventBll = _mapper.MapEventWithUser(userEvent);
        userEventBll.AppUserId = User.GetUserId();
        var updated = await _bll.EventService.UpdateUserEvent(userEventBll, eventId, User.GetUserId());
        await _bll.SaveChangesAsync();
        
        if (!updated)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Updating failed"
            });
        }
        return NoContent();
    }
    
    /// <summary>
    /// Update group event. User must be admin in that group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPut("Group/{eventId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateGroupEvent(Guid eventId, AddGroupEvent groupEvent)
    {
        var groupEventBll = _mapper.MapEventWithGroup(groupEvent);
        var updated = await _bll.EventService.UpdateGroupEvent(groupEventBll, eventId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (!updated)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Updating failed"
            });
        }
        return NoContent();
    }
    
    /// <summary>
    /// Remove event from user's event list
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpDelete("User/{eventId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteUserEvent(Guid eventId)
    {
        var deleted = await _bll.EventService.DeleteUserEvent(eventId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (!deleted)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Deleting failed"
            });
        }
        return NoContent();
    }
    
    /// <summary>
    /// Remove event from group. User must be admin in that group.
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpDelete("Group/{eventId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteGroupEvent(Guid eventId)
    {
        var deleted = await _bll.EventService.DeleteGroupEvent(eventId, User.GetUserId());
        await _bll.SaveChangesAsync();

        if (!deleted)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Deleting failed"
            });
        }
        return NoContent();
    }
}