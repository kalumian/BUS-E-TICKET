import Address from "./addressInterface";
import ContactInformation from "./contactInterface";

export default interface Business {
    businessID: number;
    businessName?: string;
    logoURL?: string;
    webSiteLink?: string;
    businessLicenseNumber?: string;
    contactInformation?: ContactInformation;
    address?: Address;  
}
