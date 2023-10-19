import {AxiosInstance} from "axios";
import {IAppUser} from "../domain/IAppUser";

export class UsersService {
    private groupUrl = 'v1/Users/';
    private axios: AxiosInstance;

    constructor(private axiosInstance: AxiosInstance) {
        this.axios = axiosInstance;
    }

    async getAll(): Promise<IAppUser[]> {
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
}