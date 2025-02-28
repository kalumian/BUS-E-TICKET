import { CForm, CFormFeedback, CFormInput, CButton, CFormLabel, CFormSelect, CInputGroup, CCardHeader, CCard, CCardBody, CInputGroupText } from "@coreui/react-pro";
import { useEffect, useState } from "react";
import { cilCheckCircle, cilNotes, cilArrowCircleLeft, cilCalendar, cilInfo, cilUser } from "@coreui/icons";
import { useSelector } from "react-redux";
import { useParams, useNavigate } from "react-router-dom";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import { EnInputMode } from "src/Enums/EnInputMode";
import { ApplicationReview, RegisterationApplication, SPRegApplicationDisplayDTO } from "src/Interfaces/applicationInterface";
import { User } from "src/Interfaces/userInterfaces";
import { fetchData } from "src/Services/apiService";
import { GetSPApplicationById, SetApplicationReview } from "src/Services/spapplicationService";
import { RootState } from "src/store";
import CIcon from "@coreui/icons-react";

const SPApplicationReview = () => {
  const { id } = useParams()
  const [error, setError] = useState<string>("")
  const [loading, setLoading] = useState(false)
  const [inputMode, setInputMode] = useState<EnInputMode>(EnInputMode.Add)
  const [ServiceProviderApplication, setServiceProviderApplication] = useState<SPRegApplicationDisplayDTO>();
  const [fetchMessage, setFetchMessage] = useState('');
  const [fetchError, setFetchError] = useState('');

  const [response, setResponse] = useState<ApplicationReview>({
    responseID: 0,
    responseText: "",
    responseDate: new Date().toISOString(),
    result: false,
    requestID: Number(id),
    respondedByID: undefined,
  });
  const navigate = useNavigate();
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const user: User | null = useSelector((state: RootState) => state.auth.user);

  useEffect(() => {
    if (token && id) {
      fetchData(async ()=> {
        const res = await GetSPApplicationById(token, Number(id))
        if(res.data.review){
          setResponse(res.data.review);             
          setInputMode(EnInputMode.read)  
        } 
        return res;             
      }, setServiceProviderApplication, setError, setLoading)
    }
  }, [token, id]);

  const handleChange : any = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setResponse((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log(response);

    if(ServiceProviderApplication && token)
    fetchData(async ()=>{
      response.requestID = ServiceProviderApplication.spRegRequestID;
      response.respondedByID = user?.UserID;            
      const res = await SetApplicationReview(token, response);
      res.message && setFetchMessage(res.message)
      navigate(-1)
    }, undefined, setFetchError, setLoading)
    
  };


  return (
    <>
      {!error ? (
        <LoadingWrapper loading={loading}>
          <CCard className="shadow-sm">
            <CCardHeader className="bg-light">
              <div className="d-flex justify-content-between align-items-center">
                <h3 className="mb-0">
                  <CIcon icon={cilNotes} className="me-2" />
                  Application Review
                </h3>
                <div>
                  <CButton
                    color="info"
                    variant="ghost"
                    className="me-2"
                    onClick={() => navigate(`/account/registration-applications/details/${ServiceProviderApplication?.spRegRequestID}`)}
                  >
                    <CIcon icon={cilInfo} className="me-2" />
                    View Application Details
                  </CButton>
                  <CButton
                    color="primary"
                    variant="ghost"
                    onClick={() => navigate(-1)}
                  >
                    <CIcon icon={cilArrowCircleLeft} className="me-2" />
                    Back
                  </CButton>
                </div>
              </div>
            </CCardHeader>
            <CCardBody>
              <CForm onSubmit={handleSubmit}>
                <div className="mb-4">
                  <CFormLabel>Application ID</CFormLabel>
                  <CInputGroup>
                    <CInputGroupText>
                      <CIcon icon={cilInfo} />
                    </CInputGroupText>
                    <CFormInput
                      type="text"
                      name="RequestID"
                      value={ServiceProviderApplication?.spRegRequestID}
                      disabled
                    />
                  </CInputGroup>
                </div>

                <div className="mb-4">
                  <CFormLabel>Review Response</CFormLabel>
                  <CInputGroup>
                    <CInputGroupText>
                      <CIcon icon={cilNotes} />
                    </CInputGroupText>
                    <CFormInput
                      type="text"
                      name="responseText"
                      placeholder="Enter your response"
                      disabled={EnInputMode.read === inputMode}
                      value={response.responseText}
                      onChange={handleChange}
                      required
                    />
                  </CInputGroup>
                  <CFormFeedback invalid>Response text is required.</CFormFeedback>
                </div>

                <div className="mb-4">
                  <CFormLabel>Response Date</CFormLabel>
                  <CInputGroup>
                    <CInputGroupText>
                      <CIcon icon={cilCalendar} />
                    </CInputGroupText>
                    <CFormInput
                      type="datetime-local"
                      name="responseDate"
                      value={inputMode === EnInputMode.read
                        ? response.responseDate
                          ? new Date(response.responseDate).toISOString().slice(0, 16)
                          : ""
                        : new Date().toISOString().slice(0, 16)}
                      disabled={true}
                      onChange={handleChange}
                      required
                    />
                  </CInputGroup>
                </div>

                <div className="mb-4">
                  <CFormLabel>Review Result</CFormLabel>
                  <CInputGroup>
                    <CInputGroupText>
                      <CIcon icon={cilCheckCircle} />
                    </CInputGroupText>
                    <CFormSelect
                      name="result"
                      disabled={EnInputMode.read === inputMode}
                      value={response.result.toString()}
                      onChange={handleChange}
                    >
                      <option value="true">Accept Application</option>
                      <option value="false">Reject Application</option>
                    </CFormSelect>
                  </CInputGroup>
                </div>

                <div className="mb-4">
                  <CFormLabel>Reviewed By</CFormLabel>
                  <CInputGroup>
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput
                      type="text"
                      name="respondedByID"
                      disabled={true}
                      value={inputMode === EnInputMode.Add ? String(user?.Username) : response?.respondedByID}
                      onChange={handleChange}
                    />
                  </CInputGroup>
                </div>

                {inputMode === EnInputMode.Add && (
                  <div className="d-flex justify-content-end">
                    <CButton 
                      type="submit" 
                      color="primary"
                    >
                      <CIcon icon={cilCheckCircle} className="me-2" />
                      Submit Review
                    </CButton>
                  </div>
                )}
              </CForm>
            </CCardBody>
          </CCard>
        </LoadingWrapper>
      ) : (
        <P_Error text={error} />
      )}
    </>
  );
};

export default SPApplicationReview;
