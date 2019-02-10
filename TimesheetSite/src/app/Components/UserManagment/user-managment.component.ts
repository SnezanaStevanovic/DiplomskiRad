import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { MatDialog } from '@angular/material';
import { RegistrationComponent } from '../Dialogs/Registration/registration.component';
import { BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {

  private readonly refreshToken = new BehaviorSubject(undefined);

  users = this.refreshToken.pipe(
    switchMap(() => this._userService.getAll())
  );

  constructor(
    private _userService: UserService,
    private _registerUserDialog: MatDialog) { }

  ngOnInit() {
  }

  public registerUserDialogOpen(): void {
    const dialogRef = this._registerUserDialog.open(RegistrationComponent);

    dialogRef.afterClosed().subscribe(dialogRes => {
        if (dialogRes) {
          this.refreshToken.next(undefined);
        }
    });

  }


}
