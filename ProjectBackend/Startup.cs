using DATOS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Negocio.Class;
using Negocio.Services;
using Negocio.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

namespace ProjectBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection Services)
        {
            Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            Services.AddDbContext<ApplicationDBContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));
                });
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
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
                        }, new string[]{ }
                    }
                });

            });

            //config
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["jwtKey"])),
                    ClockSkew = TimeSpan.Zero
                });

            Services.AddAutoMapper(typeof(AutoMapperProfiles));

            //config Identity
            Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            Services.AddAuthorization(x =>
            {
                x.AddPolicy("Admin", x => x.RequireClaim("admin"));
            });

            Services.AddScoped<ILibro, LibroClass>();
            Services.AddScoped<IAutor, AutorClass>();
            Services.AddScoped<IComentario, ComentarioClass>();
            Services.AddScoped<IEditorial, EditorialClass>();
            Services.AddScoped<IGuardado, GuardadoClass>();
            Services.AddScoped<IAuth, AuthClass>();
            Services.AddTransient<IFileStorage, FileStorageClass>();

            Services.AddCors(x =>
            {
                x.AddDefaultPolicy(y =>
                {
                    y.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                    //.WithExposeHeader() es para exponer headers
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(x => { x.MapControllers(); });
        }
    }
}
