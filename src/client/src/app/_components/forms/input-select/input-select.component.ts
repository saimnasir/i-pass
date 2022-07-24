import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { Option } from 'src/app/_model/option.model';
import { CustomValidation } from 'src/app/_model/validation.model';
 
@Component({
  selector: 'app-input-select',
  templateUrl: './input-select.component.html',
  styleUrls: ['./input-select.component.css'], providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: InputSelectComponent
    }
  ]
})
export class InputSelectComponent implements ControlValueAccessor {

  @Input() value: any;
  
  @Input() validationMessages: CustomValidation[];
  @Input() options: Option[];
  @Input() label: string;
  @Input() placeholder: string;
  @Input() hint: string;
  @Input() readOnly: boolean = false;
  @Input() appearance: MatFormFieldAppearance = 'legacy';
  @Input() showClearButton: boolean = true;
  @Input() control: AbstractControl;
  @Input() successValidation: string;
  @Input() elevation: string = '1';

  writeValue(value: any): void {
    this.value = value;
  }
  registerOnChange(onChange: any): void {   
    this.onChange = onChange;
  }
  registerOnTouched(onTouched: any): void {
    this.onTouched = onTouched;
  }

  get required(): boolean {
    let isRequired = this.validationMessages?.findIndex(s => s.type === 'required') >= 0;
    return isRequired;
  }

  touched = false;

  disabled = false; 
  onChange = (value: any) => { };

  onTouched = () => { };
  
  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }

  changeValue(value: any) {
    console.log(value);
    this.value = value;
    
    this.onChange(this.value);
  }

  clear() {
    this.value = '';
    this.onChange(this.value);
  }

}
