import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./auth.guard";
import { LoginComponent } from "./Components/Login/login.component";
import { HomeComponent } from "./Components/home/home.component";
import { RegistrationComponent } from "./Components/Registration/registration.component";
import { NavigationComponent } from "./Components/Navigation/navigation.component";




const appRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'main', canActivate: [AuthGuard], component: NavigationComponent,
        children: [
            { path: 'home', canActivate: [AuthGuard], component: HomeComponent },
            { path: '', canActivate: [AuthGuard], component: HomeComponent },
        ]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '/main/home', pathMatch: 'full' }
];


export const Routing = RouterModule.forRoot(appRoutes);