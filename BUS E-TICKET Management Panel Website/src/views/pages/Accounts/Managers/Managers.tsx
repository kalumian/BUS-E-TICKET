import { useState, useEffect } from 'react';
import {
  CTable, CTableHead, CTableRow, CTableHeaderCell, CTableBody, CTableDataCell,
  CButton, CCard, CCardHeader, CCardBody, CBadge
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import { 
  cilUser, 
  cilUserPlus, 
  cilPencil, 
  cilInfo,
  cilMagnifyingGlass 
} from '@coreui/icons';
import { GetManagers } from "../../../../Services/accountService";
import { useSelector } from 'react-redux';
import { RootState } from 'src/store';
import { Manager } from 'src/Interfaces/accountInterfaces';
import P_Error from 'src/components/P_Error';
import { useNavigate } from 'react-router-dom';
import { fetchData } from 'src/Services/apiService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import EnAccountStatus from 'src/Enums/EnAccountStatus';

const getStatusBadge = (status: number) => {
  const statusMap: { [key: number]: { color: string; label: string } } = {
    [EnAccountStatus.Active]: { color: 'success', label: 'Active' },
    [EnAccountStatus.Inactive]: { color: 'danger', label: 'Inactive' },
    [EnAccountStatus.PendingVerification]: { color: 'warning', label: 'Pending' },
    [EnAccountStatus.Deleted]: { color: 'dark', label: 'Deleted' },
  };
  return statusMap[status] || { color: 'secondary', label: 'Unknown' };
};

const ManagerAccounts = () => {
  const [managers, setManagers] = useState<Manager[]>();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const navigate = useNavigate();

  useEffect(() => {
    if(token)
      fetchData(() => GetManagers(token), setManagers, setError, setLoading, {errorMessage:"Failed to load managers"})
  }, [token]);

  if(error) return <P_Error text={error}/>

  return (
    <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilUser} className="me-2" />
            Manager Accounts
          </h3>
          <CButton 
            color="primary"
            onClick={() => navigate("/account/managers/add")}
          >
            <CIcon icon={cilUserPlus} className="me-2" />
            Add New Manager
          </CButton>
        </div>
      </CCardHeader>
      <CCardBody>
        <LoadingWrapper loading={loading}>
          <CTable hover responsive className="border">
            <CTableHead>
              <CTableRow className="bg-light">
                <CTableHeaderCell>Username</CTableHeaderCell>
                <CTableHeaderCell>Email</CTableHeaderCell>
                <CTableHeaderCell>Phone</CTableHeaderCell>
                <CTableHeaderCell>Registration Date</CTableHeaderCell>
                <CTableHeaderCell>Status</CTableHeaderCell>
                <CTableHeaderCell>Created By</CTableHeaderCell>
                <CTableHeaderCell>Actions</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {managers?.map((manager: Manager) => {
                const status = getStatusBadge(Number(manager.accountStatus));
                return (
                  <CTableRow key={manager.managerID} className="align-middle">
                    <CTableDataCell>
                      <div className="d-flex align-items-center">
                        <CIcon icon={cilUser} className="me-2 text-primary" />
                        {manager.userName}
                      </div>
                    </CTableDataCell>
                    <CTableDataCell>{manager.email}</CTableDataCell>
                    <CTableDataCell>{manager.phoneNumber}</CTableDataCell>
                    <CTableDataCell>
                      {manager.registerationDate ? 
                        new Date(manager.registerationDate).toLocaleString('en-US', 
                          { year: 'numeric', month: 'long', day: 'numeric' }
                        ) : null}
                    </CTableDataCell>
                    <CTableDataCell>
                      <CBadge color={status.color}>{status.label}</CBadge>
                    </CTableDataCell>
                    <CTableDataCell>Admin</CTableDataCell>
                    <CTableDataCell>
                      <div className="d-flex gap-2">
                        <CButton
                          color="info"
                          size="sm"
                          variant="ghost"
                          onClick={() => navigate(`/account/details/${manager.accountID}`)}
                        >
                          <CIcon icon={cilInfo} className="me-1" />
                          Details
                        </CButton>
                        <CButton
                          color="primary"
                          size="sm"
                          variant="ghost"
                          onClick={() => navigate(`/account/managers/update/${manager.accountID}`)}
                        >
                          <CIcon icon={cilPencil} className="me-1" />
                          Edit
                        </CButton>
                      </div>
                    </CTableDataCell>
                  </CTableRow>
                );
              })}
            </CTableBody>
          </CTable>
        </LoadingWrapper>
      </CCardBody>
    </CCard>
  );
};

export default ManagerAccounts;