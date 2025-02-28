import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";

export const getInvoice = async (reservationId : number) : Promise<apiRespone> => 
  apiRequest("GET", `/Invoice/GetByReservation/${reservationId}`, "")