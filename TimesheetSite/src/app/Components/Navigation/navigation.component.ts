import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { LoggedUser } from 'src/app/Model/loggedUser';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    private _authservice: AuthService,
    private _router: Router) { }

    loggedUser: LoggedUser;

    ngOnInit() {
      this.loggedUser = this._authservice.getLoggedUser();
    }

    public logout() {
      this._authservice.logout();
      this._router.navigate(['/login']);
    }

}
