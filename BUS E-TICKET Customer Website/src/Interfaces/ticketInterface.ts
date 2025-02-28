export interface TicketDTO {
    ticketID: number;
    pnr: string;
    issueDate: Date;
    
    // Invoice Details
    invoiceNumber: string;
    invoiceIssueDate: Date;
    
    // Payment Details
    paymentMethod: string;
    orderID: string;
    paymentStatus: string;
    isRefundable: boolean;
    paymentDate: Date;
    tripAmount: string;
    vat: string;
    totalAmount: string;
    discountAmount: string;
    
    // Reservation Details
    reservationDate: Date;
    
    // Trip Details
    vehicleInfo: string;
    driverInfo: string;
    tripDate: Date;
    currency: string;
    tripDuration: string;
    fromCity: string;
    toCity: string;
    
    // Service Provider Details
    businessName: string;
    logoURL: string;
    
    // Passenger Details
    passengerName: string;
    passengerNationalID: string;
    passengerGender: string;
  }

export default TicketDTO; 