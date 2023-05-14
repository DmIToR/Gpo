import { changeUserPasswordDto, createUserDto } from "../interfaces/adminDto";
import ApiService from "../services/ApiService";

class AdminApi extends ApiService {
    constructor() {
        super(`Admin`);
    }

    createUser(userName: string, email: string, password: string, role: number) {
        const data: createUserDto = {
            userName,
            email,
            password,
            role
        }
        return this.post(`Tools/CreateUser`, data, '')
    }

    deleteUser(userName: string) {
        return this.delete(`Tools/DeleteUser/${userName}`, '')
    }

    changeUserPassword(userName: string, password: string) {
        const data: changeUserPasswordDto = {
            userName,
            password,
        }
        return this.patch(`Tools/ChangeUserPassword`, data, '')
    }

    getUsers() {
        return this.get(`Tools/GetUsers`,'')
    }

    getUserByUsername(userName: string) {
        return this.get(`Tools/GetUserByUsername/${userName}`, '')
    }
}

export default new AdminApi();
