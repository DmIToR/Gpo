export interface createUserDto {
    userName: string,
    email: string,
    password: string,
    role: number
}

export interface changeUserPasswordDto {
    userName: string,
    password: string,
}