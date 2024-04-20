using social_network.backend.Extensions;

namespace social_network.backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerService();
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityService(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors(builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000"));
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
