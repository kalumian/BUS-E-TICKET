import EnPaymentMethod from "src/Enums/EnPaymentMethod";
import { Passenger } from "./passengerInterface";
import ContactInformation from "./contactInterface";

interface CreateBookingDTO {
    reservationID?: number;
    tripID: number;
    customerID?: number;
    passenger?: Passenger;
    paymentMethod: EnPaymentMethod;
  }
  
  export interface BookingDTO {
    bookingID: number;
    tripID: number;
    bookingStatus: string;
    bookingDate: Date;
    passengerName: string;
    passengerNationalID: string;
    tripDate: Date;
    fromCity: string;
    toCity: string;
  }
  export default CreateBookingDTO