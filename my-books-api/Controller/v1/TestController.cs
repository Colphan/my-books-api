using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books_api.Controllers.v1
{
[ApiVersion("1.0")]
[ApiVersion("1.2")]
[ApiVersion("1.9")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    //[HttpGet("get-test-data")]
    //[MapToApiVersion("1.0")]
    //public IActionResult getV1()
    //{
      //  return Ok("This is a version v1.0");
   // }

    [HttpGet("get-test-data")]
    [MapToApiVersion("1.2")]
    public IActionResult getV12()
    {
        return Ok("This is a version v1.2");
    }

    [HttpGet("get-test-data")]
    [MapToApiVersion("1.9")]
    public IActionResult getV19()
    {
        return Ok("This is a version v1.9");
    }
}
    
}