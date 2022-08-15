import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { SortDirection } from "@angular/material/sort/sort-direction";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { FinalResponse } from "../_model/final-response";
import { ListResponse } from "../_model/list-response";
import { SingleResponse } from "../_model/single-response";
import { AlertService } from "./alert.service";


export class BaseService<T, I> {

    headers: HttpHeaders;

    authHeaders: HttpHeaders;
    genericError = "Bir hata oluştu. Tekrarı durumunda yönetime bildiriniz.";

    constructor(protected http: HttpClient, private alertService: AlertService) {
        this.headers = new HttpHeaders({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Access-Control-Allow-Credentials': 'true',
            'Access-Control-Allow-Origin': '*',
            "Authorization": `Bearer ${this.getToken()}`
        });
        this.authHeaders = new HttpHeaders({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Access-Control-Allow-Credentials': 'true',
            'Access-Control-Allow-Origin': '*',
            "Authorization": `Bearer `
        });
    } 
 
    public combineWithApiUrl(route: string): string {
        if (route.startsWith('/')) {
            return `${environment.apiURL}${route}`;
        }
        return `${environment.apiURL}/${route}`;
    }

    getToken() {
        const data = localStorage.getItem('user');
        if (data) {
            const currentUser = JSON.parse(data);
            if (currentUser) {
                return currentUser.accessToken;
            }
        }
        return null;
    }
    getAllPaginated(route: string, sortBy: string, sortType: SortDirection, page: number, pageSize: number, searchText: string = '', decode: boolean = false): Observable<FinalResponse<ListResponse<T>>> {

        let sortDirection: string = sortType.toString();
        if (sortDirection == '') {
            sortDirection = 'none';
        }
        const params = new HttpParams()
            .append("page", page.toString())
            .append("pageSize", pageSize.toString())
            .append("sortBy", sortBy.toString())
            .append("sortType", sortDirection.toString())
            .append("decode", decode.toString())
            .append("searchText", searchText.toString());
        return this.http.get<FinalResponse<ListResponse<T>>>(this.combineWithApiUrl(route), { headers: this.headers, params: params });
    }

    getHistoryPaginated(route: string, sortBy: string, sortType: SortDirection, page: number, pageSize: number, searchText: string = '', decode: boolean = false): Observable<FinalResponse<ListResponse<T>>> {
        const params = new HttpParams()
            .append("page", page.toString())
            .append("pageSize", pageSize.toString())
            .append("sortBy", sortBy.toString())
            .append("sortType", sortType.toString())
            .append("decode", decode.toString())
            .append("searchText", searchText.toString());

        return this.http.get<FinalResponse<ListResponse<T>>>(this.combineWithApiUrl(route), { headers: this.headers, params: params });
    }

    getAll(route: string): Observable<FinalResponse<ListResponse<T>>> {
        return this.http.get<FinalResponse<ListResponse<T>>>(this.combineWithApiUrl(route), { headers: this.headers });
    }

    // get(route: string): Observable<FinalResponse<SingleResponse<T>>> {
    //     return this.http.get<FinalResponse<SingleResponse<T>>>(this.combineWithApiUrl(route), { headers: this.headers });
    // }

    create(route: string, entity: T): Observable<FinalResponse<boolean>> {
        return this.http.post<FinalResponse<boolean>>(this.combineWithApiUrl(route), entity, { headers: this.headers });
    }

    update(route: string, entity: T): Observable<FinalResponse<boolean>> {
        return this.http.put<FinalResponse<boolean>>(this.combineWithApiUrl(route), entity, { headers: this.headers });
    }

    read(route: string, id: any): Observable<FinalResponse<SingleResponse<T>>> {
        return this.http.get<FinalResponse<SingleResponse<T>>>(this.combineWithApiUrl(route) + `/${id}`, { headers: this.headers });
    }
    readWithDecoding(route: string, id: any): Observable<FinalResponse<SingleResponse<T>>> {
        const params = new HttpParams().append("decode", true.toString());
        return this.http.get<FinalResponse<SingleResponse<T>>>(this.combineWithApiUrl(route) + `/${id}`, { headers: this.headers, params: params });
    }

    post<T>(route: string, entity: any): Observable<FinalResponse<T>> {
        return this.http.post<FinalResponse<T>>(this.combineWithApiUrl(route), entity, { headers: this.headers });
    }

    get<T>(route: string): Observable<FinalResponse<T>> {
        return this.http.get<FinalResponse<T>>(this.combineWithApiUrl(route), { headers: this.headers });
    }

    externalCallBack<T>(route: string, token: string): Observable<FinalResponse<T>> {
        this.authHeaders.delete("Authorization");
        this.authHeaders.set("Authorization", `Bearer ${token}`);

        console.log('Authorization getExternalLogin', this.authHeaders.get('Authorization'));
        return this.http.get<FinalResponse<T>>(this.combineWithApiUrl(route), { headers: this.authHeaders });
    }
    put<T>(route: string, entity: any): Observable<FinalResponse<T>> {
        return this.http.put<FinalResponse<T>>(this.combineWithApiUrl(route), entity, { headers: this.headers });
    }


    public handleException(response: FinalResponse<any>) {
        let message = { severity: 'error', summary: `Hata oluştu`, detail: response.message };
        console.log('hata', message);
        if (response.code == "UNAUTHORIZED") {
            localStorage.removeItem('currentUser');
        }
        return message;
    }

    public handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error(`Backend returned code ${error.status}, `, error.error);
        }
        if (error.status === 401 || error.error.code == "UNAUTHORIZED" || error.error.Value.code == "UNAUTHORIZED") {
            localStorage.removeItem('currentUser');
            window.location.reload();
        }
    };


    alertErrorFor(errors: any, callerName: string) {
        let error = errors[0];
        let message = { severity: 'error', summary: `${callerName} - ${error.error.statusText}`, detail: error.detail };
        console.log("errors", errors);
    }

}