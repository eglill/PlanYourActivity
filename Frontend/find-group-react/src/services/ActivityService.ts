import {AxiosInstance} from "axios";
import {IActivity} from "../domain/IActivity";

export class ActivityService {
    private activityUrl = 'v1/Activity/';
    private axios: AxiosInstance;
    constructor(private axiosInstance: AxiosInstance){
        this.axios = axiosInstance;
    }

    async getAll(): Promise<IActivity[]> {
        try {
            const response = await this.axios.get<IActivity[]>(this.activityUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }
}
