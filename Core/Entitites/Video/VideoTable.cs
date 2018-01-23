
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entitites.Video
{
    public class VideoTable : EntityBase
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public int SubCatId { get; set; }
        public string Title { get; set; }
        public string PageLink { get; set; }
        public string Context { get; set; }
        public int ViewNumber { get; set; }
        public string Path { get; set; }
        public string Thumbnail { get; set; }
        public string Keywords { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int StatusId { get; set; }
        public string Editor { get; set; }
        public int OrderId { get; set; }
        public bool ISelect { get; set; }
        public byte SpotResim { get; set; }
        public string Sure { get; set; }
        public string Folder { get; set; }
        public string TempFile { get; set; }
        public int ThumbSayi { get; set; }
        public string KeywordsEn { get; set; }
        public string Spot { get; set; }
        public string OldDBId { get; set; }
        public string LowPath { get; set; }
        public int AuthorId { get; set; }
        public string GTitle { get; set; }
        public string GLink { get; set; }
      
        
    }
}
