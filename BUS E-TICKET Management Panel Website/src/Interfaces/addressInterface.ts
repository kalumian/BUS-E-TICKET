export default interface Address extends Street{
    addressID?: number;
    additionalDetails?: string;
}


interface Street extends City{
    streetID?: number;
    streetName?: string;
    cityID?: number;
}

export interface City extends Region{
    cityID?: number;
    cityName?: string;
    regionID?: number;
}

interface Region extends Country{
    regionID?: number;
    regionName?: string;
    countryID?: number;
}

interface Country {
    countryID?: number;
    countryName?: string;
}