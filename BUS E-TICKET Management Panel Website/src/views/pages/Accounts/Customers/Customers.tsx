import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import {
  CCard,
  CCardHeader,
  CCardBody,
  CTable,
  CTableHead,
  CTableRow,
  CTableHeaderCell,
  CTableBody,
  CTableDataCell,
  CButton,
  CBadge
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import {
  cilPeople,
  cilUser,
  cilPhone,
  cilEnvelopeClosed,
  cilCalendar,
  cilPencil,
  cilInfo,
  cilUserPlus
} from '@coreui/icons';
import { RootState } from 'src/store';
import { fetchData } from 'src/Services/apiService';
import { GetAllCustomers } from 'src/Services/accountService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { Customer } from 'src/Interfaces/accountInterfaces';
import EnAccountStatus from 'src/Enums/EnAccountStatus';

const Customers = () => {
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const navigate = useNavigate();

  useEffect(() => {
    if (token)
      fetchData(() => GetAllCustomers(token), setCustomers, setError, setLoading);
  }, [token]);

  const getStatusBadge = (status: number) => {
    const statusMap: { [key: number]: { color: string; label: string } } = {
      [EnAccountStatus.Active]: { color: 'success', label: 'Active' },
      [EnAccountStatus.Inactive]: { color: 'danger', label: 'Inactive' },
      [EnAccountStatus.PendingVerification]: { color: 'warning', label: 'Pending' },
      [EnAccountStatus.Deleted]: { color: 'dark', label: 'Deleted' },
    };
    return statusMap[status] || { color: 'secondary', label: 'Unknown' };
  };
  if (error) return <P_Error text={error} />;

  return (
    <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilPeople} className="me-2" size='xl'/>
            Customers
          </h3>
          <CButton 
            color="primary"
            onClick={() => navigate('/account/customers/add')}
          >
            <CIcon icon={cilUserPlus} className="me-2" />
            Add New Customer
          </CButton>
        </div>
      </CCardHeader>
      <CCardBody>
        <LoadingWrapper loading={loading}>
          <CTable hover responsive className="border">
            <CTableHead>
              <CTableRow className="bg-light">
                <CTableHeaderCell>
                  <CIcon icon={cilUser} className="me-2" />
                  Full Name
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilPhone} className="me-2" />
                  Phone Number
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilEnvelopeClosed} className="me-2" />
                  Email
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilCalendar} className="me-2" />
                  Join Date
                </CTableHeaderCell>
                <CTableHeaderCell>Status</CTableHeaderCell>
                <CTableHeaderCell>Actions</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {customers.map((customer) => {
                const status = getStatusBadge(Number(customer.account?.accountStatus));
                return (
                  <CTableRow key={customer.account.accountId} className="align-middle">
                    <CTableDataCell>
                      <div className="d-flex align-items-center">
                        <CIcon icon={cilUser} className="me-2 text-primary" />
                        {customer.person.firstName} {customer.person.lastName}
                      </div>
                    </CTableDataCell>
                    <CTableDataCell>{customer.person.contactInformation?.phoneNumber}</CTableDataCell>
                    <CTableDataCell>{customer.person.contactInformation?.email}</CTableDataCell>
                    <CTableDataCell>
                      {customer.account.registerationDate ? new Date(customer.account.registerationDate).toLocaleString('en-GB', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric'
                      }) : 'N/A'}
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
                          onClick={() => navigate(`/account/customers/details/${customer.account.accountId}`)}
                        >
                          <CIcon icon={cilInfo} className="me-1" />
                          Details
                        </CButton>
                        <CButton
                          color="primary"
                          size="sm"
                          variant="ghost"
                          // onClick={() => navigate(`/account/customers/edit/${customer.customerID}`)}
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

export default Customers;