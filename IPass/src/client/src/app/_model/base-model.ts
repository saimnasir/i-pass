export interface BaseModel<T> { 
    id: T;
    created: Date;
    updated: Date; 
    logId: string;
}