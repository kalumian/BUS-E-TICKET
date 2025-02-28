import { apiRespone } from "src/Interfaces/apiInterfaces";
import { apiRequest } from "./apiService";
import { TripRegistrationDTO } from "src/Interfaces/tripInterfaces";
import { start } from "repl";
export const AddTrip = async (token: string, trip : TripRegistrationDTO ) : Promise<apiRespone> => 
  apiRequest("POST", "trip", token, trip)

export const GetAllTrips = async (providerID: string) : Promise<apiRespone> => 
  apiRequest("GET", `trip/all/${providerID}`,"")

export const GetTripsByID = async (tripID: number) : Promise<apiRespone> => 
  apiRequest("GET", `trip/${tripID}`,"")

export const SearchTrips = async (fromCity: number, toCity: number, date: Date) => {
  const year = date.getFullYear();
  const month = date.getMonth() + 1; 
  const day = date.getDate();

  return apiRequest("GET", `trip/search?from=${fromCity}&to=${toCity}&year=${year}&month=${month}&day=${day}`, "");
};




export const formatTripDate = (tripDate : any) => {
  const date = new Date(tripDate);
  const options  : any = { year: 'numeric', month: 'long', day: 'numeric' };
  const formattedDate = date.toLocaleDateString(undefined, options); // تنسيق التاريخ
  const time = date.toLocaleTimeString(undefined, { hour: '2-digit', minute: '2-digit' }); // تنسيق الوقت
  return `${formattedDate} | ${time}`;
}; 

export const formatTripDuration = (startDate: Date, endDate: Date): string => {
  startDate = startDate instanceof Date ? startDate : new Date(startDate);
  endDate = endDate instanceof Date ? endDate : new Date(endDate);
  const diff = endDate.getTime() - startDate.getTime();
  const hours = Math.floor(diff / (1000 * 60 * 60));
  const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
  return `${hours} hours ${minutes != 0 ? minutes + " min" : ""}`;
};