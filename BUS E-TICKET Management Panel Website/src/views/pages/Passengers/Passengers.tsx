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
  cilInfo
} from '@coreui/icons';
import { RootState } from 'src/store';
import { Passenger } from "src/Interfaces/passengerInterface";
import { fetchData } from 'src/Services/apiService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { GetAllPassengers } from 'src/Services/passengerService';

const Passengers = () => {
  const [passengers, setPassengers] = useState<Passenger[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const navigate = useNavigate();

  useEffect(() => {
    if (token)
      fetchData(() => GetAllPassengers(token), setPassengers, setError, setLoading);
  }, [token]);

  if (error) return <P_Error text={error} />

  return (
    <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilPeople} className="me-2" size='xl'/>
            Passengers List
          </h3>
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
                <CTableHeaderCell>Actions</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {passengers.map((passenger: Passenger) => (
                <CTableRow key={passenger.person?.personID} className="align-middle">
                  <CTableDataCell>
                    <div className="d-flex align-items-center">
                      <CIcon icon={cilUser} className="me-2 text-primary" />
                      {passenger.person?.firstName} {passenger.person?.lastName}
                    </div>
                  </CTableDataCell>
                  <CTableDataCell>
                    {passenger.person?.contactInformation?.phoneNumber}
                  </CTableDataCell>
                  <CTableDataCell>
                  {passenger.person?.contactInformation?.email}
                  </CTableDataCell>
                   <CTableDataCell>
                    <CButton
                      color="info"
                      size="sm"
                      variant="ghost"
                      onClick={() => navigate(`/passenger/${passenger.person?.nationalID}`)}
                    >
                      <CIcon icon={cilInfo} className="me-1" />
                      Details
                    </CButton>
                  </CTableDataCell>
                </CTableRow>
              ))}
            </CTableBody>
          </CTable>
        </LoadingWrapper>
      </CCardBody>
    </CCard>
  );
};

export default Passengers;