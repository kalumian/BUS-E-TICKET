import { cilArrowCircleLeft, cilSave, cilTrash, cilUser, cilUserPlus } from "@coreui/icons";
import CIcon from "@coreui/icons-react";
import { CButton, CCard, CCardBody, CCardHeader, CForm, CSpinner } from "@coreui/react-pro";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import AccountForm from "src/components/AccountForm";
import PersonForm from "src/components/PersonForm";
import AddressForm from "src/components/AddressForm";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import P_Success from "src/components/P_Success";
import { EnInputMode } from "src/Enums/EnInputMode";
import EnAccountStatus from "src/Enums/EnAccountStatus";
import { GetCustomerById } from "src/Services/accountService";
import { RootState } from "src/store";
import { fetchData } from "src/Services/apiService";
import { Person } from "src/Interfaces/personInterface";
import { Account } from "src/Interfaces/accountInterfaces";
import ContactInformation from "src/Interfaces/contactInterface";
import Address from "src/Interfaces/addressInterface";
import ContactInformationForm from "src/components/ContactInformationForm";
import { GetPersonByNationalID } from "src/Services/personService";

const CustomerForm = () => {
    const { id } = useParams();
    const location = useLocation();
    const navigate = useNavigate();
    const [InputMode, setInputMode] = useState<EnInputMode>(EnInputMode.Add);
    const [loading, setLoading] = useState<boolean>(false);
    const [fetchError, setFetchError] = useState('');
    const [fetchResult, setFetchResult] = useState('');
    const initialPerson: Person = {
        firstName: '',
        lastName: '',
        birthDate: undefined,
        gender: undefined,
        nationalID: ''
      };
      const initialAccount: Account = {
        email: '',
        userName: '',
        password: '',  // if it's optional, you can omit this
        accountStatus: EnAccountStatus.Active  // assuming you have this enum
    };
    
    const initialContactInfo: ContactInformation = {
        email: '',
        phoneNumber: '',
    };
    
    const initialAddress: Address = {};
    const [person, setPerson] = useState<Person>(initialPerson);
    const [account, setAccount] = useState<Account>(initialAccount);
    const [contactInfo, setContactInfo] = useState<ContactInformation>(initialContactInfo);
    const [address, setAddrss] = useState<Address>(initialAddress);
    const token = useSelector((state: RootState) => state.auth.token);

    useEffect(() => {
        setFetchError("");
        setFetchResult("");
        
        if (token && id) {
            setInputMode(EnInputMode.Update);
            fetchData(async () => {
                const res = await GetCustomerById(token, id);
                setAccount(res.data.account)
                setPerson(res.data.person)
                setAddrss(res.data.address)
                setContactInfo(res.data.person.contactInformation)

            }, undefined, setFetchError, setLoading);
        } else {
            setInputMode(EnInputMode.Add);
            // setCustomer(initCustomer);
        }

        if (location.pathname.includes("details")) {
            setInputMode(EnInputMode.read);
        }
    }, [id, token, location.pathname]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setFetchError("");
        setFetchResult("");

        if (!validateForm()) {
            setFetchError("Please fill all required fields");
            return;
        }

        try {
            setLoading(true);
            if (InputMode === EnInputMode.Add) {
                fetchData(async () =>{
                    // await RegisterNewCustomer({
                    //     account: account,
                    //     person: {...person, contactInformation: contactInfo},
                    //     address: address
                    // })
                    navigate("/login");
            }, undefined, setFetchError, setLoading);
        
            } else if (InputMode === EnInputMode.Update) {
                // await UpdateCustomer(token!, id!, customer);
                setFetchResult("Customer upda`ted successfully!");
            }
        } catch (error) {
            setFetchError("Operation failed. Please try again.");
        } finally {
            setLoading(false);
        }
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

    // const handleDelete = async () => {
    //     if (!token || !id) return;
        
    //     try {
    //         setLoading(true);
    //         // await DeleteCustomer(token, id);
    //         setFetchResult("Customer deleted successfully!");
    //         navigate(-1);
    //     } catch (error) {
    //         setFetchError("Delete operation failed. Please try again.");
    //     } finally {
    //         setLoading(false);
    //     }
    // };

    const validateForm = () => {      
        return (
            account?.email &&
            account.userName &&
            (InputMode === EnInputMode.Add ? account.password : true) &&
            person?.firstName &&
            person.lastName
        );
    };
    return (
        <LoadingWrapper loading={loading}>
            <CCard className="shadow-sm">
                <CCardHeader className="bg-light">
                    <div className="d-flex justify-content-between align-items-center">
                        <h3 className="mb-0">
                            <CIcon icon={InputMode === EnInputMode.Add ? cilUserPlus : cilUser} className="me-2" />
                            {InputMode === EnInputMode.Add ? "Add New Customer" : 
                             InputMode === EnInputMode.Update ? "Update Customer" : "Customer Details"}
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
                    <CForm onSubmit={handleSubmit}>
                        <br className="mt-2"/>
                        <AccountForm 
                            account={account}
                            onAccountChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setAccount((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            InputMode={InputMode}
                        />
                         <br className="mt-2"/>
                        <PersonForm 
                            handlePersonInformation={handlePersonInformation}
                            onPersonChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  {
                                const { name, value } = e.target;

                                setPerson((prev) => ({
                                  ...prev,
                                  [name]: name === "birthDate" ? new Date(value) : value
                                }));
                            }}
                            person={person}
                            InputMode={InputMode}
                        />
                        <br className="mt-2"/>
                        <ContactInformationForm
                            onContactChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setContactInfo((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            contactInfo={contactInfo}
                            InputMode={InputMode}
                        />
                        <br className="mt-2"/>
                        <AddressForm 
                            address={address}
                            onAddressChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setAddrss((prev) => ({...prev, [e.target.name]: e.target.value}))}
                            InputMode={InputMode}
                        />
                        <div className="d-flex gap-2 mt-4">
                            {InputMode !== EnInputMode.read && (
                                <CButton 
                                    type="submit" 
                                    color="primary" 
                                    disabled={loading}
                                >
                                    <CIcon icon={InputMode === EnInputMode.Add ? cilUserPlus : cilSave} className="me-2" />
                                    {loading ? <CSpinner size="sm" /> : 
                                     (InputMode === EnInputMode.Update ? "Update" : "Register")}
                                </CButton>
                            )}
                            
                            {/* {(InputMode === EnInputMode.Update || InputMode === EnInputMode.read) && (
                                <CButton 
                                    type="button" 
                                    color="danger" 
                                    // onClick={handleDelete}
                                    disabled={loading}
                                >
                                    <CIcon icon={cilTrash} className="me-2" />
                                    {loading ? <CSpinner size="sm" /> : "Delete"}
                                </CButton>
                            )} */}
                        </div>
                    </CForm>

                    <div className="mt-3">
                        <P_Error text={fetchError} />
                        <P_Success text={fetchResult} />
                    </div>
                </CCardBody>
            </CCard>
        </LoadingWrapper>
    );
};

export default CustomerForm;