
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configuracion de Automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // Configuramos el NewtonsoftJson para la Referencia Ciclica sea Ignorada
            builder.Services.AddControllers().AddNewtonsoftJson( options => 
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
				//con esto se bloquea el uso del[ApiController]
                //quien es el que controla las validaciones por defecto
				//options.SuppressModelStateInvalidFilter = true;   
			});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //resolvemos la implementacion para saber con cual implementacion queremos utilizar
            // si ya no se quiere trabajar con el repositorio principal de SQL Server
            //builder.Services.AddTransient<IPostRepository, PostRepository>();
            // utilizamos el de Mongo DB

            //builder.Services.AddTransient<IPostRepository,PostMongoRepository>();

            // configuracion del Conection String
            var connectionString = builder.Configuration.GetConnectionString("SocialMedia");
            builder.Services.AddDbContext<SocialMediaContext>(x => x.UseSqlServer(connectionString));

            // configuracion del repositorio
            builder.Services.AddTransient<IPostRepository, PostRepository>();

            // se esta registro un filtro de forma global para la aplicacion
            builder.Services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

			//builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // register validators
        	//builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
			//builder.Services.AddFluentValidationClientsideAdapters(); // for client side
            		

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}