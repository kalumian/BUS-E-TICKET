import { CRow, CCol, CButton } from '@coreui/react';
import { useEffect, useState } from 'react';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import { City } from 'src/Interfaces/addressInterface';
import { getAllCities } from 'src/Services/addressService';
import { fetchData } from 'src/Services/apiService';
import Select from 'react-select';

const TripSearchBar = ({ onSearch, setError, setLoading }: { onSearch: any, setError: React.Dispatch<any>, setLoading: React.Dispatch<any> }) => {
  const [fromCity, setFromCity] = useState('');
  const [toCity, setToCity] = useState('');
  const [date, setDate] = useState(new Date());
  const [cities, setCities] = useState<City[]>([]);

  useEffect(() => {
    fetchData(() => getAllCities(), setCities, setError, setLoading);
  }, []);

  const cityOptions = cities.map(city => ({
    value: city.cityID,
    label: city.cityName
  }));

  return (
    <CRow className="align-items-end mb-4 g-3">
      {/* From City */}
      <CCol xs={12} md={3}>
        <div className="mb-1">
          <small className="text-muted">From</small>
        </div>
        <Select
          options={cityOptions}
          value={cityOptions.find(option => option?.value === Number(fromCity))}
          onChange={(option) => setFromCity(option ? String(option.value) : '')} // تحويل القيمة إلى string
          placeholder="Search departure city..."
          isClearable
          isSearchable
        />
      </CCol>

      {/* To City */}
      <CCol xs={12} md={3}>
        <div className="mb-1">
          <small className="text-muted">To</small>
        </div>
        <Select
          options={cityOptions}
          value={cityOptions.find(option => option?.value === Number(toCity))}
          onChange={(option) => setToCity(option ? String(option.value) : '')} // تحويل القيمة إلى string
          placeholder="Search arrival city..."
          isClearable
          isSearchable
        />
      </CCol>

      {/* Date Picker */}
      <CCol xs={12} md={3}>
        <div className="mb-1">
          <small className="text-muted">Date</small>
        </div>
        <DatePicker
          selected={date}
          onChange={(date: Date | null) => date && setDate(date)}
          className="form-control"
          dateFormat="dd/MM/yyyy"
          minDate={new Date()}
        />
      </CCol>

      {/* Search Button */}
      <CCol xs={12} md={3}>
        <CButton
          color="primary"
          className="w-100"
          onClick={() => onSearch({fromCity, toCity, date})}
        >
          Search
        </CButton>
      </CCol>
    </CRow>
  );
};

export default TripSearchBar;
