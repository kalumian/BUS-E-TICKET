import React, { LazyExoticComponent, FC, ReactNode } from 'react'
import { Translation } from 'react-i18next'
import EnUserRole from './Enums/EnUserRole'

export type Route = {
  element?: LazyExoticComponent<FC>
  exact?: boolean
  name?: ReactNode
  path?: string
  routes?: Route[]
  allowedRoles : EnUserRole[]
}

const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))
const Managers = React.lazy(() => import('./views/pages/Accounts/Managers/Managers'))
const ManagerForm = React.lazy(() => import('./views/pages/Accounts/Managers/ManagerForm'))
const ServiceProviderForm = React.lazy(() => import('./views/pages/Accounts/ServiceProviders/ServiceProviderForm'))
const ServiceProviders = React.lazy(() => import('./views/pages/Accounts/ServiceProviders/ServiceProviders'))
const SPApplications = React.lazy(() => import('./views/pages/SPApplication/SPApplications'))
const SPApplicationReview = React.lazy(() => import('./views/pages/SPApplication/SPApplicationReview.tsx'))
const SPApplicationDetails = React.lazy(() => import('./views/pages/SPApplication/SPApplicationDetails.tsx'))
const TripForm = React.lazy(() => import('./views/pages/trips/TripForm.tsx'))
const Trips = React.lazy(() => import('./views/pages/trips/Trips.tsx'))
const TripDetails = React.lazy(() => import('./views/pages/trips/TripDetails.tsx'))
const Home = React.lazy(() => import('./views/pages/Home.tsx'))
const Passengers = React.lazy(() => import('./views/pages/Passengers/Passengers.tsx'))
const PassengerForm = React.lazy(() => import('./views/pages/Passengers/PassengerForm.tsx'))
const Bookings = React.lazy(() => import('./views/pages/Booking/Bookings.tsx'))
const BookingDetail = React.lazy(() => import('./views/pages/Booking/BookingDetails.tsx'))
const Ticket = React.lazy(() => import('./views/pages/Ticket/Ticket.tsx'))
const Invoice = React.lazy(() => import('./views/pages/Invoice/Invoice.tsx'))
const Customers = React.lazy(() => import('./views/pages/Accounts/Customers/Customers.tsx'))
const CustomerForm = React.lazy(() => import('./views/pages/Accounts/Customers/CustomerForm.tsx'))



// const RegisterManagerForm = React.lazy(() => import('./components/ManagerForm'))



const routes: Route[] = [
  { path: '/', element: Home, name: <Translation>{(t) => t('home')}</Translation>, allowedRoles: [EnUserRole.Provider, EnUserRole.Admin] },
  {
    path: '/dashboard',
    name: <Translation>{(t) => t('dashboard')}</Translation>,
    element: Dashboard,
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider]
  },
  {
    path: '/account/managers',
    name: <Translation>{(t) => t('Managers')}</Translation>,
    element: Managers,
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider]
  },
  {
    path: '/account/managers/add',
    name: <Translation>{(t) => t('Add New Manager')}</Translation>,
    element: ManagerForm,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/details/:id',
    name: <Translation>{(t) => t("Show Manager's Details")}</Translation>,
    element: ManagerForm,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/managers/update/:id',
    name: <Translation>{(t) => t('Update Manager Information')}</Translation>,
    element: ManagerForm,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/service-providers',
    name: <Translation>{(t) => t('Service Providers')}</Translation>,
    element: ServiceProviders,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/service-providers/details/:id',
    name: <Translation>{(t) => t("Show Service Provider's Details")}</Translation>,
    element: ServiceProviderForm,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/service-providers/update/:id',
    name: <Translation>{(t) => t('Update Service Provider Information')}</Translation>,
    element: ServiceProviderForm,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/registration-applications/review/:id',
    name: <Translation>{(t) => t("Application Review")}</Translation>,
    element: SPApplicationReview,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/registration-applications',
    name: <Translation>{(t) => t("Registration Application")}</Translation>,
    element: SPApplications,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/account/registration-applications/details/:id',
    name: <Translation>{(t) => t("Application Detials")}</Translation>,
    element: SPApplicationDetails,
    
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/trips/add',
    name: <Translation>{(t) => t("Add New Trip")}</Translation>,
    element: TripForm,
    
    allowedRoles: [EnUserRole.Provider]
  },
  {
    path: '/trips',
    name: <Translation>{(t) => t("Trips")}</Translation>,
    element: Trips,
    allowedRoles: [EnUserRole.Provider, EnUserRole.Admin]
  },
  {
    path: '/trips/:id',
    name: <Translation>{(t) => t("Trip Details")}</Translation>,
    element: TripDetails,
    allowedRoles: [EnUserRole.Provider, EnUserRole.Admin]
  },
  {
    path: '/passengers',
    name: <Translation>{(t) => t("Passengers List")}</Translation>,
    element: Passengers,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/passenger/:id',
    name: <Translation>{(t) => t("Passenger Details")}</Translation>,
    element: PassengerForm,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/bookings',
    name: <Translation>{(t) => t("Bookings List")}</Translation>,
    element: Bookings,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/booking/:pnr',
    name: <Translation>{(t) => t("Booking Details")}</Translation>,
    element: BookingDetail,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    path: '/bookings/:id',
    name: <Translation>{(t) => t("Trip Bookings")}</Translation>,
    element: Bookings,
    allowedRoles: [EnUserRole.Admin]
  },
  { path: '/ticket/:id', name: <Translation>{(t) => t('Ticket Information')}</Translation>, allowedRoles: [EnUserRole.Admin, EnUserRole.Provider], element:Ticket},
  { path: '/invoice/:id', name: <Translation>{(t) => t('Invoice Information')}</Translation>, allowedRoles: [EnUserRole.Admin, EnUserRole.Provider], element:Invoice},
  { path: '/account/customers', name: <Translation>{(t) => t('Customers List')}</Translation>, allowedRoles: [EnUserRole.Admin, EnUserRole.Provider], element:Customers},
  { path: '/account/customers/details/:id', name: <Translation>{(t) => t('Customers Details')}</Translation>, allowedRoles: [EnUserRole.Admin, EnUserRole.Provider], element:CustomerForm},


]

export default routes
