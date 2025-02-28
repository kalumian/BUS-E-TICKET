import { CForm, CFormFeedback, CFormInput, CFormSelect } from "@coreui/react-pro";
import { EnInputMode } from "src/Enums/EnInputMode";
import { ServiceProvider } from "src/Interfaces/accountInterfaces";
import Business from "src/Interfaces/businessInterface";

const BusinessForm = ({ business, setBusiness, InputMode }: { business: Business, setBusiness: React.Dispatch<React.SetStateAction<ServiceProvider>>, InputMode: EnInputMode }) => {

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;

    setBusiness((prev: any) => {
      if (!prev) return undefined;
      return {
        ...prev,
        [name]: value
      };
    });
  };

  return (
    <>
      <CFormInput
        type="text"
        name="businessName"
        label="Business Name"
        disabled={EnInputMode.Update === InputMode || EnInputMode.read === InputMode}
        value={business?.businessName || ''}
        onChange={handleChange}
        required
      />
      <CFormFeedback invalid>Business Name is required.</CFormFeedback>

      <CFormInput
        type="text"
        name="logoURL"
        label="Logo URL"
        disabled={EnInputMode.read === InputMode}
        value={business?.logoURL || ''}
        onChange={handleChange}
        required
      />
      <CFormFeedback invalid>Logo URL is required.</CFormFeedback>

      <CFormInput
        type="email"
        name="bussinesEmail"
        label="Business Email"
        disabled={EnInputMode.read === InputMode}
        value={business?.bussinesEmail || ''}
        onChange={handleChange}
        required
      />
      <CFormFeedback invalid>Email is required.</CFormFeedback>

      <CFormInput
        type="text"
        name="businessPhoneNumber"
        label="Business Phone Number"
        disabled={EnInputMode.read === InputMode}
        value={business?.businessPhoneNumber || ''}
        onChange={handleChange}
        required
      />
      <CFormFeedback invalid>Phone Number is required.</CFormFeedback>

      <CFormInput
        type="text"
        name="webSiteLink"
        label="Website Link"
        disabled={EnInputMode.read === InputMode}
        value={business?.webSiteLink || ''}
        onChange={handleChange}
      />
      
      <CFormInput
        type="text"
        name="businessLicenseNumber"
        label="Business License Number"
        disabled={EnInputMode.Update === InputMode || EnInputMode.read === InputMode}
        value={business?.businessLicenseNumber || ''}
        onChange={handleChange}
      />
    </>
  );
};

export default BusinessForm;
