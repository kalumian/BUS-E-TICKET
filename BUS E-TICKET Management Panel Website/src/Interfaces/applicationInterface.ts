import EnApplicationStatus from "src/Enums/EnApplication";
import Business from "./businessInterface";
import { ServiceProvider } from "./accountInterfaces";

export interface RegisterationApplication {
    sPRegRequestID : number;
    requestDate? : Date;
    notes : string;
    business : Business;
    serviceProvider: ServiceProvider;
    response? : Response;
}

export interface ApplicationReview {
    responseID: number;
    responseText: string;
    responseDate: string;
    result: boolean;
    requestID: number;
    respondedByID?: string;
  }


  export interface SPRegApplicationDisplayDTO {
    accountID: string;
    spRegRequestID: number;
    requestDate: Date;
    notes?: string;
    businessName?: string;
    businessEmail?: string;
    businessPhoneNumber?: string;
    userName?: string;
    serviceProviderEmail?: string;
    serviceProviderPhoneNumber?: string;
    applicationStatus?: EnApplicationStatus;
    review?: ApplicationReview;
}

