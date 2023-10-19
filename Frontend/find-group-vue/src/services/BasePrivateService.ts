import { useIdentityStore } from '@/stores/identity';
import Axios, { type AxiosInstance } from 'axios';
import { IdentityService } from './IdentityService';
import type { IJWTResponse } from '@/domain/IJWTResponse';
import { axiosBase } from '@/api/axios';

export abstract class BasePrivateService {
    private identityStore = useIdentityStore();
    private identityService = new IdentityService();

    protected axios: AxiosInstance;

    constructor() {

        this.axios = axiosBase;

        this.axios.interceptors.request.use(
            config => {
                if (!config.headers['Authorization']) {
                    config.headers['Authorization'] = `Bearer ${this.identityStore.jwtResponse?.jwt}`;
                }
                return config;
            }, (error) => Promise.reject(error)
        );

        this.axios.interceptors.response.use(
            response => response,
            async (error) => {

                const prevRequest = error?.config;
                if (error?.response?.status === 401 && !prevRequest?.sent) {
                    prevRequest.sent = true;
                    const newAccessToken = await this.refreshToken();
                    prevRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                    return this.axios(prevRequest);
                }
                return Promise.reject(error);
            }
        );
    }

    private async refreshToken() {
        this.tokenFromStorage();
        if (this.identityStore.jwtResponse) {
            const response = await this.identityService.refreshToken(this.identityStore.jwtResponse);
            if (response !== undefined) {

                localStorage.setItem("jwtResponse", JSON.stringify(response));

                this.identityStore.setJwtResponse!({
                    jwt: response!.jwt,
                    refreshToken: response!.refreshToken
                });
                return response.jwt;
            } else {

                localStorage.removeItem("jwtResponse");

                this.identityStore.setJwtResponse!(null);
                return undefined;
            }
        }
    }

    private tokenFromStorage() {
        let response: IJWTResponse | null = JSON.parse(localStorage.getItem("jwtResponse") || "null")

        if (response != null) {
            this.identityStore.setJwtResponse!({
                jwt: response!.jwt,
                refreshToken: response!.refreshToken
            });
        }
    }
}