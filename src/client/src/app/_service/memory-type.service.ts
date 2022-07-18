import { Injectable } from '@angular/core';
import { BaseService } from './base-service'; 
import { HttpClient } from '@angular/common/http'; 
import { MemoryTypeModel } from '../_model/memory-type.model';

@Injectable()
export class MemoryTypeService extends BaseService<MemoryTypeModel, string> {

    constructor(protected override http: HttpClient) {
        super(http);
    }
    route=`/api/memoryTypes`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'memoryTypes');
    }
}