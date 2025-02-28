import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";
import CreateBookingDTO from "src/Interfaces/bookingInterface";


export const createBooking = async (bookingDTO : CreateBookingDTO) : Promise<apiRespone> => 
  apiRequest("POST", "booking", "", bookingDTO)

export const getBooking = async (bookingId : string) : Promise<apiRespone> =>
  apiRequest("GET", `booking/${bookingId}`, "")