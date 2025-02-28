import { 
  CForm, 
  CFormInput, 
  CFormSelect, 
  CCol, 
  CRow,
  CCard,
  CCardHeader,
  CCardBody,
  CInputGroup,
  CInputGroupText,
  CFormLabel,
  CFormFeedback
} from '@coreui/react';
import CIcon from '@coreui/icons-react';
import { 
  cilUser,
  cilCalendar,
  cilPeople,
  cilBadge,
} from '@coreui/icons';
import { Passenger } from '../Interfaces/passengerInterface';
import { EnInputMode } from 'src/Enums/EnInputMode';
import { fetchData } from 'src/Services/apiService';
import { GetPersonByNationalID } from 'src/Services/personService';
import { useState } from 'react';
import LoadingWrapper from './LoadingWrapper';
import { Person } from 'src/Interfaces/personInterface';

interface PersonFormProps {
  onPersonChange: any;
  person: Person;
  InputMode: EnInputMode;
  handlePersonInformation: any;
}


const PersonForm = ({ onPersonChange, person, handlePersonInformation, InputMode =  EnInputMode.Add}: PersonFormProps) => {
 const [loading, setLoading] = useState(false);

  return (
    <LoadingWrapper loading={loading}>
         <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <h4 className="mb-0">
          <CIcon icon={cilUser} className="me-2" />
          Passenger Information
        </h4>
      </CCardHeader>
      <CCardBody>
        <CForm>
          <CRow>
          <CCol md={4}>
              <div className="mb-3">
                <CFormLabel>National ID</CFormLabel>
                <CInputGroup>
                  <CInputGroupText>
                    <CIcon icon={cilBadge} />
                  </CInputGroupText>
                  <CFormInput
                    type="text"
                    onBlur={handlePersonInformation}
                    name="nationalID"
                    value={person.nationalID}
                    onChange={onPersonChange}
                    required
                  />
                </CInputGroup>
                <CFormFeedback invalid>National ID is required.</CFormFeedback>
              </div>
            </CCol>
            <CCol md={4}>
              <div className="mb-3">
                <CFormLabel>First Name</CFormLabel>
                <CInputGroup>
                  <CInputGroupText>
                    <CIcon icon={cilUser} />
                  </CInputGroupText>
                  <CFormInput
                    type="text"
                    name="firstName"
                    value={person.firstName}
                    onChange={onPersonChange}
                    required
                  />
                </CInputGroup>
                <CFormFeedback invalid>First name is required.</CFormFeedback>
              </div>
            </CCol>
            <CCol md={4}>
              <div className="mb-3">
                <CFormLabel>Last Name</CFormLabel>
                <CInputGroup>
                  <CInputGroupText>
                    <CIcon icon={cilUser} />
                  </CInputGroupText>
                  <CFormInput
                    type="text"
                    name="lastName"
                    value={person.lastName}
                    onChange={onPersonChange}
                    required
                  />
                </CInputGroup>
                <CFormFeedback invalid>Last name is required.</CFormFeedback>
              </div>
            </CCol>
          </CRow>

          <CRow>
            <CCol md={6}>
              <div className="mb-3">
                <CFormLabel>Date of Birth</CFormLabel>
                <CInputGroup>
                  <CInputGroupText>
                    <CIcon icon={cilCalendar} />
                  </CInputGroupText>
                  <CFormInput
                    type="date"
                    name="birthDate"
                    value={person.birthDate ? new Date(person.birthDate).toISOString().split('T')[0] : ''} 
                    onChange={onPersonChange}
                    required
                  />
                </CInputGroup>
                <CFormFeedback invalid>Date of birth is required.</CFormFeedback>
              </div>
            </CCol>

            <CCol md={6}>
              <div className="mb-3">
                <CFormLabel>Gender</CFormLabel>
                <CInputGroup>
                  <CInputGroupText>
                    <CIcon icon={cilPeople} />
                  </CInputGroupText>
                  <CFormSelect
                    value={person.gender}
                    onChange={onPersonChange}
                    required
                    name='gender'
                  >
                    <option value="">Select Gender</option>
                    <option value="0">Male</option>
                    <option value="1">Female</option>
                  </CFormSelect>
                </CInputGroup>
                <CFormFeedback invalid>Gender is required.</CFormFeedback>
              </div>
            </CCol>
          </CRow>

          <CRow>
           
          </CRow>
        </CForm>
      </CCardBody>
    </CCard>
    </LoadingWrapper>
 
  );
};

export default PersonForm;