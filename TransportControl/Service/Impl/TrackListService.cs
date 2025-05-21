using Microsoft.EntityFrameworkCore;
using TransportControl.Model.Entity;
using TransportControl.Model.DTO;

namespace TransportControl.Service.Impl
{
    public class TrackListService : ITrackListService
    {
        private readonly ApplicationDbContext _context;

        public TrackListService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TrackListDto>> GetAllTrackListAsync()
        {
            var trackLists = await _context.TrackLists
                .Include(t => t.Car)
                .Include(t => t.Driver)
                .Include(t => t.TrackPoints)
                .ToListAsync();

            return trackLists.Select(trackList => new TrackListDto
            {
                Id = trackList.Id,
                RemainingFuelStart = trackList.RemainingFuelStart,
                RemainingFuelEnd = trackList.RemainingFuelEnd,
                StartTime = trackList.StartTime,
                EndTime = trackList.EndTime,
                OdometrStart = trackList.OdometrStart,
                OdometrEnd = trackList.OdometrEnd,
                ValidityPeriodStart = trackList.ValidityPeriodStart,
                ValidityPeriodEnd = trackList.ValidityPeriodEnd,
                PersonnelNumber = trackList.PersonnelNumber,
                Car = trackList.Car != null ? new CarDto
                {
                    Id = trackList.Car.Id,
                    CarName = trackList.Car.CarName,
                    CarVin = trackList.Car.CarVin,
                    CarNumber = trackList.Car.CarNumber,
                    CarFuelType = trackList.Car.CarFuelType,
                    CarCategory = trackList.Car.CarCategory,
                    CarFuelUsing = trackList.Car.CarFuelUsing,
                    CarOdometr = trackList.Car.CarOdometr,
                    StartInsurance = trackList.Car.StartInsurance,
                    EndInsurance = trackList.Car.EndInsurance,
                    PersonnelNumber = trackList.Car.PersonnelNumber
                } : null,
                Driver = trackList.Driver != null ? new DriverDto
                {
                    Id = trackList.Driver.Id,
                    DriverName = trackList.Driver.DriverName,
                    DriverCategory = trackList.Driver.DriverCategory,
                    PersonnelNumber = trackList.Driver.PersonnelNumber
                } : null,
                TrackPoints = trackList.TrackPoints?.Select(tp => new TrackPointDto
                {
                    Id = tp.Id,
                    NumberPoint = tp.NumderPoint,
                    CustomerCode = tp.CustomerCode,
                    StartPointName = tp.StartPointName,
                    EndPointName = tp.EndPointName,
                    StartPointTime = tp.StartPointTime,
                    EndPointTime = tp.EndPointTime,
                    DistanceTraveled = tp.DistanceTraveled,
                    PersonnelNumber = tp.PersonnelNumber,
                    TrackListId = tp.TrackListId
                }).ToList() ?? new List<TrackPointDto>()
            }).ToList();
        }

        public async Task<List<TrackList>> TracklistAsync()
        {
            var trackList = await _context.TrackLists
                .Include(t => t.Car)
                .Include(t => t.Driver)
                .Include(t => t.TrackPoints)
                .ToListAsync();
            return trackList;


        }

        public async Task<TrackListDto?> GetTrackListByIdAsync(Guid id)
        {
            var trackList = await _context.TrackLists
                .Include(t => t.Car)
                .Include(t => t.Driver)
                .Include(t => t.TrackPoints)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trackList == null) return null;

            return new TrackListDto
            {
                Id = trackList.Id,
                RemainingFuelStart = trackList.RemainingFuelStart,
                RemainingFuelEnd = trackList.RemainingFuelEnd,
                StartTime = trackList.StartTime,
                EndTime = trackList.EndTime,
                OdometrStart = trackList.OdometrStart,
                OdometrEnd = trackList.OdometrEnd,
                ValidityPeriodStart = trackList.ValidityPeriodStart,
                ValidityPeriodEnd = trackList.ValidityPeriodEnd,
                PersonnelNumber = trackList.PersonnelNumber,
                Car = new CarDto
                {
                    Id = trackList.Car.Id,
                    CarName = trackList.Car.CarName,
                    CarVin = trackList.Car.CarVin,
                    CarNumber = trackList.Car.CarNumber,
                    CarFuelType = trackList.Car.CarFuelType,
                    CarCategory = trackList.Car.CarCategory,
                    CarFuelUsing = trackList.Car.CarFuelUsing,
                    CarOdometr = trackList.Car.CarOdometr,
                    StartInsurance = trackList.Car.StartInsurance,
                    EndInsurance = trackList.Car.EndInsurance,
                    PersonnelNumber = trackList.Car.PersonnelNumber
                },
                Driver = new DriverDto
                {
                    Id = trackList.Driver.Id,
                    DriverName = trackList.Driver.DriverName,
                    DriverCategory = trackList.Driver.DriverCategory,
                    PersonnelNumber = trackList.Driver.PersonnelNumber
                    
                },
                TrackPoints = trackList.TrackPoints.Select(tp => new TrackPointDto
                {
                    Id = tp.Id,
                    NumberPoint = tp.NumderPoint,
                    CustomerCode = tp.CustomerCode,
                    StartPointName = tp.StartPointName,
                    EndPointName = tp.EndPointName,
                    StartPointTime = tp.StartPointTime,
                    EndPointTime = tp.EndPointTime,
                    DistanceTraveled = tp.DistanceTraveled,
                    PersonnelNumber = tp.PersonnelNumber,
                    TrackListId = tp.TrackListId
                }).ToList()
            };
        }

