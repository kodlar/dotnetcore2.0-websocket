using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories.Video;
using Data.Provider.MsSql.Repositories.Video;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebSocketDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            this.Configuration = builder.Build();
        }

        //public IConfiguration Configuration { get; }
        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var videoQueryRepository = new VideoQueryRepository(Configuration);
            var videoCommandRepository = new VideoCommandRepository(Configuration);

            //services.AddSingleton<IConfiguration>(s => Configuration);
           
            services.AddSingleton<IVideoQueryRepository>(s => videoQueryRepository);
            services.AddSingleton<IVideoCommandRepository>(s => videoCommandRepository);

            services.AddWebSocketManager();
            services.AddMvc();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var wsOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(60),
                ReceiveBufferSize = 4 * 1024
            }; 
            app.UseWebSockets(wsOptions);
            app.MapWebSocketManager("/notification", serviceProvider.GetService<NotificationsMessageHandler>());
            app.UseMvc();

        }
    }
}
