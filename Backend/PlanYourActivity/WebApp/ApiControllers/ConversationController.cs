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
/// Conversation Controller class
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ConversationController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ConversationMapper _mapper;
    
    /// <summary>
    /// Conversation Controller Constructor
    /// </summary>
    /// <param name="bll">Bll</param>
    /// <param name="autoMapper">Mapper</param>
    public ConversationController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new ConversationMapper(autoMapper);
    }
    
    /// <summary>
    /// Get group conversation
    /// </summary>
    /// <returns>Public groups</returns>
    [HttpGet("{groupId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Conversation), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Conversation>> GetGroups(Guid groupId)
    {
        var data = await _bll.ConversationService.FindAsync(groupId, User.GetUserId());
        if (data == null)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "User not in group!"
            });
        }
        return _mapper.Map(data)!;
    }
    
    /// <summary>
    /// Add message
    /// </summary>
    /// <returns>ActionResult - 200 when successful and 400 when unsuccessful</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddMessage(AddMessage addMessage)
    {
        var data = await _bll.ConversationService.AddMessage(_mapper.MapAddMessage(addMessage), User.GetUserId());
        await _bll.SaveChangesAsync();
        
        if (data == null)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "User not in group!"
            });
        }

        if (data == false)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Failed to add message!"
            });
        }

        return Ok();
    }
}