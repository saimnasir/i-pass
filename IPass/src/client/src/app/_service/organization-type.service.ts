import { Injectable } from '@angular/core';
import { BaseService } from './base-service'; 
import { HttpClient } from '@angular/common/http'; 
import { OrganizationTypeModel } from '../_model/organization-type.model';

@Injectable()
export class OrganizationTypeService extends BaseService<OrganizationTypeModel, string> {

    constructor(protected override http: HttpClient) {
        super(http);
    }
    route=`/api/organizationTypes`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'organizationTypes');
    }
}