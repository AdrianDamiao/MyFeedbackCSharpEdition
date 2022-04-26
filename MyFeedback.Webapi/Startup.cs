using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyFeedback.Webapi.Middlewares;
using MyFeedback.Webapi.Services.Areas;
using MyFeedback.Webapi.Services.Colaboradores;
using MyFeedback.Webapi.Services.Empresas;
using MyFeedback.Webapi.Services.Feedbacks;
using MyFeedback.Webapi.Services.Funcoes;
using MyFeedback.Webapi.ExtensionMethods;
using MyFeedback.Webapi.Validacao;
using MyFeedback.Webapi.DTOs.Areas;

namespace MyFeedback.Webapi
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

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFeedback", Version = "v1" });
            });
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetHerokuConnectionString()));

            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFuncaoService, FuncaoService>();

            services.AddTransient<IValidator<CriaAreaInputDTO>, CriaAreaBindingModelValidador>();

            // Adiciona AutoMapper
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFeedback v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            if(env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFeedback Production v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
