using EncurtadorURL.Config;
using EncurtadorURL.Repositories;
using EncurtadorURL.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddOptions<MongoDbSettings>()
    .Bind(builder.Configuration.GetSection("MongoDbSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); //Força a validação no momento que o app inicia

builder.Services.AddSingleton<ShortUrlRepository>();
builder.Services.AddScoped<ShortUrlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    app.UseStatusCodePagesWithRedirects("/Error/NotFound");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.MapStaticAssets();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shortener}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
