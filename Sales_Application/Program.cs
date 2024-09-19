using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sales_Application.Exception;
using Sales_Application.Mappings;
using Sales_Application.Models;
using Sales_Application.Repository;
using Sales_Application.Repository.Concrete;
using Sales_Application.Repository.Contract;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Services Configuration

#region Auth Interface in Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "OAuth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
#endregion



#region Configuring Database Services
builder.Services.AddDbContext<VikasSprintContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection2")));
#endregion

#region Configuring Dependency Injection 
//Creates an instances each http request(AddScoped)
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<ITerritoryRepository, TerritoryRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<TokenRepository>();
#endregion

#region Automapper Injecting
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
#endregion

#region Configuring Authentication Services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
#endregion

#region Configuring Global Exception Services
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion

#region Logger Configuration
Log.Logger = new LoggerConfiguration().
    MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/Log-Data.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
#endregion

#region Configuring CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseSwagger();
app.UseSwaggerUI();
//MiddleWare Configuration

app.UseCors("AllowAll");

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();