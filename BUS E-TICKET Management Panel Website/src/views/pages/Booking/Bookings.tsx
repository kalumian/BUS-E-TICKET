import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
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
  cilCalendar,
  cilUser,
  cilInfo,
  cilLocationPin,
} from '@coreui/icons';
import { RootState } from 'src/store';
import { BookingDTO } from 'src/Interfaces/bookingInterface';
import { fetchData } from 'src/Services/apiService';
import { GetAllBookings, GetTripBookings } from 'src/Services/bookingService';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';

const Bookings = () => {
  const { id } = useParams();
  const [bookings, setBookings] = useState<BookingDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);
  const navigate = useNavigate();

  useEffect(() => {
    if (token) {
      if (id) {
        fetchData(() => GetTripBookings(token, id), setBookings, setError, setLoading);
      } else {
        fetchData(() => GetAllBookings(token), setBookings, setError, setLoading);
      }
    }
  }, [token, id]);

  const getStatusBadge = (status: string) => {
    const statusMap: { [key: string]: { color: string; label: string } } = {
      'Confirmed': { color: 'success', label: 'Confirmed' },
      'Pending': { color: 'warning', label: 'Pending' },
      'Cancelled': { color: 'danger', label: 'Cancelled' },
      'Completed': { color: 'info', label: 'Completed' }
    };
    return statusMap[status] || { color: 'secondary', label: 'Unknown' };
  };

  if (error) return <P_Error text={error} />

  return (
    <CCard className="shadow-sm">
      <CCardHeader className="bg-light">
        <div className="d-flex justify-content-between align-items-center">
          <h3 className="mb-0">
            <CIcon icon={cilCalendar} className="me-2" size='xl'/>
            {id ? 'Trip Bookings' : 'All Bookings'}
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
                  Passenger
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilLocationPin} className="me-2" />
                  Route
                </CTableHeaderCell>
                <CTableHeaderCell>
                  <CIcon icon={cilCalendar} className="me-2" />
                  Booking Date
                </CTableHeaderCell>
                <CTableHeaderCell>Status</CTableHeaderCell>
                <CTableHeaderCell></CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {bookings.map((booking: BookingDTO) => {
                const status = getStatusBadge(booking.bookingStatus);
                return (
                  <CTableRow key={booking.bookingID} className="align-middle">
                    <CTableDataCell>
                      <div className="d-flex align-items-center">
                        <CIcon icon={cilUser} className="me-2 text-primary" />
                        {booking.passengerName}
                      </div>
                    </CTableDataCell>
                    <CTableDataCell>
                      <CBadge color="info" className="me-2">{booking.fromCity}</CBadge>
                      <CIcon icon={cilLocationPin} className="mx-2" />
                      <CBadge color="info">{booking.toCity}</CBadge>
                    </CTableDataCell>
                    <CTableDataCell>
                      {new Date(booking.bookingDate).toLocaleString('en-GB', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit'
                      })}
                    </CTableDataCell>
                    <CTableDataCell>
                      <CBadge color={status.color}>
                        {status.label}
                      </CBadge>
                    </CTableDataCell>
                    <CTableDataCell>
                      <CButton
                        color="info"
                        size="sm"
                        variant="ghost"
                        onClick={() => navigate(`/passenger/${booking.passengerNationalID}`)}
                      >
                        <CIcon icon={cilInfo} className="me-1" />
                        Passenger Info
                      </CButton>
                      <CButton
                        color="success"
                        size="sm"
                        variant="ghost"
                        onClick={() => navigate(`/booking/${booking.pnr}`)}
                      >
                        <CIcon icon={cilInfo} className="me-1" />
                        Booking Info
                      </CButton>
                      <CButton
                        color="primary"
                        size="sm"
                        variant="ghost"
                        onClick={() => navigate(`/trips/${booking.tripID}`)}
                      >
                        <CIcon icon={cilInfo} className="me-1" />
                        Trip Info
                      </CButton>
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

export default Bookings;