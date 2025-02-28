import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {
  CButton,
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CRow,
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import { cilLockLocked, cilUser } from '@coreui/icons';
import { useDispatch } from 'react-redux';
import { loginUser } from 'src/Services/loginService';
import { fetchData } from 'src/Services/apiService';
import { login } from 'src/store';
import P_Success from 'src/components/P_Success';
import LoadingWrapper from 'src/components/LoadingWrapper';
import logo from 'src/assets/brand/logo2.png';
const P_Error = React.lazy(() => import('../../../components/P_Error'))
const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();
  const dispatch = useDispatch();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault()
    fetchData(async () : Promise<any> =>{
      const res = await loginUser(username, password)
      if(res.message) setMessage(res.message )
      dispatch(login({token: res.data.token}));
      navigate('/')
    }, undefined, setError, setLoading, undefined)
  };

  return (

    <div className="bg-body-tertiary min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
        <CCol md={8} className="text-center">
            <img
              src={logo}
              alt="Logo"
              style={{
                height: '220px',
                objectFit: 'contain',
                marginBottom: '1rem'
              }}
            />
          </CCol>
          <CCol md={8}>
            <CCardGroup>
              {/* Login Card */}
              <CCard className="p-4">
              <LoadingWrapper loading={loading}>
              <CCardBody>
                  <CForm onSubmit={handleLogin}>
                    <h1>Login</h1>
                    <p className="text-body-secondary">Sign in to your account</p>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput
                        value={username}
                        onChange={(e) => {setUsername(e.target.value); setError("")}}
                        placeholder="Username"
                        autoComplete="username"
                        required
                      />
                    </CInputGroup>
                    <CInputGroup className="mb-4">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <CFormInput
                        type="password"
                        placeholder="Password"
                        autoComplete="current-password"
                        onChange={(e) => {setPassword(e.target.value); ; setError("")}}
                        required
                      />
                    </CInputGroup>
                    <P_Error text={error}/>
                    <P_Success text={message}/>
                    <CRow>
                      <CCol xs={6}>
                        <CButton color="dark" className="px-4" type="submit">
                          Login
                        </CButton>
                      </CCol>
                      <CCol xs={6} className="text-right">
                        <CButton color="link" className="px-0">
                          Forgot password?
                        </CButton>
                      </CCol>
                    </CRow>
                  </CForm>
                </CCardBody>
            </LoadingWrapper>
        
              </CCard>
              {/* Registration Invitation Card */}
              <CCard className="text-white bg-dark py-5" style={{ width: '44%' }}>
                <CCardBody className="text-center">
                  <div>
                    <h2>Register as a Service Provider</h2>
                    <p>
                      Are you ready to offer your services and join our platform? fill the Application of Registeration now to start providing your services.
                    </p>
                    <Link to="/service-provider-new-registeration-application">
                      <CButton color="light" className="mt-3" active tabIndex={-1}>
                        Register Now
                      </CButton>
                    </Link>
                  </div>
                </CCardBody>
              </CCard>
            </CCardGroup>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  );
};

export default Login;
