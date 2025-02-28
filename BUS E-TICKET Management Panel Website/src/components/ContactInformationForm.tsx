import { cilPhone, cilEnvelopeClosed } from "@coreui/icons";
import CIcon from "@coreui/icons-react";
import { CCard, CCardBody, CCardHeader, CCol, CForm, CFormFeedback, CFormInput, CFormLabel, CInputGroup, CInputGroupText, CRow } from "@coreui/react-pro";
import { EnInputMode } from "src/Enums/EnInputMode";

const ContactInformationForm = ({ contactInfo, onContactChange, InputMode }: { contactInfo: any, onContactChange:any ,InputMode: EnInputMode }) => {

  return (
    <>
          <CRow>
            <CCol xs={12}>
              <CCard className="shadow-sm">
                <CCardHeader>
                  <h4 className="mb-0">Contact Information</h4>
                </CCardHeader>
                <CCardBody>
                  <CRow>
                    <CCol md={6}>
                      <CFormLabel>Phone Number</CFormLabel>
                      <CInputGroup>
                        <CInputGroupText>
                          <CIcon icon={cilPhone} />
                        </CInputGroupText>
                        <CFormInput
                          type="tel"
                          name="phoneNumber"
                          value={contactInfo.phoneNumber}
                          onChange={onContactChange}
                          required
                          disabled={InputMode == EnInputMode.read}
                        />
                      </CInputGroup>
                      <CFormFeedback invalid>Phone number is required.</CFormFeedback>
                    </CCol>
                    <CCol md={6}>
                      <CFormLabel>Email</CFormLabel>
                      <CInputGroup>
                        <CInputGroupText>
                          <CIcon icon={cilEnvelopeClosed} />
                        </CInputGroupText>
                        <CFormInput
                          type="email"
                          name="email"
                          value={contactInfo.email}
                          onChange={onContactChange}
                          required
                          disabled={InputMode == EnInputMode.read}
                        />
                      </CInputGroup>
                      <CFormFeedback invalid>Email is required.</CFormFeedback>
                    </CCol>
                  </CRow>
                </CCardBody>
              </CCard>
            </CCol>
          </CRow>
    </>
  );
};

export default ContactInformationForm;
