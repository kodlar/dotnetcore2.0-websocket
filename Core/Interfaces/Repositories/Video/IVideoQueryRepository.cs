using Core.Entitites.Video;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Video
{
    public interface IVideoQueryRepository : IRepository<VideoTable>
    {
        Task<List<VideoTable>> GetAll();
        Task<VideoTable> GetVideo(int videoId);
    }
}
