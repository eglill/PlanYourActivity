import { Link, useNavigate } from "react-router-dom";
import { IdentityService } from "../services/IdentityService";
import jwt_decode from "jwt-decode";
import useAuth from "../hooks/useAuth";
import useAxiosPublic from "../hooks/useAxiosPublic";

const IdentityHeader = () => {
    const { jwtResponse, setJwtResponse } = useAuth();
    const navigate = useNavigate();
    const axiosPublic = useAxiosPublic();
    const identityService = new IdentityService(axiosPublic);

    const logout = () => {
        if (jwtResponse)
            identityService.logout(jwtResponse).then(response => {
                if (setJwtResponse)
                    setJwtResponse(null);
                navigate("/");
            });
    }

    if (jwtResponse) {
        let jwtObject: any = jwt_decode(jwtResponse.jwt);

        return (
            <>
                <li className="nav-item">
                    <Link to="info" className="nav-link text-dark">
                        <UserInfo jwtObject={jwtObject} />
                    </Link>
                </li>
                <li className="nav-item">
                    <a onClick={(e) => {
                        e.preventDefault();
                        logout();
                    }} className="nav-link text-dark" href="/">Logout</a>
                </li>
            </>
        );
    }
    return (
        <>
            <li className="nav-item">
                <Link to="register" className="nav-link text-dark">Register</Link>
            </li>
            <li className="nav-item">
                <Link to="login" className="nav-link text-dark">Login</Link>
            </li>
        </>
    );
}

interface IUserInfoProps {
    jwtObject: any
}

const UserInfo = (props: IUserInfoProps) => {
    return (
        <>
            {props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'] + ' '}
            {props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname']+ ' '}
            ({props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']})
        </>
    );
}

export default IdentityHeader;
