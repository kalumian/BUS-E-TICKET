import { useEffect, useState } from 'react';
import Select from 'react-select';
import { CCard, CCardHeader, CCardBody, CFormLabel, CRow, CCol, CFormInput, CInputGroup, CInputGroupText } from '@coreui/react-pro';
import { getAllCities } from 'src/Services/addressService'; // Import function to fetch cities
import Address, { City } from 'src/Interfaces/addressInterface';
import { EnInputMode } from "src/Enums/EnInputMode"; // Assuming this exists
import { fetchData } from 'src/Services/apiService';
import LoadingWrapper from './LoadingWrapper';
import { cilHome } from '@coreui/icons';
import CIcon from '@coreui/icons-react';

const AddressForm = ({ address, onAddressChange, InputMode }: { 
  address: Address, 
  onAddressChange: any, 
  InputMode: EnInputMode 
}) => {
  const [cities, setCities] = useState<City[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
      setLoading(true);
      fetchData(() => getAllCities(), setCities, undefined, setLoading);
  }, []);

  const cityOptions = cities.map((city) => ({
    value: city.cityID,
    label: city.cityName,
  }));

  return (
    <CCard className="mb-4 shadow-sm">
      <CCardHeader className="bg-light">
        <h4 className="mb-0">Address Information</h4>
      </CCardHeader>
    <LoadingWrapper loading={loading}>
    <CCardBody>
          <CRow>
            <CCol md={6}>
              <div className="mb-3">
                <CFormLabel>City</CFormLabel>
                <Select
                  options={cityOptions}
                  value={cityOptions.find(option => option?.value === Number(address?.cityID))}
                  onChange={(e)=> {
                    const event : any = {
                      target : {
                        value : e?.value,
                        name: 'cityID'
                      }
                    }
                    onAddressChange(event)
                  }}
                  isClearable
                  isSearchable
                  placeholder="Select a City"
                  isDisabled={EnInputMode.read === InputMode }
                />
              </div>
            </CCol>
            <CCol md={6}>
                <div className="mb-3">
                  <CFormLabel>Additional Details</CFormLabel>
                  <CInputGroup>
                    <CInputGroupText>
                      <CIcon icon={cilHome} />
                    </CInputGroupText>
                    <CFormInput
                      type="text"
                      name="additionalDetails"
                      placeholder="Enter additional address details"
                      disabled={EnInputMode.read === InputMode}
                      value={address?.additionalDetails}
                      onChange={onAddressChange}
                    />
                  </CInputGroup>
                </div>
              </CCol>
          </CRow>
      </CCardBody>
    </LoadingWrapper>
    </CCard>
  );
};

export default AddressForm;