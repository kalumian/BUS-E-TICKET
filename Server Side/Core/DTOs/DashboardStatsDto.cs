using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalTicketsSold { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveProviders { get; set; }
        public int ScheduledTrips { get; set; }
        public int CancelledBookings { get; set; }
        public int TotalUsers { get; set; }
        public decimal AverageRevenuePerTrip { get; set; }
        public int PendingBookings { get; set; }
    }
}
