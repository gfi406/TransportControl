using Microsoft.EntityFrameworkCore;
using TransportControl.Model.Entity;
using TransportControl.Model.DTO;

namespace TransportControl.Service.Impl
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto?> GetCarByIdAsync(Guid id)
        {
            var car = await _context.Cars
                .Include(c => c.TrackLists)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null) return null;

            return new CarDto
            {
                CarName = car.CarName,
                CarVin = car.CarVin,
                CarNumber = car.CarNumber,
                CarCategory = car.CarCategory,
                CarFuelUsing = car.CarFuelUsing,
                CarFuelType = car.CarFuelType,
                CarOdometr = car.CarOdometr,
                StartInsurance = car.StartInsurance,
                EndInsurance = car.EndInsurance,
                PersonnelNumber = car.PersonnelNumber,
                //TrackLists = car.TrackLists.Select(t => new TrackListDto
                //{
                //    Id = t.Id,
                //    StartTime = t.StartTime
                //}).ToList()
            };
        }

        public async Task<Car> CreateCarAsync(CreateCarDto dto)
        {
            var car = new Car
            {
                CarName = dto.CarName,
                CarVin = dto.CarVin,
                CarNumber = dto.CarNumber,
                CarCategory = dto.CarCategory,
                CarFuelUsing = dto.CarFuelUsing,
                CarFuelType = dto.CarFuelType,
                CarOdometr = dto.CarOdometr,
                StartInsurance = dto.StartInsurance,
                EndInsurance = dto.EndInsurance
                
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> UpdateCarAsync(Guid id, CarDto dto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;

            car.CarName = dto.CarName;
            car.CarVin = dto.CarVin;
            car.CarNumber = dto.CarNumber;
            car.CarCategory = dto.CarCategory;
            car.CarFuelUsing = dto.CarFuelUsing;
            car.StartInsurance = dto.StartInsurance;
            car.EndInsurance = dto.EndInsurance;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
