import { BaseModel } from "src/app/_model/base-model";
import { OrganizationTypeModel } from "./organization-type.model";

export class OrganizationModel implements BaseModel <string>  {
     created: Date;
     updated: Date;
     logId: string;
     title: string; 
     id: string; 
     active:boolean = true;

     organizationTypeId: string; 
     organizationType: OrganizationTypeModel; 
     
     parentOrganizationId: string; 
     parentOrganization:OrganizationModel; 
}