import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;


  constructor(private _formBuilder: FormBuilder) {

    this.registerForm = this._formBuilder.group({
      'Username': new FormControl('', Validators.required),
      'FirstName': new FormControl('', Validators.required),
      'LastName': new FormControl('', Validators.required),
      'Email': new FormControl('', [Validators.required, Validators.email]),
      'Password': new FormControl('', Validators.required)
    });
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




}
