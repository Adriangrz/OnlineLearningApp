import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function dateGreaterThanNowValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    return Date.parse(control.value) > new Date().getTime()
      ? null
      : { dateGreaterThanNow: true };
  };
}
