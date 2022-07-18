import { BaseModel } from "src/app/_model/base-model";

export class OrganizationTypeModel implements BaseModel <string>  {
     title: string; 
     id: string;  
     created: Date;
     updated: Date;
     logId: string;
}