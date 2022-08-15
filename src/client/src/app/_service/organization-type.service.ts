import { Injectable } from '@angular/core';
import { BaseService } from './base-service'; 
import { HttpClient } from '@angular/common/http'; 
import { OrganizationTypeModel } from '../_model/organization-type.model';
import { AlertService } from './alert.service';

@Injectable()
export class OrganizationTypeService extends BaseService<OrganizationTypeModel, string> {

    constructor(protected override http: HttpClient, alertService: AlertService) {
        super(http, alertService);
    }
    route=`/api/organizationTypes`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'organizationTypes');
    }
}