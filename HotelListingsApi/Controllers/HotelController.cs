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
    public class HotelController : ControllerBase
    {
        // Services
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(
            IRepositoryWrapper repositoryWrapper,
            ILogger<HotelController> logger,
            IMapper mapper)
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



        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _repositoryWrapper.Hotels.Get(exprs => exprs.Id == id, new List<string> { "Country" });
                var results = _mapper.Map<HotelDTO>(hotel);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotel)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later.");
            }
        }


        // [Authorize(Roles = "Adminstartor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO createHotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt for {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(createHotelDTO);
                await _repositoryWrapper.Hotels.Insert(hotel);
                await _repositoryWrapper.Save();

                return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateHotel)}");
                return Problem($"Something Went Wrong in the please try again");
            }
        }

        // [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO updateHotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Invalid Update attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = await _repositoryWrapper.Hotels.Get(exprs => exprs.Id == id);
                if(hotel == null)
                {
                    _logger.LogInformation($"Somthing went wrong in {nameof(UpdateHotel)}");
                    
                }

                _mapper.Map(updateHotelDTO, hotel);
                _repositoryWrapper.Hotels.Update(hotel);
                await _repositoryWrapper.Save();
                return NoContent();
            }

            catch (Exception ex)
            {
               _logger.LogInformation(ex, $"Something went wrong in the {nameof(UpdateHotel)}");
               return StatusCode(500, "Internal server error, please try again later");
            }
        }

        // [Authorize(Roles = "Adminstator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteHotel)}");
                return BadRequest();
            }

            try
            {
                var hotel = await _repositoryWrapper.Hotels.Get(exprs => exprs.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Invalid Delete attempt in {nameof(DeleteHotel)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _repositoryWrapper.Hotels.Delete(id);
                await _repositoryWrapper.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
               _logger.LogError(ex, $"Something went wrong in {nameof(DeleteHotel)}");
               return StatusCode(500, "Internal Server Error, Please try again later");
            }

        }
    }


}