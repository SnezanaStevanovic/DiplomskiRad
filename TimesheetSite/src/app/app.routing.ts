import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./auth.guard";
import { LoginComponent } from "./Components/Login/login.component";
import { HomeComponent } from "./Components/Home/home.component";
import { NavigationComponent } from "./Components/Navigation/navigation.component";
import { ProjectsComponent } from "./Components/Projects/projects.component";
import { UserManagmentComponent } from './Components/UserManagment/user-managment.component';
import { ProjectDetailsComponent } from './Components/project-details/project-details.component';
import { TaskDetailsComponent } from './Components/task-details/task-details.component';




const appRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'main', canActivate: [AuthGuard], component: NavigationComponent,
        children: [
            { path: 'home', canActivate: [AuthGuard], component: HomeComponent },
            { path: 'projects', canActivate: [AuthGuard], component: ProjectsComponent },
            { path: 'usermanagment', canActivate: [AuthGuard], component: UserManagmentComponent },
            { path: 'projectDetails', canActivate: [AuthGuard], component: ProjectDetailsComponent },
            { path: 'taskDetails', canActivate: [AuthGuard], component: TaskDetailsComponent },
            { path: '', canActivate: [AuthGuard], component: HomeComponent },
        ]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '/main/home', pathMatch: 'full' }
];


export const Routing = RouterModule.forRoot(appRoutes);
