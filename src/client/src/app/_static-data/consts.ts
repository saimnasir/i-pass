import { CustomValidation} from "../_model/validation.model";

export var phoneNumberRequired = new CustomValidation('required', 'Phone number is required');
export var phoneNumberMinLength = new CustomValidation('minlength', 'Phone number must be at least 10 characters long');
export var phoneNumberMaxlength = new CustomValidation('maxlength', 'Phone number cannot be more than 10 characters long');
export var phoneNumberPattern = new CustomValidation('pattern', 'Phone number must contain only numbers and letters');
export var phoneNumberExists = new CustomValidation('validPhoneNumber', 'Phone number has already been taken');

export var passwordRequired = new CustomValidation('required', 'Password is required');
export var passwordminlength = new CustomValidation('minlength', 'Password must be at least 8 characters long');
export var passwordPattern = new CustomValidation('pattern', 'Password must contain at least one uppercase, one lowercase, and one number');

export var confirmPasswordminlength = new CustomValidation('required', 'Confirm password is required');
export var confirmPasswordPattern = new CustomValidation('mustMatch', 'Confirm password must match with password!');

export var PasswordRegex = '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{7,}';
export var PhoneNumberRegex = '^[0-9]*$';

export var PhoneNumberValidationMessages = [
    phoneNumberRequired,
    phoneNumberMinLength,
    phoneNumberMaxlength,
    phoneNumberPattern,
    phoneNumberExists
];

export var PasswordValidationMessages = [
    passwordRequired,
    passwordminlength,
    passwordPattern
];

export var ConfirmPasswordValidationMessages = [
    confirmPasswordminlength,
    confirmPasswordPattern
];
 