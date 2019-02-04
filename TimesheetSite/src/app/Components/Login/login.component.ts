import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/Model/LoginRequest';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

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
  private url = '/main';

  loginForm: FormGroup;


  constructor(
    private _userSerice: UserService,
    private _authService: AuthService,
    private _router: Router,
    private _formBuilder: FormBuilder) {

    this.loginForm = this._formBuilder.group({
      'Email': new FormControl('', [Validators.required, Validators.email]),
      'Password': new FormControl('', Validators.required)
    });

  }



  ngOnInit() {
  }

  getErrorMessage() {
    if (this.loginForm.controls['Email']) {
      return this.loginForm.controls['Email'].hasError('required') ? 'You must enter a value' :
        this.loginForm.controls['Email'].hasError('email') ? 'Not a valid email' :
          '';
    } else {
      return '';
    }
  }


  dataChanged(ev: any) {
    this.showError = false;
  }

  login(): void {
    this.loading = true;

    this.loginParam.email = this.loginForm.get('Email').value;
    this.loginParam.password = this.loginForm.get('Password').value;
    this._userSerice.login(this.loginParam).subscribe(res => {

      if (res.success) {
        this._authService.login(res.token);
        this.loading = false;
        this._router.navigate([this.url]);
      } else {
        this.showError = true;
        this.loading = false;
      }
    }, () => {
      this.showError = true;
      this.loading = false;
    });
  }

}
