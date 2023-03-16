using Serilog;
using System.Reflection;
using CliFx.Infrastructure;
using ICommand = CliFx.ICommand;
using TicTacToeAPI.Infrastructure.DataBase;
using TicTacToeAPI.Core.Mappings;
using TicTacToeAPI.Core.Interfaces;
using TicTacToeAPI.Core;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using CliFx.Attributes;
using DrfLikePaginations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace TicTacToeAPI.Presentation
{
    public class ApiCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console)
        {
            Log.Information("Initializing API...");
            // We create our host and run our web api!
            await Program.CreateHostBuilder(Array.Empty<string>()).Build().RunAsync();
        }

        public class Startup
        {
            public IConfiguration Configuration { get; }

            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=TicTacToeAPI.Players.db"));
                services.AddControllersWithViews();

                services.AddAutoMapper(config =>
                {
                    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                    config.AddProfile(new AssemblyMappingProfile(typeof(IAppDbContext).Assembly));
                });

                services.AddApplication();
                services.AddPersistence(Configuration);
                services.AddControllers();

                services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                    });
                });

                services.AddVersionedApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
                        ConfigureSwaggerOptions>();
                services.AddSwaggerGen();
                services.AddApiVersioning(opt =>
                {
                    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                    opt.AssumeDefaultVersionWhenUnspecified = true;
                    opt.ReportApiVersions = true;
                    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                    new HeaderApiVersionReader("x-api-version"),
                                                                    new MediaTypeApiVersionReader("x-api-version"));
                });
                services.AddHttpContextAccessor();
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        config.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                        config.RoutePrefix = string.Empty;
                    }
                });

                app.UseSerilogRequestLogging();
                app.UseRouting();
                app.UseCors("AllowAll");
                app.UseAuthorization();
                app.UseApiVersioning();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                Log.Information("'Configure' step is done! Ready to go!");
            }
        }
    }
}
