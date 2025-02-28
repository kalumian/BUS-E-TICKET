import React from 'react';
import { CRow, CCol, CCard, CCardBody, CButton } from '@coreui/react';
import { TripDisplayDTO } from 'src/Interfaces/tripInterfaces';
import { formatTripDate, formatTripDuration } from 'src/Services/tripService';
import { useNavigate } from 'react-router-dom';


const TripCard = ({ trip }: { trip: TripDisplayDTO }) => {
  const navigate = useNavigate(); 
    return (
      <CRow className="justify-content-center mb-3">
        <CCol sm="12" md="10" lg="10">
          <CCard
            className="trip-card"
            style={{
              transition: "transform 0.3s ease-in-out",
              boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)"
            }}
          >
            <CCardBody
              className="trip-card-body"
              style={{
                display: "flex",
                alignItems: "center",
                padding: "15px"
              }}
            >
              {/* Logo Section - Left */}
              <div className="me-4" style={{ minWidth: "100px" }}>
                <img
                  src={trip.businessLogoURL || 'https://www.havayolu101.com/wp-content/uploads/2008/02/Kamil_koc_logo.jpg'}
                  alt={trip.businessName}
                  className="img-fluid"
                  style={{ width: "80px", height: "80px", objectFit: "contain" }}
                />
              </div>
  
              {/* Time and Duration Section */}
              <div className="me-4" style={{ minWidth: "150px" }}>
                <div className="fw-bold">{formatTripDate(trip.tripDate)}</div>
                <div style={{ fontSize: "0.9rem", color: "#666" }}>
                {formatTripDuration(trip.tripDate, trip.arrivalDate)}
              </div>
              </div>
  
              {/* Route Section */}
              <div className="me-4" style={{ flex: 1 }}>
                <div className="fw-bold fs-5">
                  {trip.startCity} → {trip.endCity}
                </div>
                <div style={{ fontSize: "0.9rem", color: "#666" }}>
                  {`${trip.startLocationName} - ${trip.endLocationName}`}
                </div>
              </div>
  
              {/* Price Section */}
              <div className="me-4 text-end">
                <div className="fw-bold fs-5">
                  {trip.price} {trip.currency}
                </div>
                <div style={{ fontSize: "0.9rem", color: "#666" }}>
                  {trip.availableSeatsCount} seats left
                </div>
              </div>
  
              {/* Book Button */}
              <div>
                <CButton 
                  color="primary"
                  onClick={()=> navigate(`${trip.tripID}`)}
                >
                  Book Ticket
                </CButton>
              </div>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    
    );
  };


  export default TripCard;