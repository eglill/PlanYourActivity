import {AxiosInstance} from "axios";
import {ICountry} from "../domain/ICountry";

export class CountryService {
    private genderUrl = 'v1/Country/';
    private axios: AxiosInstance;
    constructor(private axiosInstance: AxiosInstance){
        this.axios = axiosInstance;
    }

    async getAll(): Promise<ICountry[]> {
        try {
            const response = await this.axios.get<ICountry[]>(this.genderUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }
}
