import { Guid } from "guid-typescript";
import { BaseModel } from "src/app/_model/base-model";
import { MemoryTypeModel } from "./memory-type.model";
import { OrganizationModel } from "./organization.model";

export class MemoryModel implements BaseModel <string> {
     id: string;
     created: Date;
     updated: Date;
     logId: string;
   
     title: string; 

     organizationId: string;
     organization:OrganizationModel; 
     
     memoryTypeId: string;
     memoryType:MemoryTypeModel;  
     
     environmentTypeId: string;
     environmentType:MemoryTypeModel; 
     
     userName: string; 
     isUserNameSecure: boolean; 
     
     email: string; 
     isUEmailSecure: boolean;
     
     hostOrIpAddress: string; 
     isHostOrIpAddressSecure: boolean;
     
     port: string; 
     isPortSecure: boolean;

     password: string; 
     description: string;  

     active:boolean = true;
}