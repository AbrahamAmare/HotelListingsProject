using System.Threading.Tasks;
using HotelListingsApi.DTO;

namespace HotelListingsApi.Interface
{
    public interface IAuthService
    {
        Task<bool> ValidateUser(LoginDTO loginDTO);
        Task<string> CreateToken();
    }
}