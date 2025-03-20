import { useEffect, useState } from 'react';
import { CRow, CCol, CCard, CCardBody, CContainer, CCardHeader, CCardText } from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import { 
  cilLibrary, 
  cilBusAlt, 
  cilSpeedometer,
  cilPeople,
  cilDollar 
} from '@coreui/icons';
import { DashboardStats } from 'src/Interfaces/dashboardStatus';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store';
import { getDashboardStats } from 'src/Services/dashboardService';
import { fetchData } from 'src/Services/apiService';
import LoadingWrapper from 'src/components/LoadingWrapper';

const DashboardWidget = ({ 
  icon, 
  color, 
  value, 
  label, 
  trend 
}: { 
  icon: any; 
  color: string; 
  value: string | number; 
  label: string; 
  trend?: string 
}) => (
  <CCard className={`border-${color} shadow-sm h-100`}>
    <CCardBody>
      <div className="d-flex justify-content-between align-items-start">
        <div>
          <div className={`text-${color} fs-4 fw-bold mb-2`}>{value}</div>
          <div className="text-medium-emphasis text-uppercase fw-semibold">{label}</div>
          {trend && <small className="text-success fw-semibold">{trend}</small>}
        </div>
        <div className={`bg-${color} bg-opacity-10 p-3 rounded-circle`}>
          <CIcon icon={icon} height={24} className={`text-${color}`} />
        </div>
      </div>
    </CCardBody>
  </CCard>
);

const Dashboard = () => {
  const [stats, setStats] = useState<DashboardStats | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const token: string | null = useSelector((state: RootState) => state.auth.token);

  useEffect(() => {
    if(token) {
      fetchData(() => getDashboardStats(token), setStats, setError, setLoading, {token: token})
    }
  }, [token]);


  if (error) return <div className="text-center text-danger">{error}</div>;

  const dashboardWidgets = [
    {
      icon: cilLibrary,
      color: 'primary',
      value: stats?.totalTicketsSold || 0,
      label: 'Total Tickets',
      trend: '+12% from last month'
    },
    {
      icon: cilDollar,
      color: 'success',
      value: `$${stats?.totalRevenue || 0}`,
      label: 'Total Revenue',
      trend: '+5.4% increase'
    },
    {
      icon: cilBusAlt,
      color: 'warning',
      value: stats?.scheduledTrips || 0,
      label: 'Active Trips',
      trend: '24 upcoming'
    },
    {
      icon: cilPeople,
      color: 'info',
      value: stats?.totalUsers || 0,
      label: 'Total Users',
      trend: '15 new today'
    },
    {
      icon: cilSpeedometer,
      color: 'danger',
      value: `$${stats?.averageRevenuePerTrip || 0}`,
      label: 'Avg. Trip Revenue',
      trend: '+2.5% growth'
    }
  ];

  return (
    <LoadingWrapper loading={loading}>
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
        <CRow>
          <CCol xs={12}>
            <h2 className="mb-4">Dashboard Overview</h2>
          </CCol>
        </CRow>
        <CRow className="g-4">
          {dashboardWidgets.map((widget, index) => (
            <CCol key={index} xs={12} sm={6} lg={4} xl={3}>
              <DashboardWidget {...widget} />
            </CCol>
          ))}
        </CRow>
    </CContainer>
    </LoadingWrapper>
  );
};

export default Dashboard;