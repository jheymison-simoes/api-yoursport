using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YourSport.Api.Configuration;
using YourSport.Api.ViewModels.User;
using YourSport.Application.Exceptions;
using YourSport.Application.Responses.User;
using YourSport.Application.ServiceModels.User;
using YourSport.Business.Models;

namespace YourSport.Api.Controllers;

[Route("[controller]")]
public class UserController : BaseController<UserController>
{
    private readonly ResourceSet _resourceSet;

    public UserController(
        ILogger<UserController> logger, 
        IOptions<AppSettings> appSettings, 
        IMapper mapper,
        ResourceManager resourceManager,
        CultureInfo cultureInfo)
        : base(logger, appSettings, mapper)
    {
        _resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseResult<UserResponse>>> Create(CreateUserViewModel request)
    {
        try
        {
            var user = _mapper.Map<User>(request);
            var userResponse = _mapper.Map<UserResponse>(user);
            return new ResponseResult<UserResponse>()
            {
                Result = userResponse
            };
        }
        catch (UserException uEx)
        {
            return ResponseResultError<UserResponse>(uEx);
        }
        catch (Exception ex)
        {
            return ResponseResultInternalError<UserResponse>(nameof(Create), ex);
        }
    }
}