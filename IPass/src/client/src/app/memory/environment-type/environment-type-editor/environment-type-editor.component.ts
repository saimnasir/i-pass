import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Guid } from 'guid-typescript'; 
import { EnvironmentTypeModel } from 'src/app/_model/environment-type.model';
import { EnvironmentTypeService } from 'src/app/_service/environment-type.service'; 

@Component({
  selector: 'app-environment-type-editor',
  templateUrl: './environment-type-editor.component.html',
  styleUrls: ['./environment-type-editor.component.scss']
})
export class EnvironmentTypeEditorComponent implements OnInit {

  
  model = new EnvironmentTypeModel ();
  title :string;
  isNew = false;
  constructor(private environmentTypeService: EnvironmentTypeService, 
    private route: ActivatedRoute,
    protected router: Router) { }


  ngOnInit(): void {
 
    this.getModel();
  }

  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNew = id == null;
    if (!this.isNew) {
      this.environmentTypeService.readWithDecoding(this.environmentTypeService.route, id).subscribe({
        next: (response) => {
          if (response.success) {
            this.model = response.data.data; 
            this.title = this.model.title;
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

  save() {
    if (this.isNew) {
      this.environmentTypeService.create(this.environmentTypeService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('created');
            this.router.navigate(['environment-types']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    } else {
      this.environmentTypeService.update(this.environmentTypeService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('updated');
            this.router.navigate(['environment-types']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

}
