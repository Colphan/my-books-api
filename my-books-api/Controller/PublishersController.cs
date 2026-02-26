using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

 public class PublishersController : ControllerBase
{
 private PublishersService _publishersService;

 public PublishersController(PublishersService publishersService)
    {
             _publishersService = publishersService;
    }

[HttpPost("add-publisher")]

public IActionResult AddBook([FromBody]PublisherVM publisher)
    {
        _publishersService.AddPublisher(publisher);
        return Ok();

    }

}

}