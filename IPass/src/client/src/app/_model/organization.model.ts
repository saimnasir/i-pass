import { BaseModel } from "src/app/_model/base-model";

export class OrganizationModel implements BaseModel <string>  {
     created: Date;
     updated: Date;
     logId: string;
     title: string; 
     id: string; 
     active:boolean = true;
     organizationTypeId: string; 
}