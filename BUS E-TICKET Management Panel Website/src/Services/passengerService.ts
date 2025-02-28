import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";

export const GetAllPassengers = async (NationalID : string) : Promise<apiRespone> => 
  apiRequest("GET", `/passenger`, "")


export const GetPassengerByID = async (Token: string, BookingID : string) : Promise<apiRespone> => 
    apiRequest("GET", `/passenger/${BookingID}`, Token)
