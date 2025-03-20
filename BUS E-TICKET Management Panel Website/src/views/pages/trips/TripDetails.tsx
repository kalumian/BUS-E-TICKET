import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import LoadingWrapper from "src/components/LoadingWrapper";
import P_Error from "src/components/P_Error";
import { TripDisplayDTO } from "src/Interfaces/tripInterfaces";
import { fetchData } from "src/Services/apiService";
import { GetTripsByID } from "src/Services/tripService"; 
import {
  CCard,
  CCardHeader,
  CCardBody,
  CButton,
  CRow,
  CCol,
  CBadge,
  CImage
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import {
  cilCarAlt,
  cilArrowCircleLeft,
  cilLocationPin,
  cilClock,
  cilMoney,
  cilPeople,
  cilMap,
  cilUser,
  cilInfo
} from '@coreui/icons';


const TripDetails = () => {
  const { id } = useParams();
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState(false);
  const [tripDetails, setTripDetails] = useState<TripDisplayDTO>(); 
  const [fetchMessage, setFetchMessage] = useState('');
  const [fetchError, setFetchError] = useState('');

  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      fetchData(
        () => GetTripsByID(Number(id)),
        setTripDetails,
        setError,
        setLoading
      );
    }
  }, [id]);



  return (
    <>
      {!error ? (
        <LoadingWrapper loading={loading}>
          <CCard className="shadow-sm">
            <CCardHeader className="bg-light">
              <div className="d-flex justify-content-between align-items-center">
                <h3 className="mb-0">
                  <CIcon icon={cilCarAlt} className="me-2" />
                  Trip Details
                </h3>
             <div>
             <CButton 
                  color="info" 
                  variant="ghost"
                  onClick={()=>navigate("/trips")}
                >
                  <CIcon icon={cilArrowCircleLeft} className="me-2" />
                  Trips
                </CButton>
                    <CButton
                  color="success"
                  size="sm"
                  variant="ghost"
                  onClick={() => navigate(`/bookings/${tripDetails?.tripID}`)}
                >
                  <CIcon icon={cilInfo} className="me-1" />
                  Bookings
                </CButton>
                <CButton 
                  color="primary" 
                  variant="ghost"
                  onClick={()=>navigate(-1)}
                >
                  <CIcon icon={cilArrowCircleLeft} className="me-2" />
                  Back
                </CButton>
             </div>
              </div>
            </CCardHeader>
            <CCardBody>
              <CRow>
                {/* Company Information */}
                <CCol md={4} className="mb-4">
                  <CCard className="h-100">
                    <CCardHeader className="bg-light">
                      <h5 className="mb-0">Company Information</h5>
                    </CCardHeader>
                    <CCardBody className="text-center">
                      <CImage
                        src={tripDetails?.businessLogoURL}
                        width={150}
                        className="mb-3"
                      />
                      <h4>{tripDetails?.businessName}</h4>
                    </CCardBody>
                  </CCard>
                </CCol>

                {/* Trip Basic Info */}
                <CCol md={8} className="mb-4">
                  <CCard className="h-100">
                    <CCardHeader className="bg-light">
                      <h5 className="mb-0">Trip Information</h5>
                    </CCardHeader>
                    <CCardBody>
                      <CRow>
                        <CCol sm={6} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilClock} className="me-2 text-primary" />
                            <strong>Departure:</strong>
                          </div>
                          <p>{tripDetails?.tripDate ? 
                            new Date(tripDetails.tripDate).toLocaleString('en-GB', {
                              dateStyle: 'full',
                              timeStyle: 'short'
                            }) : "Not Selected"}
                          </p>
                        </CCol>
                        <CCol sm={6} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilClock} className="me-2 text-primary" />
                            <strong>Arrival:</strong>
                          </div>
                          <p>{tripDetails?.arrivalDate ? 
                            new Date(tripDetails.arrivalDate).toLocaleString('en-GB', {
                              dateStyle: 'full',
                              timeStyle: 'short'
                            }) : "Not Available"}
                          </p>
                        </CCol>
                        <CCol sm={6} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilMoney} className="me-2 text-success" />
                            <strong>Price:</strong>
                          </div>
                          <CBadge color="success">
                            {tripDetails?.price} {tripDetails?.currency}
                          </CBadge>
                        </CCol>
                        <CCol sm={6} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilPeople} className="me-2 text-info" />
                            <strong>Seats:</strong>
                          </div>
                          <div>
                            <CBadge color="info" className="me-2">
                              Available: {tripDetails?.totalSeats} / {tripDetails?.bookedSeatCount}
                            </CBadge>
                            <CBadge color="secondary">
                              Total: {tripDetails?.totalSeats}
                            </CBadge>
                          </div>
                        </CCol>
                      </CRow>
                    </CCardBody>
                  </CCard>
                </CCol>

                {/* Locations */}
                <CCol md={12} className="mb-4">
                  <CCard>
                    <CCardHeader className="bg-light">
                      <h5 className="mb-0">
                        <CIcon icon={cilLocationPin} className="me-2" />
                        Route Information
                      </h5>
                    </CCardHeader>
                    <CCardBody>
                      <CRow>
                        <CCol md={6} className="mb-3">
                          <h6 className="text-primary">Departure Location</h6>
                          <div className="ps-3 border-start border-primary">
                            <p className="mb-1"><strong>{tripDetails?.startLocationName}</strong></p>
                            <p className="mb-1">{tripDetails?.startCity}</p>
                            <p className="mb-1">{tripDetails?.startAdditionalDetails}</p>
                            <CButton
                              color="link"
                              href={tripDetails?.startLocationURL}
                              target="_blank"
                              className="p-0"
                            >
                              <CIcon icon={cilMap} className="me-1" />
                              View on Map
                            </CButton>
                          </div>
                        </CCol>
                        <CCol md={6} className="mb-3">
                          <h6 className="text-primary">Arrival Location</h6>
                          <div className="ps-3 border-start border-primary">
                            <p className="mb-1"><strong>{tripDetails?.endLocationName}</strong></p>
                            <p className="mb-1">{tripDetails?.endCity}</p>
                            <p className="mb-1">{tripDetails?.endAdditionalDetails}</p>
                            <CButton
                              color="link"
                              href={tripDetails?.endLocationURL}
                              target="_blank"
                              className="p-0"
                            >
                              <CIcon icon={cilMap} className="me-1" />
                              View on Map
                            </CButton>
                          </div>
                        </CCol>
                      </CRow>
                    </CCardBody>
                  </CCard>
                </CCol>

                {/* Additional Info */}
                <CCol md={12}>
                  <CCard>
                    <CCardHeader className="bg-light">
                      <h5 className="mb-0">Additional Information</h5>
                    </CCardHeader>
                    <CCardBody>
                      <CRow>
                        <CCol md={4} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilUser} className="me-2 text-primary" />
                            <strong>Driver Info:</strong>
                          </div>
                          <p>{tripDetails?.driverInfo}</p>
                        </CCol>
                        <CCol md={4} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilClock} className="me-2 text-primary" />
                            <strong>Duration:</strong>
                          </div>
                          <CBadge color="info">{tripDetails?.tripDuration}</CBadge>
                        </CCol>
                        <CCol md={4} className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <CIcon icon={cilCarAlt} className="me-2 text-primary" />
                            <strong>Status:</strong>
                          </div>
                          <CBadge color={
                            tripDetails?.tripStatus === "isWaiting" ? "warning" : "success"
                          }>
                            {tripDetails?.tripStatus === "isWaiting" ? "Waiting" : tripDetails?.tripStatus}
                          </CBadge>
                        </CCol>
                      </CRow>
                    </CCardBody>
                  </CCard>
                </CCol>
              </CRow>
            </CCardBody>
          </CCard>
        </LoadingWrapper>
      ) : (
        <P_Error text={error} />
      )}
    </>
  );
};

export default TripDetails;
