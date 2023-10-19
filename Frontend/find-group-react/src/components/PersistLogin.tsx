import { Outlet } from "react-router-dom";
import {useEffect, useState} from "react";
import useAuth from '../hooks/useAuth';
import useRefreshToken from "../hooks/useRefreshToken";

const PersistLogin = () => {
    const { jwtResponse } = useAuth();
    const [isLoading, setIsLoading] = useState(true);
    const refresh = useRefreshToken();

    useEffect( () => {
        let isMounted = true;
        const refreshToken = async () => {
            try {
                await refresh();
            } catch (e) {
                console.error(e);
            } finally {
                isMounted && setIsLoading(false);
            }
        }
        !jwtResponse?.jwt ? refreshToken() : setIsLoading(false);

        return function cleanup() {
            isMounted = false;
        }
    }, [])

    return (
        <>
            {isLoading
                ? <p>Loading...</p>
                : <Outlet />
            }
        </>
    );
}

export default PersistLogin