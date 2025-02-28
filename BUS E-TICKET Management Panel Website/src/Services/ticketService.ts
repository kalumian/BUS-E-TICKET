import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";

export const getTicket = async (reservationId : number) : Promise<apiRespone> => 
  apiRequest("GET", `/Ticket/GetByReservation/${reservationId}`, "")