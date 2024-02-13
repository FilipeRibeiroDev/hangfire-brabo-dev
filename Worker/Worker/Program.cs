using Hangfire;
using Hangfire.Console;
using Hangfire.Redis.StackExchange;
using StackExchange.Redis;
using Worker.Configuration;
using Worker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(options =>
{
    var connectionString = builder.Configuration.GetValue<string>("RedisConnection");
    var redis = ConnectionMultiplexer.Connect(connectionString);

    options.UseRedisStorage(redis, options: new RedisStorageOptions { Prefix = $"HANG_FIRE" });
    options.UseConsole();
});
builder.Services.AddHangfireServer();
builder.Services.AddHostedService<MonitorService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = HangFireDashboard.AuthAuthorizationFilters()
});

app.Run();
