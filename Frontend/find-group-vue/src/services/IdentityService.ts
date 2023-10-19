import Axios, { type AxiosInstance } from 'axios';
import { type IJWTResponse } from "../domain/IJWTResponse";
import { type ILoginData } from "../domain/ILoginData";
import { type IRegisterData } from "../domain/IRegisterData";
import { BasePrivateService } from "./BasePrivateService";
import { BasePublicService } from './BasePublicService';

export class IdentityService extends BasePublicService{

    private baseUrl = 'v1/identity/account/';

    constructor() {
        super();
    }

    async register(data: IRegisterData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>(this.baseUrl + 'register', data);

            console.log('register response', response);
            if (response.status === 200) {

                localStorage.setItem("jwtResponse", JSON.stringify(response.data));

                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async login(data: ILoginData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>(this.baseUrl + 'login', data);

            console.log('login response', response);
            if (response.status === 200) {

                localStorage.setItem("jwtResponse", JSON.stringify(response.data));

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
                this.baseUrl + 'logout', 
                data,
                {
                    headers: {
                        'Authorization': 'Bearer ' + data.jwt
                    }
                }
            );

            console.log('logout response', response);
            if (response.status === 200) {
                localStorage.removeItem("jwtResponse");
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
                this.baseUrl + 'refreshtoken', 
                data
            );

            console.log('refresh token response', response);
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