import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';

import { AvatarModule } from "primeng/avatar";

import { MatMenuModule } from "@angular/material/menu";
import { MatIconModule } from "@angular/material/icon";
import { MatBadgeModule } from "@angular/material/badge";

import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatTabsModule } from "@angular/material/tabs";

import { MatSidenavModule } from "@angular/material/sidenav";
import { MatButtonModule } from "@angular/material/button";
import { MatListModule } from "@angular/material/list";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatTreeModule } from '@angular/material/tree';

import { MatStepperModule } from "@angular/material/stepper";
import { MatInputModule } from "@angular/material/input";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatSortModule } from "@angular/material/sort";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatChipsModule } from '@angular/material/chips';

import { MatSelectModule } from "@angular/material/select";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";

import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatRadioModule } from "@angular/material/radio";
import { DrawerRailModule } from "angular-material-rail-drawer";
import {
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxMatTimepickerModule
} from "@angular-material-components/datetime-picker";
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { FlexLayoutModule } from '@angular/flex-layout';
import { InputFieldComponent } from "./_components/forms/input-field/input-field.component";
import { InputFieldExComponent } from "./_components/forms/input-field-ex/input-field-ex.component";
import { MediaQueryStatusComponent } from "./_components/media-query-status.component";
import { AppRoutingModule } from "./app-routing.module";
import { MemoryService } from "./_service/memory.service";
import { EnvironmentTypeService } from "./_service/environment-type.service";
import { MemoryTypeService } from "./_service/memory-type.service";
import { OrganizationTypeService } from "./_service/organization-type.service";
import { OrganizationService } from "./_service/organization.service";
import { PinCodeService } from "./_service/pin-code.service";
import { InputSelectComponent } from './_components/forms/input-select/input-select.component';
import { InputSwitchComponent } from './_components/forms/input-switch/input-switch.component';
import { TableSearchDialog } from './common/table-search/table-search-dialog';
import { LoadingComponent } from './common/loading/loading';
import { MatSnackBarComponent } from './common/snack-bar/mat-snack-bar.component';
import { AlertComponent } from './common/alert/alert.component';
import { SnackBarSuccessComponent } from './common/snack-bar/snack-bar-succes/snack-bar-success';
import { SnackBarErrorComponent } from './common/snack-bar/snack-bar-error/snack-bar-error';
import { SnackBarInfoComponent } from './common/snack-bar/snack-bar-info/snack-bar-info';
import { SnackBarWarningComponent } from './common/snack-bar/snack-bar-warning/snack-bar-warning';
import { SnackBarBaseComponent } from './common/snack-bar/snack-bar-base/snack-bar-base';


@NgModule({

    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        AvatarModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule,

        MatTreeModule,
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

    ],
    declarations: [
        InputFieldComponent,
        InputFieldExComponent,
        MediaQueryStatusComponent,
        InputSelectComponent,
        InputSwitchComponent,

        TableSearchDialog,
        LoadingComponent,
        MatSnackBarComponent,
        SnackBarBaseComponent,
        SnackBarSuccessComponent,
        SnackBarErrorComponent,
        SnackBarInfoComponent,
        SnackBarWarningComponent,
        AlertComponent
    ],
    exports: [
        InputFieldComponent,
        InputSelectComponent,
        InputFieldExComponent,
        MediaQueryStatusComponent,
        InputSwitchComponent,
        LoadingComponent,
        MatSnackBarComponent,
        AlertComponent,

        BrowserAnimationsModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        AvatarModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule,

        MatTreeModule,
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

    ],
    providers: [
        MemoryService,
        OrganizationService,
        OrganizationTypeService,
        MemoryTypeService,
        EnvironmentTypeService,
        PinCodeService,
        MatSnackBarComponent
    ]
})
export class IPassCommonModule {
}
