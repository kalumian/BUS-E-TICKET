import EnGender from "../Enums/EnGender";
import ContactInformation from "./contactInterface";

export interface Person {
    personID?: number | null;
    nationalID?: string;
    firstName?: string;
    lastName?: string;
    birthDate?: Date;
    gender?: EnGender;
    contactInformation?: ContactInformation;
}