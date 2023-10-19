import {AxiosInstance} from "axios";
import {IAppUser} from "../domain/IAppUser";

export class BlockUserService {
    private groupUrl = 'v1/BlockUser/';
    private axios: AxiosInstance;

    constructor(private axiosInstance: AxiosInstance) {
        this.axios = axiosInstance;
    }

    async getAllBlockedUsers(): Promise<IAppUser[]> {
        try {
            const response = await this.axios.get<IAppUser[]>(this.groupUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async blockUser(userId: string): Promise<boolean> {
        try {
            const response = await this.axios.post(this.groupUrl + userId);
            if (response.status === 200) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async unblockUser(userId: string): Promise<boolean> {
        try {
            const response = await this.axios.delete(this.groupUrl + userId);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }
}