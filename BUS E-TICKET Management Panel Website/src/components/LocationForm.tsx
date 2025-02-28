import { CForm, CFormInput } from "@coreui/react-pro";
import { EnInputMode } from "src/Enums/EnInputMode";
import AddressForm from "./AddressForm";
import { LocationDTO } from "src/Interfaces/locationInterface";
import EnLocationType from "src/Enums/EnLocationType";
import Address from "src/Interfaces/addressInterface";
interface LocationFormProps {
  location: LocationDTO;
  onLocationChange:any;
  onAddressChange:any;
  address : Address
  InputMode: EnInputMode;
  locationType: EnLocationType;
}
const LocationForm = ({
  location,
  onLocationChange,
  InputMode,
  locationType,
  address,
  onAddressChange
}:LocationFormProps) => {


  return (
    <CForm>
      {/* Location Name Field */}
      <h5>{locationType === EnLocationType.end? "End Location Information" : "Start Location Information"}</h5>
      <hr />
      <CFormInput
        type="text"
        name="locationName"
        label="Location Name"
        value={location?.locationName}
        onChange={onLocationChange}
        disabled={InputMode === EnInputMode.read}
        required
      />

      {/* Location URL Field */}
      <CFormInput
        type="text"
        name="locationURL"
        label="Location URL"
        value={location?.locationURL}
        onChange={onLocationChange}
        disabled={InputMode === EnInputMode.read}
      />

      {/* Address Form */}
     <div className="mt-4">
      <AddressForm
          address={address}
          onAddressChange={onAddressChange}
          InputMode={InputMode}/>
     </div>
      </CForm>
  );
};

export default LocationForm;
