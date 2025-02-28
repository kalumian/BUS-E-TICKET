import { CFormSelect, CFormFeedback } from "@coreui/react-pro";
import { useEffect, useState } from "react";
import { getCurrencies } from "src/Services/currencyService"; // استيراد دالة جلب العملات
import { EnInputMode } from "src/Enums/EnInputMode";
import { fetchData } from "src/Services/apiService";

interface CurrencyFormProps {
  currencyID: number;
  setCurrencyID: (currencyID: number) => void;
  inputMode: EnInputMode;
}

const CurrencyForm = ({ currencyID, setCurrencyID, inputMode }: CurrencyFormProps) => {
  const [currencies, setCurrencies] = useState<{ currencyID: number; currencyName: string, symbol:string }[]>([]);

  useEffect(() => {
    fetchData(()=>
        getCurrencies()
    , setCurrencies)
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setCurrencyID(Number(e.target.value));
  };

  return (
    <>
      <CFormSelect
        name="currencyID"
        label="Currency"
        value={currencyID}
        onChange={handleChange}
        disabled={inputMode === EnInputMode.read} // مقفل عند وضع القراءة فقط
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
    </>
  );
};

export default CurrencyForm;
