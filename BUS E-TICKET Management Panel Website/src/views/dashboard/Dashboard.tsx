import { useEffect, useState, Suspense } from 'react';
import { CRow, CCol, CSpinner, CCard, CCardBody } from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import { 
  cilLibrary, 
  cilMoney, 
  cilBusAlt, 
  cilUser, 
  cilChartLine,
  cilSpeedometer,
  cilPeople,
  cilDollar 
} from '@coreui/icons';
import { DashboardStats } from 'src/Interfaces/dashboardStatus';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store';
import { getDashboardStats } from 'src/Services/dashboardService';
import { fetchData } from 'src/Services/apiService';

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

  const LoadingSpinner = () => (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <CSpinner color="primary" variant="grow" />
    </div>
  );

  if (loading) return <LoadingSpinner />;
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
    <Suspense fallback={<LoadingSpinner />}>
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
    </Suspense>
  );
};

export default Dashboard;