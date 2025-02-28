import Address from "./addressInterface";
import ContactInformation from "./contactInterface";

export default interface Business extends ContactInformation, Address {
    businessID: number;
    businessName?: string;
    logoURL?: string;
    bussinesEmail?: string;
    businessPhoneNumber?: string;
    webSiteLink?: string;
    businessLicenseNumber?: string;
}
