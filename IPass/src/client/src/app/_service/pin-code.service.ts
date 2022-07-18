import { Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { HttpClient } from '@angular/common/http';
import { PinCodeModel } from '../_model/pin-code.model';
import { Observable } from 'rxjs';
import { FinalResponse } from '../_model/final-response';
import { ListResponse } from '../_model/list-response';
import { SingleResponse } from '../_model/single-response';

@Injectable()
export class PinCodeService extends BaseService<PinCodeModel, string> {

    constructor(protected override http: HttpClient) {
        super(http);
    }
    route = `/api/pinCodes`;

    alertError(errors: any) {
        super.alertErrorFor(errors, 'pinCodes');
    }

    getPinCode( code: string): Observable<FinalResponse<SingleResponse<PinCodeModel>>> {
        let model = {
            code: code
        };
        return this.post(`${this.route}/check`, model);
    }
}