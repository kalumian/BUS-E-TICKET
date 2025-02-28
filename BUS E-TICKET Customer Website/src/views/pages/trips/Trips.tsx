import TripCard from 'src/components/TripCard';
import { TripDisplayDTO } from 'src/Interfaces/tripInterfaces';
import { Dispatch, SetStateAction, useState } from 'react';
import TripSearchBar from 'src/components/TripSearchBar';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { GetAllTrips, SearchTrips } from 'src/Services/tripService';
import { fetchData } from 'src/Services/apiService';



  // const _trips: TripDisplayDTO[] = [
  //   {
  //     tripID: 1,
  //     vehicleInfo: "Bus - Volvo XC90",
  //     driverInfo: "John Doe, License #12345",
  //     tripDate: new Date(2025, 1, 20, 8, 0, 0), // Month is 0-based, so 1 means February
  //     arrivalDate: new Date(2025, 1, 20, 10, 30, 0),
  //     price: 150.00,
  //     currency: "USD",
  //     tripDuration: "2:30:00",
  //     totalSeats: 50,
  //     availableSeatsCount: 20,
  //     tripStatus: "Active",
  //     businessName: "Global Bus Services",
  //     businessLogoURL: "https://www.havayolu101.com/wp-content/uploads/2008/02/Kamil_koc_logo.jpg",
  //     startLocationName: "Downtown Station",
  //     startLocationURL: "https://example.com/start",
  //     startCity: "New York",
  //     startAdditionalDetails: "Next to the central park",
  //     endLocationName: "Central City Terminal",
  //     endLocationURL: "https://example.com/end",
  //     endCity: "Boston",
  //     endAdditionalDetails: "Near the subway entrance",
  //   },
  //   {
  //     tripID: 2,
  //     vehicleInfo: "Train - SuperSpeed Express",
  //     driverInfo: "Emily Smith, License #67890",
  //     tripDate: new Date(2025, 1, 22, 7, 15, 0),
  //     arrivalDate: new Date(2025, 1, 22, 9, 45, 0),
  //     price: 120.00,
  //     currency: "EUR",
  //     tripDuration: "2:30:00",
  //     totalSeats: 100,
  //     availableSeatsCount: 35,
  //     tripStatus: "Completed",
  //     businessName: "Euro Railways",
  //     businessLogoURL: "https://www.havayolu101.com/wp-content/uploads/2008/02/Kamil_koc_logo.jpg",
  //     startLocationName: "Paris Gare de Lyon",
  //     startLocationURL: "https://example.com/rail-start",
  //     startCity: "Paris",
  //     startAdditionalDetails: "Main terminal, platform 5",
  //     endLocationName: "London King's Cross",
  //     endLocationURL: "https://example.com/rail-end",
  //     endCity: "London",
  //     endAdditionalDetails: "Platform 2, near the escalator",
  //   },
  //   {
  //     tripID: 3,
  //     vehicleInfo: "Private Jet - Gulfstream G650",
  //     driverInfo: "Mark Turner, License #11223",
  //     tripDate: new Date(2025, 1, 25, 10, 0, 0),
  //     arrivalDate: new Date(2025, 1, 25, 12, 30, 0),
  //     price: 5000.00,
  //     currency: "USD",
  //     tripDuration: "2:30:00",
  //     totalSeats: 10,
  //     availableSeatsCount: 5,
  //     tripStatus: "Active",
  //     businessName: "Elite Air Services",
  //     businessLogoURL: "https://www.havayolu101.com/wp-content/uploads/2008/02/Kamil_koc_logo.jpg",
  //     startLocationName: "JFK International Airport",
  //     startLocationURL: "https://example.com/jfk-start",
  //     startCity: "New York",
  //     startAdditionalDetails: "Terminal 1, Gate 3",
  //     endLocationName: "Los Angeles International Airport",
  //     endLocationURL: "https://example.com/lax-end",
  //     endCity: "Los Angeles",
  //     endAdditionalDetails: "Near VIP lounge",
  //   },
  // ];
  

const Trips = () => {
    const [trips, seTtrips] = useState<TripDisplayDTO[]>([]);
    const [loading, setLoading] = useState<boolean>(false)
    const [error, setError] = useState<string>("")

    const handleSearch = ({ fromCity, toCity, date }: { fromCity: string, toCity: string, date: Date }) => {
      if (!fromCity || !toCity || !date) {
        setError("Please select valid cities and date.");
        return;
      }
    
      setLoading(true);
      fetchData(() => SearchTrips(Number(fromCity), Number(toCity), date), seTtrips, setError, setLoading);
    };

    return (
      <>
        <TripSearchBar onSearch={handleSearch} setError={setError} setLoading={setLoading}/>
        {error ? 
            <P_Error text={error}/>
            :
            <LoadingWrapper loading={loading}>

         {  trips && trips.map((trip) => (
                <TripCard key={trip?.tripID} trip={trip} />
            ))
}
            </LoadingWrapper>

        }
        </>
    )
};


export default Trips;

