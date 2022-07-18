import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatFormFieldAppearance } from '@angular/material/form-field';

@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.css']
})
export class InputFieldComponent  {

  @Input() value:any;
  @Input() className:string;
  @Input() label:string;
  @Input() type:string= 'text';
  @Input() placeholder:string;
  @Input() pattern:string| RegExp;
  @Input() hint:string;
  @Input() required:boolean = true;
  @Input() readOnly:boolean= false;
  @Input() validationMessage:string ='This field is required';
  @Input() appearance:MatFormFieldAppearance='outline'; 
  @Input() min: number;
  @Input() max: number;
  @Input() showClearButton:boolean = true;

  @Output() valueChange = new EventEmitter<any>();

}
