using TransportControl.Model.DTO;
using TransportControl.Model.Entity;


namespace TransportControl.Service
{
    public interface IDriverService
    {
        public Task<List<DriverDto>> GetAllDriversAsync();
        public  Task<DriverDto?> GetDriverByIdAsync(Guid id);
        public Task<Driver> CreateDriverAsync(CreateDriverDto dto);
        public  Task<bool> UpdateDriverAsync(Guid id, DriverDto dto);
        public Task<bool> DeleteDriverAsync(Guid id);
    }
}
