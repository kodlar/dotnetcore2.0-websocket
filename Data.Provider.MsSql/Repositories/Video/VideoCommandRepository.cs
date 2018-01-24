using Core.Interfaces.Repositories.Video;
using Dapper;
using Data.Core.Entitites.Video;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Data.Provider.MsSql.Repositories.Video
{
    public class VideoCommandRepository : BaseRepository<VideoTable>, IVideoCommandRepository
    {
        //protected VideoCommandRepository(IConfiguration configuration, string connectionStringKey, string tableName, string schemaName = "dbo") : base(configuration, connectionStringKey, tableName, schemaName)
        //{
        //}

        public VideoCommandRepository(IConfiguration configuration):base(configuration, "ArtTvSQLConnection","Video")
        {

        }

        public void ExecuteVideoViewNumber(VideoTable video)
        {           
            string sp = "[dbo].[2017_Web_UpdateVideoDetailViewNumber]";
            Execute(db => db.Execute(sp,
                new { Id = video.Id },
               commandType: CommandType.StoredProcedure, commandTimeout: 60));
        }
    }
}
