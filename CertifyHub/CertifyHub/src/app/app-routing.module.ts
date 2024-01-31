import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authorizationGuard } from './authorization.guard';

const routes: Routes = [
  {
    path: 'learner',
    loadChildren: ()=>import('./learner/learner.module').then(x=> x.LearnerModule),
    canActivate:[authorizationGuard]
  },
  {
    path: 'admin',
    loadChildren: ()=>import('./admin/admin.module').then(x=> x.AdminModule),
    canActivate:[authorizationGuard]
  },
  {
    path: 'auth',
    loadChildren: ()=>import('./auth/auth.module').then(x=> x.AuthModule)

  },
  {
    path: 'instructor',
    loadChildren: ()=>import('./instructor/instructor.module').then(x=> x.InstructorModule)

  },
  {
    path: 'CertfyHub',
    loadChildren: ()=>import('./home/home.module').then(x=> x.HomeModule)

  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
