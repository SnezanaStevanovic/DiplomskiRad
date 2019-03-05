import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/Login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './Components/Home/home.component';
import { Routing } from './app.routing';
import { AppMaterialModules } from './material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RegistrationComponent } from './Components/Dialogs/Registration/registration.component';
import { JwtModule } from '@auth0/angular-jwt';
import { NavigationComponent } from './Components/Navigation/navigation.component';
import { TimesheetCardComponent } from './Components/HomeCards/timesheet-card/timesheet-card.component';
import { YourTasksComponent } from './Components/HomeCards/your-tasks-card/your-tasks.component';
import { LastDaysWorkCardComponent } from './Components/HomeCards/last-days-work-card/last-days-work-card.component';
import { TimerComponentComponent } from './Components/Timer/timer-component.component';
import { JwtInterceptorService } from './Services/JWT/jwtInterceptor.service';
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule } from '@angular/material';
import { ProjectsComponent } from './Components/Projects/projects.component';
import { CreateProjectDialogComponent } from './Components/Dialogs/CreateProjectDialog/create-project-dialog.component';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { UserManagmentComponent } from './Components/UserManagment/user-managment.component';
import { CreateTaskDialogComponent } from './Components/Dialogs/create-task-dialog/create-task-dialog.component';
import { ProjectDetailsComponent } from './Components/project-details/project-details.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegistrationComponent,
    NavigationComponent,
    TimesheetCardComponent,
    YourTasksComponent,
    LastDaysWorkCardComponent,
    TimerComponentComponent,
    ProjectsComponent,
    CreateProjectDialogComponent,
    UserManagmentComponent,
    CreateTaskDialogComponent,
    ProjectDetailsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    Routing,
    AppMaterialModules,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatDialogModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: jwtTokenGetter,
        whitelistedDomains: ['localhost:5001'],
      }
    }),
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true },
              {provide: LocationStrategy, useClass: HashLocationStrategy},
              {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: true}}],
  entryComponents : [CreateProjectDialogComponent, RegistrationComponent, CreateTaskDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function jwtTokenGetter() {
  return localStorage.getItem('token');
}
