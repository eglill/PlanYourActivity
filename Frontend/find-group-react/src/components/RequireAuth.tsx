import useAuth from "../hooks/useAuth";
import {Outlet, useLocation, Navigate} from "react-router-dom";

const RequireAuth = () => {
    const { jwtResponse } = useAuth();
    const location = useLocation();

    return (
        jwtResponse?.jwt ?
            <Outlet />
            : <Navigate to="/login" state={{ from: location}} replace />
    );
}

export default RequireAuth;