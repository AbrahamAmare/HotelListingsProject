using System;
using System.Threading.Tasks;
using HotelListingsApi.Data;
using HotelListingsApi.Interface;
using HotelListingsApi.Model;

namespace HotelListingsApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DatabaseContext _context;
        private IRepositoryBase<Country> _countries;
        private IRepositoryBase<Hotel> _hotels;

        public RepositoryWrapper(DatabaseContext context)
        {
            _context = context;

        }
        public IRepositoryBase<Country> Countries => _countries ??= new RepositoryBase<Country>(_context);

        public IRepositoryBase<Hotel> Hotels => _hotels ??= new RepositoryBase<Hotel>(_context);
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}