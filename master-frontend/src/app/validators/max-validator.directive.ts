import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validators } from '@angular/forms';

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: '[max]',
  providers: [{ provide: NG_VALIDATORS, useExisting: MaxValidatorDirective, multi: true }]
})
export class MaxValidatorDirective {

  @Input() max: number;

  validate(control: AbstractControl): {[key: string]: any} | null {
    return Validators.max(this.max)(control);
  }

  constructor() { }
}
