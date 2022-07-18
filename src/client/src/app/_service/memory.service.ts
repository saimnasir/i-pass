import { Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { MemoryModel } from '../_model/memory.model';
import { HttpClient } from '@angular/common/http';
import { Guid } from 'guid-typescript';

@Injectable()
export class MemoryService extends BaseService<MemoryModel, string> {

    constructor(protected override http: HttpClient) {
        super(http);
    }
    route=`/api/memories`;      
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'memories');
    }
}