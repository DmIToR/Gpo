import { ProfileUserDto } from "../interfaces/profileDto";
import ApiService from "../services/ApiService";

class profileApi extends ApiService {
    constructor() {
        super(`Profile`);
    }

    getUserProfile(id: string): Promise<ProfileUserDto> {
        return this.get(`${id}`)
    }
}

export default new profileApi();
