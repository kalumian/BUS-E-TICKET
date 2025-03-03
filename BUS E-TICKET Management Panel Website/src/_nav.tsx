import { ElementType } from 'react'
import { CNavGroup, CNavItem, CNavTitle } from '@coreui/react-pro'
import { Translation } from 'react-i18next'
import { FaTachometerAlt, FaUsers, FaUserPlus, FaUserFriends, FaClipboard, FaFileAlt, FaCheckCircle, FaBus, FaDollarSign, FaTicketAlt, FaCarAlt, FaSquare, FaPlus, FaCreditCard, FaRegCreditCard } from 'react-icons/fa'
import { IoIosAnalytics, IoIosPeople, IoIosPersonAdd, IoIosContacts, IoIosList, IoIosClipboard, IoIosCar, IoIosCart } from 'react-icons/io'
import EnUserRole from './Enums/EnUserRole'

export type Badge = {
  color: string
  text: string
}

export type NavItem = {
  badge?: Badge
  component: string | ElementType
  href?: string
  icon?: string | JSX.Element
  items?: NavItem[]
  name: string | JSX.Element
  to?: string
  allowedRoles: EnUserRole[]
}

const _nav: NavItem[] = [
  {
    component: CNavItem,
    name: <Translation>{(t) => t('dashboard')}</Translation>,
    to: '/dashboard',
    icon: <IoIosAnalytics className="nav-icon" />,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavTitle,
    name: <Translation>{(t) => t('Accounts Management')}</Translation>,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('Managers')}</Translation>,
    to: '/account/managers',
    icon: <IoIosPeople className="nav-icon" />,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('Service Providers')}</Translation>,
    to: '/account/service-providers',
    icon: <IoIosContacts className="nav-icon" />, 
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('Customers')}</Translation>,
    to: '/account/customers',
    icon: <IoIosPersonAdd className="nav-icon" />,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavTitle,
    name: <Translation>{(t) => t('Registration Management')}</Translation>,
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('Registration Applications')}</Translation>,
    to: '/account/registration-applications',
    icon: <IoIosList className="nav-icon" />, 
    allowedRoles: [EnUserRole.Admin]
  },
  {
    component: CNavTitle,
    name: <Translation>{(t) => t('Trips Management')}</Translation>,
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider]
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('All Trips List')}</Translation>,
    to: '/trips',
    icon: <FaBus className="nav-icon" />,
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider]
  },
  {
    component: CNavItem,
    name: 'Add New Trip',
    to: '/trips/add',
    icon: <FaPlus className="nav-icon" />, 
    allowedRoles: [EnUserRole.Provider],
  },
  {
    component: CNavTitle,
    name: <Translation>{(t) => t('Booking Management')}</Translation>,
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider]
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('All Bookings List')}</Translation> ,
    to: '/bookings',
    icon: <IoIosList className="nav-icon" />,  
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider],
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('Passengers')}</Translation> ,
    to: '/Passengers',
    icon: <IoIosPeople className="nav-icon" />,  
    allowedRoles: [EnUserRole.Admin, EnUserRole.Provider],
  },
  {
    component: CNavItem,
    name: <Translation>{(t) => t('Payments')}</Translation>,
    to: '/Payments',
    icon: <FaRegCreditCard className="nav-icon" />, 
    allowedRoles: [EnUserRole.Provider, EnUserRole.Admin]
  },
  
]

export default _nav
