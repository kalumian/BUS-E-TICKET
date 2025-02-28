import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  CButton, 
  CCard, 
  CCardBody, 
  CCol, 
  CContainer, 
  CForm, 
  CFormInput, 
  CInputGroup, 
  CRow,
  CCardHeader,
  CCardFooter
} from '@coreui/react';
import { BookingDTO } from 'src/Interfaces/bookingInterface';
import { getBooking } from 'src/Services/bookingService';
import { fetchData } from 'src/Services/apiService';
import P_Error from 'src/components/P_Error';

const SearchBooking = () => {
  const navigate = useNavigate();
  const [pnr, setPnr] = useState('');
  const [Booking, setBooking] = useState<BookingDTO | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (!pnr.trim()) return;

    setBooking(null);
    setError('');
    fetchData(() => getBooking(pnr), setBooking, setError, setLoading);
  };

  return (
    <CContainer className="mt-4">
      <CRow>
        <CCol xs={12}>
          <CCard className="shadow-sm mb-4">
            <CCardHeader className="bg-light">
              <h4 className="mb-0">Search Your Booking</h4>
            </CCardHeader>
            <CCardBody className="p-4">
              <CRow className="justify-content-center">
                <CCol md={8} lg={6}>
                  <p className="text-muted text-center mb-4">
                    Enter your PNR (Passenger Name Record) number to view your booking details
                  </p>
                  <CForm onSubmit={handleSearch}>
                    <CInputGroup size="lg">
                      <CFormInput
                        type="text"
                        value={pnr}
                        onChange={(e) => setPnr(e.target.value)}
                        placeholder="Enter PNR number..."
                        required
                        disabled={loading}
                      />
                      <CButton 
                        type="submit" 
                        color="primary"
                        disabled={loading || !pnr.trim()}
                      >
                        {loading ? (
                          <>
                            <span className="spinner-border spinner-border-sm me-2" />
                            Searching...
                          </>
                        ) : (
                          <>
                            <i className="fas fa-search me-2"></i>
                            Search
                          </>
                        )}
                      </CButton>
                    </CInputGroup>
                  </CForm>
                </CCol>
              </CRow>
            </CCardBody>
          </CCard>

          {error && <P_Error text={error} />}

          {/* Result Card */}
          {Booking && (
            <CCard className="booking-card shadow-sm">
              <CCardHeader className="bg-light d-flex justify-content-between align-items-center">
                <div>
                  <h5 className="mb-0">Booking #{Booking.bookingID}</h5>
                </div>
                <span className={`badge bg-${Booking.bookingStatus === 'Active' ? 'success' : 'secondary'}`}>
                  {Booking.bookingStatus}
                </span>
              </CCardHeader>
              
              <CCardBody className="p-4">
                <CRow>
                  <CCol md={8}>
                    <div className="d-flex mb-4">
                      <div className="route-indicator position-relative me-4">
                        <div className="rounded-circle bg-primary" style={{ width: '12px', height: '12px' }} />
                        <div className="position-absolute" 
                          style={{ left: '5px', top: '12px', height: '30px', width: '2px', background: 'var(--cui-primary)' }} />
                        <div className="rounded-circle bg-primary" style={{ width: '12px', height: '12px', marginTop: '30px' }} />
                      </div>
                      <div className="flex-grow-1">
                        <div className="mb-3">
                          <div className="text-muted small">From</div>
                          <div className="h5 mb-0">{Booking.fromCity}</div>
                        </div>
                        <div>
                          <div className="text-muted small">To</div>
                          <div className="h5 mb-0">{Booking.toCity}</div>
                        </div>
                      </div>
                      <div className="text-end ms-4">
                        <div className="text-muted small">Trip Date</div>
                        <div className="mb-2">{new Date(Booking.tripDate).toLocaleDateString()}</div>
                        <div className="text-muted small">Time</div>
                        <div>{new Date(Booking.tripDate).toLocaleTimeString()}</div>
                      </div>
                    </div>
                  </CCol>
                  <CCol md={4} className="border-start">
                    <div className="h-100 d-flex flex-column justify-content-center">
                      <div className="text-muted small mb-1">Passenger</div>
                      <div className="h5 mb-1">{Booking.passengerName}</div>
                      <div className="small text-muted mb-3">ID: {Booking.passengerNationalID}</div>
                      <div className="text-muted small mb-1">Booking Date</div>
                      <div>{new Date(Booking.bookingDate).toLocaleDateString()}</div>
                    </div>
                  </CCol>
                </CRow>
              </CCardBody>

              <CCardFooter className="bg-light">
                <CRow className="g-2">
                  <CCol xs={6}>
                    <CButton 
                      color="primary" 
                      className="w-100"
                      onClick={() => navigate(`/ticket/${Booking.bookingID}`)}
                    >
                      <i className="fas fa-ticket-alt me-2"></i>
                      View Ticket
                    </CButton>
                  </CCol>
                  <CCol xs={6}>
                    <CButton 
                      color="info" 
                      variant="outline"
                      className="w-100"
                      onClick={() => navigate(`/invoice/${Booking.bookingID}`)}
                    >
                      <i className="fas fa-file-invoice me-2"></i>
                      View Invoice
                    </CButton>
                  </CCol>
                </CRow>
              </CCardFooter>
            </CCard>
          )}
        </CCol>
      </CRow>
    </CContainer>
  );
};

export default SearchBooking;