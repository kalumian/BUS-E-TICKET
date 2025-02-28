// src/components/ServiceProviderApplication.tsx
import { useState, useEffect } from 'react';
import { CTable, CTableHead, CTableRow, CTableHeaderCell, CTableBody, CTableDataCell, CButton } from '@coreui/react-pro';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store';
import P_Error from 'src/components/P_Error';
import { useNavigate } from 'react-router-dom';
import LoadingWrapper from 'src/components/LoadingWrapper';
import { RegisterationApplication, SPRegApplicationDisplayDTO } from 'src/Interfaces/applicationInterface';
import { fetchData } from 'src/Services/apiService';
import { GetAllSPApplications } from 'src/Services/spapplicationService';
import EnApplicationStatus from 'src/Enums/EnApplication';
import {
  CCard,
  CCardHeader,
  CCardBody,
  CBadge
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import {
  cilBuilding,
  cilNotes,
  cilInfo,
  cilUser,
  cilPhone,
  cilCalendar,
  cilMagnifyingGlass
} from '@coreui/icons';


const ServiceProviderApplication = () => {
  const [ServiceProviderApplication, setServiceProviderApplication] = useState<SPRegApplicationDisplayDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const navigate = useNavigate();
  const getStatusBadge = (status: number) => {
    const statusMap: { [key: number]: { color: string; label: string } } = {
      2: { color: 'success', label: 'Approved' },
      0: { color: 'warning', label: 'Pending' },
      1: { color: 'danger', label: 'Rejected' }
    };
    return statusMap[status] || { color: 'secondary', label: 'Unknown' };
  };
  useEffect(() => {
    if (token) {
      fetchData(()=> GetAllSPApplications(token), setServiceProviderApplication, setError, setLoading, { errorMessage: "Failed to load service providers" })
    }

  }, [token]);

  if (error) return <P_Error text={error} />;

  return (
    <CCard className="shadow-sm" >
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilBuilding} className="me-2" />
            Service Provider Applications
          </h3>
          <div className="d-flex gap-2">
            {/* Add any additional header buttons here if needed */}
          </div>
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
                <CTableHeaderCell>
                  <CIcon icon={cilUser} className="me-2" />
                  Username
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilNotes} className="me-2" />
                  Status
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilPhone} className="me-2" />
                  Business Phone
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilCalendar} className="me-2" />
                  Request Date
                </CTableHeaderCell>
                <CTableHeaderCell>Actions</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {ServiceProviderApplication.map((application: SPRegApplicationDisplayDTO) => {
                const status = getStatusBadge(application.applicationStatus || 0);
                return (
                  <CTableRow key={application.spRegRequestID} className="align-middle">
                    <CTableDataCell>
                      <div className="d-flex align-items-center">
                        <CIcon icon={cilBuilding} className="me-2 text-primary" />
                        {application.businessName}
                      </div>
                    </CTableDataCell>
                    <CTableDataCell>{application.userName}</CTableDataCell>
                    <CTableDataCell>
                      <CBadge color={status.color}>{status.label}</CBadge>
                    </CTableDataCell>
                    <CTableDataCell>{application.businessPhoneNumber}</CTableDataCell>
                    <CTableDataCell>
                      {new Date(application.requestDate).toLocaleString('en-GB', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit'
                      })}
                    </CTableDataCell>
                    <CTableDataCell>
                      <div className="d-flex gap-2">
                        <CButton
                          color="info"
                          size="sm"
                          variant="ghost"
                          onClick={() => navigate(`/account/registration-applications/details/${application.spRegRequestID}`)}
                        >
                          <CIcon icon={cilMagnifyingGlass} className="me-1" />
                          Details
                        </CButton>
                        <CButton
                          color="primary"
                          size="sm"
                          variant="ghost"
                          onClick={() => navigate(`/account/registration-applications/review/${application.spRegRequestID}`)}
                          disabled={application.applicationStatus !== 0}
                        >
                          <CIcon icon={cilNotes} className="me-1" />
                          Review
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

export default ServiceProviderApplication;
