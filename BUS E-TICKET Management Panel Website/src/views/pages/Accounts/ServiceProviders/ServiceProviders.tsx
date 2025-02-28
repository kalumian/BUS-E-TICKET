import { useState, useEffect } from 'react';
import {
  CTable, CTableHead, CTableRow, CTableHeaderCell, CTableBody, CTableDataCell,
  CButton, CCard, CCardHeader, CCardBody, CBadge
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import { 
  cilBuilding,
  cilPencil, 
  cilInfo,
  cilPlus 
} from '@coreui/icons';
import { GetServiceProviders } from "../../../../Services/accountService";
import { useSelector } from 'react-redux';
import { RootState } from 'src/store';
import { ServiceProvider } from 'src/Interfaces/accountInterfaces';
import P_Error from 'src/components/P_Error';
import { useNavigate } from 'react-router-dom';
import { fetchData } from 'src/Services/apiService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import EnAccountStatus from 'src/Enums/EnAccountStatus';

const getStatusBadge = (status: number) => {
  const statusMap: { [key: number]: { color: string, label: string } } = {
    [EnAccountStatus.Active]: { color: 'success', label: 'Active' },
    [EnAccountStatus.Inactive]: { color: 'danger', label: 'Inactive' },
    [EnAccountStatus.PendingVerification]: { color: 'warning', label: 'Pending' },
    [EnAccountStatus.Deleted]: { color: 'dark', label: 'Deleted' },
  };
  return statusMap[status] || { color: 'secondary', label: 'Unknown' };
};

const ServiceProviderAccounts = () => {
  const [serviceProviders, setServiceProviders] = useState<ServiceProvider[]>();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const navigate = useNavigate();

  useEffect(() => {
    if (token)
      fetchData(() => GetServiceProviders(token), setServiceProviders, setError, setLoading);
  }, [token]);

  if (error) return <P_Error text={error} />

  return (
    <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilBuilding} className="me-2" />
            Service Provider Accounts
          </h3>
        </div>
      </CCardHeader>
      <CCardBody>
        <LoadingWrapper loading={loading}>
          <CTable hover responsive className="border">
            <CTableHead>
              <CTableRow className="bg-light">
                <CTableHeaderCell>
                  <CIcon icon={cilBuilding} className="me-2" />
                  Business Name
                </CTableHeaderCell>
                <CTableHeaderCell>License Number</CTableHeaderCell>
                <CTableHeaderCell>Username</CTableHeaderCell>
                <CTableHeaderCell>Registration Date</CTableHeaderCell>
                <CTableHeaderCell>Status</CTableHeaderCell>
                <CTableHeaderCell>Actions</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {serviceProviders?.map((provider: ServiceProvider) => {
                const status = getStatusBadge(Number(provider.account?.accountStatus));
                return (
                  <CTableRow key={provider.serviceProviderID} className="align-middle">
                    <CTableDataCell>
                      <div className="d-flex align-items-center">
                        <CIcon icon={cilBuilding} className="me-2 text-primary" />
                        {provider.business?.businessName}
                      </div>
                    </CTableDataCell>
                    <CTableDataCell>{provider.business?.businessLicenseNumber}</CTableDataCell>
                    <CTableDataCell>{provider.account?.userName}</CTableDataCell>
                    <CTableDataCell>
                      {provider.account?.registerationDate ? 
                        new Date(provider.account?.registerationDate).toLocaleString('en-US', 
                          { year: 'numeric', month: 'long', day: 'numeric' }
                        ) : null}
                    </CTableDataCell>
                    <CTableDataCell>
                      <CBadge color={status.color}>{status.label}</CBadge>
                    </CTableDataCell>
                    <CTableDataCell>
                      <div className="d-flex gap-2">
                        <CButton
                          color="info"
                          size="sm"
                          variant="ghost"
                          onClick={() => navigate(`/account/service-providers/details/${provider.account?.accountId}`)}
                        >
                          <CIcon icon={cilInfo} className="me-1" />
                          Details
                        </CButton>
                        <CButton
                          color="primary"
                          size="sm"
                          variant="ghost"
                          onClick={() => navigate(`/account/service-providers/update/${provider.account?.accountId}`)}
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

export default ServiceProviderAccounts;