import { Injectable } from '@angular/core';
import { BaseService } from './base-service'; 
import { HttpClient } from '@angular/common/http'; 
import { MemoryTypeModel } from '../_model/memory-type.model';
import { AlertService } from './alert.service';

@Injectable()
export class MemoryTypeService extends BaseService<MemoryTypeModel, string> {

    constructor(protected override http: HttpClient, alertService: AlertService) {
        super(http, alertService);
    }
    route=`/api/memoryTypes`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'memoryTypes');
    }
}