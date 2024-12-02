using Microsoft.AspNetCore.Mvc;

namespace SimplifyScrum.Utils.Requests;

public  class ResponseProducer
{
    private static ResponseProducer _shared = new ResponseProducer();

    public static ResponseProducer Shared
    {
        get
        {
            return _shared;
        }
    }

    private ResponseProducer() { }
    
    public  IActionResult InternalServerError(string? message = null)
    {
        return new ObjectResult(new { Message = message ?? Messages.Messages.GenericError500 }) { StatusCode = 500 };
    }
    
    public IActionResult NoContent()
    {
        
        return new ObjectResult(new { Message = Messages.Messages.Generic204, Body = new List<string>() }) { StatusCode = 204};
    }
    
    public IActionResult BadRequest(string message)
    {
        return new ObjectResult(new { Message = message }) { StatusCode = 400 };
    }
}