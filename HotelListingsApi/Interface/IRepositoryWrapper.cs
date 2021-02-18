using System;
using System.Threading.Tasks;
using HotelListingsApi.Model;

namespace HotelListingsApi.Interface
{
    public interface IRepositoryWrapper : IDisposable
    {
        IRepositoryBase<Country> Countries { get; }
        IRepositoryBase<Hotel> Hotels { get; }

        Task Save();
    }
}