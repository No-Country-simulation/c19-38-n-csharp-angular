using c19_38_BackEnd.Configuracion;
using c19_38_BackEnd.Datos;
using c19_38_BackEnd.Interfaces;
using c19_38_BackEnd.Modelos;
using c19_38_BackEnd.Repositorio;
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
        //Contraseña gmail: sNvsd9t=SV}hV!L

        //Somee
        //Usuario Somee: c19-38nocountry
        //Contraseña Somee: sNvsd9t=SV}hV!L


        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
 
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Añadir DbContext al contenedor de servicios
            builder.Services.AddDbContext<DefaultContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            // Configurar Identity para el manejo de usuarios y roles, tambien se configura para los requerimientos de la contraseña
            builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DefaultContext>();

            // Añadir servicios Scoped para el manejo de usuarios, inicio de sesión y roles.
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

            // Configurar JWT settings.
            var bindJwtSettings = new JwtSettings();
            //Obtiene la configuracion almacenada en appSettings.json de la key "JwtSettings":
            builder.Configuration.Bind("JwtSettings", bindJwtSettings);

            var key = Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey);

            //Añado la validacion del token jwt para cada request
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

            // Configurar políticas de autorización.
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Roles.Atleta, policy => policy.RequireRole(Roles.Atleta));
                options.AddPolicy(Roles.Entrenador, policy => policy.RequireRole(Roles.Entrenador));
            });

            // Configurar Swagger para la documentación de la API.
            builder.Services.AddSwaggerGen(swaggerConfiguration=>
            {
                //Encabezado de la API
                swaggerConfiguration.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API REST Documentación",
                    Description = "API para el Front End Angular para la simulación de No Country",
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

                //Configuración para añadir comentarios en XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerConfiguration.IncludeXmlComments(xmlPath);
            });

            // Añadir validaciones con FluentValidation.
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();


            // Configurar JWT settings.
            var cloudSettings = new CloudinarySettings();
            //Obtiene la configuracion almacenada en appSettings.json de la key "JwtSettings":
            builder.Configuration.Bind("CloudinarySettings", cloudSettings);

            //Añadiendo servicios al contenedor

            //Este es un ejemplo de como se añadiria un repositorio generico para la entidad Usuario (Funcional)
            builder.Services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
            builder.Services.AddSingleton(bindJwtSettings);
            builder.Services.AddSingleton(cloudSettings);

            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Crea los roles en caso de que no existan
            await CrearRoles(app);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("Cors policy for Front End Angular");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static async Task CrearRoles(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = { Roles.Entrenador, Roles.Atleta };
                IdentityResult roleResult;

                foreach (var rol in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(rol);
                    if (!roleExist)
                    {
                        // Crear los roles y guardarlos en la base de datos
                        roleResult = await roleManager.CreateAsync(new IdentityRole(rol));
                    }
                }
            }
        }
    }
}
