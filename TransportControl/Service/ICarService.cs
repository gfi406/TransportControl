using TransportControl.Model.Entity;
using TransportControl.Model.DTO;

namespace TransportControl.Service
{
    public interface ICarService
    {
        public Task<CarDto?> GetCarByIdAsync(Guid id);
        public Task<Car> CreateCarAsync(CreateCarDto dto);
        public  Task<bool> UpdateCarAsync(Guid id, CarDto dto);
        public  Task<bool> DeleteCarAsync(Guid id);

    }
}
