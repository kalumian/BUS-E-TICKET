import EnAccountStatus from "src/Enums/EnAccountStatus"
import Business from "./businessInterface"

export interface Account{
    status: EnAccountStatus
    accountID? : string
    userName? : string
    email? : string
    phoneNumber? : string
    registerationDate? : Date
    password?: string,
    accountStatus? : EnAccountStatus
}

export interface Manager extends Account{
    managerID? : number
    createdBy? : number | string
}


export interface Customer extends Account{
    customerID? : number
}

export interface ServiceProvider extends Account, Business{
    serviceProviderID? : number
}

export interface RegisterAccountDTO {
    accountId?: string;
    userName?: string;
    password?: string;
    email?: string;
    phoneNumber?: string;
    status? : EnAccountStatus 
  }
  
  export interface RegisterManagerAccountDTO {
    account: RegisterAccountDTO;
    createdByID: string | undefined;
  }
