import { accountData, accountId } from "../interfaces/accountDto";
import ApiService from "../services/ApiService";

class AccountApi extends ApiService {
    constructor() {
        super(`Account`);
    }

    signIn(userName: string, password: string): Promise<accountId>  {
        const data: accountData = {
            username: userName,
            password: password
        }
        return this.post(`SignIn`, data)
    }
}

export default new AccountApi();
