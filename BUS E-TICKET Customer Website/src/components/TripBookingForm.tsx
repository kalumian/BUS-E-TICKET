import { useState } from 'react';
import PersonForm from '../components/PersonForm';
import EnPaymentMethod from '../Enums/EnPaymentMethod';
import { CCard, CCardBody, CCardHeader, CForm, CFormInput, CFormCheck, CButton, CRow, CCol, CFormSelect } from '@coreui/react';
import LoadingWrapper from './LoadingWrapper';
import { fetchData } from 'src/Services/apiService';
import { createBooking } from 'src/Services/bookingService';
import P_Error from './P_Error';
import ContactInformationForm from './ContactInformationForm';
import { EnInputMode } from 'src/Enums/EnInputMode';
import ContactInformation from 'src/Interfaces/contactInterface';
import { GetPersonByNationalID } from 'src/Services/personService';
import { Person } from 'src/Interfaces/personInterface';
import { CAlert } from '@coreui/react-pro';

const TripBooking = ({TripID} : {TripID? : number}) => {
  const PersonIni  : Person = {} 
  const [person, setPerson] = useState<Person>(PersonIni);
  const [contactInfo, setContactInfo] = useState<ContactInformation>({});
  const [paymentMethod, setPaymentMethod] = useState<EnPaymentMethod>(EnPaymentMethod.PayPal);
  const [termsAccepted, setTermsAccepted] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [paymentLink, setPaymentLink] = useState("");
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    fetchData(async () => {
      setError("")
      setPaymentLink("")
        if (TripID === undefined) {
          setError('TripID is required');
          setLoading(false);
          return;
        }
        console.log(person.nationalID);
        
        const res = await createBooking({
          tripID: TripID,
          passenger: {passengerID: 0, person: {...person, contactInformation: {...contactInfo, contactInformationID: null}, personID:null}},
          paymentMethod: paymentMethod
        })
        
        setTimeout(() => {
          window.open(res.data.paymentLink);
        }, 100);
        setPaymentLink(res.data.paymentLink)
      }, undefined, setError, setLoading);
  };
  const handlePersonInformation = (e: React.FocusEvent<HTMLInputElement>) => {
    const value = e.target.value;
    fetchData(async ()=>{
     setLoading(true);
     const res = await GetPersonByNationalID(value)     
     setPerson({...res.data});
     setContactInfo({...res.data.contactInformation});
    }, undefined, undefined, setLoading);
    
  };
  return (
    <LoadingWrapper loading={loading}>
      {TripID && (
        <CRow className="mb-3">
          <CCol xs={12} className="mt-4">
            <CCard className="shadow-sm">
              <CCardHeader>
                <h4 className="mb-0">Booking Information</h4>
              </CCardHeader>
              <CCardBody>
                <CForm onSubmit={handleSubmit}>
                    <div className="mt-4">
                    <PersonForm
                      onPersonChange={(e : React.ChangeEvent<HTMLInputElement>) => setPerson((prev) => ({...prev, [e.target.name]: e.target.value}))}
                      person={person}
                      InputMode={EnInputMode.Add}
                      handlePersonInformation={handlePersonInformation}
                    />
                    </div>     
                    <div className="mt-4">
                    <ContactInformationForm
                    contactInfo={contactInfo}
                    onContactChange={(e : React.ChangeEvent<HTMLInputElement>) => setContactInfo((prev) => ({...prev, [e.target.name]: e.target.value}))}
                    InputMode={EnInputMode.Add}
                    />
                    </div>                          
                <div className="mt-4">
                <h5>Payment Information</h5>
                <CCol md={6} className="mt-3">
                    <CFormSelect
                        label="Payment Method"
                        value={paymentMethod}
                        onChange={(e : React.ChangeEvent<HTMLSelectElement>) => setPaymentMethod(e.target.value as unknown as EnPaymentMethod)}
                        required
                    >
                        <option value="">Select Payment Method</option>
                        <option value={EnPaymentMethod.CreditCard}>Credit Card</option>
                        <option value={EnPaymentMethod.PayPal}>PayPal</option>
                        <option value={EnPaymentMethod.BankTransfer}>Bank Transfer</option>
                    </CFormSelect>
                </CCol>
                </div>
                <div className="mt-4">
                    <CFormCheck
                      id="terms"
                      label="I accept the terms and conditions"
                      checked={termsAccepted}
                      onChange={(e) => setTermsAccepted(e.target.checked)}
                    />
                  </div>
                  <div className="mt-4 text-end">
                    <CButton 
                      type="submit" 
                      color="primary"
                      disabled={!termsAccepted}
                    >
                      Proceed to Payment
                    </CButton>
                  </div>
          
                </CForm>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      )}
      sb-vgnf037324672@personal.example.com  
      A=Q*Uu64

    <P_Error text={error} />
    {paymentLink && <CAlert color='info'>If you are not automatically redirected to the payment gateway, please click the following link to complete your payment: <a target="_blank" href={paymentLink}>Click Here</a></CAlert>}
    </LoadingWrapper>
  );
};

export default TripBooking;

