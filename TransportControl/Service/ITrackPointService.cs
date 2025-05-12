using TransportControl.Model.Entity;
using TransportControl.Model.DTO;

namespace TransportControl.Service
{
    public interface ITrackPointService
    {
        public Task<TrackPointDto?> GetTrackPointByIdAsync(Guid id);
        public Task<TrackPoint> CreateTrackPointAsync(CreateTrackPointDto dto);
        public Task<bool> UpdateTrackPointAsync(Guid id, TrackPointDto dto);
        public Task<bool> DeleteTrackPointAsync(Guid id);

    }
}
