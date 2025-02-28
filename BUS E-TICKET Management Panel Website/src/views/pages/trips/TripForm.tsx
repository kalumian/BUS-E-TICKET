import { CButton, CForm, CFormFeedback, CFormInput, CFormLabel, CSpinner } from "@coreui/react-pro";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Navigate, useLocation, useNavigate, useParams } from "react-router-dom";
import CurrencyForm from "src/components/CurrencyForm";
import LoadingWrapper from "src/components/LoadingWrapper";
import LocationForm from "src/components/LocationForm";
import P_Error from "src/components/P_Error";
import P_Success from "src/components/P_Success";
import { EnInputMode } from "src/Enums/EnInputMode";
import EnLocationType from "src/Enums/EnLocationType";
import { LocationDTO } from "src/Interfaces/locationInterface";
import { TripRegistrationDTO } from "src/Interfaces/tripInterfaces";
import { User } from "src/Interfaces/userInterfaces";
import { AddTrip } from "src/Services/tripService";
import { fetchData } from "src/Services/apiService";
import { RootState } from "src/store";
import { 
    CCard,
    CCardHeader,
    CCardBody,
    CRow,
    CCol,
    CInputGroup,
    CInputGroupText,
    CImage
  } from "@coreui/react-pro";
  import CIcon from '@coreui/icons-react';
  import { 
    cilUser,
    cilCarAlt,
    cilClock,
    cilMoney,
    cilLocationPin,
    cilCalendar,
    cilPeople,
    cilSave,
    cilTrash
  } from '@coreui/icons';
