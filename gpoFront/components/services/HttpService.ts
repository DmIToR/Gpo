export class HttpService {
    private baseApi: string = '';
    private httpClient: any;

    constructor(baseApiPath: string = '', httpClient: any) {
        this.baseApi = baseApiPath;
        this.httpClient = httpClient;
    }

    get baseHeaders() {
        return {
            'Content-Type': 'application/json',
        };
    }

    protected async get(path: string, token?: string) {
        const response = await this.httpClient.get(`${this.baseApi}/${path}`, 
        token ? {
            headers: {
                Authorization: `${token}`,
                'Content-Type': 'application/json'
            }
        } : {
            headers: this.baseHeaders
        });

        return response.data;
    }

    protected async post<T>(path: string, body: T, token: string) {
        const response = await this.httpClient.post(
            `${this.baseApi}/${path}`,
            body,
            {
                headers: {
                    Authorization: `${token}`,
                    'Content-Type': 'application/json'
                }
            }
        );

        return response.data;
    }

    protected async put<T>(path: string, body: T) {
        const response = await this.httpClient.put(
            `${this.baseApi}/${path}`,
            body,
            {
                headers: this.baseHeaders,
            }
        );

        return response.data;
    }
    

    protected async delete(path: string, token: string) {
        const response = await this.httpClient.delete(
            `${this.baseApi}/${path}`,
            {
                headers: {
                    Authorization: `${token}`,
                    'Content-Type': 'application/json'
                }
            }
        );

        return response.data;
    }
}
