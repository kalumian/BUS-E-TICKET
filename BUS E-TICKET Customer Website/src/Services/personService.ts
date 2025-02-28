import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";

export const GetPersonByNationalID = async (NationalID : string) : Promise<apiRespone> => 
  apiRequest("GET", `/person/${NationalID}`, "")