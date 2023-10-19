import {IGender} from "../domain/IGender";

export interface IRegisterData {
    password: string,
    confirmPassword: string,
    email: string,
    firstName: string,
    lastName: string,
    gender: string,
    birthDate: string
}
