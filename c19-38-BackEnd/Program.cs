using c19_38_BackEnd.Configuracion;
using c19_38_BackEnd.Datos;
using c19_38_BackEnd.Modelos;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text;
namespace c19_38_BackEnd
{
    public class Program
    {
        //Corre gmail: c1938nocountry@gmail.com
        //Contrase�a gmail: sNvsd9t=SV}hV!L

        //Somee
        //Usuario Somee: c19-38nocountry
        //Contrase�a Somee: sNvsd9t=SV}hV!L


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //A�adir DbContext al contenedor de servicios
            builder.Services.AddDbContext<DefaultContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });


            builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DefaultContext>();

            builder.Services.AddScoped<UserManager<Usuario>>();
            builder.Services.AddScoped<SignInManager<Usuario>>();
            builder.Services.AddScoped<RoleManager<IdentityRole<int>>>();

            //Cors generico (temporalmente) para el consumo en el front, proximamente se reconfigurara especificamente para el proyecto en despliegue de Angular
            builder.Services.AddCors(corsConfiguration =>
            {
                corsConfiguration.AddPolicy("Cors policy for Front End Angular", configurePolicy =>
                {
                    configurePolicy.AllowAnyOrigin();
                    configurePolicy.AllowAnyMethod();
                    configurePolicy.AllowAnyHeader();
                });
            });

            var bindJwtSettings = new JwtSettings();
            //Obtiene la configuracion almacenada en appSettings.json de la key "JwtSettings":
            builder.Configuration.Bind("JwtSettings", bindJwtSettings);


            builder.Services.AddSingleton(bindJwtSettings);

            var key = Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey);

            //A�ado la validacion del token jwt para cada request
            builder.Services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = bindJwtSettings.ValidateIssuer,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        RequireExpirationTime = bindJwtSettings.RequiredExpirationTime,
                        ValidateLifetime = bindJwtSettings.ValidateLifeTime
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Atleta", policy => policy.RequireRole("Atleta"));
                options.AddPolicy("Entrenador", policy => policy.RequireRole("Entrenador"));
            });

            builder.Services.AddSwaggerGen(swaggerConfiguration=>
            {
                //Encabezado de la API
                swaggerConfiguration.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API REST Documentaci�n",
                    Description = "API para el Front End Angular para la simulaci�n de No Country",
                    Version = "v1"
                });

                swaggerConfiguration.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autorizacion JWT en Header usando el esquema Bearer"
                });

                swaggerConfiguration.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },new string[]{ }
                    } 
                });

                //Configuraci�n para a�adir comentarios en XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerConfiguration.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("Cors policy for Front End Angular");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
