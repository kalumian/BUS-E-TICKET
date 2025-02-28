import { CButton, CCard, CCardBody, CCol, CContainer, CRow } from '@coreui/react';
import { useNavigate } from 'react-router-dom';

const PaymentFailure = () => {
  const navigate = useNavigate();

  return (
    <div className="bg-light min-vh-100 d-flex flex-column">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={8} lg={6} xl={5}>
            <CCard className="mx-4 mt-5 shadow-lg">
              <CCardBody className="p-4">
                <div className="text-center mb-4">
                  <div className="mb-4">
                    <i className="fas fa-times-circle text-danger" 
                       style={{ fontSize: '4rem' }}></i>
                  </div>
                  <h2 className="text-danger mb-2">Payment Failed</h2>
                  <p className="text-muted">
                    We're sorry, but your payment could not be processed. 
                    Please try again or choose a different payment method.
                  </p>
                </div>

                <CRow className="g-3">
                  <CCol xs={12}>
                    <CButton 
                      color="primary" 
                      className="w-100"
                      onClick={() => navigate(-1)}
                    >
                      <i className="fas fa-arrow-left me-2"></i>
                      Go Back
                    </CButton>
                  </CCol>

                  <CCol xs={12}>
                    <CButton 
                      color="info" 
                      variant="outline"
                      className="w-100"
                      onClick={() => navigate('/trips')}
                    >
                      <i className="fas fa-search me-2"></i>
                      Search Trips
                    </CButton>
                  </CCol>
                </CRow>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  );
};

export default PaymentFailure;