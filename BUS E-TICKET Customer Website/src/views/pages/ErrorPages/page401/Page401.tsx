import {
  CButton,
  CCol,
  CContainer,
  CRow,
} from '@coreui/react-pro'
import { useNavigate } from 'react-router-dom'
import { logoutUser } from 'src/Services/loginService';
import { useDispatch } from 'react-redux';

const Page401 = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleGoHome = () => {
    navigate('/');
  };

  const handleLogout = () => {
    logoutUser(dispatch);
    navigate('/');
  };

  const handleBackPage = () => {
    navigate(-1);
  };

  return (
    <div className="bg-body-tertiary min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={6} className="text-center">
            <h1 className="display-3">401</h1>
            <h4 className="pt-3">Unauthorized Access</h4>
            <p className="text-body-secondary">
              You do not have permission to view this page.
            </p>
            <CButton className='m-2' color="primary"  onClick={handleGoHome}>
                Go to Home
            </CButton>
            <CButton className='m-2' color="primary" onClick={handleLogout}>
                Logout
            </CButton>
            <CButton className='m-2' color="primary" onClick={handleBackPage}>
                Back Page
            </CButton>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Page401
