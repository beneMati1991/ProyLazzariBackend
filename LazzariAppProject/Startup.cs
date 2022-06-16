using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DataAccess;
using Repositories;
using Entities;
using Core.Business.Interfaces;
using Core.Business;
using Abstractions;
using AutoMapper;
using Core.Mapper;
using FluentValidation;
using Core.Validator;
using Core.Helper;
using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Core.Helper.Paginacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Services;
using Services.NormalizacionDatosGeofApi;
using Core.Helper.Autenticacion;

namespace LazzariAppProject
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
           services.AddCors();

           services.AddControllers().AddNewtonsoftJson(options =>
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LazzariAppProject", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);
            });

            // services.AddEntityFrameworkSqlServer();
            services.AddDbContext<ApiDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("LazzariAppProject")));

            // Servicios Business/Lógica
            services.AddTransient<IComercioBusiness, ComercioBusiness>();
            services.AddTransient<IConsumidorBusiness, ConsumidorBusiness>();
            services.AddTransient<IListaCompraBusiness, ListaCompraBusiness>();
            services.AddTransient<IProductoBusiness, ProductoBusiness>();
            services.AddTransient<IRolBusiness, RolBusiness>();
            services.AddTransient<IUnidadDeMedidaBusiness, UnidadDeMedidaBusiness>();
            services.AddTransient<IUsuarioBusiness, UsuarioBusiness>();
            services.AddTransient<IProductoBusiness, ProductoBusiness>();

            //Api Normalización de direcciones
            services.AddTransient<INormalizacionDatosGeograficos, NormalizacionDatosGeograficosService>();

            // Repositorios / Data Context
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IDbContext, DataAccess.DbContext>();

            // Mapper
            services.AddAutoMapper(typeof(ApiMappings));
            services.AddTransient<IUsuarioMapper, UsuarioMapper>();

            //  Validaciones.
            services.AddScoped<IValidator<Producto>, ProductoValidator>();

            // Auth
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddTransient<ITokenHandler, Core.Helper.Autenticacion.TokenHandler>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true

                };
            });

            //Paginacion
            services.AddTransient<IPaginationFilter, PaginationFilter>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LazzariAppProject v1"));
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
