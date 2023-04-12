import axios from 'axios';

export default function getHttpClient(apiPath: string) {
    const httpClient = axios.create({
        baseURL: apiPath,
        withCredentials: false,
    });

    httpClient.interceptors.response.use(
        (response) => response,
        async (error) => {
            const originalRequest = error.config;

            // console.debug(
            //     `Request failed with error code ${error.response?.status}`
            // );

            throw error;
        }
    );

    return httpClient;
}
