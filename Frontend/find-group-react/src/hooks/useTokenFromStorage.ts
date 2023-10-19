import useAuth from "./useAuth";
import {IJWTResponse} from "../dto/IJWTResponse";

const useTokenFromStorage = () => {
    const { jwtResponse, setJwtResponse } = useAuth();

    const loadToken = () => {
        const response: IJWTResponse | null = JSON.parse(localStorage.getItem("jwtResponse") || "null")

        if (response != null) {
            setJwtResponse!({
                    ...jwtResponse,
                    jwt: response!.jwt,
                    refreshToken: response!.refreshToken
                });
        }
    }
    return loadToken;
}

export default useTokenFromStorage