import { Component, Input } from "@angular/core";
import { PinCodeService } from "src/app/_service/pin-code.service";

@Component({
    selector: 'app-loading',
    templateUrl: 'loading.html',
    styleUrls: ['./loading.scss']
})
export class LoadingComponent {
 
    @Input() loading: boolean;    

    constructor(  
    ) { }

    
}
 