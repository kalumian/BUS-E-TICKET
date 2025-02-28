// src/Services/addressService.ts

import { Dispatch, SetStateAction } from 'react';
import { apiRequest, fetchData } from './apiService';
import { apiRespone } from 'src/Interfaces/apiInterfaces';

// Function to get countries
export const getCountries = async (setCountries: Dispatch<SetStateAction<never[]>>, setLoading: Dispatch<SetStateAction<boolean>>) => {
  await fetchData(() => apiRequest("GET", "/address/countries"), setCountries, undefined, setLoading, { errorMessage: "Error fetching countries" });
};

// Function to get regions by country
export const getRegionsByCountry = async (countryID: number, setRegions: Dispatch<SetStateAction<never[]>>, setLoading: Dispatch<SetStateAction<boolean>>) => {
  await fetchData(() => apiRequest("GET", `/address/regions?countryID=${countryID}`), setRegions, undefined, setLoading, { errorMessage: "Error fetching regions" });
};

// Function to get cities by region
export const getCitiesByRegion = async (regionID: number, setCities: Dispatch<SetStateAction<never[]>>, setLoading: Dispatch<SetStateAction<boolean>>) => {
  await fetchData(() => apiRequest("GET", `/address/cities?regionID=${regionID}`), setCities, undefined, setLoading, { errorMessage: "Error fetching cities" });
};

export const getAllCities = async () : Promise<apiRespone> =>  apiRequest("GET", `/address/cities`,"")

