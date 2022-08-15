import { Injectable } from '@angular/core';
import { BaseService } from './base-service'; 
import { HttpClient } from '@angular/common/http'; 
import { EnvironmentTypeModel } from '../_model/environment-type.model';
import { AlertService } from './alert.service';

@Injectable()
export class EnvironmentTypeService extends BaseService<EnvironmentTypeModel, string> {

    constructor(protected override http: HttpClient, alertService: AlertService) {
        super(http, alertService);
    }
    route=`/api/environmentTypes`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'environmentTypes');
    }
}