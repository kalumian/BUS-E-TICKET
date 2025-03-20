import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { TripDisplayDTO } from 'src/Interfaces/tripInterfaces';
import { GetTripsByID } from 'src/Services/tripService';
import { fetchData } from 'src/Services/apiService';
import { CCard, CCardBody, CCardHeader, CCol, CRow } from '@coreui/react';
import P_Error from 'src/components/P_Error';
import LoadingWrapper from 'src/components/LoadingWrapper';
import TripBookingForm from 'src/components/TripBookingForm';

const TripBooking = () => {
  const { id } = useParams();
  const [trip, setTrip] = useState<TripDisplayDTO | null>(null);
  const [error, setError] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    if (id) {
      fetchData(() => GetTripsByID(Number(id)), setTrip, setError, setLoading);
    }
  }, [id]);

  if (error) {
    return <P_Error text={error} />;
  }

  return (
    <LoadingWrapper loading={loading}>
      {trip && (
        <CRow>
          <CCol xs={12}>
            <CCard className="mb-4 shadow-sm">
              <CCardHeader className="bg-light border-bottom">
                <div className="d-flex justify-content-between align-items-center">
                  <h4 className="mb-0">Trip Information</h4>
                  <span className={`badge bg-${trip.tripStatus === 'Active' ? 'success' : 'secondary'} fs-6`}>
                    {trip.tripStatus}
                  </span>
                </div>
              </CCardHeader>
              <CCardBody>
                <CRow>
                  {/* Company Info */}
                  <CCol md={12} className="mb-4 border-bottom pb-3">
                    <div className="d-flex align-items-center">
                      <img
                        src={trip.businessLogoURL}
                        alt={trip.businessName}
                        style={{ width: '80px', height: '80px', objectFit: 'contain' }}
                        className="me-4"
                      />
                      <div>
                        <h5 className="mb-1">{trip.businessName}</h5>
                        <div className="text-muted">{trip.vehicleInfo}</div>
                      </div>
                      <div className="ms-auto text-end">
                        <div className="h2 mb-0 text-primary">{trip.price}</div>
                        <div className="text-muted">{trip.currency}</div>
                      </div>
                    </div>
                  </CCol>

                  {/* Route Information */}
                  <CCol md={4}>
                    <div className="route-container position-relative p-3" 
                         style={{ background: '#f8f9fa', borderRadius: '10px' }}>
                      {/* From Location */}
                      <div className="mb-4 position-relative">
                        <div className="d-flex">
                          <div className="route-indicator position-relative">
                            <div className="rounded-circle bg-primary" 
                                 style={{ width: '16px', height: '16px' }} />
                            <div className="position-absolute" 
                                 style={{ 
                                   left: '7px', 
                                   top: '16px', 
                                   bottom: '-40px', 
                                   width: '2px', 
                                   background: 'var(--cui-primary)' 
                                 }} />
                          </div>
                          <div className="ms-4">
                            <div className="text-muted mb-1">From</div>
                            <div className="h4 mb-1">{trip.startCity}</div>
                            <div className="text-primary">{trip.startLocationName}</div>
                            <small className="text-muted">{trip.startAdditionalDetails}</small>
                          </div>
                        </div>
                      </div>

                      {/* To Location */}
                      <div className="position-relative">
                        <div className="d-flex">
                          <div className="route-indicator">
                            <div className="rounded-circle bg-primary" 
                                 style={{ width: '16px', height: '16px' }} />
                          </div>
                          <div className="ms-4">
                            <div className="text-muted mb-1">To</div>
                            <div className="h4 mb-1">{trip.endCity}</div>
                            <div className="text-primary">{trip.endLocationName}</div>
                            <small className="text-muted">{trip.endAdditionalDetails}</small>
                          </div>
                        </div>
                      </div>
                    </div>
                  </CCol>

                  {/* Time and Seats Info */}
                  <CCol md={8}>
                    <div className="h-100 d-flex justify-content-center">
                      <div className="time-info p-3 rounded">
                        <div className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <i className="fas fa-clock text-primary me-2"></i>
                            <div className="text-muted">Departure</div>
                          </div>
                          <div className="h5 mb-0">
                            {new Date(trip.tripDate).toLocaleTimeString([], { 
                              hour: '2-digit', 
                              minute: '2-digit' 
                            })}
                          </div>
                          <div className="small text-muted">
                            {new Date(trip.tripDate).toLocaleDateString()}
                          </div>
                        </div>

                        <div className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <i className="fas fa-hourglass-half text-primary me-2"></i>
                            <div className="text-muted">Duration</div>
                          </div>
                          <div className="h5 mb-0">{trip.tripDuration}</div>
                        </div>

                        <div>
                          <div className="d-flex align-items-center mb-2">
                            <i className="fas fa-chair text-primary me-2"></i>
                            <div className="text-muted">Available Seats</div>
                          </div>
                          <div className="h5 mb-0">
                             {(trip.totalSeats  && trip.bookedSeatCount) &&  trip.totalSeats - trip.bookedSeatCount}
                          </div>
                        </div>
                      </div>
                    </div>
                  </CCol>
                </CRow>
              </CCardBody>
            </CCard>

            {/* Reservation Form Card */}
                <TripBookingForm TripID = {trip.tripID}/>
          </CCol>
        </CRow>
      )}
    </LoadingWrapper>
  );
};

export default TripBooking;