import EnTripStatus from "src/Enums/EnTripsStatus";
import { LocationDTO } from "./locationInterface";

export interface TripRegistrationDTO {
    tripID?: number;
    vehicleInfo?: string;
    driverInfo?: string;
    tripDate?: Date;
    totalSeats?: number;
    price?: number;
    tripStatus?: EnTripStatus;
    tripDuration?: string; 
    serviceProviderID?: string;
    currencyID: number;
    startLocation: LocationDTO;
    endLocation: LocationDTO;
  }

export interface TripDisplayDTO {
  tripID?: number;
  vehicleInfo?: string;
  driverInfo?: string;
  tripDate: Date;
  arrivalDate: Date;
  price?: number;
  currency?: string;
  tripDuration?: string;
  totalSeats?: number;
  bookedSeatCount?: number;
  tripStatus?: string;

  businessName?: string;
  businessLogoURL?: string;

  startLocationName?: string;
  startLocationURL?: string;
  startCity?: string;
  startAdditionalDetails?: string;

  endLocationName?: string;
  endLocationURL?: string;
  endCity?: string;
  endAdditionalDetails?: string;
}
