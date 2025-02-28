import EnApplicationStatus from "src/Enums/EnApplication";

export interface RegisterationApplication {
    accountID: string
    spRegRequestID: number;
    requestDate: string;
    notes: string;
    businessName: string;
    businessEmail: string;
    businessPhoneNumber: string;
    userName: string;
    serviceProviderEmail: string;
    serviceProviderPhoneNumber: string;
    applicationStatus: EnApplicationStatus
    Review : ApplicationReview
}

export interface ApplicationReview {
    responseID: number;
    responseText: string;
    responseDate: string;
    result: boolean;
    requestID: number;
    respondedByID?: string;
  }