using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Repositories.Video;
using Microsoft.AspNetCore.Mvc;
using Xabe.FFmpeg;

namespace WebSocketDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private NotificationsMessageHandler _notificationsMessageHandler { get; set; }
        private readonly IVideoQueryRepository _videoQueryRepository;
        public ValuesController(NotificationsMessageHandler notificationsMessageHandler, IVideoQueryRepository videoQueryRepository)
        {
            _notificationsMessageHandler = notificationsMessageHandler;
            _videoQueryRepository = videoQueryRepository;
            FFbase.FFmpegDir = Path.Combine(System.Environment.CurrentDirectory, "ffmpegbins");
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {


            List<string> lst = new List<string>();

            //lst.Add("C:\Users\okeskiner\Documents\tras\3497.mp4");
            //lst.Add("C:\Users\okeskiner\Documents\tras\3497.webm");


            /* //1.Faz -> Dosya indir
            
            await _notificationsMessageHandler.SendMessageToAllAsync("Dosya indirilmeye başlandı");
            Uri adres = new Uri("http://data.ekavart.tv/ekav/temp/mp4/2018/1/26/3497.mp4");
        
            using (WebClient wc = new WebClient())
            {
                //await wc.DownloadFileTaskAsync(adres, @"C:\Users\okeskiner\Documents\tras\3497.mp4");
                wc.DownloadFile(adres, @"C:\Users\okeskiner\Documents\tras\3497.mp4");
            }
            */


            /* //2.Faz -> webm formata dönüştür
            await _notificationsMessageHandler.SendMessageToAllAsync("Webm formatına dönüştürülüyor...");
            

            string inputVideoPath = Path.Combine(@"C:\Users\okeskiner\Documents\tras\3497.mp4");

            IMediaInfo mediaInfo = new MediaInfo(inputVideoPath);
            string output = $"Video fullName -> {mediaInfo.FileInfo.FullName}";

            string outputPathWebm = Path.ChangeExtension(mediaInfo.FileInfo.FullName, ".webm");
            //https://trac.ffmpeg.org/wiki/Encode/VP9

            //IConversion conversion = new Conversion().SetInput(inputVideoPath).AddParameter($"-c:v libvpx-vp9 -crf 30 -b:v 0").SetOutput(outputPathWebm);
            //IConversion conversion = new Conversion().SetInput(inputVideoPath).AddParameter($"-c:v libvpx-vp9 -lossless 1").SetOutput(outputPathWebm);
            IConversion conversion = new Conversion().SetInput(inputVideoPath).SetOutput(outputPathWebm);
            conversion.OnProgress += async (duration, totalLength) =>
            {
                var percent = (int)(Math.Round(duration.TotalSeconds / totalLength.TotalSeconds, 2) * 100);
          
                await _notificationsMessageHandler.SendMessageToAllAsync($"[{duration} / {totalLength}] {percent}%");
            };

          

            bool conversionResult = await conversion.Start();
            */


            ///* //3.Faz -> ftp yap
            //http://www.codingvision.net/networking/c-connecting-to-ftp-server

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://myFtpAddress.tld/myFile.txt");

            ftpRequest.Credentials = new NetworkCredential("username", "password");
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            byte[] fileContent;

            using (StreamReader sr = new StreamReader("myFile.txt"))
            {
                fileContent = Encoding.UTF8.GetBytes(sr.ReadToEnd());
            }

            using (Stream sw = ftpRequest.GetRequestStream())
            {
                sw.Write(fileContent, 0, fileContent.Length);
            }

            ftpRequest.GetResponse();

            /*  //4.Faz -> veritabanına yazdır

           
             await _notificationsMessageHandler.SendMessageToAllAsync("dosya path ve durum veritabanına yazdırılıyor.");
               List<string> lst = new List<string>();
               foreach (var item in _videoQueryRepository.GetAll())
               {
                   
                   //http://data.ekavart.tv/ekav/temp/mp4/2018/1/26/3497.mp4

                   lst.Add(item.Id + "-");
                   //lst.Add(item.Title + "-");
                   lst.Add("http://data.ekavart.tv/ekav" + item.Path);
                   lst.Add(item.LowPath);
                   lst.Add(item.Folder);

               }
           



            */

             /* //5.Faz ->  daha sonra ilgili klasörden dosyaları sil, burada dosyaları listeye ekle foreachle dön ve sil...
              if(File.Exists(@"C:\test.txt"))
                {
                    File.Delete(@"C:\test.txt");
                }
             */



            

            return lst; 
        }

   

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
