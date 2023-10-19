import axios from 'axios';

const BASE_URL = 'http://localhost:5056/api/';

export const axiosBase = axios.create({
    baseURL: BASE_URL,
    headers: {
        common: {'Content-Type': 'application/json'}
    }
});