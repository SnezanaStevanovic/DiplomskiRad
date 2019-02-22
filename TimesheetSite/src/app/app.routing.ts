import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./auth.guard";
import { LoginComponent } from "./Components/Login/login.component";
import { HomeComponent } from "./Components/Home/home.component";
import { NavigationComponent } from "./Components/Navigation/navigation.component";
import { ProjectsComponent } from "./Components/Projects/projects.component";
import { UserManagmentComponent } from './Components/UserManagment/user-managment.component';




const appRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'main', canActivate: [AuthGuard], component: NavigationComponent,
        children: [
            { path: 'home', canActivate: [AuthGuard], component: HomeComponent },
            { path: 'projects', canActivate: [AuthGuard], component: ProjectsComponent },
            { path: 'usermanagment', canActivate: [AuthGuard], component: UserManagmentComponent },
            { path: '', canActivate: [AuthGuard], component: HomeComponent },
        ]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '/main/home', pathMatch: 'full' }
];


export const Routing = RouterModule.forRoot(appRoutes);
