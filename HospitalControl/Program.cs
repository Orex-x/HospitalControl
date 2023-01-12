using EasyData.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using HospitalControl;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection")
                 ?? throw new ApplicationException("Can't find connection string");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection, builder =>
{
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
}), ServiceLifetime.Transient);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });

var app = builder.Build();

app.MapEasyData(options => {
    options.UseDbContext<AppDbContext>();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss"));
    }
}