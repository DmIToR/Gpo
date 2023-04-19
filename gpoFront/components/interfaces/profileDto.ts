export interface ProfileUserDto {
  profile: {
    group?: string;
    department?: string;
    post?: string;
    id: string;
    name: string;
    surname: string;
    patronymic: string;
  };
  role: number;
}

export interface ProfileUserInfo {
  group?: string;
  department?: string;
  post?: string;
  id: string;
  name: string;
  surname: string;
  patronymic: string;
}
