using System.Configuration;
using System.Reflection;
using Business.Services;
using GecisKontrol.DAL.Data;
using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using GecisKontrol.Domain.Model.IdentityModel;
using Microsoft.Extensions.DependencyInjection; // Add this line
using Microsoft.Extensions.Configuration; // Add this line
using Utilities.Extensions;
using GecisKontrol.Middleware;
using Microsoft.AspNetCore.Identity;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using GecisKontrol.Domain.DTOs.UserDTOs;
using GecisKontrol.Models.LoginModels;
using GecisKontrol.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GecisKontrol.Domain.Model.JWT;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<GecisKontrol.DAL.Data.DbContext>(provider =>
{
	var configuration = provider.GetRequiredService<IConfiguration>();
	return new GecisKontrol.DAL.Data.DbContext(configuration);
});



//builder.Services.AddScopedServicesFromAssembly(
//	assembly: typeof(IErrorLogService).Assembly, // Domain.Interfaces assembly'si
//	interfaceNamespacePrefix: "GecisKontrol.Domain.Interfaces",
//	implementationNamespacePrefix: "GecisKontrol.Business.Services"
//);

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDeviceEmployeeMappingRepository, DeviceEmployeeMappingRepository>();
builder.Services.AddScoped<IDeviceProfileRepository, DeviceProfileRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceRoleRepository, DeviceRoleRepository>();
builder.Services.AddScoped<IEmployeeCardMappingRepository, EmployeeCardMappingRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IGateRepository, GateRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeviceGateMappingRepository, DeviceGateMappingRepository>(); 
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IErrorLogService, ErrorLogService>();


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(
    builder.Configuration.GetConnectionString("RedisConnectionString")
));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"]
    };
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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
            new string[] {}
        }
    });
});


var app = builder.Build();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
await Utilities.Setup.CreateAdminAccount(services, builder.Configuration);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();

}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c => // Swagger UI'ý yapýlandýr
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Swagger UI'ýn eriþileceði yol (örneðin: https://localhost:5001/swagger)
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();