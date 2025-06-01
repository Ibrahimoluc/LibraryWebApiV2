using System.Text;
using LibraryWebApi.Models;
using LibraryWebApi.Repo.Abstract;
using LibraryWebApi.Repo.Concrete;
using LibraryWebApi.RoleServices;
using LibraryWebApi.Service.Abstract;
using LibraryWebApi.Service.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//swagger bearer token ekleme özelligi getirmek için, options eklendi
//Bu haliyle calistirmak icin, value kýmýna: Bearer <token>
//seklinde yapman lazim
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lowercase
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token only (without 'Bearer ' prefix)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


//jwt Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdlfkAMlk3kjasnd13123AFdasdASDmlak3q"))
    };
});


//authorization için
//builder.Services.AddIdentity<IdentityUser, IdentityRole>();

//adding and configuring db context
builder.Configuration.AddEnvironmentVariables(prefix: "LibraryWebApi_");

var connectionString = builder.Configuration.GetValue<string>("DefaultConnection");
Console.WriteLine("ConnectionString:" + connectionString);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

//adding dependecy injections, made by me
builder.Services.AddScoped<IRepo<Field>, Repo<Field>>();
builder.Services.AddScoped<IRepo<Author>, Repo<Author>>();
builder.Services.AddScoped<IRepo<User>, Repo<User>>();

builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IRoleHelper<UserRole>, RoleHelper>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//sonradan bir satir sunu ekledim
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
