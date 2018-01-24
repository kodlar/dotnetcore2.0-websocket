using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xabe.FFmpeg;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSocketDemo
{
    [Route("api/[controller]")]
    public class ConvertController : Controller
    {
        NotificationsMessageHandler _notificationsMessageHandler { get; set; }

        public ConvertController(NotificationsMessageHandler notificationsMessageHandler)
        {
            _notificationsMessageHandler = notificationsMessageHandler;
            FFbase.FFmpegDir = "/usr/local/bin";
        }

        

        [Route("Index")]
        [HttpGet]
        public async Task<IEnumerable<string>> Index()
        {

            //string testVideoPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Temp", "erhan.mp4");
            //"/Users/ogan/Projects/WebSocketDemo/WebSocketDemo/bin/Debug/netcoreapp2.0/Temp/erhan.mp4"
            string inputVideoPath = Path.Combine(System.Environment.CurrentDirectory, "Temp", "erhan.mp4");
           
            IMediaInfo mediaInfo = new MediaInfo(inputVideoPath);
            string output = $"Video fullName -> {mediaInfo.FileInfo.FullName}";
           
            string outputPathWebm = Path.ChangeExtension(mediaInfo.FileInfo.FullName, ".webm");
            IConversion conversion = new Conversion().SetInput(inputVideoPath).AddParameter($"-c:v libvpx-vp9 -crf 30 -b:v 0").SetOutput(outputPathWebm);

            conversion.OnProgress += async (duration, totalLength) =>
            {
                var percent = (int)(Math.Round(duration.TotalSeconds / totalLength.TotalSeconds, 2) * 100);
                //_logger.LogInformation($"[{e.Duration} / {e.TotalLength}] {percent}%");
                await _notificationsMessageHandler.SendMessageToAllAsync($"[{duration} / {totalLength}] {percent}%");
            };

            //conversion.OnDataReceived += (sender, args) => { ffmpegOuput += $"{args.Data}{Environment.NewLine}"; };
            /*
            conversion.OnDataReceived += async (sender, args) =>
            {
                await _notificationsMessageHandler.SendMessageToAllAsync($"{args.Data}");
            };
            */

            bool conversionResult = await conversion.Start();
           
            return new string[] { "Result", mediaInfo.Properties.AudioFormat,
                                            mediaInfo.Properties.VideoFormat,
                                            mediaInfo.FileInfo.FullName,
                                            mediaInfo.FileInfo.Directory.FullName,
                                            mediaInfo.FileInfo.Length.ToString(),
                                            mediaInfo.Properties.Duration.Minutes.ToString(), conversionResult.ToString()};



        }


        [Route("Liste")]
        [HttpGet]
        public IEnumerable<string> Liste()
        {
            string inputVideoPath = Path.Combine(System.Environment.CurrentDirectory, "Temp", "erhan.mp4");

            IMediaInfo mediaInfo = new MediaInfo(inputVideoPath);
            string output = $"Video fullName -> {mediaInfo.FileInfo.FullName}";

            string outputPathWebm = Path.ChangeExtension(mediaInfo.FileInfo.FullName, ".webm");
            string output1PathWebm = Path.ChangeExtension(mediaInfo.FileInfo.FullName, "1.webm");


            var lst = new List<string>();
            lst.Add(outputPathWebm);
            lst.Add(output1PathWebm);


            var queue = new ConversionQueue(parallel: false);
            for (int i = 0; i < lst.Count(); i++)
            {
                var currentItem = lst[i];
                IConversion con = new Conversion().SetInput(inputVideoPath).SetOutput(currentItem);
                con.OnProgress += async (duration, totalLength) =>
                {
                    var percent = (int)(Math.Round(duration.TotalSeconds / totalLength.TotalSeconds, 2) * 100);
                    //_logger.LogInformation($"[{e.Duration} / {e.TotalLength}] {percent}%");
                    await _notificationsMessageHandler.SendMessageToAllAsync($"[{duration} / {totalLength}] {percent}%");
                };
                string message = string.Empty;
                queue.OnException += async (number, count, conversion) =>
                {
                    //System.Console.Out.WriteLine($"Exception when converting file {number}/{count}");
                    message = $"Exception when converting file {number}/{count}";
                    await _notificationsMessageHandler.SendMessageToAllAsync(message);
                };

                queue.OnConverted += async (number, count, conversion) =>
                {
                    //System.Console.Out.WriteLine($"File {number}/{count} converted into {conversion.OutputFilePath}");
                    message = $"File {number}/{count} converted into {conversion.OutputFilePath}";
                    await _notificationsMessageHandler.SendMessageToAllAsync(message);
                };
                queue.Add(con);
            }

            /*
            IConversion conversion1 = new Conversion().SetInput(inputVideoPath).SetOutput(outputPathWebm);
            IConversion conversion2 = new Conversion().SetInput(inputVideoPath).AddParameter($"-c:v libvpx-vp9 -crf 30 -b:v 0").SetOutput(output1PathWebm);


            conversion1.OnProgress += async (duration, totalLength) =>
            {
                var percent = (int)(Math.Round(duration.TotalSeconds / totalLength.TotalSeconds, 2) * 100);
                //_logger.LogInformation($"[{e.Duration} / {e.TotalLength}] {percent}%");
                await _notificationsMessageHandler.SendMessageToAllAsync($"[{duration} / {totalLength}] {percent}%");
            };

            conversion2.OnProgress += async (duration, totalLength) =>
            {
                var percent = (int)(Math.Round(duration.TotalSeconds / totalLength.TotalSeconds, 2) * 100);
                //_logger.LogInformation($"[{e.Duration} / {e.TotalLength}] {percent}%");
                await _notificationsMessageHandler.SendMessageToAllAsync($"[{duration} / {totalLength}] {percent}%");
            };

            string message = string.Empty;
            queue.OnException += async (number, count, conversion) =>
            {
                //System.Console.Out.WriteLine($"Exception when converting file {number}/{count}");
                message = $"Exception when converting file {number}/{count}";
                await _notificationsMessageHandler.SendMessageToAllAsync(message);
            };

            queue.OnConverted += async (number, count, conversion) =>
            {
                //System.Console.Out.WriteLine($"File {number}/{count} converted into {conversion.OutputFilePath}");
                message = $"File {number}/{count} converted into {conversion.OutputFilePath}";
                await _notificationsMessageHandler.SendMessageToAllAsync(message);
            };
            queue.Add(conversion1);
            queue.Add(conversion2);
            */
            queue.Start();

            return new string[] { "Result", mediaInfo.Properties.AudioFormat,
                                            mediaInfo.Properties.VideoFormat,
                                            mediaInfo.FileInfo.FullName,
                                            mediaInfo.FileInfo.Directory.FullName,
                                            mediaInfo.FileInfo.Length.ToString(),
                mediaInfo.Properties.Duration.Minutes.ToString(), queue.ToString()};
        }
    }
}
