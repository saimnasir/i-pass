import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { CustomValidation } from 'src/app/_model/validation.model';

@Component({
  selector: 'app-input-field-ex',
  templateUrl: './input-field-ex.component.html',
  styleUrls: ['./input-field-ex.component.css']
})
export class InputFieldExComponent {

  @Input() value: any; 
  @Input() className: string;
  @Input() label: string;
  @Input() type: string = 'text';
  @Input() placeholder: string;
  @Input() pattern: string | RegExp;
  @Input() hint: string;
  @Input() required: boolean = true;
  @Input() readOnly: boolean = false;
  @Input() validationMessage: string = 'This field is required';
  @Input() appearance: MatFormFieldAppearance = 'outline';
  @Input() min: number;
  @Input() max: number;
  @Input() showClearButton: boolean = true;

  @Output() valueChange = new EventEmitter<any>();

}
