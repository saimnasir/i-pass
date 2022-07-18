import { BaseModel } from "src/app/_model/base-model";

export class PinCodeModel implements BaseModel <string>  {
     created: Date;
     updated: Date;
     logId: string;
     code: string; 
     id: string; 
     active:boolean = true; 
}