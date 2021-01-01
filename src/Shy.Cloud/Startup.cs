namespace Shy.Cloud
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.AzureAD.UI;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.DiscordBot.Repositories;

    public class Startup
    {
        private static string connStr;

        public Startup(IConfiguration configuration)
        {
            Log.Logger = Log.Logger.ForContext("Realm", "ShyCloud");
            Configuration = configuration;
            Log.Warning("Startup.");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Warning("ConfigureServices.");
            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //    .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();
            ////services.AddSwaggerGen(c =>
            ////{
            ////    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShybotCloud", Version = "v1" });
            ////});

            services.AddTransient<IExplainRepository>(f => new ExplainRepository());

            try
            {
                Log.Warning("Read config str.");
                Startup.connStr = Configuration.GetConnectionString("Cloud");
                Log.Warning("Make repos");

                //services.AddTransient<IChannelRepository>(f => new ChannelRepository(connStr));
                //services.AddTransient<IMessageRepository>(f => new MessageRepository(connStr));
                //services.AddTransient<IUserRepository>(f => new UserRepository(connStr));
                Log.Warning("Done.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get connstr?");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Warning("Configure.");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthentication();
            // app.UseAuthorization();
            Log.Warning("Prep endpoints.");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
