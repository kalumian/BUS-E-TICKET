import React from 'react'
import {
  CCard,
  CCardBody,
  CCardHeader,
  CCol,
  CRow,
  CContainer,
  CCardText,
} from '@coreui/react-pro'

const Home = () => {
  return (
    <CContainer>
      <CRow className="justify-content-center">
        <CCol md={12}>
          <CCard className="mb-4">
            <CCardHeader className="text-center">
              <h1 className="display-4">E-Bus Ticketing System</h1>
              <h3 className="text-muted">Graduation Project 2025</h3>
              <h4 className="text-primary">Muhammad Kalumian</h4>
            </CCardHeader>
            <CCardBody>
              <div className="mb-4">
                <h2 className="text-center mb-4">Project Overview</h2>
                <CCardText>
                  An integrated system for managing bus tickets and reservations, 
                  designed to streamline the travel process and organize trips 
                  through a complete electronic platform.
                </CCardText>
              </div>

              <CRow>
                <CCol md={4}>
                  <CCard className="h-100 shadow-sm">
                    <CCardBody>
                      <h3 className="text-primary">Admin Dashboard</h3>
                      <CCardText>
                        Enables administrators to monitor and manage all operations,
                        users, and analytics through a comprehensive control panel.
                      </CCardText>
                    </CCardBody>
                  </CCard>
                </CCol>

                <CCol md={4}>
                  <CCard className="h-100 shadow-sm">
                    <CCardBody>
                      <h3 className="text-primary">Service Provider Portal</h3>
                      <CCardText>
                        Allows transport companies to manage their trips,
                        schedule routes, and update bus information efficiently.
                      </CCardText>
                    </CCardBody>
                  </CCard>
                </CCol>

                <CCol md={4}>
                  <CCard className="h-100 shadow-sm">
                    <CCardBody>
                      <h3 className="text-primary">Booking System</h3>
                      <CCardText>
                        User-friendly interface for travelers to book tickets,
                        track their journeys, and manage their travel plans.
                      </CCardText>
                    </CCardBody>
                  </CCard>
                </CCol>
              </CRow>

              <CRow className="mt-4">
                <CCol md={12}>
                  <CCard className="text-center">
                    <CCardBody>
                      <h4>Key Features</h4>
                      <ul className="list-unstyled">
                        <li>Real-time booking and tracking</li>
                        <li>Secure payment processing</li>
                        <li>Advanced route management</li>
                        <li>Analytics and reporting</li>
                        <li>Multi-user support system</li>
                      </ul>
                    </CCardBody>
                  </CCard>
                </CCol>
              </CRow>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </CContainer>
  )
}

export default Home