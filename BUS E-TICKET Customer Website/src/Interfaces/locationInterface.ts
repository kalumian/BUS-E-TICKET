import Address from "./addressInterface";


export interface LocationDTO {
  locationID?: number;
  locationName?: string;
  locationURL?: string;
  address: Address;
}

