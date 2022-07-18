import { Injectable } from '@angular/core';
import { BaseService } from './base-service'; 
import { HttpClient } from '@angular/common/http'; 
import { EnvironmentTypeModel } from '../_model/environment-type.model';

@Injectable()
export class EnvironmentTypeService extends BaseService<EnvironmentTypeModel, string> {

    constructor(protected override http: HttpClient) {
        super(http);
    }
    route=`/api/environmentTypes`; 
   
    alertError(errors: any) {
        super.alertErrorFor(errors, 'environmentTypes');
    }
}