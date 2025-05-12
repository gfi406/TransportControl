
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Charting;
using System.Text.Json.Serialization;
using TransportControl.Service;
using TransportControl.Service.Impl;
using TransportControl.Service.Documentation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.DocInclusionPredicate((docName, apiDesc) => true);
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ICarService,CarService>();
builder.Services.AddScoped<IDriverService,DriverService>();
builder.Services.AddScoped<ITrackListService,TrackListService>();
builder.Services.AddScoped<ITrackPointService,TrackPointService>();
builder.Services.AddScoped<ITrackListDocumentationService, TrackListDocumentationService>();
builder.Services.AddScoped<TrackListDocumentation>();
builder.Services.AddScoped<TrackListExcelGenerator>();
builder.Services.AddScoped<TrackListDocumentationGenerator>();


var app = builder.Build();
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.Migrate();
        dbContext.Database.CanConnect();
        Console.WriteLine("✅ Database connection is OK!");
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database Error: {ex.Message}");
        throw new Exception("Connection error");
    }
}

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Notes API");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();
app.Run();
