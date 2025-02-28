import { Account } from "./accountInterfaces";
import Address from "./addressInterface";
import ContactInformation from "./contactInterface";
import { Person } from "./personInterface";

export interface Customer {
    account: Account;
    person: Person;
    address: Address;
}