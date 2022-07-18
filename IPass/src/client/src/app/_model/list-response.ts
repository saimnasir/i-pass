export interface ListResponse<T> { 
    data: Array<T>;
    page:number;
    pageSize:number;
    pageCount:number;
    totalCount:number;
}