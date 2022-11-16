using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using SimpleApiWithAzureVault.Models;
using SimpleApiWithAzureVault.Services;

namespace SimpleApiWithAzureVault
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? keyVaultUri = Environment.GetEnvironmentVariable("VaultUri");
            if(keyVaultUri is not null)
            {
                var keyVaultEndpoint = new Uri(keyVaultUri);
                var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

                builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
            }

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IWeatherService, WeatherService>();
            builder.Services.AddOptions<WeatherEndpointOptions>()
                .BindConfiguration("WeatherEndpointOptions");
            
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