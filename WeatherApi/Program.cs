using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Authentication;
using WeatherApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
});

builder.Services.AddHttpClient();

// Configure AppSettings using Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

 
//builder.WebHost.UseKestrel(options =>
//{
    
//    options.ConfigureHttpsDefaults(httpsOptions =>
//    {
//        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
//    });
   

//});
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, 7177); // HTTP
    //options.Listen(IPAddress.Any, 443, listenOptions => // HTTPS
    //{
    //    listenOptions.UseHttps("path/to/your/ssl/certificate.pfx", "certificatePassword");
    //});
});
var app = builder.Build();
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));
}


//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
