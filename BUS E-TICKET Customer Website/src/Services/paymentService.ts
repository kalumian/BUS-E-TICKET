import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";



export const ConfirmPayment = async (reservationId : number) : Promise<apiRespone> => 
  apiRequest("GET", `/payment/paypal/executePayment?reservationId=${reservationId}`, "")