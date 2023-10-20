﻿using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class UserManagementController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly ILogger<UserManagementController> _logger;

    public UserManagementController(IRegistrationService registrationService, ILogger<UserManagementController> logger)
    {
        _registrationService = registrationService;
        _logger = logger;
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(RegisterUserDto userDto, CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Information, userDto.Email);
        await _registrationService.RegisterAsync(userDto);
        return Ok();
    }

    [HttpGet("profile/{guid}")]
    public Task<IActionResult> GetProfile(Guid guid, CancellationToken cancellationToken) 
    {
        throw new NotImplementedException();
    }

    [HttpGet("users")]
    public Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("profile/{guid:guid}")]
    public Task<IActionResult> DeleteProfile(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("profile")]
    public Task<IActionResult> UpdateProfile(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("user/{guid:guid}")]
    public Task<IActionResult> DeleteUser(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}