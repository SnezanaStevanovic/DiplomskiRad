import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./auth.guard";
import { LoginComponent } from "./Components/Login/login.component";
import { HomeComponent } from "./Components/home/home.component";
import { RegistrationComponent } from "./Components/Registration/registration.component";




const appRoutes: Routes = [
    { path: 'main', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '/main', pathMatch: 'full' }
];

export const Routing = RouterModule.forRoot(appRoutes);