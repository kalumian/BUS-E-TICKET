import { CSpinner } from "@coreui/react-pro";
import React from "react";

interface LoadingWrapperProps {
    loading: boolean;
    children: React.ReactNode;
}

const LoadingWrapper: React.FC<LoadingWrapperProps> = ({ loading, children }) => {
    if (loading) {
        return <div className="d-flex justify-content-center"><CSpinner variant="grow" color="primary"/></div>;
    }
    return <>{children}</>;
};

export default LoadingWrapper;
