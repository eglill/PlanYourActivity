import useAuth from "./useAuth";
import {IdentityService} from "../services/IdentityService";
import useTokenFromStorage from "./useTokenFromStorage";
import useAxiosPublic from "./useAxiosPublic";

const useRefreshToken = () => {
    const { jwtResponse, setJwtResponse } = useAuth();
    const axiosPublic = useAxiosPublic();
    const identityService = new IdentityService(axiosPublic);
    const tokenFromStorage = useTokenFromStorage();

    const refresh = async () => {
        tokenFromStorage();
        if (jwtResponse) {
            const response = await identityService.refreshToken(jwtResponse);
            if (response !== undefined) {
                localStorage.setItem("jwtResponse", JSON.stringify(response));
                setJwtResponse!({
                    ...jwtResponse,
                    jwt: response!.jwt,
                    refreshToken: response!.refreshToken
                });
                return response.jwt;
            } else {
                localStorage.removeItem("jwtResponse");
                setJwtResponse!(null);
                return undefined;
            }
        }
    }
    return refresh;
};

export default useRefreshToken;