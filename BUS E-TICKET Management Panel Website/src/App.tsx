import React, { Suspense, useEffect } from 'react'
import { HashRouter, Route, Routes } from 'react-router-dom'
import { useSelector } from 'react-redux'

import { CSpinner, useColorModes } from '@coreui/react-pro'

import './scss/style.scss'

// We use those styles to show code examples, you should remove them in your application.
import './scss/examples.scss'

import  { UIState } from './Interfaces/uiSetting'

// Containers
const DefaultLayout = React.lazy(() => import('./layout/DefaultLayout'))

const Login = React.lazy(() => import('./views/pages/login/Login'))
const Register = React.lazy(() => import('./views/pages/register/Register'))
// const Page404 = React.lazy(() => import('./views/pages/ErrorPages/page404/Page404'))
// const Page500 = React.lazy(() => import('./views/pages/ErrorPages/page500/Page500'))
const Page401  = React.lazy(() => import('./views/pages/ErrorPages/page401/Page401'))
const ServiceProviderRegisterApplication = React.lazy(() => import('./views/pages/Accounts/ServiceProviders/ServiceProviderForm'))

// Email App
const EmailApp = React.lazy(() => import('./views/apps/email/EmailApp'))

const App = () => {
  const { isColorModeSet, setColorMode } = useColorModes(
    'coreui-pro-react-admin-template-theme-light',
  )
  const storedTheme = useSelector((state: UIState) => state.theme)

  useEffect(() => {
    const urlParams = new URLSearchParams(window.location.href.split('?')[1])
    let theme = urlParams.get('theme')

    if (theme !== null && theme.match(/^[A-Za-z0-9\s]+/)) {
      theme = theme.match(/^[A-Za-z0-9\s]+/)![0]
    }

    if (theme) {
      setColorMode(theme)
    }

    if (isColorModeSet()) {
      return
    }

    setColorMode(storedTheme)
  }, []) // eslint-disable-line react-hooks/exhaustive-deps

  return (
    <HashRouter>
      <Suspense
        fallback={
          <div className="pt-3 text-center">
            <CSpinner color="primary" variant="grow" />
          </div>
        }
      >
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          {/* <Route path="/404" element={<Page404/>} /> */}
          {/* <Route path="/500" element={<Page500/>} /> */}
          <Route path='/service-provider-new-registeration-application' element={<ServiceProviderRegisterApplication/>}/>
          <Route path='/unauthorized' element={<Page401/>}/>
          <Route path="/apps/email/*" element={<EmailApp />} />
          <Route path="*" element={<DefaultLayout />} />
        </Routes>
      </Suspense>
    </HashRouter>
  )
}

export default App
