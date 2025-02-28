import {CFormFeedback, CFormInput, CFormSelect, CRow, CCol, CFormLabel } from "@coreui/react-pro";
import EnAccountStatus from "src/Enums/EnAccountStatus";
import { EnInputMode } from "src/Enums/EnInputMode";
import { Account } from "src/Interfaces/accountInterfaces";
import { 
  CCard,
  CCardHeader,
  CCardBody,
  CInputGroup,
  CInputGroupText 
} from "@coreui/react-pro";
import CIcon from '@coreui/icons-react';
import { 
  cilUser,
  cilEnvelopeClosed,
  cilLockLocked,
  cilPhone,
  cilSettings
} from '@coreui/icons';

const AccountForm = ({ account, onAccountChange, InputMode }: { account: Account, onAccountChange: any, InputMode: EnInputMode }) => {

  return (
    <CCard className="mb-4 shadow-sm">
    <CCardHeader className="bg-light">
      <h4 className="mb-0">
        <CIcon icon={cilUser} className="me-2" />
        Account Information
      </h4>
    </CCardHeader>
    <CCardBody>
      <CRow>
        <CCol md={6}>
          <div className="mb-3">
            <CFormLabel>Username</CFormLabel>
            <CInputGroup>
              <CInputGroupText>
                <CIcon icon={cilUser} />
              </CInputGroupText>
              <CFormInput
                type="text"
                name="userName"
                disabled={EnInputMode.Update === InputMode || EnInputMode.read === InputMode}
                value={account?.userName || ''}
                onChange={onAccountChange}
                required
              />
            </CInputGroup>
            <CFormFeedback invalid>Username is required.</CFormFeedback>
          </div>
        </CCol>
        
        <CCol md={6}>
          <div className="mb-3">
            <CFormLabel>Email Address</CFormLabel>
            <CInputGroup>
              <CInputGroupText>
                <CIcon icon={cilEnvelopeClosed} />
              </CInputGroupText>
              <CFormInput
                type="email"
                name="email"
                disabled={EnInputMode.read === InputMode}
                value={account?.email || ''}
                onChange={onAccountChange}
                required
              />
            </CInputGroup>
            <CFormFeedback invalid>Email is required.</CFormFeedback>
          </div>
        </CCol>
      </CRow>

      <CRow>
        <CCol md={6}>
          <div className="mb-3">
            <CFormLabel>Password</CFormLabel>
            <CInputGroup>
              <CInputGroupText>
                <CIcon icon={cilLockLocked} />
              </CInputGroupText>
              <CFormInput
                type="password"
                name="password"
                disabled={EnInputMode.Update === InputMode || EnInputMode.read === InputMode}
                value={account?.password || ''}
                placeholder={EnInputMode.Update === InputMode || EnInputMode.read === InputMode ? '***************' : ''}
                onChange={onAccountChange}
                required
              />
            </CInputGroup>
            <CFormFeedback invalid>Password is required.</CFormFeedback>
          </div>
        </CCol>

        <CCol md={6}>
          <div className="mb-3">
            <CFormLabel>Phone Number</CFormLabel>
            <CInputGroup>
              <CInputGroupText>
                <CIcon icon={cilPhone} />
              </CInputGroupText>
              <CFormInput
                type="text"
                name="phoneNumber"
                disabled={EnInputMode.read === InputMode}
                value={account?.phoneNumber || ''}
                onChange={onAccountChange}
                required
              />
            </CInputGroup>
            <CFormFeedback invalid>Phone Number is required.</CFormFeedback>
          </div>
        </CCol>
      </CRow>

      <CRow>
        <CCol md={6}>
          <div className="mb-3">
            <CFormLabel>Account Status</CFormLabel>
            <CInputGroup>
              <CInputGroupText>
                <CIcon icon={cilSettings} />
              </CInputGroupText>
              <CFormSelect
                name="accountStatus"
                value={account?.accountStatus || "0"}
                onChange={onAccountChange}
                disabled={EnInputMode.read === InputMode || account?.accountStatus === 2 || EnInputMode.Add === InputMode}
              >
                <option value="0">Active</option>
                <option value="1">Inactive</option>
                {!(EnInputMode.Add === InputMode) && 
                  <option value="2" disabled>Pending Verification</option>
                } 
                <option value="3" disabled>Deleted</option>
              </CFormSelect>
            </CInputGroup>
          </div>
        </CCol>
      </CRow>
    </CCardBody>
  </CCard>
  );
};

export default AccountForm;