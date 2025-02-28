import { CCol, CRow } from '@coreui/react';
import InvoiceDTO from 'src/Interfaces/invoiceInterface';

interface InvoiceTemplateProps {
  invoice: InvoiceDTO;
}

const InvoiceTemplate = ({ invoice }: InvoiceTemplateProps) => {
  return (
    <div className="invoice-container p-4">
      {/* Header Section */}
      <div className="invoice-header mb-4">
        <CRow className="align-items-center">
          <CCol md={6}>
            <div className="d-flex align-items-center">
              <img 
                src={invoice.logoURL} 
                alt={invoice.businessName} 
                className="company-logo me-3"
              />
              <div>
                <h2 className="mb-0">{invoice.businessName}</h2>
                <div className="text-muted small">Service Provider</div>
              </div>
            </div>
          </CCol>
          <CCol md={6} className="text-md-end">
            <div className="invoice-details">
              <h3 className="mb-1">Tax Invoice</h3>
              <div className="text-muted mb-1">Invoice #: {invoice.invoiceNumber}</div>
              <div className="text-muted">Issue Date: {new Date(invoice.issueDate).toLocaleDateString()}</div>
            </div>
          </CCol>
        </CRow>
      </div>

      {/* Main Content */}
      <div className="invoice-body rounded p-4 mb-4">
        <CRow className="g-4">
          <CCol md={6}>
            <div className="info-box h-100 bg-white rounded p-3 shadow-sm">
              <h5 className="border-bottom pb-2 mb-3">Payment Details</h5>
              <div className="d-flex justify-content-between mb-2">
                <div>
                  <div className="text-muted small">Payment Method</div>
                  <div>{invoice.paymentMethod}</div>
                </div>
                <div className="text-end">
                  <div className="text-muted small">Order ID</div>
                  <div>{invoice.orderID}</div>
                </div>
              </div>
              <div className="d-flex justify-content-between">
                <div>
                  <div className="text-muted small">Status</div>
                  <div className={`badge bg-${invoice.paymentStatus === 'Paid' ? 'success' : 'warning'}`}>
                    {invoice.paymentStatus}
                  </div>
                </div>
                <div className="text-end">
                  <div className="text-muted small">Payment Date</div>
                  <div>{new Date(invoice.paymentDate).toLocaleDateString()}</div>
                </div>
              </div>
            </div>
          </CCol>
        </CRow>
      </div>

      {/* Amount Summary */}
      <div className="amount-summary bg-opacity-10 rounded p-4">
        <h5 className="mb-4">Amount Summary</h5>
        <CRow className="g-3">
          <CCol xs={6} md={3}>
            <div className="amount-box text-center p-3 bg-white rounded shadow-sm">
              <div className="text-muted small">Trip Amount</div>
              <div className="h5 mb-0">{invoice.tripAmount} {invoice.currency}</div>
            </div>
          </CCol>
          <CCol xs={6} md={3}>
            <div className="amount-box text-center p-3 bg-white rounded shadow-sm">
              <div className="text-muted small">VAT</div>
              <div className="h5 mb-0">{invoice.vat}</div>
            </div>
          </CCol>
          <CCol xs={6} md={3}>
            <div className="amount-box text-center p-3 bg-white rounded shadow-sm">
              <div className="text-muted small">Discount</div>
              <div className="h5 mb-0">{invoice.discountAmount}</div>
            </div>
          </CCol>
          <CCol xs={6} md={3}>
            <div className="amount-box text-center p-3 bg-white rounded shadow-sm">
              <div className="text-muted small">Total Amount</div>
              <div className="h5 mb-0 text-primary">{invoice.totalAmount} {invoice.currency}</div>
            </div>
          </CCol>
        </CRow>
      </div>
    </div>
  );
};

export default InvoiceTemplate;