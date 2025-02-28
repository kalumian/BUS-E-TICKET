import { Navigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from '../store';
import EnUserRole from 'src/Enums/EnUserRole';
interface ProtectedRouteProps {
  children: JSX.Element;
  allowedRoles: EnUserRole[];
}

const ProtectedRoute = ({ children, allowedRoles }: ProtectedRouteProps) => {
    const token : string | null = useSelector((state: RootState) => state.auth.token)
    const userRole : EnUserRole | null = useSelector((state: RootState) => state.auth.user?.Role ?? EnUserRole.Unkown)
    const isAuthorized : boolean = allowedRoles.includes(EnUserRole.Unkown) ? true : allowedRoles.includes(userRole);
    if (!token && !isAuthorized) return <Navigate to="/login" replace />
    if (!isAuthorized) return <Navigate to="/unauthorized" replace />
    
    return children
};

export default ProtectedRoute;
