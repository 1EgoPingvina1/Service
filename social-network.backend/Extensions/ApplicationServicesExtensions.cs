using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using social_network.backend.Data;
using social_network.backend.Data.Repository;
using social_network.backend.Interfaces;
using social_network.backend.mongodb.settings;
using social_network.backend.Service;

namespace social_network.backend.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.Configure<MongodbSettings>(configuration.GetSection(nameof(MongodbSettings)));
            var mongoDBSettings = configuration.GetSection("MongoDBSettings").Get<MongodbSettings>();
            services.AddScoped<IMongoClient>(sp =>
            {
                return new MongoClient(mongoDBSettings.ConnectionString);
            });
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors();

            return services;
        }
    }
}
