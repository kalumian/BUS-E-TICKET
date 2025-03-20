import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useParams, useNavigate } from "react-router-dom";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import { RootState } from "src/store";
import { fetchData } from "src/Services/apiService";
import { GetSPApplicationById } from "src/Services/spapplicationService";
import { ApplicationReview, RegisterationApplication, SPRegApplicationDisplayDTO } from "src/Interfaces/applicationInterface";
import CIcon from '@coreui/icons-react';
import { CCard, CCardHeader, CCardBody, CListGroup, CListGroupItem, CBadge, CButton, CRow, CCol } from '@coreui/react';
import {
cilArrowCircleLeft,
cilBuilding,
cilEnvelopeClosed,
cilPhone,
cilCalendar,
cilNotes,
cilCheckCircle,
cilUser
} from '@coreui/icons';

const SPApplicationDetails = () => {
  const { id } = useParams();
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState(false);
  const [ServiceProviderApplication, setServiceProviderApplication] = useState<SPRegApplicationDisplayDTO | null>(null);
  const [response, setResponse] = useState<ApplicationReview | null>(null);
  const [fetchMessage, setFetchMessage] = useState('');
  const [fetchError, setFetchError] = useState('');
  
  const navigate = useNavigate();
  const token: string | null = useSelector((state: RootState) => state.auth.token);

  useEffect(() => {
    if (token && id) {
      fetchData(
        async () => {
          const res = await GetSPApplicationById(token, Number(id));
          return res;
        },
        (data) => {
          setServiceProviderApplication(data);
          if (data?.review) {
            setResponse(data.review);
          }
        },
        setError,
        setLoading,
        { token }
      );
    }
  }, [token, id]);

  const handleGoBack = () => {
    navigate(-1); // Navigate back to the previous page
  };

  return (
    <>
      {!error ? (
        <LoadingWrapper loading={loading}>
          <CCard className="shadow-sm">
            <CCardHeader className="bg-light">
              <div className="d-flex justify-content-between align-items-center">
                <h3 className="mb-0">
                  <CIcon icon={cilBuilding} className="me-2" />
                  Service Provider Application Details
                </h3>
                <CButton 
                  color="primary" 
                  variant="ghost"
                  onClick={handleGoBack}
                >
                  <CIcon icon={cilArrowCircleLeft} className="me-2" />
                  Back
                </CButton>
              </div>
            </CCardHeader>
            <CCardBody>
              <CRow>
                {/* Main Application Details */}
                <CCol md={6}>
                  <CCard className="mb-4">
                    <CCardHeader className="bg-light">
                      <h5 className="mb-0">Application Information</h5>
                    </CCardHeader>
                    <CCardBody>
                      <CListGroup flush>
                        <CListGroupItem>
                          <div className="d-flex justify-content-between">
                            <strong>Application ID:</strong>
                            <CBadge color="info">{ServiceProviderApplication?.spRegRequestID}</CBadge>
                          </div>
                        </CListGroupItem>
                        <CListGroupItem>
                          <div className="d-flex align-items-center">
                            <CIcon icon={cilCalendar} className="me-2 text-primary" />
                            <div>
                              <div className="text-medium-emphasis small">Request Date</div>
                              <strong>
                                {ServiceProviderApplication?.requestDate ? 
                                  new Date(ServiceProviderApplication.requestDate).toLocaleString('en-GB', {
                                    dateStyle: 'full',
                                    timeStyle: 'short'
                                  }) : "Not Selected"}
                              </strong>
                            </div>
                          </div>
                        </CListGroupItem>
                        <CListGroupItem>
                          <div className="d-flex align-items-center">
                            <CIcon icon={cilBuilding} className="me-2 text-primary" />
                            <div>
                              <div className="text-medium-emphasis small">Business Name</div>
                              <strong>{ServiceProviderApplication?.businessName}</strong>
                            </div>
                          </div>
                        </CListGroupItem>
                        <CListGroupItem>
                          <div className="d-flex align-items-center">
                            <CIcon icon={cilEnvelopeClosed} className="me-2 text-primary" />
                            <div>
                              <div className="text-medium-emphasis small">Business Email</div>
                              <strong>{ServiceProviderApplication?.businessEmail}</strong>
                            </div>
                          </div>
                        </CListGroupItem>
                        <CListGroupItem>
                          <div className="d-flex align-items-center">
                            <CIcon icon={cilPhone} className="me-2 text-primary" />
                            <div>
                              <div className="text-medium-emphasis small">Business Phone</div>
                              <strong>{ServiceProviderApplication?.businessPhoneNumber}</strong>
                            </div>
                          </div>
                        </CListGroupItem>
                        <CListGroupItem>
                          <div className="d-flex justify-content-between align-items-center">
                            <strong>Status:</strong>
                            <CBadge color={ServiceProviderApplication?.review?.result  == null ? "warning" : "success"}>
                              {ServiceProviderApplication?.review?.result == null ? "Pending" : "Reviewed"}
                            </CBadge>
                          </div>
                        </CListGroupItem>
                      </CListGroup>
                    </CCardBody>
                  </CCard>
                </CCol>
  
                {/* Notes and Review Section */}
                <CCol md={6}>
                  <CCard className="mb-4">
                    <CCardHeader className="bg-light">
                      <h5 className="mb-0">
                        <CIcon icon={cilNotes} className="me-2" />
                        Notes & Review
                      </h5>
                    </CCardHeader>
                    <CCardBody>
                      <div className="mb-4">
                        <h6 className="text-medium-emphasis">Application Notes</h6>
                        <p className="border-start border-4 border-primary ps-3">
                          {ServiceProviderApplication?.notes || "No notes provided"}
                        </p>
                      </div>
  
                      <div className="mt-4">
                        <h6 className="text-medium-emphasis mb-3">Review Details</h6>
                        {response ? (
                          <CListGroup flush>
                            <CListGroupItem>
                              <div className="d-flex align-items-center">
                                <CIcon icon={cilCheckCircle} className={`me-2 text-${response.result ? 'success' : 'danger'}`} />
                                <div>
                                  <div className="text-medium-emphasis small">Result</div>
                                  <CBadge color={response.result ? 'success' : 'danger'}>
                                    {response.result ? "Accepted" : "Rejected"}
                                  </CBadge>
                                </div>
                              </div>
                            </CListGroupItem>
                            <CListGroupItem>
                              <div className="text-medium-emphasis small">Response</div>
                              <p className="mb-0">{response.responseText}</p>
                            </CListGroupItem>
                            <CListGroupItem>
                              <div className="d-flex align-items-center">
                                <CIcon icon={cilCalendar} className="me-2 text-primary" />
                                <div>
                                  <div className="text-medium-emphasis small">Response Date</div>
                                  <strong>{new Date(response.responseDate).toLocaleString('en-GB')}</strong>
                                </div>
                              </div>
                            </CListGroupItem>
                            <CListGroupItem>
                              <div className="d-flex align-items-center">
                                <CIcon icon={cilUser} className="me-2 text-primary" />
                                <div>
                                  <div className="text-medium-emphasis small">Reviewed By</div>
                                  <strong>{response.respondedByID}</strong>
                                </div>
                              </div>
                            </CListGroupItem>
                          </CListGroup>
                        ) : (
                          <div className="text-center text-medium-emphasis p-3 border rounded">
                            No response has been provided yet.
                          </div>
                        )}
                      </div>
                    </CCardBody>
                  </CCard>
                </CCol>
              </CRow>
            </CCardBody>
          </CCard>
        </LoadingWrapper>
      ) : (
        <P_Error text={error} />
      )}
    </>
  )
};

export default SPApplicationDetails;
