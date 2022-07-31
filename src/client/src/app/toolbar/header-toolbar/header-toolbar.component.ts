import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_service/account.service';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { Observable } from 'rxjs';
import { MatDrawerMode } from '@angular/material/sidenav';

class Navigation {
  title: string;
  link: string;
  description: string;
  showOnDrawer = false;
  showOnNavbar = false;
  constructor(link: string, title: string, description: string, showOnDrawer: boolean = true, showOnNavbar: boolean = false) {
    this.title = title;
    this.link = link;
    this.description = description;
    this.showOnDrawer = showOnDrawer;
    this.showOnNavbar = showOnNavbar;
  }
}

@Component({
  selector: 'app-nav-bar',
  templateUrl: './header-toolbar.component.html',
  styleUrls: ['./header-toolbar.component.css'],
  providers: [Location, { provide: LocationStrategy, useClass: PathLocationStrategy }],
})
export class HeaderToolbarComponent implements OnInit {
  showFiller = true;

  navigations: Navigation[] = [
    new Navigation('/', 'home', 'home', false, false),
    new Navigation('/memories', 'Memories', 'Do not keep in your mind!', true, true),
    new Navigation('/dashboard', 'Dashboard', 'Your mind dashboard..', true, false),
    new Navigation('/memory-types', 'Memory Types', 'Mail, Rdp, Microsoft Teams etc.', true, false),
    new Navigation('/organizations', 'Organizations', 'Microsoft, Patika, Google etc.', true, false),
    new Navigation('/organization-types', 'Organization Types', 'Work, Online Sale, Banks etc.', true, false),
    new Navigation('/environment-types', 'EnvironmentTypes', 'Dev, Test, Prod etc.', true, false),
    new Navigation('/profile', 'Profile', 'Setting up your profile!', true, false),
  ];

  seletedNav: Navigation | undefined;
  xlocation: Location;
  media$: Observable<MediaChange[]>;
  constructor(location: Location,
    public accountService: AccountService,
    public router: Router,
    media: MediaObserver) {
    this.media$ = media.asObservable();
    this.xlocation = location;
  }

  activeMediaQuery = '';
  drawerMode: MatDrawerMode;
  ngOnInit(): void {
    this.navigations;
    this.onMenuChange(this.xlocation.path());
    this.drawerMode = 'push';
    this.media$
      .subscribe((change) => {
        this.drawerMode = change.find(s => s.mqAlias === 'xs') ? 'over' : 'side';
        change.forEach((item) => {
          this.activeMediaQuery = item
            ? `'${item.mqAlias}' = (${item.mediaQuery})`
            : '';
          // if (item.mqAlias === 'xs') {
          //   this.drawerMode = 'over';
          // }
          // else{        
          //   this.drawerMode = 'side';
          // }
          console.log('activeMediaQuery', this.activeMediaQuery);
        })
      });
  }
  search() {
  }

  onMenuChange(link: string) {

    let path = this.xlocation.path();
    if (link) {
      path = link;
    }
    console.log('path', path);

    this.seletedNav = this.navigations.find(s => s.link === path);
    if (!this.seletedNav) {
      path = '/';
      this.seletedNav = this.navigations.find(s => s.link === path);
    }

  }
  get drawerNavigations() {
    return this.navigations.filter(s => s.showOnDrawer === true);
  }

  get navbarNavigations() {
    return this.navigations.filter(s => s.showOnNavbar === true);
  }

}

