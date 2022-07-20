import { Component, Input } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NG_VALUE_ACCESSOR, ValidationErrors } from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { CustomValidation } from 'src/app/_model/validation.model';

@Component({
  selector: 'app-form-group-input',
  templateUrl: './form-group-input.component.html',
  styleUrls: ['./form-group-input.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: FormGroupInputComponent
    }
  ]
})
export class FormGroupInputComponent implements ControlValueAccessor {

  @Input() value: any;

  @Input() validationMessages: CustomValidation [];
  @Input() label: string;
  @Input() type: string = 'text';
  @Input() placeholder: string;
  @Input() hint: string;
  @Input() required: boolean = true;
  @Input() readOnly: boolean = false;
  @Input() appearance: MatFormFieldAppearance = 'outline';
  @Input() min: number;
  @Input() max: number;
  @Input() showClearButton: boolean = true;
  @Input() control: AbstractControl;
  //@Output() valueChange = new EventEmitter<any>();

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

}
