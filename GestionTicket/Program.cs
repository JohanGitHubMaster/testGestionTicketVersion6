using GestionTicket;
using GestionTicket.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<TicketManageContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TicketConnection")));

builder.Services.AddInfrastructure();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
   options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
   {
      Scheme = "Bearer",
      BearerFormat = "JWT",
      In = ParameterLocation.Header,
      Name = "Authorization",
      Description = "Bearer Authentification with JWT Token",
      Type = SecuritySchemeType.Http
   });
   options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidateIssuer = false,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MaCleSecreteHS512MaCleSecreteHS512MaCleSecreteHS512MaCleSecreteHS512"))

       };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
