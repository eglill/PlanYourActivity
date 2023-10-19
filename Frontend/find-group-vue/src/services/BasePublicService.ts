import { axiosBase } from "@/api/axios";
import type { AxiosInstance } from "axios";

export abstract class BasePublicService {
    protected axios: AxiosInstance;

    constructor() {

        this.axios = axiosBase;
    }
}