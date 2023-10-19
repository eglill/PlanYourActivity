import axios from 'axios';

const BASE_URL = 'http://localhost:5056/api/';

export const axiosPublic = axios.create({
    baseURL: BASE_URL,
    headers: {
        common: {'Content-Type': 'application/json'}
    }
});

export const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    headers: {
        common: {'Content-Type': 'application/json'}
    }
});