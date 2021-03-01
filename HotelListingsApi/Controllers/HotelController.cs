using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingsApi.DTO;
using HotelListingsApi.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HotelListingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        // Services
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;
 
        public HotelController(
            IRepositoryWrapper repositoryWrapper,
            ILogger<HotelController> logger,
            IMapper mapper )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = _logger = logger;
            _mapper = mapper;
        }

    [HttpGet]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotels()
    {
        try
        {
            var Hotels = await _repositoryWrapper.Hotels.GetAll();
             var results = _mapper.Map<IList<HotelDTO>>(Hotels);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}");
            return StatusCode(500, "Internal Server Error, Please Try Again Later.");
        }
    }


    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotel(int id)
    {
        try
        {
            var Hotel = await _repositoryWrapper.Hotels.Get(exprs => exprs.Id == id, new List<string> { "Hotels" });
            var results = _mapper.Map<HotelDTO>(Hotel);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotel)}");
            return StatusCode(500, "Internal Server Error, Please Try Again Later.");
        }
    }
    }
}