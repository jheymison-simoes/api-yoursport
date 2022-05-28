using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YourSport.Api.Configuration;

namespace YourSport.Api.Controllers;

[ApiController]
public abstract class BaseController<TController> : ControllerBase
{
    protected readonly ILogger<TController> _logger;
    protected readonly IMapper _mapper;
    protected readonly AppSettings AppSettings;
    
    public BaseController(
        ILogger<TController> logger, 
        IOptions<AppSettings> appSettings, 
        IMapper mapper)
    {
        _logger = logger;
        @AppSettings = appSettings.Value;
        _mapper = mapper;
    }

    public BaseController(AppSettings appSettings)
    {
        @AppSettings = appSettings;
    }

    protected ResponseResult<T> ResponseResultError<T>(Exception ex) where T : class
    {
        _logger.LogInformation(ex, ex.ToString(), null);
        return ResponseResultError<T>(ex.Message);
    }
    
    protected ResponseResult<T> ResponseResultInternalError<T>(string controller, Exception ex) where T : class
    {
        _logger.LogError(ex, ex.ToString(), null);

        return this.ResponseResultInternalError<T>(controller, ex.ToString());
    }
    
    protected ResponseResult<T> ResultResponseNotAllowed<T>(Exception ex) where T : class
    {
        _logger.LogInformation(ex, ex.ToString(), null);
        return ResponseResultError<T>(ex.Message, HttpStatusCode.Forbidden);
    }
    
    protected ResponseResult<T> ResponseResultError<T>(string errorMessage) where T : class
    {
        return ResponseResultError<T>(errorMessage, HttpStatusCode.BadRequest);
    }
    
    protected ResponseResult<T> ResponseResultInternalError<T>(string controller, string mensagemErro) where T : class
    {
        return ResponseResultError<T>(mensagemErro, HttpStatusCode.InternalServerError);
    }
    
    protected ResponseResult<T> ResponseResultError<T>(string errorMessage, HttpStatusCode httpStatusCode) where T : class
    {
        Response.StatusCode = (int)httpStatusCode;
        return new ResponseResult<T>()
        {
            Error = true,
            ErrorMessage = new List<string>(){ errorMessage },
            Result = null
        };
    }
}