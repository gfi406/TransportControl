using TransportControl.Model.Entity;
using TransportControl.Model.DTO;

namespace TransportControl.Service
{
    public interface ITrackListService
    {
        public Task<List<TrackListDto>> GetAllTrackListAsync();
        public  Task<TrackListDto?> GetTrackListByIdAsync(Guid id);
        public  Task<TrackList> CreateTrackListAsync(CreateTrackListDto dto);
        public  Task<bool> UpdateTrackListAsync(Guid id, TrackListDto dto);
        public Task<TrackList> AddCarToTrackListAsync(AddCarToTrackListDto dto);
        public  Task<TrackList> AddDriverToTrackListAsync(AddDriverToTrackListDto dto);
        Task<TrackList> TrackListCloseAsync(TrackListCloseDto closeDto);

        public Task<bool> DeleteTrackListAsync(Guid id);
    }
}
