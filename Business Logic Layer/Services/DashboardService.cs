using Business_Logic_Layer.Services;
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

    public class DashboardService(IUnitOfWork unitOfWork, UserManager<AuthoUser> userManager) : GeneralService(unitOfWork)
    {
        public DashboardStatsDto GetDashboardStats()
        {
            try
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
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }
    }
}