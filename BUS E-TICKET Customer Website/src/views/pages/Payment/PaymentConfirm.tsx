import { useEffect, useState } from 'react';
import { CButton, CCard, CCardBody, CCol, CContainer, CRow } from '@coreui/react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { fetchData } from 'src/Services/apiService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { ConfirmPayment } from 'src/Services/paymentService';

const PaymentConfirm = () => {
  const [searchParams] = useSearchParams();
  const reservationId = searchParams.get("reservationId");
  const navigate = useNavigate();
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');
  const [confirmed, setConfirmed] = useState<boolean>(false);

  useEffect(() => {
    console.log(reservationId);
    if (reservationId) {
      fetchData(
        () => ConfirmPayment(Number(reservationId)),
        () => setConfirmed(true),
        setError,
        setLoading
      );
    }
  }, [reservationId]);

  if (error || !reservationId) {
    console.log(reservationId);
    return <P_Error text={error ? error : "Payment Failed"} />;
  }

  return (
    <LoadingWrapper loading={loading}>
      {confirmed && (
        <div className="bg-light min-vh-100 d-flex flex-column">
          <CContainer>
            <CRow className="justify-content-center">
              <CCol md={8} lg={6} xl={5}>
                <CCard className="mx-4 mt-5 shadow-lg">
                  <CCardBody className="p-4">
                    <div className="text-center mb-4">
                      <div className="mb-4">
                        <i className="fas fa-check-circle text-success" 
                           style={{ fontSize: '4rem' }}></i>
                      </div>
                      <h2 className="text-success mb-2">Payment Successful!</h2>
                      <p className="text-muted">
                        Your payment has been processed successfully. 
                        You can now view or download your ticket and invoice.
                      </p>
                    </div>

                    <CRow className="g-3">
                      <CCol xs={12}>
                        <CButton 
                          color="primary" 
                          className="w-100"
                          onClick={() => navigate(`/ticket/${reservationId}`)}
                        >
                          <i className="fas fa-ticket-alt me-2"></i>
                          View Ticket
                        </CButton>
                      </CCol>

                      <CCol xs={12}>
                        <CButton 
                          color="info" 
                          variant="outline"
                          className="w-100"
                          onClick={() => navigate(`/invoice/${reservationId}`)}
                        >
                          <i className="fas fa-file-invoice me-2"></i>
                          View Invoice
                        </CButton>
                      </CCol>

                      <CCol xs={12}>
                        <CButton 
                          color="success" 
                          variant="ghost"
                          className="w-100"
                          onClick={() => navigate('/trips')}
                        >
                          <i className="fas fa-search me-2"></i>
                          Book Another Trip
                        </CButton>
                      </CCol>
                    </CRow>
                  </CCardBody>
                </CCard>
              </CCol>
            </CRow>
          </CContainer>
        </div>
      )}
    </LoadingWrapper>
  );
};

export default PaymentConfirm;