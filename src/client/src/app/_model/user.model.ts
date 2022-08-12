import { PinCodeModel } from "./pin-code.model";

export class User {
    id: string;
    phoneNumber: string;
    email: string;
    password: string;
    userName: string;
    firstName: string;
    lastName: string;
    token: string;
}

export class RegisterModel{
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
    active: true;
}

export class LoginResult {
    accessToken: string;
    refreshToken: string;
    tokenType: string;
    expiresIn: number;
    isActivationCodeValidated: boolean; 
    isProfileCompleted: boolean; 
    isActivationCodeSent: boolean; 
    isContractsAccepted: boolean; 
}

export class ProfileModel{
    user: User ;
    pinCode : PinCodeModel ;
}