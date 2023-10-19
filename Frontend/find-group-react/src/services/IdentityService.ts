import { IJWTResponse } from "../dto/IJWTResponse";
import { IRegisterData } from "../dto/IRegisterData";
import {ILoginData} from "../dto/ILoginData";
import {AxiosInstance} from "axios";

export class IdentityService {
    private identityUrl = 'v1/identity/account/';
    private axios: AxiosInstance;
    constructor(private axiosInstance: AxiosInstance){
        this.axios = axiosInstance;
    }

    async register(data: IRegisterData): Promise<string | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>(this.identityUrl + 'register', data);


            if (response.status === 200) {
                return "";
            }
            return undefined;
        } catch (e : any) {
            if (e.response.data.error) {
                return e.response.data.error;
            }
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async login(data: ILoginData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>(this.identityUrl + 'login', data);

            if (response.status === 200) {
                if (data.persist) {
                    localStorage.setItem("jwtResponse", JSON.stringify(response.data))
                }
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async logout(data: IJWTResponse): Promise<true | undefined> {
        try {
            const response = await this.axios.post(
                this.identityUrl + 'logout',
                data,
                {
                    headers: {
                        'Authorization': 'Bearer ' + data.jwt
                    }
                }
            );

            if (response.status === 200) {
                localStorage.removeItem("jwtResponse")
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async refreshToken(data: IJWTResponse): Promise<IJWTResponse | undefined> {
        try {

            const response = await this.axios.post<IJWTResponse>(
                this.identityUrl + 'refreshtoken',
                data
            );
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
}
