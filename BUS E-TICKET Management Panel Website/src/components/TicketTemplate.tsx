import { CCol, CRow } from '@coreui/react';
import { TicketDTO } from 'src/Interfaces/ticketInterface';
// import './TicketTemplate.css';

interface TicketTemplateProps {
  ticket: TicketDTO;
}

const TicketTemplate = ({ ticket }: TicketTemplateProps) => {
  return (
    <div className="ticket-container p-4 ticket">
      {/* Header Section */}
      <div className="ticket-header mb-4 d-flex align-items-center justify-content-between">
        <div className="d-flex align-items-center">
          <img 
            src={ticket.logoURL} 
            alt={ticket.businessName} 
            className="company-logo me-3"
          />
          <div>
            <h2 className="mb-0">{ticket.businessName}</h2>
            <div className="text-muted">Bus E-Ticket</div>
          </div>
        </div>
        <div className="text-end">
          <div className="h4 mb-0">PNR: {ticket.pnr}</div>
          <small className="text-muted">Issue Date: {new Date(ticket.issueDate).toLocaleDateString()}</small>
        </div>
      </div>

      {/* Trip Details */}
      <CRow className="trip-details mb-4 gx-5">
        <CCol md={4} className="mb-3">
          <div className="route-info">
            <div className="text-muted mb-1">From</div>
            <div className="h4">{ticket.fromCity}</div>
            <div className="text-muted mb-1">To</div>
            <div className="h4">{ticket.toCity}</div>
          </div>
        </CCol>
        <CCol md={4} className="mb-3">
          <div className="time-info">
            <div className="text-muted mb-1">Departure</div>
            <div className="h5">{new Date(ticket.tripDate).toLocaleString()}</div>
            <div className="text-muted mb-1">Duration</div>
            <div className="h5">{ticket.tripDuration}</div>
          </div>
        </CCol>
        <CCol md={4} className="mb-3">
          <div className="vehicle-info">
            <div className="text-muted mb-1">Vehicle</div>
            <div className="h5">{ticket.vehicleInfo}</div>
            <div className="text-muted mb-1">Driver</div>
            <div className="h5">{ticket.driverInfo}</div>
          </div>
        </CCol>
      </CRow>

      {/* Passenger Details */}
      <div className="passenger-details mb-4 p-3 bg-light rounded">
        <h4 className="mb-3">Passenger Information</h4>
        <CRow>
          <CCol md={4}>
            <div className="text-muted mb-1">Name</div>
            <div className="h5">{ticket.passengerName}</div>
          </CCol>
          <CCol md={4}>
            <div className="text-muted mb-1">National ID</div>
            <div className="h5">{ticket.passengerNationalID}</div>
          </CCol>
          <CCol md={4}>
            <div className="text-muted mb-1">Gender</div>
            <div className="h5">{ticket.passengerGender}</div>
          </CCol>
        </CRow>
      </div>

      {/* Payment Details */}
      <div className="payment-details p-3 bg-light rounded">
        <h4 className="mb-3">Payment Information</h4>
        <CRow>
          <CCol md={3}>
            <div className="text-muted mb-1">Amount</div>
            <div className="h5">
              {ticket.tripAmount} {ticket.currency}
            </div>
          </CCol>
          <CCol md={3}>
            <div className="text-muted mb-1">VAT</div>
            <div className="h5">{ticket.vat}</div>
          </CCol>
          <CCol md={3}>
            <div className="text-muted mb-1">Discount</div>
            <div className="h5">{ticket.discountAmount}</div>
          </CCol>
          <CCol md={3}>
            <div className="text-muted mb-1">Total</div>
            <div className="h5 text-primary">
              {ticket.totalAmount} {ticket.currency}
            </div>
          </CCol>
        </CRow>
      </div>
    </div>
  );
};

export default TicketTemplate;