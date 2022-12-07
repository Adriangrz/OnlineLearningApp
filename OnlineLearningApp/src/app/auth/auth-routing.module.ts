import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoggedInAuthGuard } from './guards/logged-in-auth.guard';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  {
    path: 'logowanie',
    component: LoginComponent,
    canActivate: [LoggedInAuthGuard],
  },
  {
    path: 'rejestracja',
    component: RegistrationComponent,
    canActivate: [LoggedInAuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
