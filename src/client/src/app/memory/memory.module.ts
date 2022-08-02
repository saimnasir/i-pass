import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';   
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'; 
import {MatCardModule} from '@angular/material/card';
  
import {AvatarModule} from "primeng/avatar";  
import { AppRoutingModule } from '../app-routing.module';
import { MemoryEditorComponent } from './memory/memory-editor/memory-editor.component';
import { MemoryListComponent } from './memory/memory-list/memory-list.component';
import { MemoryService } from '../_service/memory.service';

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
import { OrganizationEditorComponent } from './organization/organization-editor/organization-editor.component';
import { OrganizationListComponent } from './organization/organization-list/organization-list.component';
import { OrganizationService } from '../_service/organization.service';
import { OrganizationTypeService } from '../_service/organization-type.service';
import { OrganizationTypeEditorComponent } from './organization-type/organization-type-editor/organization-type-editor.component';
import { OrganizationTypeListComponent } from './organization-type/organization-type-list/organization-type-list.component';
import { MemoryTypeService } from '../_service/memory-type.service';
import { MemoryTypeListComponent } from './memory-type/memory-type-list/memory-type-list.component';
import { MemoryTypeEditorComponent } from './memory-type/memory-type-editor/memory-type-editor.component';
import { EnvironmentTypeEditorComponent } from './environment-type/environment-type-editor/environment-type-editor.component';
import { EnvironmentTypeListComponent } from './environment-type/environment-type-list/environment-type-list.component';
import { EnvironmentTypeService } from '../_service/environment-type.service';
import { DashboardListComponent } from './dashboard/memory-list/dashboard.component';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

import {MatProgressBarModule} from '@angular/material/progress-bar';
import { UnlockMemoryDialog } from './memory/unlock-memory-dialog/unlock-memory-dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { PinCodeService } from '../_service/pin-code.service';
import { MemoryHistoryListComponent } from './memory/memory-history-list/memory-history-list.component';
import { IPassCommonModule } from '../ipass-common.module';
import { MemoryCardComponent } from './memory/memory-card-list/memory-card/memory-card.component';
import { MemoryCardListComponent } from './memory/memory-card-list/memory-card-list.component';


@NgModule({
  declarations: [ 
    DashboardListComponent,
    MemoryEditorComponent,
    MemoryListComponent,
    OrganizationEditorComponent,
    OrganizationListComponent,
    OrganizationTypeEditorComponent,
    OrganizationTypeListComponent,
    MemoryTypeListComponent,
    MemoryTypeEditorComponent,
    EnvironmentTypeEditorComponent,
    EnvironmentTypeListComponent,
    UnlockMemoryDialog,
    MemoryHistoryListComponent,
    MemoryCardComponent,
    MemoryCardListComponent
     
  ],
  imports: [
    IPassCommonModule,     
  ],
  providers: [
    MemoryService,
    OrganizationService,
    OrganizationTypeService,
    MemoryTypeService,
    EnvironmentTypeService,
    PinCodeService
  ],
  exports: [
    MemoryEditorComponent,
    MemoryListComponent,
    OrganizationEditorComponent,
    OrganizationListComponent,
    OrganizationTypeEditorComponent,
    OrganizationTypeListComponent,
    MemoryCardListComponent
  ]
})
export class MemoryModule { }
