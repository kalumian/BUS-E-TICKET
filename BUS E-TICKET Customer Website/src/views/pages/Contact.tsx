import {
    CCard,
    CCardBody,
    CCol,
    CContainer,
    CRow,
    CCardHeader,
  } from '@coreui/react';
  
  const Contact = () => {
    return (
      <CContainer className="mt-4 Contact">
            <CCard className="shadow-sm contact-card mt-4">
            <CCardHeader className="bg-light">
                <h4 className="mb-0">Let's Connect</h4>
            </CCardHeader>
            <CCardBody className="p-4">
                <CRow className="g-4">
                <CCol md={6} lg={3}>
                    <div className="contact-item text-center p-4 rounded bg-light h-100">
                    <div className="icon-wrapper mb-3">
                        <i className="fas fa-envelope"></i>
                    </div>
                    <h5 className="mb-2">Email</h5>
                    <a href="mailto:muhammadkalumian@gmail.com" 
                       className="text-decoration-none text-break">
                        muhammadkalumian@gmail.com
                    </a>
                    </div>
                </CCol>

                <CCol md={6} lg={3}>
                    <div className="contact-item text-center p-4 rounded bg-light h-100">
                    <div className="icon-wrapper mb-3">
                        <i className="fas fa-phone"></i>
                    </div>
                    <h5 className="mb-2">Phone</h5>
                    <div className="mb-1">+90 506 264 6625</div>
                    <div>+966 50 197 4250</div>
                    </div>
                </CCol>

                <CCol md={6} lg={3}>
                    <div className="contact-item text-center p-4 rounded bg-light h-100">
                    <div className="icon-wrapper mb-3">
                        <i className="fas fa-map-marker-alt"></i>
                    </div>
                    <h5 className="mb-2">Location</h5>
                    <div>Bandırma, Turkey</div>
                    <small className="text-muted">Turkish Republic</small>
                    </div>
                </CCol>

                <CCol md={6} lg={3}>
                    <div className="contact-item text-center p-4 rounded bg-light h-100">
                    <div className="icon-wrapper mb-3">
                        <i className="fas fa-globe"></i>
                    </div>
                    <h5 className="mb-2">Social</h5>
                    <div className="social-links">
                        <a href="https://github.com/kalumian" 
                           target="_blank" 
                           rel="noopener noreferrer" 
                           className="me-2">
                            github
                        </a>
                        <a href="https://www.linkedin.com/in/kalumian?originalSubdomain=sa" 
                           target="_blank" 
                           rel="noopener noreferrer" 
                           className="me-2">
                            linkedin
                        </a>
                    </div>
                    </div>
                </CCol>
                </CRow>
            </CCardBody>
            </CCard>
      </CContainer>
    );
  };
  
  export default Contact;