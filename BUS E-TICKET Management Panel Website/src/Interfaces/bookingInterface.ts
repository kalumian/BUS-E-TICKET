import { Passenger } from "./passengerInterface";
import ContactInformation from "./contactInterface";
import EnPaymentMethod from "src/Enums/EnPaymentMethod";

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
    pnr: string;
    toCity: string;
  }
  export default CreateBookingDTO