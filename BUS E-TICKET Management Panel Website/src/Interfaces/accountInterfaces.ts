import EnAccountStatus from "src/Enums/EnAccountStatus"
import Business from "./businessInterface"
import EnApplicationStatus from "src/Enums/EnApplication"
import { Person } from "./personInterface"
import { Address } from "cluster"

export interface Account{
    accountId? : string
    userName? : string
    email? : string
    phoneNumber? : string
    registerationDate? : Date
    password?: string,
    accountStatus? : EnAccountStatus
}

export interface Manager{
    managerID? : number
    createdBy? : number | string
    account? : Account
}


export interface Customer {
    account: Account;
    person: Person;
    address: Address;
}

export interface ServiceProvider{
    serviceProviderID? : number
    business? : Business
    account? : Account
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

