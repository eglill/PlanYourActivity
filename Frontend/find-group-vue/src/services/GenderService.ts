import type { IGender } from "@/domain/IGender";
import { BasePublicService } from "./BasePublicService";

export class GenderService  extends BasePublicService {

    
    private baseUrl = 'v1/Gender/';
    constructor() {
        super();
    }

    async getAll(): Promise<IGender[]> {
        try {
            const response = await this.axios.get<IGender[]>(this.baseUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }
}