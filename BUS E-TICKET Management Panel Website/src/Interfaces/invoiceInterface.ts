 interface InvoiceDTO {
    invoiceID: number;
    invoiceNumber: string;
    issueDate: Date;
    paymentMethod: string;
    orderID: string;
    paymentStatus: string;
    isRefundable: boolean;
    paymentDate: Date;
    vat: string;
    totalAmount: string;
    discountAmount: string;
    tripAmount: string;
    currency: string;
    businessName: string;
    logoURL: string;
  }
export default InvoiceDTO;