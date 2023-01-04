using FormulaApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFormulaRepository, FormulaRepository>();
builder.Services.AddDbContext<FormulaDbContext>(o => o.UseInMemoryDatabase("FormulaDb"));

// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InJ2cyIsInN1YiI6InJ2cyIsImp0aSI6IjQ0OWRjZTk5IiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6MzAwMTkiLCJodHRwczovL2xvY2FsaG9zdDo0NDM3NiIsImh0dHA6Ly9sb2NhbGhvc3Q6NTA2NSIsImh0dHBzOi8vbG9jYWxob3N0OjcwMzYiXSwibmJmIjoxNjcyMzkyNTY0LCJleHAiOjE2ODAxNjg1NjQsImlhdCI6MTY3MjM5MjU2NiwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.n_jvfKqCTzUPRNtyBst2ucY36XLzgbtGFNK3Yi--4Ew
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new() { Title = "FormulaApi", Version = "v1" });
});

builder.Services.Configure<SwaggerGeneratorOptions>(o => 
{ 
    o.InferSecuritySchemes = true; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/random/number", [Authorize] (int from, int to) =>
{
    var random = new Random();
    return random.Next(from, to);
})
.WithName("random number")
.WithOpenApi(operation =>
{
    operation.Security = GetDefaultListOpenApiSecurityRequirement();
    return operation;
});

app.MapPost("/api/random/interval", [Authorize] (int from, int to) =>
{
    var random = new Random();
    var a = random.Next(from, to);
    var b = random.Next(from, to);
    return $"[{Math.Min(a, b)}; {Math.Max(a, b)}]";  
})
.WithName("random interval")
.WithOpenApi(operation =>
{
    operation.Security = GetDefaultListOpenApiSecurityRequirement();
    return operation;
});


app.MapPost("/api/random/order", [Authorize] (int from, int to, int count) =>
{
    var random = new Random();
    var arrray = new int[count];
    for(var i=0; i< count; i++)
    {
        arrray[i] = random.Next(from, to);
    }
    return random.Next(from, to);
})
.WithName("random order")
.WithOpenApi(operation =>
{
    operation.Security = GetDefaultListOpenApiSecurityRequirement();
    return operation;
});

app.Run();


static List<OpenApiSecurityRequirement> GetDefaultListOpenApiSecurityRequirement()
{
    return new List<OpenApiSecurityRequirement>
    {
        new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme,
                },
                Scheme = SecuritySchemeType.Http.ToString(),
                Name = JwtBearerDefaults.AuthenticationScheme,
            },
            new List<string>()
            }
        }
    };
}

