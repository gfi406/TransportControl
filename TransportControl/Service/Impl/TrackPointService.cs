using TransportControl.Model.Entity;
using TransportControl.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace TransportControl.Service.Impl
{
    public class TrackPointService : ITrackPointService
    {
        private readonly ApplicationDbContext _context;

        public TrackPointService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrackPointDto?> GetTrackPointByIdAsync(Guid id)
        {
            var trackPoint = await _context.TrackPoints.FindAsync(id);
            if (trackPoint == null) return null;

            return new TrackPointDto
            {
                Id = trackPoint.Id,
                NumberPoint = trackPoint.NumderPoint,
                CustomerCode = trackPoint.CustomerCode,
                StartPointName = trackPoint.StartPointName,
                EndPointName = trackPoint.EndPointName,
                StartPointTime = trackPoint.StartPointTime,
                EndPointTime = trackPoint.EndPointTime,
                DistanceTraveled = trackPoint.DistanceTraveled,
                PersonnelNumber = trackPoint.PersonnelNumber,
                TrackListId = trackPoint.TrackListId
            };
        }

        public async Task<TrackPoint> CreateTrackPointAsync(CreateTrackPointDto dto)
        {
            var trackListExists = await _context.TrackLists.AnyAsync(t => t.Id == dto.TrackListId);
            if (!trackListExists)
            {
                throw new ArgumentException($"TrackList с ID {dto.TrackListId} не существует");
            }

            // 2. Создаем новую точку
            var trackPoint = new TrackPoint
            {
                NumderPoint = dto.NumberPoint,
                CustomerCode = dto.CustomerCode,
                StartPointName = dto.StartPointName,
                EndPointName = dto.EndPointName,
                StartPointTime = dto.StartPointTime,
                EndPointTime = dto.EndPointTime,
                DistanceTraveled = dto.DistanceTraveled,
                TrackListId = dto.TrackListId
            };

            _context.TrackPoints.Add(trackPoint);
            await _context.SaveChangesAsync();
            return trackPoint;
        }

        public async Task<bool> UpdateTrackPointAsync(Guid id, TrackPointDto dto)
        {
            var trackPoint = await _context.TrackPoints.FindAsync(id);
            if (trackPoint == null) return false;

            trackPoint.NumderPoint = dto.NumberPoint;
            trackPoint.CustomerCode = dto.CustomerCode;
            trackPoint.StartPointName = dto.StartPointName;
            trackPoint.EndPointName = dto.EndPointName;
            trackPoint.StartPointTime = dto.StartPointTime;
            trackPoint.EndPointTime = dto.EndPointTime;
            trackPoint.DistanceTraveled = dto.DistanceTraveled;
            trackPoint.TrackListId = dto.TrackListId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTrackPointAsync(Guid id)
        {
            var trackPoint = await _context.TrackPoints.FindAsync(id);
            if (trackPoint == null) return false;

            _context.TrackPoints.Remove(trackPoint);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
