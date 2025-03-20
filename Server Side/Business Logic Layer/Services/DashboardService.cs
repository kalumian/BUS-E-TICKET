using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Orders;


namespace Business_Logic_Layer.Services
{

    public class DashboardService(IUnitOfWork unitOfWork, UserManager<AuthoUser> userManager, UserService userService) : GeneralService(unitOfWork)
    {
        private readonly UserService _userService = userService;
        public DashboardStatsDto GetDashboardStats()
        {
            
            if(_userService.GetCurrentUserRole() == EnUserRole.Provider)
            {
                return GetDashboardStatsForProvider();
            }
            else
            {
                return GetDashboardStatsForManager();
            }
     
        
        }
        public DashboardStatsDto GetDashboardStatsForProvider()
        {
            var ProviderID = _userService.GetCurrentUserID();
            var totalTicketsSold = unitOfWork.Tickets.GetAllQueryable()
             .Include(i => i.Invoice)
             .ThenInclude(i => i.Payment)
             .ThenInclude(i => i.Reservation)
             .ThenInclude(i => i.Trip)
             .ThenInclude(i => i.ServiceProvider)
             .Where(i => i.Invoice.Payment.Reservation.Trip.ServiceProvider.AccountID == ProviderID)
             .Count();

            var totalRevenue = unitOfWork.Payments.GetAllQueryable()
                .Where(p => p.Reservation.Trip.ServiceProvider.AccountID == ProviderID)
                .Sum(p => (decimal?)p.TotalAmount) ?? 0;  // تجنب الخطأ في حال عدم وجود بيانات

            var activeProviders = unitOfWork.ServiceProviders.GetAllQueryable()
                .Include(s => s.Account)
                .Where(p => p.Account.AccountStatus == EnAccountStatus.Active && p.AccountID == ProviderID)
                .Count();

            var scheduledTrips = unitOfWork.Trips.GetAllQueryable()
                .Where(t => t.TripStatus == EnTripStatus.Completed && t.ServiceProvider.AccountID == ProviderID)
                .Count();

            var cancelledBookings = unitOfWork.Reservations.GetAllQueryable()
                .Where(b => b.ReservationStatus == EnReservationStatus.Failed && b.Trip.ServiceProvider.AccountID == ProviderID)
                .Count();

            var totalUsers = userManager.Users
                .Where(u => u.Id == ProviderID) // التأكد من احتساب المستخدمين الخاصين بمزود الخدمة
                .Count();

            var averageRevenuePerTrip = scheduledTrips > 0 ? totalRevenue / scheduledTrips : 0;

            var pendingBookings = unitOfWork.Reservations.GetAllQueryable()
                .Where(b => b.ReservationStatus == EnReservationStatus.Pending && b.Trip.ServiceProvider.AccountID == ProviderID)
                .Count();

            return new DashboardStatsDto
            {
                TotalTicketsSold = totalTicketsSold,
                TotalRevenue = totalRevenue,
                ActiveProviders = activeProviders,
                ScheduledTrips = scheduledTrips,
                CancelledBookings = cancelledBookings,
                TotalUsers = totalUsers,
                AverageRevenuePerTrip = averageRevenuePerTrip,
                PendingBookings = pendingBookings,
            };
        }
        public DashboardStatsDto GetDashboardStatsForManager()
        {
            var totalTicketsSold = unitOfWork.Tickets.Count();
            var totalRevenue = unitOfWork.Payments.GetAll().Sum(p => p.TotalAmount);
            var activeProviders = unitOfWork.ServiceProviders.GetAllQueryable().Include(s => s.Account).Count(p => p.Account.AccountStatus == EnAccountStatus.Active);
            var scheduledTrips = unitOfWork.Trips.GetAllQueryable().Count(t => t.TripStatus == EnTripStatus.Completed);
            var cancelledBookings = unitOfWork.Reservations.GetAllQueryable().Count(b => b.ReservationStatus == EnReservationStatus.Failed);
            var totalUsers = userManager.Users.Count();
            var averageRevenuePerTrip = scheduledTrips > 0 ? totalRevenue / scheduledTrips : 0;
            var pendingBookings = unitOfWork.Reservations.GetAllQueryable().Count(b => b.ReservationStatus == EnReservationStatus.Pending);


            return new DashboardStatsDto
            {
                TotalTicketsSold = totalTicketsSold,
                TotalRevenue = totalRevenue,
                ActiveProviders = activeProviders,
                ScheduledTrips = scheduledTrips,
                CancelledBookings = cancelledBookings,
                TotalUsers = totalUsers,
                AverageRevenuePerTrip = averageRevenuePerTrip,
                PendingBookings = pendingBookings,
            };
        }
    }
}