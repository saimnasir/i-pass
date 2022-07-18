import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_service/account.service';

@Component({
  selector: 'app-header-toolbar',
  templateUrl: './header-toolbar.component.html',
  styleUrls: ['./header-toolbar.component.css']
})
export class HeaderToolbarComponent implements OnInit {
  showFiller = false;
   
  constructor(public accountService: AccountService) { 
  }
  ngOnInit(): void { 
  }

  search() {
  }

}

