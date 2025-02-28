import { CFormSelect, CFormFeedback } from "@coreui/react-pro";
import { useEffect, useState } from "react";
import { getCurrencies } from "src/Services/currencyService"; // استيراد دالة جلب العملات
import { EnInputMode } from "src/Enums/EnInputMode";
import { fetchData } from "src/Services/apiService";
import LoadingWrapper from "./LoadingWrapper";

interface CurrencyFormProps {
  currencyID: number;
  onCurrencyChange: any;
  inputMode: EnInputMode;
}

const CurrencyForm = ({ currencyID, onCurrencyChange, inputMode }: CurrencyFormProps) => {
  const [currencies, setCurrencies] = useState<{ currencyID: number; currencyName: string, symbol:string }[]>([]);
  const [loading, setLoading] = useState<boolean>(false);


  useEffect(() => {
    fetchData(()=>
        getCurrencies()
    , setCurrencies,undefined, setLoading)
  }, []);

  return (
    <LoadingWrapper loading={loading}>
      <CFormSelect
        name="currencyID"
        label="Currency"
        value={currencyID}
        onChange={onCurrencyChange}
        disabled={inputMode === EnInputMode.read} 
        required
      >
        <option value="">Select Currency</option>
        {currencies.map((currency) => (
          <option key={currency.currencyID} value={currency.currencyID}>
            {currency.currencyName}| {currency.symbol}
          </option>
        ))}
      </CFormSelect>
      <CFormFeedback invalid>Currency is required.</CFormFeedback>
    </LoadingWrapper>
  );
};

export default CurrencyForm;
