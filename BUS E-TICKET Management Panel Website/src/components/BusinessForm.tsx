import { 
  cilBuilding, 
  cilImage, 
  cilBadge,
  cilEnvelopeClosed,
  cilPhone 
} from "@coreui/icons";
import CIcon from "@coreui/icons-react";
import { 
  CCard, 
  CCardBody, 
  CCardHeader, 
  CForm, 
  CFormFeedback, 
  CFormInput, 
  CFormLabel,
  CInputGroup,
  CInputGroupText,
  CRow,
  CCol
} from "@coreui/react-pro";
import { EnInputMode } from "src/Enums/EnInputMode";
import Business from "src/Interfaces/businessInterface";

const BusinessForm = ({ 
  business, 
  onBusinessChange, 
  InputMode 
}: { 
  business: Business, 
  onBusinessChange: any, 
  InputMode: EnInputMode 
}) => {
  return (
    <CCard className="mb-4 border">
      <CCardHeader className="bg-light">
        <h4 className="mb-0">
          <CIcon icon={cilBuilding} className="me-2" />
          Business Information
        </h4>
      </CCardHeader>
      <CCardBody>
        <CRow>
          <CCol md={6}>
            <div className="mb-3">
              <CFormLabel>Business Name</CFormLabel>
              <CInputGroup>
                <CInputGroupText>
                  <CIcon icon={cilBuilding} />
                </CInputGroupText>
                <CFormInput
                  type="text"
                  name="businessName"
                  placeholder="Enter business name"
                  disabled={EnInputMode.Update === InputMode || EnInputMode.read === InputMode}
                  value={business?.businessName || ''}
                  onChange={onBusinessChange}
                  required
                />
              </CInputGroup>
              <CFormFeedback invalid>Business Name is required.</CFormFeedback>
            </div>
          </CCol>

          <CCol md={6}>
            <div className="mb-3">
              <CFormLabel>Logo URL</CFormLabel>
              <CInputGroup>
                <CInputGroupText>
                  <CIcon icon={cilImage} />
                </CInputGroupText>
                <CFormInput
                  type="text"
                  name="logoURL"
                  placeholder="Enter logo URL"
                  disabled={EnInputMode.read === InputMode}
                  value={business?.logoURL || ''}
                  onChange={onBusinessChange}
                  required
                />
              </CInputGroup>
              <CFormFeedback invalid>Logo URL is required.</CFormFeedback>
            </div>
          </CCol>
        </CRow>
        <CRow>
          <CCol md={6}>
            <div className="mb-3">
              <CFormLabel>Website Link</CFormLabel>
              <CInputGroup>
                <CInputGroupText>
                  <CIcon icon={cilImage} />
                </CInputGroupText>
                <CFormInput
                  type="url"
                  name="webSiteLink"
                  placeholder="Enter website URL"
                  disabled={EnInputMode.read === InputMode}
                  value={business?.webSiteLink || ''}
                  onChange={onBusinessChange}
                />
              </CInputGroup>
            </div>
          </CCol>

          <CCol md={6}>
            <div className="mb-3">
              <CFormLabel>Business License Number</CFormLabel>
              <CInputGroup>
                <CInputGroupText>
                  <CIcon icon={cilBadge} />
                </CInputGroupText>
                <CFormInput
                  type="text"
                  name="businessLicenseNumber"
                  placeholder="Enter license number"
                  disabled={EnInputMode.Update === InputMode || EnInputMode.read === InputMode}
                  value={business?.businessLicenseNumber || ''}
                  onChange={onBusinessChange}
                  required
                />
              </CInputGroup>
              <CFormFeedback invalid>License Number is required.</CFormFeedback>
            </div>
          </CCol>
        </CRow>
      </CCardBody>
    </CCard>
  );
};

export default BusinessForm;