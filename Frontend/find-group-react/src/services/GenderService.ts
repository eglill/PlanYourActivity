import { IGender } from "../domain/IGender";
import {AxiosInstance} from "axios";

export class GenderService {
    private genderUrl = 'v1/Gender/';
    private axios: AxiosInstance;
    constructor(private axiosInstance: AxiosInstance){
        this.axios = axiosInstance;
    }

    async getAll(): Promise<IGender[]> {
        try {
            const response = await this.axios.get<IGender[]>(this.genderUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async post(gender: IGender) : Promise<IGender | undefined> {
        try {
            const response = await this.axios.post(this.genderUrl, gender);
            if (response.status === 201) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
    }
}
