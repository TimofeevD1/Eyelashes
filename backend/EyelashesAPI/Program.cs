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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EyelashesAPI.Services;
using Microsoft.OpenApi.Models;

namespace EyelashesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string is missing in environment variables.");

            // 🔹 Регистрация сервисов
            builder.Services.AddSingleton<JwtService>();
            builder.Services.AddDataAccess(connectionString);
            builder.Services.AddBussinessLogic();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            // 🔹 Логирование
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // 🔹 Настройка Telegram-бота
            builder.Services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.Telegram));
            builder.Services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    var options = sp.GetRequiredService<IOptions<TelegramOptions>>().Value;
                    return new TelegramBotClient(options.Token, httpClient);
                });

            builder.Services.AddSingleton<ITelegramMessageService, TelegramMessageService>();
            builder.Services.AddHostedService<TelegramBotBackgroundService>();

            // 🔹 Настройка Swagger с поддержкой JWT-аутентификации
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EyelashesAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите **Bearer YOUR_TOKEN_HERE** для авторизации"
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
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // 🔹 Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // 🔹 Настройка аутентификации и авторизации
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization();

            AppConstants.Init();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            // 🔹 Применение миграций БД
            using (var scope = app.Services.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
                databaseInitializer.ApplyMigrationsAsync().GetAwaiter().GetResult();
            }

            // 🔹 Middleware
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
        }
    }
}
