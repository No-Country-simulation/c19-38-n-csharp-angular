using c19_38_BackEnd.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            

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

            builder.Services.AddSwaggerGen(swaggerConfiguration=>
            {
                //Encabezado de la API
                swaggerConfiguration.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API REST Documentación",
                    Description = "API para el Front End Angular para la simulación de No Country",
                    Version = "v1"
                });

                //Configuración para añadir comentarios en XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerConfiguration.IncludeXmlComments(xmlPath);
            });


            var bindJwtSettings = new JwtSettings();
            //Obtiene la configuracion almacenada en appSettings.json de la key "JwtSettings":
            builder.Configuration.Bind("JwtSettings", bindJwtSettings);

            var key = Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey);

            //Añado la validacion del token jwt para cada request
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
