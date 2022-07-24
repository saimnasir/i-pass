import { Component, Input } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NG_VALUE_ACCESSOR, ValidationErrors } from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { CustomValidation } from 'src/app/_model/validation.model';

@Component({
  selector: 'app-input-switch',
  templateUrl: './input-switch.component.html',
  styleUrls: ['./input-switch.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: InputSwitchComponent
    }
  ]
})
export class InputSwitchComponent implements ControlValueAccessor {

  @Input() checked: boolean;

  @Input() validationMessages: CustomValidation[];
  @Input() label: string;
  @Input() type: string = 'text';
  @Input() formControlName: string;
  @Input() hint: string;
  @Input() readOnly: boolean = false;
  @Input() appearance: MatFormFieldAppearance = 'legacy';
  @Input() min: number;
  @Input() max: number;
  @Input() showClearButton: boolean = true;
  @Input() control: AbstractControl;
  @Input() successValidation: string;

  hide: boolean = this.type != 'password';
  get isPassword(): boolean {
    return this.type == 'password';
  }
  onChange = (value: any) => { };

  onTouched = () => { };

  touched = false;

  disabled = false; 
  
  writeValue(value: boolean) {
    this.checked = value;
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

  toogleChange(e: MatSlideToggleChange) {
    this.checked = e.checked;
    this.onChange(this.checked);
  }
  
}
