using DataAccess;
using BussinessLogic;
using Microsoft.Extensions.Configuration;
using EyelashesAPI.Configuration;
using EyelashesAPI.TelegramBot.Options;
using EyelashesAPI.TelegramBot;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Microsoft.Extensions.Logging;
using DataAccess.Interfaces;

namespace EyelashesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string is not set in environment variables.");


            // Add services to the container.
            builder.Services.AddHostedService<TelegramBotBackgroundService>();
            builder.Services.Configure<TelegramOptions>(builder.Configuration.GetSection(TelegramOptions.Telegram));
            builder.Services.AddHttpClient("telegram_bot_client")
                            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                            {
                                var options = sp.GetRequiredService<IOptions<TelegramOptions>>().Value;
                                return new TelegramBotClient(options.Token, httpClient);
                            });
            builder.Services.AddSingleton<ITelegramMessageService, TelegramMessageService>();
            builder.Services.AddDataAccess(connectionString);
            builder.Services.AddHttpClient();
            builder.Services.AddBussinessLogic();
            builder.Services.AddControllers();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Разрешаем запросы с любого источника
                          .AllowAnyHeader()  // Разрешаем все заголовки
                          .AllowAnyMethod(); // Разрешаем все HTTP методы (GET, POST и т.д.)
                });
            });

            AppConstants.Init();

            var app = builder.Build();

       
            using (var scope = app.Services.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
                databaseInitializer.ApplyMigrationsAsync().GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
