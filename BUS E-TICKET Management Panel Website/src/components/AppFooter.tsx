import React from 'react'
import { CFooter } from '@coreui/react-pro'

const AppFooter = () => {
  return (
    <CFooter className="px-4">
      <div>
        <a href="https://coreui.io" target="_blank" rel="noopener noreferrer">
        </a>
        <span className="ms-1">&copy; 2025 BUS E-TICKET.</span>
      </div>
      <div className="ms-auto">
        <span className="me-1">Created By</span>
        <a
          href="https://www.linkedin.com/in/kalumian/"
          target="_blank"
          rel="noopener noreferrer"
        >
          KALUMIAN
        </a>
      </div>
    </CFooter>
  )
}

export default AppFooter
