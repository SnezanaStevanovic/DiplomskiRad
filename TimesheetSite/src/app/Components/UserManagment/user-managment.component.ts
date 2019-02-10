import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { MatDialog } from '@angular/material';
import { RegistrationComponent } from '../Dialogs/Registration/registration.component';

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {

  users = this._userService.getAll();

  constructor(
    private _userService: UserService,
    private _registerUserDialog: MatDialog) { }

  ngOnInit() {
  }

  public registerUserDialogOpen(): void {
    const dialogRef = this._registerUserDialog.open(RegistrationComponent);
  }


}
