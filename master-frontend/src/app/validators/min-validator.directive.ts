import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, Validators } from '@angular/forms';

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: '[min]',
  providers: [{ provide: NG_VALIDATORS, useExisting: MinValidatorDirective, multi: true }]
})
export class MinValidatorDirective implements Validator {

  // tslint:disable-next-line:no-input-rename
  @Input('min') minv: number;

  validate(control: AbstractControl): {[key: string]: any} | null {
    return Validators.min(this.minv)(control);
  }

  constructor() { }

}
