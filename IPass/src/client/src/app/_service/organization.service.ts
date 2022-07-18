import { Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { MemoryModel } from '../_model/memory.model';
import { HttpClient } from '@angular/common/http';
import { Guid } from 'guid-typescript';
import { OrganizationModel } from '../_model/organization.model';

@Injectable()
export class OrganizationService extends BaseService<OrganizationModel, string> {

    constructor(protected override http: HttpClient) {
        super(http);
    }
    route=`/api/organizations`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'organizations');
    }
}