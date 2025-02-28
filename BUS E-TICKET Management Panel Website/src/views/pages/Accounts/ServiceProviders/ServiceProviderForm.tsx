import { Suspense, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Navigate, useLocation, useNavigate, useParams } from "react-router-dom";
import AccountForm from "src/components/AccountForm";
import AddressForm from "src/components/AddressForm";
import BusinessForm from "src/components/BusinessForm";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import P_Success from "src/components/P_Success";
import EnAccountStatus from "src/Enums/EnAccountStatus";
import { EnInputMode } from "src/Enums/EnInputMode";
import { Account, ServiceProvider } from "src/Interfaces/accountInterfaces";
import { User } from "src/Interfaces/userInterfaces";
import { CreateNewRegisterationApplication, GetServiceProviderByID } from "src/Services/accountService";
import { fetchData } from "src/Services/apiService";
import { RootState } from "src/store";
import { 
    CButton, 
    CCard, 
    CCardBody, 
    CCardHeader,
    CForm, 
    CSpinner 
  } from "@coreui/react-pro";
  import CIcon from '@coreui/icons-react';
  import { 
    cilBuilding,
    cilSave,
    cilUserPlus,
    cilArrowCircleLeft
  } from '@coreui/icons';
import ContactInformationForm from "src/components/ContactInformationForm";
import Business from "src/Interfaces/businessInterface";
import ContactInformation from "src/Interfaces/contactInterface";
import Address from "src/Interfaces/addressInterface";
import { RegisterationApplication } from "src/Interfaces/applicationInterface";
// import { IoIosBusiness } from "react-icons/io";
const ServiceProviderForm = () => {
    const { id } = useParams();
    const location = useLocation();
    const [InputMode, setInputMode] = useState<EnInputMode>(EnInputMode.Add);
    
    const [loading, setLoading] = useState<boolean>(false);
    const [fetchError, setFetchError] = useState('');
    const [fetchResult, setFetchResult] = useState('');
    const [error, setError] = useState('');
    const token: string | null = useSelector((state: RootState) => state.auth.token);
    const user: User | null = useSelector((state: RootState) => state.auth.user);
    const [business, setBusiness] = useState<Business>({});
    const [address, setAddress] = useState<Address>({});
    const [account, setAccount] = useState<Account>({});
    const [contactInformation, SetContactInformation] = useState<ContactInformation>({});

    const navigate = useNavigate();
    useEffect(() => {
        setError("");
        setFetchError("");
        setFetchResult("");

        if (token && id) {
            setInputMode(EnInputMode.Update);
            fetchData(async () =>{
                const res = await GetServiceProviderByID(token, id)
                setBusiness(res.data?.business);
                setAddress(res.data?.business.address);
                setAccount(res.data?.account);
                SetContactInformation(res.data?.business.contactInformation);
                }, undefined, setError, setLoading);
        } else {
            setInputMode(EnInputMode.Add);
        }

        if (location.pathname.includes("details")) {
            setInputMode(EnInputMode.read);
        }
    }, [id, token, location.pathname]);

    const AddServiceProviderHandle = async () => {
        
      
        if (!account.email || !account.password || !account.userName) {            
            setFetchError("All fields are required!");   return; }
            fetchData(async () => {       
                const application: RegisterationApplication = {
                    sPRegRequestID: 0,
                    notes: '',
                    business: { ...business, contactInformation, address: address },
                    serviceProvider: {
                        account: account
                    },
                };
                console.log(application);
                const res = await CreateNewRegisterationApplication(application);
                if (res.message) setFetchResult(res.message);
            
            
            }, undefined, setFetchError, setLoading);
          
    
    };

    const Submit = (e: React.MouseEvent | any) => {
        e.preventDefault();
        setFetchError("");
        setFetchResult("");

        switch (InputMode) {
            case EnInputMode.Add:
                AddServiceProviderHandle();
                break;
            case EnInputMode.Update:
                break;
        }
    };

    return (
        <LoadingWrapper loading={loading}>
            <CCard className="shadow-sm mb-4">
                <CCardHeader className="bg-light">
                    <div className="d-flex justify-content-between align-items-center">
                        <h3 className="mb-0">
                            <CIcon icon={cilBuilding} className="me-2" />
                            {InputMode === EnInputMode.Add ? "New Service Provider Registration" :
                             InputMode === EnInputMode.Update ? "Update Service Provider" : 
                             "Service Provider Details"}
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
                    <CForm className={!error ? "" : "d-none"}>
                        <div className="my-3">

                        <BusinessForm 
                            business={business} 
                            onBusinessChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setBusiness((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            InputMode={InputMode} 
                        />
                             </div>
                             <div className="my-3">

                        <ContactInformationForm 
                            contactInfo={contactInformation} 
                            onContactChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  SetContactInformation((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            InputMode={InputMode} 
                        />                             
                        </div>
                        <div className="my-3">
                        <AddressForm 
                            address={address} 
                            onAddressChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setAddress((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            InputMode={InputMode} 
                        />
                                                     </div>
                                                     <div className="my-3">

                        <AccountForm 
                            account={account} 
                            onAccountChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setAccount((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            InputMode={InputMode} 
                        />
                                                     </div>

                        {InputMode !== EnInputMode.read && (
                            <div className="d-flex justify-content-end mt-4">
                                <CButton 
                                    type="submit" 
                                    color="primary" 
                                    onClick={Submit} 
                                    disabled={loading}
                                >
                                    <CIcon 
                                        icon={InputMode === EnInputMode.Add ? cilUserPlus : cilSave} 
                                        className="me-2" 
                                    />
                                    {loading ? (
                                        <CSpinner size="sm" />
                                    ) : (
                                        InputMode === EnInputMode.Update ? 
                                        "Update Service Provider" : 
                                        "Submit Registration Application"
                                    )}
                                </CButton>
                            </div>
                        )}
                    </CForm>

                    <div className="mt-3">
                        <P_Error text={error} />
                        <P_Error text={fetchError} />
                        <P_Success text={fetchResult} />
                    </div>
                </CCardBody>
            </CCard>
        </LoadingWrapper>
    );
};

export default ServiceProviderForm;
