#define SQLServer // Redis
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


if (builder.Environment.IsDevelopment())
{
    #region snippet_AddDistributedMemoryCache
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSingleton<IDistributedCache, MemoryDistributedCache>();
    #endregion
}
else
{
#if SQLServer
    #region snippet_AddDistributedSqlServerCache
    builder.Services.AddDistributedSqlServerCache(options =>
    {
        options.ConnectionString = builder.Configuration.GetConnectionString("DistCache_ConnectionString");
        options.SchemaName = "dbo";
        options.TableName = "TestCache";
    });
    builder.Services.AddSingleton<IDistributedCache, SqlServerCache>();
    #endregion
#else
    #region snippet_AddStackExchangeRedisCache
                builder.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
                    options.InstanceName = "SampleInstance";
                });
                builder.Services.AddSingleton<IDistributedCache, RedisCache>();
    #endregion
#endif
}


var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{
    var currentTimeUTC = DateTime.UtcNow.ToString();
    byte[] encodedCurrentTimeUTC = System.Text.Encoding.UTF8.GetBytes(currentTimeUTC);
    var options = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(20));
    app.Services.GetService<IDistributedCache>().Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
