using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using PressDot.Contracts.Response;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.RazorRenderer;
using PressDot.Framework.Attribute;
using PressDot.Framework.Middleware;
using PressDot.Registrar;

namespace PressDot
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
            services.AddControllers();
            services.AddMvc();
            services.RegisterDependencies(Configuration);
            services.AddScoped(typeof(IRazorPartialToStringRenderer), typeof(RazorPartialToStringRenderer));
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "PressDot.xml");
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddSwaggerDocument(config =>
            {
                config.OperationProcessors.Add(new AuthorizationHeaderParameterOperationProcessor());
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Pressdot ";
                    document.Info.Description = "Pressdot API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "Name",
                        Email = "Email ",
                        Url = "Url "
                    };
                    document.Info.License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };

                };
            }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");
                        if (exception is PressDotValidationException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = exception.Message
                            }.ToString());
                        }
                        else if (exception is PressDotNotFoundException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = exception.Message
                            }.ToString());
                        }
                        else if (exception is PressDotAlreadyExistException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = ((PressDotAlreadyExistException)exception).Code,
                                Message = exception.Message
                            }.ToString());
                        }
                        else if (exception is PressDotException)
                        {
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = exception.Message
                            }.ToString());
                        }
                        else if (exception is DbUpdateException)
                        {
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = exception.InnerException.Message
                            }.ToString());
                        }
                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = exception.Message
                            }.ToString());
                        }

                    }
                });

            });

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials()); // allow credentials
            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();
           
             app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}
