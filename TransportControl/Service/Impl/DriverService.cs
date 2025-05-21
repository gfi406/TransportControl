using Microsoft.EntityFrameworkCore;
using TransportControl.Model.DTO;
using TransportControl.Model.Entity;
using TransportControl.Model.DTO;

namespace TransportControl.Service.Impl
{
    public class DriverService : IDriverService
    {
        private readonly ApplicationDbContext _context;

        public DriverService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DriverDto>> GetAllDriversAsync()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return drivers.Select(d => new DriverDto
            {
                Id = d.Id,
                DriverName = d.DriverName,
                DriverCategory = d.DriverCategory,
                PersonnelNumber = d.PersonnelNumber
            }).ToList();
            
            
        }

        public async Task<DriverDto?> GetDriverByIdAsync(Guid id)
        {
            var driver = await _context.Drivers         
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null) return null;

            return new DriverDto
            {
                Id = driver.Id,
                DriverName = driver.DriverName,
                DriverCategory = driver.DriverCategory,
                PersonnelNumber = driver.PersonnelNumber
               
            };
        }

        public async Task<Driver> CreateDriverAsync(CreateDriverDto dto)
        {
            var driver = new Driver
            {                   
                DriverName = dto.DriverName,
                DriverCategory = dto.DriverCategory
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> UpdateDriverAsync(Guid id, DriverDto dto)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return false;

            driver.DriverName = dto.DriverName;
            driver.DriverCategory = dto.DriverCategory;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDriverAsync(Guid id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return false;

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