        public async Task<TrackList> CreateTrackListAsync(CreateTrackListDto dto)
        {
            var trackList = new TrackList
            {
                RemainingFuelStart = dto.RemainingFuelStart,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,                
                ValidityPeriodStart = dto.ValidityPeriodStart,
                ValidityPeriodEnd = dto.ValidityPeriodEnd
                
            };

            _context.TrackLists.Add(trackList);
            await _context.SaveChangesAsync();
            return trackList;
        }
        
        public async Task<TrackList> AddCarToTrackListAsync(AddCarToTrackListDto dto)
        {
            var trackList = await _context.TrackLists.FirstOrDefaultAsync(t => t.Id == dto.TrackListId);
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == dto.CarId);

            if (trackList == null || car == null)
                throw new InvalidOperationException("Not found");

            if (car.EndInsurance <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Страховка на данный автомобиль истекла, добавление невозможно");
            }           
                trackList.OdometrStart = car.CarOdometr;
                trackList.CarId = car.Id;
                trackList.Car = car;
            

            await _context.SaveChangesAsync();
            return trackList;
        }
        public async Task<TrackList> AddDriverToTrackListAsync(AddDriverToTrackListDto dto)
        {   
            var trackList = await _context.TrackLists.Include(t => t.Car).FirstOrDefaultAsync(t => t.Id == dto.TrackListId);
            var driver =    await _context.Drivers.FirstOrDefaultAsync(d  => d.Id == dto.DriverId);

            if (trackList == null || driver == null)
                throw new InvalidOperationException("Not found");

            if (trackList.Car == null)
            {
                throw new InvalidOperationException("У путевого листа не указан автомобиль");
            }
            if (trackList.Car.CarCategory != driver.DriverCategory)
            {
                throw new InvalidOperationException("Водитель не имеет права управлять данным транспортным средством");
            }
            
                trackList.Driver = driver;
                trackList.DriverId = driver.Id;
            

            await _context.SaveChangesAsync();
            return trackList;

        }



        public async Task<bool> UpdateTrackListAsync(Guid id, TrackListDto dto)
        {
            var trackList = await _context.TrackLists.FindAsync(id);
            if (trackList == null) return false;

            trackList.RemainingFuelStart = dto.RemainingFuelStart;
            trackList.RemainingFuelEnd = dto.RemainingFuelEnd;
            trackList.StartTime = dto.StartTime;
            trackList.EndTime = dto.EndTime;
            trackList.OdometrStart = dto.OdometrStart;
            trackList.OdometrEnd = dto.OdometrEnd;
            trackList.ValidityPeriodStart = dto.ValidityPeriodStart;
            trackList.ValidityPeriodEnd = dto.ValidityPeriodEnd;
            trackList.CarId = dto.Car.Id;
            trackList.DriverId = dto.Driver.Id;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<TrackList> TrackListCloseAsync(TrackListCloseDto closeDto)
        {
            var trackList = await _context.TrackLists
                .Include(t => t.Car)
                .Include(t => t.TrackPoints)
                .FirstOrDefaultAsync(t => t.Id == closeDto.TrackListId);

            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == trackList.CarId);

            if (trackList == null)
                throw new InvalidOperationException("Not found");

            
            var totalDistance = trackList.TrackPoints.Sum(tp => tp.DistanceTraveled);
            if (trackList.OdometrStart + totalDistance != closeDto.OdometrValue)
            {
                throw new InvalidOperationException("Не соответствие с Пройденой дистанцией");
            }

            trackList.OdometrEnd = trackList.OdometrStart + totalDistance;
            trackList.RemainingFuelEnd = closeDto.FuelRemainder;
            trackList.EndTime = closeDto.ReturnTime;
            car.CarOdometr = (int)(trackList.OdometrStart + totalDistance);


            await _context.SaveChangesAsync();
            return trackList;
        }


        public async Task<bool> DeleteTrackListAsync(Guid id)
        {
            var trackList = await _context.TrackLists.FindAsync(id);
            if (trackList == null) return false;

            _context.TrackLists.Remove(trackList);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
