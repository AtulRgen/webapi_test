using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TestCoreWebApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using System.Web.Http;
using Microsoft.AspNetCore.Http;


namespace TestCoreWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }



            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);



            var connection = @"Data Source = 192.168.0.183; Initial Catalog = test; Integrated Security = True; Persist Security Info = True; User ID = sa; Password = ROOT#123;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddDbContext<Employee>(options => options.UseSqlServer(connection));
            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });



            //[! for Json Indented]
            /*
            var mvcCore = services.AddMvcCore();
            mvcCore.AddJsonFormatters(options => options.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddMvcCore().AddCors().AddJsonFormatters();
            services.Configure<MvcOptions>(options => { options.Filters.Add(new CorsAuthorizationFilterFactory("*")); });
            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });
            */


            //[! Tesing Purpose]
            /*
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:1025", "http://api-testatul.cloudapps.click2cloud.org","http://front-testatul.cloudapps.click2cloud.org"));
            });*/
            

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseStaticFiles();

            //[! CORS]
            /*
            app.UseCors
                (
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                );
                */
            //app.UseCors("AllowSpecificOrigin");
            app.Use(async (context, next) => { context.Response.Headers.Append("Access-Control-Allow-Origin", "*"); await next(); });
            app.UseMvcWithDefaultRoute();
         




        }
    }
}
