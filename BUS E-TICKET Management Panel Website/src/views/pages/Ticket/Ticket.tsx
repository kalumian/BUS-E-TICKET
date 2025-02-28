import { useEffect, useState, useRef } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { CButton, CCard, CCardBody, CCardHeader, CContainer } from '@coreui/react';
import { fetchData } from 'src/Services/apiService';
import { getTicket } from 'src/Services/ticketService';
import { TicketDTO } from 'src/Interfaces/ticketInterface';
import LoadingWrapper from 'src/components/LoadingWrapper';
import P_Error from 'src/components/P_Error';
import { useReactToPrint } from 'react-to-print';
import TicketTemplate from '../../../components/TicketTemplate';
import html2pdf from 'html2pdf.js'; 
import CIcon from '@coreui/icons-react';
import { cilArrowCircleLeft } from '@coreui/icons';
const Ticket = () => {
  const { id } = useParams();
  const [ticket, setTicket] = useState<TicketDTO | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const componentRef = useRef<HTMLDivElement>(null);
  const navigate = useNavigate()
  useEffect(() => {    
    if (id) {
      fetchData(() => getTicket(Number(id)), setTicket, setError, setLoading);
    }
  }, [id]);

  const handlePrint : any = useReactToPrint({
    documentTitle: `Ticket-${ticket?.pnr || ''}`,
    pageStyle: '@page { size: A4; margin: 20mm; }',
    onAfterPrint: () => console.log('Print completed'),
    content: () => componentRef.current!,
  } as any);

  const handleDownload = () => {
    const element = componentRef.current;
    if (element) {
      html2pdf()
        .from(element)
        .save(`Ticket-${ticket?.pnr || ''}.pdf`);
    }
  };

  if (error || !id) {
    return <P_Error text={error ? error : "Ticket with Booking ID Is Not Exisit "} />;
  }

  return (
    <LoadingWrapper loading={loading}>
      {ticket && (
        <CContainer className="mt-4">
          <CCard className="shadow-sm">
            <CCardHeader className="d-flex justify-content-between align-items-center">
              <h3 className="mb-0">E-Ticket</h3>
              <div>
                <CButton 
                  color="primary" 
                  className="me-2" 
                  onClick={handlePrint}
                >
                  <i className="fas fa-print me-2"></i>
                  Print Ticket
                </CButton>
                <CButton 
                  color="secondary" 
                  onClick={handleDownload}
                >
                  <i className="fas fa-download me-2"></i>
                  Download Ticket
                </CButton>
              <CButton 
                color="primary" 
                variant="ghost"
                className='mx-2'
                onClick={() => navigate(-1)}
            >
                <CIcon icon={cilArrowCircleLeft} className="me-2" />
                Back
            </CButton>
              </div>
            </CCardHeader>
            <CCardBody>
              <div ref={componentRef}>
                <TicketTemplate ticket={ticket} />
              </div>
            </CCardBody>
          </CCard>
        </CContainer>
      )}
    </LoadingWrapper>
  );
};
export default Ticket;