import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  CTable, 
  CTableHead, 
  CTableRow, 
  CTableHeaderCell, 
  CTableBody, 
  CTableDataCell, 
  CButton,
  CCard,
  CCardHeader,
  CCardBody,
  CBadge
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import { 
  cilBrush,
  cilLocationPin,
  cilClock,
  cilPeople,
  cilInfo,
  cilPlus,
  cilBuilding
} from '@coreui/icons';
import { GetAllTrips } from 'src/Services/tripService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { TripDisplayDTO } from 'src/Interfaces/tripInterfaces';
import { fetchData } from 'src/Services/apiService';
import { RootState } from 'src/store';
import { useSelector } from 'react-redux';
import { User } from 'src/Interfaces/userInterfaces';
import UserRole from 'src/Enums/EnUserRole';

const Trips = () => {
  const [trips, setTrips] = useState<TripDisplayDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const user : User | null  = useSelector((state: RootState) => state.auth.user);

  useEffect(() => {
    if(user?.UserID)
      fetchData(()=> GetAllTrips(user?.UserID), setTrips, setError, setLoading)
  }, [user?.UserID]);

  if (error) return <P_Error text={error} />;

  return (
    <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilBuilding} className="me-2" />
            Trips Management
          </h3>
        {user?.Role === UserRole.Provider && <>
          <CButton 
            color="primary"
            onClick={() => navigate("/trips/add")}
          >
            <CIcon icon={cilPlus} className="me-2" />
            Add New Trip
          </CButton>
        </> }
        </div>
      </CCardHeader>
      <CCardBody>
        <LoadingWrapper loading={loading}>
          <CTable hover responsive className="border">
            <CTableHead>
              <CTableRow className="bg-light">
                <CTableHeaderCell>
                  <CIcon icon={cilBrush} className="me-2" />
                  Company
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilLocationPin} className="me-2" />
                  From - To
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilClock} className="me-2" />
                  Schedule
                </CTableHeaderCell>
                <CTableHeaderCell>Duration</CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilPeople} className="me-2" />
                  Seats
                </CTableHeaderCell>
                <CTableHeaderCell></CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {trips.map(trip=> (
                <CTableRow key={trip.tripID} className="align-middle">
                  <CTableDataCell>
                    <div className="d-flex align-items-center">
                      <CIcon icon={cilBrush} className="me-2 text-primary" />
                      {trip.businessName}
                    </div>
                  </CTableDataCell>
                  <CTableDataCell>
                    <CBadge color="info" className="me-2">{trip.startCity}</CBadge>
                    <CIcon icon={cilLocationPin} className="mx-2" />
                    <CBadge color="info">{trip.endCity}</CBadge>
                  </CTableDataCell>
                  <CTableDataCell >
                    <div>
                      <small className="text-medium-emphasis">Departure:</small><br/>
                      {new Date(trip.tripDate).toLocaleString('en-GB', { 
                        timeZone: 'Europe/Istanbul',
                        dateStyle: 'medium',
                        timeStyle: 'short'
                      })}
                      <br/>
                      <small className="text-medium-emphasis">Arrival:</small><br/>
                      {new Date(trip.arrivalDate).toLocaleString('en-GB', { 
                        timeZone: 'Europe/Istanbul',
                        dateStyle: 'medium',
                        timeStyle: 'short'
                      })}
                    </div>
                  </CTableDataCell>
                  <CTableDataCell>
                    <CBadge color="success">{trip.tripDuration}</CBadge>
                  </CTableDataCell>
                  <CTableDataCell>
                    <CBadge color="primary">{trip.totalSeats} seats</CBadge>
                  </CTableDataCell>
                  <CTableDataCell>
                    <CButton
                      color="info"
                      size="sm"
                      variant="ghost"
                      onClick={() => navigate(`/trips/${trip.tripID}`)}
                    >
                      <CIcon icon={cilInfo} className="me-2" />
                      Details
                    </CButton>
                    <CButton
                    color="success"
                    size="sm"
                    variant="ghost"
                    onClick={() => navigate(`/bookings/${trip.tripID}`)}
                  >
                    <CIcon icon={cilInfo} className="me-1" />
                    Bookings
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

export default Trips;