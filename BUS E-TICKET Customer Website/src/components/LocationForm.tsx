import { CForm, CFormInput } from "@coreui/react-pro";
import { EnInputMode } from "src/Enums/EnInputMode";
import AddressForm from "./AddressForm";
import { LocationDTO } from "src/Interfaces/locationInterface";
import EnLocationType from "src/Enums/EnLocationType";
import Address from "src/Interfaces/addressInterface";

const LocationForm = ({
  location,
  setLocation,
  InputMode,
  locationType,
  setAddress
}: {
  location: LocationDTO;
  setLocation: any;
  setAddress : any;
  InputMode: EnInputMode;
  locationType : EnLocationType
}) => {


  return (
    <CForm>
      {/* Location Name Field */}
      <hr />
      <p>{locationType === EnLocationType.end? "end" : "start"}</p>
      <CFormInput
        type="text"
        name="locationName"
        label="Location Name"
        value={location?.locationName}
        onChange={setLocation}
        disabled={InputMode === EnInputMode.read}
        required
      />

      {/* Location URL Field */}
      <CFormInput
        type="text"
        name="locationURL"
        label="Location URL"
        value={location?.locationURL}
        onChange={setLocation}
        disabled={InputMode === EnInputMode.read}
      />

      {/* Address Form */}
      <AddressForm
        address={location?.address}
        setAddress={setAddress}
        InputMode={InputMode}
/>    </CForm>
  );
};

export default LocationForm;
