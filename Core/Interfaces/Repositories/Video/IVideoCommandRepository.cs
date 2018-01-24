using Data.Core.Entitites.Video;
using Data.Core.Interfaces.Repositories;

namespace Core.Interfaces.Repositories.Video
{
    public interface IVideoCommandRepository : IRepository<VideoTable>
    {
        void ExecuteVideoViewNumber(VideoTable video);

        //Task<Event> ExecuteEvent(Event ev);
    }
}
