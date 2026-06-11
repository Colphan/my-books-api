using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.Services;
using my_books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using my_books_api.Exceptions;
using my_books_api.Data.Models;
using my_books_api.ActionResult;
using Microsoft.Extensions.Logging;


namespace my_books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;
        private readonly LogsService _logsService;

        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger, LogsService logsService)
        {

            _publishersService = publishersService;
            _logger = logger;
            _logsService = logsService;
        }

[HttpGet("get-all-publishers")]
public IActionResult GetAllPublishers(
    string? sortBy,
    string? searchString,
    int pageNumber = 1)
{
    try
    {
        Console.WriteLine("ENTERED GET ALL");

        _logsService.AddLog(
            "Information",
            "GetAllPublishers executed"
        );

        Console.WriteLine("AFTER ADD LOG");

        _logger.LogInformation(
            "This is a log in GetAllPublishers()"
        );

        var _result =
            _publishersService.GetAllPublishers(
                sortBy,
                searchString,
                pageNumber
            );

        return Ok(_result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return BadRequest(ex.Message);
    }
}

        [HttpGet("get-publisher-by-with-id/{id}")]

        public CustomActionResult GetPublisherById(int id)
        {

            // throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                //return _response;
                //return Ok(_response);
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _response
                };

                return new CustomActionResult(_responseObj);

            }
            else
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Exception = new Exception("This is comiing from publishers cotroller")
                };
                return new CustomActionResult(_responseObj);

                //return null;
                //return NotFound();
            }
        }


        [HttpGet("get-publisher-books-with-authors/{id}")]

        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);

        }

        [HttpDelete("delete-Publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}