import getHttpClient from './HttpClient';
import { HttpService } from './HttpService';

const httpClient = getHttpClient('http://localhost:5299/');

export default class ApiService extends HttpService {
    constructor(baseApiPath: string = '') {
        super(baseApiPath, httpClient);
    }
}
