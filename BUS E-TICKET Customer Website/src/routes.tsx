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

const Trips = React.lazy(() => import('./views/pages/trips/Trips'))
const TripBooking = React.lazy(() => import('./views/pages/trips/TripBooking'))
const PaymentFailure = React.lazy(() => import('./views/pages/Payment/PaymentFailure'))
const PaymentConfirm = React.lazy(() => import('./views/pages/Payment/PaymentConfirm'))
const Ticket = React.lazy(() => import('./views/pages/Ticket/Ticket'))
const Invoice = React.lazy(() => import('./views/pages/Invoice/Invoice'))
const SearchBooking = React.lazy(() => import('./views/pages/Booking/SearchBooking'))
const Contact = React.lazy(() => import('./views/pages/Contact'))
const About = React.lazy(() => import('./views/pages/About'))
const Register = React.lazy(() => import('./views/pages/Customer/CustomerForm'))
const Login = React.lazy(() => import('./views/pages/login/Login'))
const Home = React.lazy(() => import('./components/home'))




// const RegisterManagerForm = React.lazy(() => import('./components/ManagerForm'))
const routes: Route[] = [
  { path: '/', element: Home, name: <Translation>{(t) => t('Trips')}</Translation>, allowedRoles: [EnUserRole.Unkown]},
  { path: '/trips', name: <Translation>{(t) => t('Trips')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:Trips},
  { path: '/trips/:id', name: <Translation>{(t) => t('Buy a Ticket')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:TripBooking},
  { path: '/payment/failure', name: <Translation>{(t) => t('Payment Is Failure')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:PaymentFailure},
  { path: '/payment/confirm', name: <Translation>{(t) => t('Payment Is Success')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:PaymentConfirm},
  { path: '/ticket/:id', name: <Translation>{(t) => t('Ticket Information')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:Ticket},
  { path: '/invoice/:id', name: <Translation>{(t) => t('Invoice Information')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:Invoice},
  { path: '/search-booking', name: <Translation>{(t) => t('Search Booking')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:SearchBooking},
  { path: '/contact', name: <Translation>{(t) => t('Contact Us')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:Contact},
  { path: '/about', name: <Translation>{(t) => t('Contact Us')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:About},
  { path: '/Register', name: <Translation>{(t) => t('Register New Customer')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:Register},
  { path: '/login', name: <Translation>{(t) => t('Login')}</Translation>, allowedRoles: [EnUserRole.Unkown], element:Login},
]

export default routes
