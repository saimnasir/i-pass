export interface FinalResponse<T> { 
    code: string;
    success: boolean;
    message: string;
    logId: string;
    data: T;
}