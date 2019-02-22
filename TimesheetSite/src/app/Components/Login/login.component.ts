import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from 'src/app/Model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public errorText = 'Wrong username or password';
  public showError = false;
  public loading = false;
  public hide = false;
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

    if (!this.loginForm.valid) {
      return;
    }

    this.loading = true;

    const loginUser: User = new User();
    loginUser.email = this.loginForm.get('Email').value;
    loginUser.password = this.loginForm.get('Password').value;
    this._userSerice.login(loginUser).subscribe(res => {

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
