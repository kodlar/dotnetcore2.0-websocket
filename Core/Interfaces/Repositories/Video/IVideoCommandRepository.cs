using Core.Entitites.Video;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Repositories.Video
{
    public interface IVideoCommandRepository : IRepository<VideoTable>
    {
        void ExecuteVideo(VideoTable video);

        //Task<Event> ExecuteEvent(Event ev);
    }
}
