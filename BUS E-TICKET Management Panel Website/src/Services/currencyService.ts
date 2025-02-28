import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";

export const getCurrencies = async () : Promise<apiRespone> => 
  apiRequest("GET", "Currency", "")