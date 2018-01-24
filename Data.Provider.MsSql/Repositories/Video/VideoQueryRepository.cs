using Core.Interfaces.Repositories.Video;
using Data.Core.Entitites.Video;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;

namespace Data.Provider.MsSql.Repositories.Video
{
    public class VideoQueryRepository : BaseRepository<VideoTable>, IVideoQueryRepository
    {
        public VideoQueryRepository(IConfiguration configuration):base(configuration, "ArtTvSQLConnection", "Video")
        {

        }
        public async Task<VideoTable> GetVideoAsync(int videoId)
        {
            return await WithConnection(async c => 
            {
                var p = new DynamicParameters();
                p.Add("Id", videoId, DbType.Int32);
                var video = await c.QueryAsync<VideoTable>(sql: "[dbo].[XXX]",
                    param: p,
                    commandType: CommandType.StoredProcedure);

                return video.FirstOrDefault();
            });
        }

        public async Task<List<VideoTable>> GetAllAsync()
        {
            return await WithConnection(async c =>
            {
                
                var videoList = await c.QueryAsync<VideoTable>(sql: "[dbo].[xxx]",
                    commandType: CommandType.StoredProcedure);

                return videoList.ToList();
            });
        }

        List<VideoTable> IVideoQueryRepository.GetAll()
        {
            List<VideoTable> videoList;

            using (IDbConnection db = GetConnection())
            {
                db.Open();
                videoList = db.Query<VideoTable>("SELECT TOP 10 * From [dbo].[Video]").ToList();
                
            }
            
            return videoList;
        }

        public VideoTable GetVideo(int videoId)
        {
            VideoTable video;
            using(IDbConnection db = GetConnection())
            {
                video = db.Query<VideoTable>("Select * From [dbo].[Video] with(NOLOCK) where Id = " + videoId.ToString()).FirstOrDefault();
                
            }
            return video;
        }
    }
}
