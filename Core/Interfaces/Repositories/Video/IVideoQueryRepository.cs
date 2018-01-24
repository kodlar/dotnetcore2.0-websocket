using Data.Core.Entitites.Video;
using Data.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Video
{
    public interface IVideoQueryRepository : IRepository<VideoTable>
    {
        Task<List<VideoTable>> GetAllAsync();
        Task<VideoTable> GetVideoAsync(int videoId);
        List<VideoTable> GetAll();
        VideoTable GetVideo(int videoId);
    }
}
