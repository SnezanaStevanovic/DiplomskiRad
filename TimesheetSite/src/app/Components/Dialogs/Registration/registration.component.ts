import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { RegistrationRequest } from 'src/app/Model/registrationRequest';
import { Employee } from 'src/app/Model/employee';
import { User } from 'src/app/Model/user';
import { Role } from 'src/app/Model/role.enum';
import { Gender } from 'src/app/Model/gender.enum';
import { MustMatch } from 'src/app/Helpers/must-match.validator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  hide = false;
  rolesKeys: any[];
  genderKeys: any[];
  roles = Role;
  genders = Gender;
  errorMessage: string;
  showErrorMessage = false;

  get f() { return this.registerForm.controls; }

  constructor(
    public dialogRef: MatDialogRef<RegistrationComponent>,
    private _formBuilder: FormBuilder,
    private _userService: UserService) {

    this.registerForm = this._formBuilder.group({
      'FirstName': new FormControl('', Validators.required),
      'LastName': new FormControl('', Validators.required),
      'Email': new FormControl('', [Validators.required, Validators.email]),
      'Adress': new FormControl(''),
      'Role': new FormControl('', [Validators.required]),
      'DateOfBirth': new FormControl(''),
      'Gender': new FormControl(''),
      'Password': new FormControl('', Validators.required),
      'ConfirmPass': new FormControl('', Validators.required)
    }, {
      validator: MustMatch('Password', 'ConfirmPass')
    });
    this.rolesKeys = Object.keys(this.roles).filter(Number);
    this.genderKeys = Object.keys(this.genders).filter(Number);
  }

  ngOnInit() {

  }

  getErrorMessage() {
    if (this.registerForm.controls['Email']) {
      return this.registerForm.controls['Email'].hasError('required') ? 'You must enter a value' :
        this.registerForm.controls['Email'].hasError('email') ? 'Not a valid email' :
          '';
    } else {
      return '';
    }
  }


public register(): void {
  if (!this.registerForm.valid) {
    return;
  }
  const registrationRequest: RegistrationRequest  = new RegistrationRequest();
  registrationRequest.newEmployee = new Employee();
  registrationRequest.newUser = new User();
  registrationRequest.newEmployee.firstName = this.registerForm.get('FirstName').value;
  registrationRequest.newEmployee.lastName = this.registerForm.get('LastName').value;
  registrationRequest.newEmployee.adress = this.registerForm.get('Adress').value;
  registrationRequest.newEmployee.role = this.registerForm.get('Role').value;
  registrationRequest.newEmployee.dateOfBirth = this.registerForm.get('DateOfBirth').value;
  registrationRequest.newEmployee.gender = this.registerForm.get('Gender').value;

  registrationRequest.newUser.email = this.registerForm.get('Email').value;
  registrationRequest.newUser.password = this.registerForm.get('Password').value;

  this._userService.register(registrationRequest).subscribe(res => {
        if (res.success) {
          this.dialogRef.close(true);
        } else {
          this.errorMessage = res.message;
          this.showErrorMessage = true;
        }
  });


}

}