import EnTripStatus from "src/Enums/EnTripsStatus";
const TripForm = () => {
    const { id } = useParams();
    const location = useLocation();
    const [InputMode, setInputMode] = useState<EnInputMode>(EnInputMode.Add);
    
    const initAddress = {
        addressID: undefined,
        additionalDetails: "",
        countryName: "",
        regionName: "",
        countryID: 0,
        cityName: "",
        regionID: 0,
        streetID: undefined,
        streetName: "",
        cityID: 0
    }
    const initLocation : LocationDTO = { locationID: undefined, locationName: "", locationURL: "", address: initAddress }
   
    const [loading, setLoading] = useState<boolean>(false);
    const [fetchError, setFetchError] = useState("");
    const [fetchResult, setFetchResult] = useState("");
    const [error, setError] = useState("");
    const token: string | null = useSelector((state: RootState) => state.auth.token);
    const user: User | null = useSelector((state: RootState) => state.auth.user);
    const [startLocation, setStartLocation] = useState<LocationDTO>(initLocation);
    const [endLocation, setEndLocation] = useState<LocationDTO>(initLocation);
    const [startAddress, setStartAddress] = useState(initAddress);
    const [endAddress, setEndAddress] = useState(initAddress);
    const [tripInfo, setTripInfo] = useState<TripRegistrationDTO>({});

    // Handle trip info changes
    const HandleOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        
        setTripInfo(prev => ({
            ...prev,
            [name]: name === "tripDate" ? new Date(value) : value
        }));

        console.log(tripInfo)
        
    };
    const navigate = useNavigate();

    useEffect(() => {
        setError("");
        setFetchError("");
        setFetchResult("");

        if (token && id) {
            setInputMode(EnInputMode.Update);
            // fetchData(() => GetTripByID(token, id), setTrip, setError, setLoading);
        } else {
            setInputMode(EnInputMode.Add);
            // setTripInfo({});
        }

        if (location.pathname.includes("details")) {
            setInputMode(EnInputMode.read);
        }
    }, [id, token, location.pathname]);

    const AddTripHandle = async (combinedData: TripRegistrationDTO) => {        
        if (!combinedData.vehicleInfo || !combinedData.driverInfo || 
            !combinedData.tripDate || !combinedData.totalSeats || 
            !combinedData.price) {
            setFetchError("All fields are required!");
            return;
        }
        if (token)
            fetchData(async () => {
                const res = await AddTrip(token, combinedData);
                res.message && setFetchResult(res.message);
                navigate("/trips");
            }, undefined, setFetchError, setLoading);
    };


    const UpdateTripHandle = async () => {
        if (!token || !id) return;
    };

    const DeleteTripHandle = async () => {
        if (!token || !id) return;
    };

    const combineData = (): TripRegistrationDTO => {
       
        // Validate start location data
        const startLocationData = {
            ...startLocation,
            locationName: startLocation.locationName || "Start Location",
            locationURL: startLocation.locationURL || "",
            address: {
                ...startAddress,
                countryID: Number(startAddress.countryID),
                regionID: Number(startAddress.regionID),
                cityID: Number(startAddress.cityID),
                streetID: startAddress.streetID ? Number(startAddress.streetID) : undefined,
                additionalDetails: startAddress.additionalDetails || ""
            }
        };
    
        // Validate end location data
        const endLocationData = {
            ...endLocation,
            locationName: endLocation.locationName || "End Location",
            locationURL: endLocation.locationURL || "",
            address: {
                ...endAddress,
                countryID: Number(endAddress.countryID),
                regionID: Number(endAddress.regionID),
                cityID: Number(endAddress.cityID),
                streetID: endAddress.streetID ? Number(endAddress.streetID) : undefined,
                additionalDetails: endAddress.additionalDetails || ""
            }
        };
    
        // Combine and validate all data
        const combinedData: TripRegistrationDTO = {
            ...tripInfo,
            tripID: 0,
            tripDate: tripInfo.tripDate ? new Date(tripInfo.tripDate) : new Date(),
            tripDuration: Number(tripInfo.tripDuration),
            vehicleInfo: tripInfo.vehicleInfo || "",
            driverInfo: tripInfo.driverInfo || "",
            price: Number(tripInfo.price) || 0,
            currencyID: Number(tripInfo.currencyID) || 0,
            totalSeats: Number(tripInfo.totalSeats) || 0,
            startLocation: startLocationData,
            endLocation: endLocationData,
            tripStatus: EnTripStatus.isWaiting,
            serviceProviderID: user?.UserID || "", // Add business service provider ID from user
        };
    
        // Log combined data for debugging
        console.log('Combined Trip Data:', combinedData);
    
        return combinedData;
    };
    const Submit = (e: React.MouseEvent | any) => {
        e.preventDefault();
        setFetchError("");
        setFetchResult("");

        const combinedData = combineData();

        switch (InputMode) {
            case EnInputMode.Add:
                AddTripHandle(combinedData);
                break;
            case EnInputMode.Update:
                // UpdateTripHandle(combinedData);
                break;
        }
    };


    if (!user) {
        return <Navigate to="/login" />;
    }

    return (
        <LoadingWrapper loading={loading}>
                <CCard className="shadow-sm">
            <CCardHeader className="bg-light">
                <div className="d-flex justify-content-between align-items-center">
                    <h3 className="mb-0">
                        <CIcon icon={cilCarAlt} className="me-2" />
                        {InputMode === EnInputMode.Add ? "Add New Trip" : 
                         InputMode === EnInputMode.Update ? "Update Trip" : "Trip Details"}
                    </h3>
                </div>
            </CCardHeader>
            <CCardBody>
                <CForm className={!error ? "" : "d-none"}>
                    <CRow>
                        <CCol md={6}>
                            <div className="mb-3">
                                <CFormLabel>Vehicle Information</CFormLabel>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilCarAlt} />
                                    </CInputGroupText>
                                    <CFormInput
                                        type="text"
                                        name="vehicleInfo"
                                        placeholder="Enter vehicle details"
                                        value={tripInfo.vehicleInfo}
                                        onChange={HandleOnChange}
                                        required
                                    />
                                </CInputGroup>
                                <CFormFeedback invalid>Vehicle information is required</CFormFeedback>
                            </div>
                        </CCol>
                        <CCol md={6}>
                            <div className="mb-3">
                                <CFormLabel>Driver Information</CFormLabel>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilUser} />
                                    </CInputGroupText>
                                    <CFormInput
                                        type="text"
                                        name="driverInfo"
                                        placeholder="Enter driver details"
                                        value={tripInfo.driverInfo}
                                        onChange={HandleOnChange}
                                        required
                                    />
                                </CInputGroup>
                                <CFormFeedback invalid>Driver information is required</CFormFeedback>
                            </div>
                        </CCol>
                    </CRow>

                    <CRow>
                        <CCol md={6}>
                            <div className="mb-3">
                                <CFormLabel>Trip Date & Time</CFormLabel>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilCalendar} />
                                    </CInputGroupText>
                                    <CFormInput
                                        type="datetime-local"
                                        name="tripDate"
                                        value={tripInfo.tripDate ? new Date(tripInfo.tripDate.getTime() - tripInfo.tripDate.getTimezoneOffset() * 60000).toISOString().slice(0, 16) : ""}
                                        onChange={HandleOnChange}
                                        min={new Date().toISOString().slice(0, 16)}
                                        required
                                    />
                                </CInputGroup>
                                <CFormFeedback invalid>Trip date and time is required</CFormFeedback>
                            </div>
                        </CCol>
                        <CCol md={6}>
                            <div className="mb-3">
                                <CFormLabel>Trip Duration</CFormLabel>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilClock} />
                                    </CInputGroupText>
                                    <CFormInput
                                        type="text"
                                        name="tripDuration"
                                        placeholder="Duration in minutes (e.g., 75)"
                                        value={tripInfo.tripDuration}
                                        onChange={HandleOnChange}
                                        required
                                    />
                                </CInputGroup>
                                <CFormFeedback invalid>Trip duration is required</CFormFeedback>
                            </div>
                        </CCol>
                    </CRow>

                    <CRow>
                        <CCol md={4}>
                            <div className="mb-3">
                                <CFormLabel>Total Seats</CFormLabel>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilPeople} />
                                    </CInputGroupText>
                                    <CFormInput
                                        type="number"
                                        name="totalSeats"
                                        placeholder="Enter total seats"
                                        value={tripInfo.totalSeats}
                                        onChange={HandleOnChange}
                                        required
                                    />
                                </CInputGroup>
                                <CFormFeedback invalid>Total seats is required</CFormFeedback>
                            </div>
                        </CCol>
                        <CCol md={4}>
                            <div className="mb-3">
                                <CFormLabel>Price</CFormLabel>
                                <CInputGroup>
                                    <CInputGroupText>
                                        <CIcon icon={cilMoney} />
                                    </CInputGroupText>
                                    <CFormInput
                                        type="number"
                                        name="price"
                                        placeholder="Enter trip price"
                                        value={tripInfo.price}
                                        onChange={HandleOnChange}
                                        required
                                    />
                                </CInputGroup>
                                <CFormFeedback invalid>Price is required</CFormFeedback>
                            </div>
                        </CCol>
                        <CCol md={4}>
                            <div className="mb-3">
                                <CFormLabel>Currency</CFormLabel>
                                <CurrencyForm 
                                    currencyID={tripInfo.currencyID} 
                                    onCurrencyChange={HandleOnChange} 
                                    inputMode={InputMode}
                                />
                            </div>
                        </CCol>
                    </CRow>

                        {/* Location Forms */}
                        <CCard className="mb-4">
                            <CCardHeader className="bg-light">
                                <h5 className="mb-0">
                                    <CIcon icon={cilLocationPin} className="me-2" />
                                    Trip Route
                                </h5>
                            </CCardHeader>
                            <CCardBody>
                                <CRow>
                                <CCol md={6}>
                                        <LocationForm
                                            location={startLocation}
                                            onLocationChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setStartLocation((prev) => ({...prev, [e.target.name]: e.target.value}))}
                                            onAddressChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setStartAddress((prev) => ({...prev, [e.target.name]: e.target.value}))}
                                            address={startAddress}
                                            InputMode={InputMode}
                                            locationType={EnLocationType.stert}
                                        />
                                    </CCol>
                                    <CCol md={6}>
                                        <LocationForm
                                            location={endLocation}
                                            onLocationChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setEndLocation((prev) => ({...prev, [e.target.name]: e.target.value}))}
                                            onAddressChange={(e : React.ChangeEvent<HTMLSelectElement>) =>  setEndAddress((prev) => ({...prev, [e.target.name]: e.target.value}))}
                                            InputMode={InputMode}
                                            address={endAddress}
                                            locationType={EnLocationType.end}
                                        />
                                    </CCol>
                                </CRow>
                            </CCardBody>
                        </CCard>

                        <div className="d-flex justify-content-end gap-2 mt-4">
                            {InputMode !== EnInputMode.read && (
                                <CButton 
                                    type="submit" 
                                    color="primary" 
                                    onClick={Submit} 
                                    disabled={loading}
                                >
                                    <CIcon icon={cilSave} className="me-2" />
                                    {loading ? <CSpinner size="sm" /> : 
                                     InputMode === EnInputMode.Update ? "Update Trip" : "Add Trip"}
                                </CButton>
                            )}

                            {(InputMode === EnInputMode.Update || InputMode === EnInputMode.read) && (
                                <CButton 
                                    type="button" 
                                    color="danger" 
                                    onClick={DeleteTripHandle}
                                >
                                    <CIcon icon={cilTrash} className="me-2" />
                                    {loading ? <CSpinner size="sm" /> : "Delete Trip"}
                                </CButton>
                            )}
                        </div>
                    </CForm>
                    <div className="mt-3">
                        <P_Error text={error} />
                        <P_Error text={fetchError} />
                        <P_Success text={fetchResult} />
                    </div>

                </CCardBody>
            </CCard>
        </LoadingWrapper>
    );
};

export default TripForm;
