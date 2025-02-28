import { cilArrowCircleLeft, cilUser } from "@coreui/icons";
import CIcon from "@coreui/icons-react";
import { 
    CButton, 
    CCard, 
    CCardBody, 
    CCardHeader, 
    CForm,
    CRow,
    CCol,
    CBadge,
    CFormInput,
    CInputGroup,
    CInputGroupText
} from "@coreui/react-pro";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import PersonForm from "src/components/PersonForm";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import { EnInputMode } from "src/Enums/EnInputMode";
import { Person } from "src/Interfaces/personInterface";
import ContactInformation from "src/Interfaces/contactInterface";
import { RootState } from "src/store";
import { fetchData } from "src/Services/apiService";
import { GetPassengerByID } from "src/Services/passengerService";
import ContactInformationForm from "src/components/ContactInformationForm";

const PassengerForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [InputMode, setInputMode] = useState<EnInputMode>(EnInputMode.read);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState('');
    
    const initialPerson: Person = {
        firstName: '',
        lastName: '',
        birthDate: undefined,
        gender: undefined,
        nationalID: ''
    };
    
    const initialContactInfo: ContactInformation = {
        email: '',
        phoneNumber: '',
    };
    
    const [person, setPerson] = useState<Person>(initialPerson);
    const [contactInfo, setContactInfo] = useState<ContactInformation>(initialContactInfo);
    const [passengerID, setPassengerID] = useState("#");
    const token = useSelector((state: RootState) => state.auth.token);

    useEffect(() => {
        if (token && id) {
            fetchData(
                async () => {
                    const res = await GetPassengerByID(token, id);
                    setPerson(res.data.person);
                    setContactInfo(res.data.person.contactInformation);
                    setPassengerID(res.data.passengerID);
                },
                undefined,
                setError,
                setLoading
            );
        }
    }, [id, token]);

    return (
        <LoadingWrapper loading={loading}>
            <CCard className="shadow-sm">
                <CCardHeader className="bg-light">
                    <div className="d-flex justify-content-between align-items-center">
                        <h3 className="mb-0">
                            <CIcon icon={cilUser} className="me-2" />
                            Passenger Details
                        </h3>
                        <CButton 
                            color="primary" 
                            variant="ghost"
                            onClick={() => navigate(-1)}
                        >
                            <CIcon icon={cilArrowCircleLeft} className="me-2" />
                            Back
                        </CButton>
                    </div>
                </CCardHeader>
                <CCardBody>
                    <CForm>
                        <CRow className="mb-4">
                            <CCol>
                                <h5>
                                    <CIcon icon={cilUser} className="me-2" />
                                    Passenger ID
                                </h5>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilUser} />
                                    </CInputGroupText>
                                    <CFormInput 
                                        type="text" 
                                        value={passengerID} 
                                        disabled 
                                        className="form-control" 
                                    />
                                </CInputGroup>
                            </CCol>
                        </CRow>
                        <PersonForm 
                            person={person}
                            onPersonChange={() => { } }
                            InputMode={InputMode}
                            handlePersonInformation={undefined}
                            
                        />
                        <div className="mt-4">
                            <ContactInformationForm
                                contactInfo={contactInfo}
                                onContactChange={() => {}}
                                InputMode={InputMode}
                            />
                        </div>
                    </CForm>
                    {error && <P_Error text={error} />}
                </CCardBody>
            </CCard>
        </LoadingWrapper>
    );
};

export default PassengerForm;