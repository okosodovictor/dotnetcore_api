using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using VacationSolution.Web.DTOS;
using VacationSolution.Web.Entities;
using VacationSolution.Web.Interfaces;
using VacationSolution.Web.Repositories;

namespace VacationSolution.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {

                config.ReturnHttpNotAcceptable = true;
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                config.InputFormatters.Add(new XmlSerializerInputFormatter());
            });

            services.AddEntityFrameworkSqlServer();
            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddScoped<IVacationRequestRepository, VacationRequestRepository>();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "My First .net Core Web Api",
                    Version = "v1"
                });
            });
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("resoucesAdmin", policyAdmi =>
            //    {

            //    });
            //    options.AddPolicy("resourceUser", policyUser =>
            //    {
            //        policyUser.RequireClaim("role", "resources.user");
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            env.ConfigureNLog("nlog.Config");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorFeature != null)
                        {
                            var logger = loggerfactory.CreateLogger("Global Exceptionn Logeer");
                            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);

                        }
                        await context.Response.WriteAsync("There was an error");
                    });
                });
            }

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = "https://demo.identityserver.io",
            //    ApiName = "api1",

            //AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});



            loggerfactory.AddConsole(Configuration.GetSection("Logging"));
            loggerfactory.AddDebug(LogLevel.Error);

            //Add NLOG
            loggerfactory.AddNLog();

            ///app.us
            //Add Nlog.Web
            app.AddNLogWeb();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "my first .net core web Api");
            });
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<VacationRequest, VacationRequestDTO>().ReverseMap();
            });

            //app.UseWelcomePage();
        }
    }
}
