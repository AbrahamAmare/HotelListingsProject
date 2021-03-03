using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingsApi.DTO;
using HotelListingsApi.Interface;
using HotelListingsApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // [Authorize(Roles = "Adminstrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO createCountryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(createCountryDTO);
                await _repositoryWrapper.Countries.Insert(country);
                await _repositoryWrapper.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(CreateCountry)}");
                return StatusCode(500, "Internal Server Error, Please try again later");
            }

        }

        // [Authorize]
        [HttpPut("{id:int}")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO updateCountryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Invalid update attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _repositoryWrapper.Countries.Get(exprs => exprs.Id == id);

                if (country == null)
                {
                    _logger.LogInformation($"Invalid update attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(updateCountryDTO, country);
                _repositoryWrapper.Countries.Update(country);
                await _repositoryWrapper.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
               _logger.LogInformation(ex, $"Invalid update attempt in {nameof(UpdateCountry)}");
                return StatusCode(500, "Something went wrong, Please try again");
            }
        }

         // [Authorize(Roles = "Adminstator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }

            try
            {
                var country = await _repositoryWrapper.Countries.Get(exprs => exprs.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid Delete attempt in {nameof(DeleteCountry)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _repositoryWrapper.Countries.Delete(id);
                await _repositoryWrapper.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
               _logger.LogError(ex, $"Something went wrong in {nameof(DeleteCountry)}");
               return StatusCode(500, "Internal Server Error, Please try again later");
            }
        }
    }
}