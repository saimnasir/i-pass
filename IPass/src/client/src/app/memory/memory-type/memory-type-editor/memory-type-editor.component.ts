import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { MemoryTypeModel } from 'src/app/_model/memory-type.model';
import { MemoryTypeService } from 'src/app/_service/memory-type.service';

@Component({
  selector: 'app-memory-type-editor',
  templateUrl: './memory-type-editor.component.html',
  styleUrls: ['./memory-type-editor.component.css']
})
export class MemoryTypeEditorComponent implements OnInit {


  model = new MemoryTypeModel();
  title: string;
  isNew = false;
  constructor(private memoryTypeService: MemoryTypeService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    protected router: Router) { }


  ngOnInit(): void {
    this.getModel();
  }

  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNew = id == null;
    if (id) {
      this.memoryTypeService.readWithDecoding(this.memoryTypeService.route, id).subscribe({
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
      this.memoryTypeService.create(this.memoryTypeService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('created');
            this.router.navigate(['memory-types']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    } else {
      this.memoryTypeService.update(this.memoryTypeService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('updated');
            this.router.navigate(['memory-types']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

}
