using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
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
using System.Reflection;
using System.IO;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyFeedback.Webapi.Models.Sessoes;

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

            // Adiciona o Identity
            services.AddIdentity<Usuario, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddControllers()
                    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
                    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            
            // Adiciona o Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFeedback", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Digite Bearer [espaço] e o token válido como o exemplo abaixo.\r\n\rExemplo: Bearer eyJhbGciOiJI..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                        new string[] {}
                    }
                });
            });

            // Adiciona Autenticação
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => 
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
                    };
                options.AutomaticRefreshInterval = TimeSpan.FromDays(1);
            });

            // Adiciona conexao com o banco
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetHerokuConnectionString()));

            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFuncaoService, FuncaoService>();

            // Adiciona AutoMapper
            services.AddAutoMapper(typeof(Startup));
        }

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
