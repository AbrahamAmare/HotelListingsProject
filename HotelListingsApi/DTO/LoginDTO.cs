using System.ComponentModel.DataAnnotations;

namespace HotelListingsApi.DTO
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email  { get; set; }

        [Required]
        public string Password { get; set; }
    }
}