import { ApplicationReview } from "src/Interfaces/applicationInterface";
import { apiRequest } from "./apiService";

export const GetAllSPApplications = async (token: string) => 
    apiRequest("GET", `ServiceProvider/applications`, token);

export const GetSPApplicationById = async (token: string, id: number) => 
    apiRequest("GET", `ServiceProvider/applications/${id}`, token);
  
export const SetApplicationReview = async (token: string, revire: ApplicationReview) => 
    apiRequest("POST", `ServiceProvider/application/review`, token, revire);