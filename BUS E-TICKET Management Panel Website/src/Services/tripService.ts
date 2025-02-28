import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";
import { TripRegistrationDTO } from "src/Interfaces/tripInterfaces";
export const AddTrip = async (token: string, trip : TripRegistrationDTO ) : Promise<apiRespone> => 
  apiRequest("POST", "trip", token, trip)

export const GetAllTrips = async (providerID: string) : Promise<apiRespone> => 
  apiRequest("GET", `trip/all/${providerID}`,"")

export const GetTripsByID = async (tripID: number) : Promise<apiRespone> => 
  apiRequest("GET", `trip/${tripID}`,"")
