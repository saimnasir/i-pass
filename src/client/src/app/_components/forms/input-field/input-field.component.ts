import { Component, Input } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NG_VALUE_ACCESSOR, ValidationErrors } from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { findIndex } from 'rxjs';
import { CustomValidation } from 'src/app/_model/validation.model';

@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: InputFieldComponent
    }
  ]
})
export class InputFieldComponent implements ControlValueAccessor {

  @Input() value: any;
  @Input() isTextArea: boolean = false;

  @Input() validationMessages: CustomValidation[];
  @Input() label: string;
  @Input() type: string = 'text';
  @Input() placeholder: string;
  @Input() hint: string;
  @Input() readOnly: boolean = false;
  @Input() appearance: MatFormFieldAppearance = 'legacy';
  @Input() min: number;
  @Input() max: number;
  @Input() showClearButton: boolean = true;
  @Input() control: AbstractControl;
  @Input() successValidation: string;
  @Input() elevation: string = '1';
  @Input() showError: boolean = false;

  hide: boolean = this.type != 'password';
  get isPassword(): boolean {
    return this.type == 'password';
  }
  get required(): boolean { 
    let isRequired = this.validationMessages?.findIndex(s => s.type === 'required') >= 0;   
    return isRequired;
  }
 
  onChange = (value: any) => { };

  onTouched = () => { };

  touched = false;

  disabled = false;

  writeValue(value: any) {
    this.value = value;
  }

  registerOnChange(onChange: any) {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: any) {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }

  changeValue(e: any) {
    this.value = e.target.value;
    this.onChange(this.value);
  }

  clear() {
    this.value = '';
    this.onChange(this.value);
  }

  visibilityMouseEnter() {
    this.hide = false;
  }
  visibilityMouseLeave() {
    this.hide = true;
  }
}
