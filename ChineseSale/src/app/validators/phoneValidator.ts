import { AbstractControl, ValidatorFn } from '@angular/forms';

export function phoneNumberValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const phoneNumberPattern = /^\d{10}$/; // פטרן למספר טלפון בעל 10 ספרות
    
    const isValid = phoneNumberPattern.test(control.value);
    return isValid ? null : { 'invalidPhoneNumber': { value: control.value } };
  };
}