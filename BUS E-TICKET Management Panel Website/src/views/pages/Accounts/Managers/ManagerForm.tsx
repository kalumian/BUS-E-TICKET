import { cilArrowCircleLeft, cilSave, cilTrash, cilUser, cilUserPlus } from "@coreui/icons";
import CIcon from "@coreui/icons-react";
import { CButton, CCard, CCardBody, CCardHeader, CForm, CFormInput, CSpinner } from "@coreui/react-pro";
import { Suspense, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Navigate, useLocation, useNavigate, useParams } from "react-router-dom";
import AccountForm from "src/components/AccountForm";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import P_Success from "src/components/P_Success";
import EnAccountStatus from "src/Enums/EnAccountStatus";
import { EnInputMode } from "src/Enums/EnInputMode";
import { Account, Manager, RegisterManagerAccountDTO } from "src/Interfaces/accountInterfaces";
import { User } from "src/Interfaces/userInterfaces";
import { AddManager, DeleteManager, GetManagerByID, UpdateManager } from "src/Services/accountService";
import { fetchData } from "src/Services/apiService";
import { RootState } from "src/store";

const ManagerForm = () => {

    const { id } = useParams();
    const location = useLocation();
    const [InputMode, setInputMode] = useState<EnInputMode>(EnInputMode.Add);

    const initAccount = {
        email: "",
        userName: "",
        password: "",
        phoneNumber: "",
        status: EnAccountStatus.Active,
    };

    const [account, setAccount] = useState<Account>(initAccount);
    const [manager, setManager] = useState<Manager>({});
    const [loading, setLoading] = useState<boolean>(false);
    const [fetchError, setFetchError] = useState('');
    const [fetchResult, setFetchResult] = useState('');
    const [error, setError] = useState('');
    const token: string | null = useSelector((state: RootState) => state.auth.token);
    const user: User | null = useSelector((state: RootState) => state.auth.user);
    const navigate = useNavigate();
    useEffect(() => {
        setError("");
        setFetchError("");
        setFetchResult("");        
        if (token && id) {
            setInputMode(EnInputMode.Update);
            fetchData(() => GetManagerByID(token, id), setAccount, setError, setLoading);
        } else {
            setInputMode(EnInputMode.Add);
            setAccount(initAccount);
        }

        if (location.pathname.includes("details")) {
            setInputMode(EnInputMode.read);
        }
    }, [id, token, location.pathname]);

    const AddManagerHandle = async () => {
      if (!account?.email || !account?.password || !account?.userName) {
          setFetchError("All fields are required!");
          return;
      }
  
      const NewManager: RegisterManagerAccountDTO = {
          account: {
              accountId: "",
              email: account.email,
              password: account.password,
              userName: account.userName,
              phoneNumber: account.phoneNumber,
              status: account.status, // Include status
          },
          createdByID: user?.UserID,
      };
  
      if (token) 
        fetchData(async () => {
          const res = await AddManager(token, NewManager);
          setAccount(initAccount);
          res.message && setFetchResult(res.message);
          navigate(-1)
        },undefined,setFetchError,setLoading)
  };
  
  const UpdateManagerHandle = async () => {
      if (!token || !id) return;
  
      const UpdatedManager: RegisterManagerAccountDTO = {
          account: {
              accountId: id,
              email: account?.email,
              password: account?.password,
              userName: account?.userName,
              phoneNumber: account?.phoneNumber,
              status: account?.status, // Include status
          },
          createdByID: user?.UserID,
      };
  
      if (token) 
        fetchData(async () => {
          const res = await UpdateManager(token, UpdatedManager);
          if (res.message) setFetchResult(res.message);
          navigate(-1)
        },undefined,setFetchError,setLoading)
  };
  
  const DeleteManagerHandle = async () => {
    if (!token || !id ) return;
    try {
      if(!account.accountID) throw new Error("Manager Account Id Is Not Exisit")
        const res = await DeleteManager(token, account.accountID );
        if (res.message) setFetchResult(res.message);
        navigate(-1)
    } catch (error) {
        setFetchError("Error deleting manager.");
    }
};
    const Submit = (e: React.MouseEvent | any) => {
        e.preventDefault();
        setFetchError("");
        setFetchResult("");

        switch (InputMode) {
            case EnInputMode.Add:
                AddManagerHandle();
                break;
            case EnInputMode.Update:
                UpdateManagerHandle();
                break;
        }
    };

    if (!user) {
        return <Navigate to="/login" />;
    }

    return (
        <LoadingWrapper loading={loading}>
        <CCard className="shadow-sm">
            <CCardHeader className="bg-light">
                <div className="d-flex justify-content-between align-items-center">
                    <h3 className="mb-0">
                        <CIcon icon={InputMode === EnInputMode.Add ? cilUserPlus : cilUser} 
                              className="me-2" /> 
                        {InputMode === EnInputMode.Add ? "Add New Manager" : 
                         InputMode === EnInputMode.Update ? "Update Manager" : "Manager Details"}
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
                    <AccountForm 
                        account={account} 
                        onAccountChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setAccount((prev) => ({...prev, [e.target.name]: e.target.value}))}
                        InputMode={InputMode} 
                    />
                    <CFormInput
                        type="text"
                        name="CreatedBy"
                        label="Created By"
                        disabled 
                        value={InputMode === EnInputMode.Add ? 
                               String(user?.Username) : manager?.createdBy}
                        required 
                    />  

                    <div className="d-flex gap-2 mt-4">
                        {InputMode !== EnInputMode.read && (
                            <CButton 
                                type="submit" 
                                color="primary" 
                                onClick={Submit} 
                                disabled={loading}
                            >
                                <CIcon icon={InputMode === EnInputMode.Add ? 
                                       cilUserPlus : cilSave} 
                                       className="me-2" />
                                {loading ? <CSpinner size="sm" /> : 
                                 (InputMode === EnInputMode.Update ? "Update" : "Add")}
                            </CButton>
                        )}
                       
                        {(InputMode === EnInputMode.Update || 
                          InputMode === EnInputMode.read) && 
                            <CButton 
                                type="button" 
                                color="danger" 
                                onClick={DeleteManagerHandle}
                            >
                                <CIcon icon={cilTrash} className="me-2" />
                                {loading ? <CSpinner size="sm" /> : "Delete"}
                            </CButton>
                        }
                    </div>
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

export default ManagerForm;
