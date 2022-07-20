import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {AvatarModule} from "primeng/avatar";
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MemoryModule } from './memory/memory.module';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { HeaderToolbarComponent } from './toolbar/header-toolbar/header-toolbar.component';
import { FooterToolbarComponent } from './toolbar/footer-toolbar/footer-toolbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip'; 
import { AccountModule } from './account/account.module'; 
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor'; 
  

@NgModule({
  declarations: [
    AppComponent,    
    HeaderToolbarComponent,
    FooterToolbarComponent,   
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule, 
    FormsModule,
    HttpClientModule,
    AvatarModule,
    AppRoutingModule,
    MemoryModule,  
    AccountModule, 
    
    ReactiveFormsModule,  
    MatExpansionModule,
    MatIconModule,
    MatFormFieldModule, 
    MatGridListModule,
    MatDividerModule,
    MatButtonModule, 
    MatListModule,
    MatInputModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatToolbarModule,
    MatSidenavModule,
    MatMenuModule, 
    MatSelectModule,
    MatChipsModule,
    
  ],
  providers: [ 
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
