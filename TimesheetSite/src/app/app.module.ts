import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/Login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './Components/home/home.component';
import { Routing } from './app.routing';
import { AppMaterialModules } from './material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RegistrationComponent } from './Components/Registration/registration.component';
import { JwtModule } from '@auth0/angular-jwt';
import { LayoutModule } from '@angular/cdk/layout';
import { NavigationComponent } from './Components/Navigation/navigation.component';
import { TimesheetCardComponent } from './Components/HomeCards/timesheet-card/timesheet-card.component';
import { YourTasksComponent } from './Components/HomeCards/your-tasks-card/your-tasks.component';
import { LastDaysWorkCardComponent } from './Components/HomeCards/last-days-work-card/last-days-work-card.component';
import { TimerComponentComponent } from './Components/timer-component/timer-component.component';
import { JwtInterceptorService } from './Services/JWT/jwtInterceptor.service';





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
    TimerComponentComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    Routing,
    AppMaterialModules,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('token');
        },
        whitelistedDomains: ['localhost:5001'],
      }
    }),
    LayoutModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
