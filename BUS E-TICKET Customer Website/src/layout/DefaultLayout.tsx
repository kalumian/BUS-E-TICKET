import {AppContent, AppFooter, AppHeader } from '../components/index'

const DefaultLayout = () => {
  return (
    <>
      <div className="wrapper d-flex flex-column min-vh-100">
        <AppHeader />
        <div className="body flex-grow-1">
          <AppContent />
        </div>
        <AppFooter />
      </div>
    </>
  )
}

export default DefaultLayout
