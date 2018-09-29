import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/Model/LoginRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  public loginParam = new LoginRequest();
  private errorText = 'Wrong Username or Password';
  private showError = false;
  private loading = false;
  private url = '/main'

  constructor(
    private _userSerice: UserService,
    private _router: Router) { }



  ngOnInit() {
  }

  dataChanged(ev: any) {
    this.showError = false;
  }

  login(): void {
    this.loading = true;
    this._userSerice.login(this.loginParam).subscribe(res => {
      sessionStorage.setItem('token', res.token);
      this.loading = false;
      this._router.navigate([this.url]);
    }, () => {
      this.showError = true;
      this.loading = false;
    });
  }

}
