using Microsoft.AspNetCore.Identity;

namespace HotelListingsApi.Data
{
    public class HotelListingsApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}