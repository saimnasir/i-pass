import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatFormFieldAppearance } from '@angular/material/form-field';

@Component({
  selector: 'app-form-group-input',
  templateUrl: './form-group-input.component.html',
  styleUrls: ['./form-group-input.component.css']
})
export class FormGroupInputComponent {

  @Input() formControlName: string;
  @Input() value: any;
  @Input() class: string;
  @Input() label: string;
  @Input() type: string = 'text';
  @Input() placeholder: string;
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
