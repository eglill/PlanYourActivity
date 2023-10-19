import {AxiosInstance} from "axios";
import {IColour} from "../domain/IColour";

export class ColourService {
    private genderUrl = 'v1/Colour/';
    private axios: AxiosInstance;
    constructor(private axiosInstance: AxiosInstance){
        this.axios = axiosInstance;
    }

    async getAll(): Promise<IColour[]> {
        try {
            const response = await this.axios.get<IColour[]>(this.genderUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }
}
