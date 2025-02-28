import { CSpinner } from "@coreui/react-pro";
import React from "react";

interface LoadingWrapperProps {
    loading: boolean;
    children: React.ReactNode;
}

const LoadingWrapper: React.FC<LoadingWrapperProps> = ({ loading, children }) => {
    if (loading) {
        return <><CSpinner color="primary"/></>;
    }
    return <>{children}</>;
};

export default LoadingWrapper;
