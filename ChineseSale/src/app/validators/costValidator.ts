import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function CostValidator(min: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (control.value) {
            if ( control.value < min)
                return { PriceTooLow: { value: control.value } }
        }
        return null;
    };
}