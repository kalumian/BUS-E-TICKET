using Core_Layer.Entities.Trip.Reservation;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.Exceptions;

namespace Business_Logic_Layer.Services
{
    public class TicketService(IUnitOfWork unitOfWork) : GeneralService(unitOfWork)
    {
        public async Task<TicketEntity> CreateTicketAsync(InvoiceEntity invoice)
        {
            _ = await _unitOfWork.Invoices.GetByIdAsync(invoice.InvoiceID) ?? throw new NotFoundException("Invoice not found.");

            var ticket = new TicketEntity
            {
                PNR = Guid.NewGuid().ToString()[..10],  
                IssueDate = DateTime.Now,
                Invoice = invoice,
                InvoiceID = invoice.InvoiceID
            };

            await CreateEntityAsync(ticket, saveChanges: true);
            return ticket;
        }
    }
}
