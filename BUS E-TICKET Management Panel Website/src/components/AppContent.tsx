import { Suspense } from 'react'
import { Route, Routes, useNavigate } from 'react-router-dom'
import { CContainer, CSpinner } from '@coreui/react-pro'

// routes config
import routes from '../routes'
import ProtectedRoute from './ProtectedRoute'
import React from 'react'
const Page404 = React.lazy(() => import('../views/pages/ErrorPages/page404/Page404'))
const AppContent = () => {

  return (
    <CContainer lg className="px-4">
      <Suspense fallback={<CSpinner color="primary" />}>
        <Routes>
          {routes.map((route, idx) => {
            return (
              route.element &&
                <Route key={idx} path={route.path}
                 element={
                  <ProtectedRoute allowedRoles={route.allowedRoles}>
                    <route.element />
                  </ProtectedRoute> } />
            )
          })}
          <Route path='*' element={<Page404/>}/>
        </Routes>
      </Suspense>
    </CContainer>
  )
}

// const Redirector = () => {
//   const navigate = useNavigate()
// }
export default AppContent
