import { PinCodeModel } from "./pin-code.model";

export class User {
    id: string;
    phoneNumber: string;
    password: string;
    userName: string;
    firstName: string;
    lastName: string;
    token: string;
}

export class RegisterModel{
    phoneNumber: string;
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
    user = new User();
    pinCode = new PinCodeModel();
}