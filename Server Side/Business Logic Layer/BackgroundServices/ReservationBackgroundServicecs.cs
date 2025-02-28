using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.Entities;
using Core_Layer.Enums;
using Microsoft.EntityFrameworkCore;

public class ReservationBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessPendingReservationsAsync();
            await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken); 
        }
    }

    private async Task ProcessPendingReservationsAsync()
    {
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var currentTime = DateTime.UtcNow;
            var reservation = await unitOfWork.Reservations
                .FirstOrDefaultAsync(r => r.ReservationStatus == EnReservationStatus.Pending
                                         && r.ReservationDate <= currentTime.AddMinutes(-3));
            if (reservation != null)
            {
                // تحديث حالة الحجز إلى "فايد"
                reservation.ReservationStatus = EnReservationStatus.Failed;
                var payment = await unitOfWork.Payments
                    .FirstOrDefaultAsync(p => p.ReservationID == reservation.ReservationID && p.PaymentStatus == EnPaymentStatus.Pending);
                if (payment != null)
                    payment.PaymentStatus = EnPaymentStatus.Failed;
                
                await unitOfWork.SaveChangesAsync();
                Console.WriteLine($"Reservation {reservation.ReservationID} status changed to Failed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ProcessPendingReservationsAsync: {ex}");
        }
    }
}
