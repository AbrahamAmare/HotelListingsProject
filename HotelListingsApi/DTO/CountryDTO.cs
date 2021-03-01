using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelListingsApi.Model;

namespace HotelListingsApi.DTO
{
    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage= "Country name is too long")]
        public string Name { get; set; } 
        [Required]
        [StringLength(maximumLength: 5, ErrorMessage= "SCO for country is too long")]
        public string SCO { get; set; }
    }

// Extending this pattern by creating DTO per Operation
// Single Responsibility Pattern ????
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<Hotel> Hotels { get; set;}

    }
}

