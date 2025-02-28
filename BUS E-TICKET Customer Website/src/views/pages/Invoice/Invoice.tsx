import { useEffect, useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import { CButton, CCard, CCardBody, CCardHeader, CContainer } from '@coreui/react';
import { fetchData } from 'src/Services/apiService';
import { getInvoice } from 'src/Services/invoiceService';
import InvoiceDTO  from 'src/Interfaces/invoiceInterface';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { useReactToPrint } from 'react-to-print';
import InvoiceTemplate from '../../../components/InvoiceTemplate';

const Invoice = () => {
  const { id } = useParams();
  const [invoice, setInvoice] = useState<InvoiceDTO | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const componentRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (id) {
      fetchData(() => getInvoice(Number(id)), setInvoice, setError, setLoading);
    }
  }, [id]);

  const handlePrint = useReactToPrint({
    documentTitle: `Invoice-${invoice?.invoiceNumber || ''}`,
    copyStyles: true,
    pageStyle: '@page { size: A4; margin: 20mm; }',
    onBeforeGetContent: () => {
      if (!componentRef.current) {
        return Promise.reject('Printing failed: Could not find invoice content');
      }
      return Promise.resolve();
    },
    onAfterPrint: () => {
      console.log('Print completed');
    },
    content: () => componentRef.current as HTMLElement,
  });

  if (error) {
    return <P_Error text={error} />;
  }

  return (
    <LoadingWrapper loading={loading}>
      {invoice && (
        <CContainer className="mt-4">
          <CCard className="shadow-sm">
            <CCardHeader className="d-flex justify-content-between align-items-center">
              <h3 className="mb-0">Invoice</h3>
              <CButton 
                color="primary" 
                onClick={handlePrint}
              >
                <i className="fas fa-print me-2"></i>
                Print Invoice
              </CButton>
            </CCardHeader>
            <CCardBody>
              <div ref={componentRef}>
                <InvoiceTemplate invoice={invoice} />
              </div>
            </CCardBody>
          </CCard>
        </CContainer>
      )}
    </LoadingWrapper>
  );
};

export default Invoice;