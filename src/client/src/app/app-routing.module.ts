import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 
import { AccountComponent } from './account/account/account.component';
import { ExternalComponent } from './account/external/external.component';
import { MyProfileComponent} from './account/my-profile/my-profile.component';
import { AuthGuard } from './helpers/auth.guard';
import { DashboardListComponent } from './memory/dashboard/memory-list/dashboard.component';
import { EnvironmentTypeEditorComponent } from './memory/environment-type/environment-type-editor/environment-type-editor.component';
import { EnvironmentTypeListComponent } from './memory/environment-type/environment-type-list/environment-type-list.component';
import { MemoryTypeEditorComponent } from './memory/memory-type/memory-type-editor/memory-type-editor.component';
import { MemoryTypeListComponent } from './memory/memory-type/memory-type-list/memory-type-list.component';
import { MemoryEditorComponent } from './memory/memory/memory-editor/memory-editor.component';
import { MemoryHistoryListComponent } from './memory/memory/memory-history-list/memory-history-list.component';
import { MemoryListComponent } from './memory/memory/memory-list/memory-list.component';
import { OrganizationTypeEditorComponent } from './memory/organization-type/organization-type-editor/organization-type-editor.component';
import { OrganizationTypeListComponent } from './memory/organization-type/organization-type-list/organization-type-list.component';
import { OrganizationEditorComponent } from './memory/organization/organization-editor/organization-editor.component';
import { OrganizationListComponent } from './memory/organization/organization-list/organization-list.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardListComponent , canActivate: [AuthGuard]  }, 
  { path: 'account', component: AccountComponent , canActivate: [AuthGuard] }, 

  { path: 'memories', component: MemoryListComponent, canActivate: [AuthGuard]  },
  { path: 'memories/add', component: MemoryEditorComponent, canActivate: [AuthGuard]  },
  { path: 'memories/read/:id', component: MemoryEditorComponent , canActivate: [AuthGuard] },
  { path: 'memories/edit/:id', component: MemoryEditorComponent , canActivate: [AuthGuard] },
  { path: 'memories/history/:id', component: MemoryHistoryListComponent , canActivate: [AuthGuard] },

  { path: 'organizations', component: OrganizationListComponent , canActivate: [AuthGuard] },
  { path: 'organizations/add', component: OrganizationEditorComponent , canActivate: [AuthGuard] }, 
  { path: 'organizations/edit/:id', component: OrganizationEditorComponent , canActivate: [AuthGuard] },

  { path: 'organization-types', component: OrganizationTypeListComponent , canActivate: [AuthGuard] },
  { path: 'organization-types/add', component: OrganizationTypeEditorComponent , canActivate: [AuthGuard] },
  { path: 'organization-types/edit/:id', component: OrganizationTypeEditorComponent , canActivate: [AuthGuard] },
  
  
  { path: 'memory-types', component: MemoryTypeListComponent , canActivate: [AuthGuard] },
  { path: 'memory-types/add', component: MemoryTypeEditorComponent , canActivate: [AuthGuard] },
  { path: 'memory-types/edit/:id', component: MemoryTypeEditorComponent , canActivate: [AuthGuard] },

  { path: 'environment-types', component: EnvironmentTypeListComponent , canActivate: [AuthGuard] },
  { path: 'environment-types/add', component: EnvironmentTypeEditorComponent , canActivate: [AuthGuard] },
  { path: 'environment-types/edit/:id', component: EnvironmentTypeEditorComponent , canActivate: [AuthGuard] },

  { path: 'profile', component: MyProfileComponent , canActivate: [AuthGuard] },
  { path: 'register/external', component: ExternalComponent  },

  // otherwise redirect to home
  { path: '**', redirectTo: '' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
