import { Guid } from "guid-typescript";
import { BaseModel } from "src/app/_model/base-model";

export class MemoryTypeModel  implements BaseModel <string>  {
     id: string;
     created: Date;
     updated: Date;
     logId: string;
     title: string;  
}