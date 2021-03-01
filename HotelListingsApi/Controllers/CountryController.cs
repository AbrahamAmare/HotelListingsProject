using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingsApi.DTO;
using HotelListingsApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountryController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        public CountryController(IRepositoryWrapper repositoryWrapper,
                ILogger<CountryController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

    [HttpGet]

    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var countries = await _repositoryWrapper.Countries.GetAll();
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
            return StatusCode(500, "Internal Server Error, Please Try Again Later.");
        }
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        try
        {
            var country = await _repositoryWrapper.Countries.Get(exprs => exprs.Id == id, new List<string> { "Hotels" });
            var results = _mapper.Map<CountryDTO>(country);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
            return StatusCode(500, "Internal Server Error, Please Try Again Later.");
        }
    }

}
}