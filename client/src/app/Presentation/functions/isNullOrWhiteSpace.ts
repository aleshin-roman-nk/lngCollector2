import { AbstractControl, ValidatorFn } from '@angular/forms';

export function isNullOrWhiteSpace(): ValidatorFn {
  return (control: AbstractControl): {[key: string]: any} | null => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': 'value is only whitespace' };
  };
}
