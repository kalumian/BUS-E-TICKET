using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Core_Layer;
using Microsoft.AspNetCore.Identity;
using Core_Layer.Entities.Actors;
using Business_Logic_Layer.Services;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.Interfaces.Actors_Interfaces;
using Business_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using BUS_E_TICKET.Middlewares;
using Core_Layer.DTOs;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Register Swagger services for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection With Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(DatabaseConnectionSettings.DatabaseStringConnection));


// Identity Service Initilization
builder.Services.AddIdentity<AuthoUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// AddScoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ManagerService>();
builder.Services.AddScoped<BaseUserService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<ContactInformationService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<UserManager<AuthoUser>>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Customer Middleware 
// ErrorHandler 
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
