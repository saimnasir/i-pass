import { NgModule } from '@angular/core'; 
import { CommonModule } from '@angular/common';
  
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
 
import { HttpClientModule } from '@angular/common/http'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { Routes } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
  
import {AvatarModule} from "primeng/avatar";  
import { AppRoutingModule } from '../app-routing.module'; 

import {MatMenuModule} from "@angular/material/menu";
import {MatIconModule} from "@angular/material/icon";
import {MatBadgeModule} from "@angular/material/badge"; 

import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatTabsModule} from "@angular/material/tabs";

import {MatSidenavModule} from "@angular/material/sidenav";
import {MatButtonModule} from "@angular/material/button";
import {MatListModule} from "@angular/material/list";
import {MatToolbarModule} from "@angular/material/toolbar";

import {MatStepperModule} from "@angular/material/stepper";
import {MatInputModule} from "@angular/material/input";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatExpansionModule} from "@angular/material/expansion";
import {MatSortModule} from "@angular/material/sort";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatTooltipModule} from "@angular/material/tooltip";
import {MatChipsModule} from '@angular/material/chips';

import {MatSelectModule} from "@angular/material/select";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";

import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatRadioModule} from "@angular/material/radio";
import {DrawerRailModule} from "angular-material-rail-drawer";
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule
} from "@angular-material-components/datetime-picker"; 
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

import {MatProgressBarModule} from '@angular/material/progress-bar';
 import { MatDialogModule } from '@angular/material/dialog';
import { ProfileComponent } from './profile/profile.component';
import { PinCodeService } from '../_service/pin-code.service';
import { AccountService } from '../_service/account.service';
import { InputFieldComponent } from '../_components/forms/input-field/input-field.component';
import { FormGroupInputComponent } from '../_components/forms/form-group-input/form-group-input.component';
import { ExternalComponent } from './external/external.component';
import { FlexLayoutModule } from '@angular/flex-layout';
 

@NgModule({
    imports: [
        BrowserAnimationsModule, 
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        AvatarModule,
        CommonModule ,
        FormsModule, 
        ReactiveFormsModule,
         
        MatMenuModule,
        MatIconModule,
        MatBadgeModule,
        MatTableModule,
        MatPaginatorModule,
        MatTabsModule,
        MatSidenavModule,
        MatButtonModule,
        MatListModule,
        MatToolbarModule,
        DrawerRailModule,
        MatStepperModule,
        MatInputModule,
        MatGridListModule,
        MatExpansionModule,
        MatSortModule,
        MatSnackBarModule,
        MatTooltipModule,
        MatChipsModule,
        MatSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatCheckboxModule,
        NgxMatDatetimePickerModule,
        NgxMatTimepickerModule,
        NgxMatNativeDateModule,
        MatRadioModule,
        MatCardModule,
        MatSlideToggleModule,
        
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatDialogModule,
        FlexLayoutModule
    ],
    providers: [ 
      PinCodeService,
      AccountService
    ],
    declarations: [ 
        LoginComponent,
        RegisterComponent,
        ProfileComponent,
        InputFieldComponent,
        FormGroupInputComponent,
        ExternalComponent,
    ]
})
export class AccountModule { }