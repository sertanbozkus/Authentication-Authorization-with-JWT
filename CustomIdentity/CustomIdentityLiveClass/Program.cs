using CustomIdentityLiveClass.Context;
using CustomIdentityLiveClass.Managers;
using CustomIdentityLiveClass.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {

            ValidateIssuer = true, // Issuer validationı yap.
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // appsettingsteki değer.
            ValidateAudience = true, // Audience validationı yap.
            ValidAudience = builder.Configuration["Jwt:Audience"], // appsettingsteki değer
            ValidateLifetime = true, // Geçerlilik zamanı validationı yap.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)) //  appsettingsteki key.
        



        };
    });

var cn = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(cn));

builder.Services.AddScoped<IUserService, UserManager>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



// Controllerlar isteği alır, Servis içerisindeki metoda gönderir. Servisler Db metotlarıyla ekleme yapar.