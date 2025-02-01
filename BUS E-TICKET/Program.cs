using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Core_Layer.Entities.Actors;
using Business_Logic_Layer.Services;
using BUS_E_TICKET.Middlewares;
using Business_Logic_Layer.Services.Actors;
using Data_Access_Layer.UnitOfWork;
using Business_Logic_Layer.Services.Actors.ServiceProvider;
using BUS_E_TICKET.Extensions;
using Business_Logic_Layer.Services.Payment;
using Core_Layer.Configurations;
using Core_Layer.Interfaces.Payment;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Register Swagger services for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggarWithJtwConfig();

// Connection With Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(DatabaseConnectionSettings.DatabaseStringConnection),
    ServiceLifetime.Scoped);

// Identity Service Initilization
builder.Services.AddIdentity<AuthoUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// AddScoped
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<PayPalService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<SPRegResponseService>();
builder.Services.AddScoped<ManagerService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<ServiceProviderService>();
builder.Services.AddScoped<BaseUserService>();
builder.Services.AddScoped<BusinessService>();
builder.Services.AddScoped<SPRegRequestService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<ContactInformationService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<UserManager<AuthoUser>>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ReservationService>();
builder.Services.AddHostedService<ReservationBackgroundService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomJwtAuth(builder.Configuration);



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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
