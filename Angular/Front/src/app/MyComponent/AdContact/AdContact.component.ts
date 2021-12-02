import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { MatDialog } from '@angular/material/dialog';
import { Parameter } from 'src/app/Interfaces/IParameter';

@Component({
  selector: 'app-add-contact',
  templateUrl: './AdContact.component.html',
  styleUrls: ['./AdContact.component.css']
})
export class AddContactComponent implements OnInit {

  Id = 0;
  Login: string;
  Password: string;
  Name: string;
  Role: string;
  Mail: string;

  changeMode = false;

  Categories: Parameter[] = [];
  States: Parameter[] = [];

  Contacts = [];
  endContacts = false;
  ContactsSearch = '';

  // tslint:disable-next-line: max-line-length
  constructor(private auth: AuthorizationService, private router: Router, private adsService: AdsServise, private _snackBar: MatSnackBar, public dialog: MatDialog) { }

  displayedColumns: string[] = ['Name', 'b1'];
  displayedUserColumns: string[] = ['Name', 'Role', 'Login', 'b1'];

  ngOnInit() {
    if (this.auth.Status != 'Admin') {
      this.router.navigate(['/']);
    }
    this.LoadCategories();
    this.LoadStates();
    this.LoadContacts(0);
  }
  LoadCategories() {
    this.adsService.GetCategories()
      .subscribe((responce: Parameter[]) => {
        this.Categories = responce;
      },
        err => {
          this.router.navigate(['/servereception']);
        });
  }
  LoadStates() {
    this.adsService.GetStates()
      .subscribe((responce: Parameter[]) => {
        this.States = responce;
      },
        err => {
          this.router.navigate(['/servereception']);
        });
  }
  LoadContacts(id: number) {
    console.log('начиная с', id)
    let line = this.ContactsSearch;
    if (this.ContactsSearch == '') {
      line = '"""';
    }
    this.adsService.GetUsers(id, line).subscribe((responce: []) => {
      console.log(responce);
      if (responce.length < 10) {
        this.endContacts = true;
      }
      this.Contacts = [...this.Contacts, ...responce];
    },
      err => {
        this.router.navigate(['/servereception']);
      });
  }

  CreateUser() {
    let body = {
      Login: this.Login,
      Password: this.Password,
      Name: this.Name,
      Role: this.Role,
      Mails: [{ Name: this.Mail }],
    };
    console.log(body);
    this.adsService.AddContact(body).subscribe((resp: any) => {
      this._snackBar.open('Добавлено', 'Угу', { duration: 2000 });
      this.Login = '';
      this.Password = '';
      this.Name = '';
      this.Mail = '';
      this.Contacts = [];
      this.LoadContacts(0);
    },
      err => {
        this._snackBar.open('Упс, косяк', 'Угу', { duration: 2000 });
      });
  }

  openDialogCategory(action, obj, explanation) {
    obj.explanation = explanation;
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Добавить') {
        this.adsService.AddCategory(result.data)
          .subscribe((responce) => {
            this.LoadCategories();
          });
      } else if (result.event == 'Изменить') {
        this.adsService.ChangeCategory(result.data)
          .subscribe((responce) => {
            this.LoadCategories();
          });
      } else if (result.event == 'Удалить') {
        this.adsService.DeleteCategory(result.data)
          .subscribe((responce) => {
            this.LoadCategories();
          });
      }
    });
  }

  openDialogUser(action, obj, explanation) {
    obj.action = action;
    obj.explanation = explanation;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Удалить') {
        this.adsService.DeleteContact(result.data.Id).subscribe(() => {
          this.Contacts = [];
          this.LoadContacts(0);
        },
          err => {
            this.router.navigate(['/servereception']);
          });
      }
    });
  }

  openDialogState(action, obj, explanation) {
    obj.action = action;
    obj.explanation = explanation;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Добавить') {
        this.adsService.AddState(result.data)
          .subscribe((responce) => {
            this.LoadStates();
          });
      } else if (result.event == 'Изменить') {
        this.adsService.ChangeState(result.data)
          .subscribe((responce) => {
            this.LoadStates();
          });
      } else if (result.event == 'Удалить') {
        this.adsService.DeleteState(result.data)
          .subscribe((responce) => {
            this.LoadStates();
          });
      }
    });
  }

  onTableScroll(e) {
    if (this.endContacts) {
      return;
    }
    const tableViewHeight = e.target.offsetHeight // viewport: ~500px
    const tableScrollHeight = e.target.scrollHeight // length of all table
    const scrollLocation = e.target.scrollTop; // how far user scrolled

    const limit = tableScrollHeight - tableViewHeight;

    console.log(scrollLocation, '>', limit);
    if (scrollLocation == limit) {
      this.LoadContacts(this.Contacts[this.Contacts.length - 1].Id);
    }
  }

  Find() {
    this.Contacts = [];
    this.LoadContacts(0);
  }
  UpdateUser() {
    const body = {
      Id: this.Id,
      Login: this.Login,
      Password: this.Password,
      Name: this.Name,
      Role: this.Role,
    };
    console.log(body);
    this.adsService.ChangeContact(body).subscribe((resp: any) => {
      this._snackBar.open('Изменено', 'Угу', { duration: 2000 });
      this.Id = 0;
      this.Login = '';
      this.Password = '';
      this.Name = '';
      this.Mail = '';
      this.changeMode = false;
      this.Contacts = [];
      this.LoadContacts(0);
    },
      err => {
        this._snackBar.open('Упс, косяк', 'Угу', { duration: 2000 });
      });
  }
  SteChangeValue(element) {
    this.Id = element.Id
    this.Login = element.Login;
    this.Name = element.Name;
    this.Role = element.Role;
    this.Password = '';
    this.changeMode = true;
  }
}
