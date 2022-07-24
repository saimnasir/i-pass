import {  HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core'; 

import { AppComponent } from './app.component';   
import { MemoryModule } from './memory/memory.module'; 
import { HeaderToolbarComponent } from './toolbar/header-toolbar/header-toolbar.component';
import { FooterToolbarComponent } from './toolbar/footer-toolbar/footer-toolbar.component'; 
import { AccountModule } from './account/account.module'; 
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';  
import { IPassCommonModule } from './ipass-common.module';

@NgModule({
  declarations: [
    AppComponent,    
    HeaderToolbarComponent,
    FooterToolbarComponent,   
    //MediaQueryStatusComponent
  ],
  imports: [
    AccountModule,   
    MemoryModule,
    IPassCommonModule

  ],
  providers: [ 
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
