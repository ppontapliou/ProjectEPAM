
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Parameter } from 'src/app/Interfaces/IParameter';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-contact-component',
  templateUrl: './contact-component.component.html',
  styleUrls: ['./contact-component.component.css']
})
export class ContactComponentComponent implements OnInit {

  @ViewChild('readOnlyTemplatePhone', { static: false }) readOnlyTemplatePhone: TemplateRef<any>;
  @ViewChild('editTemplatePhone', { static: false }) editTemplatePhone: TemplateRef<any>;
  @ViewChild('readOnlyTemplateMail', { static: false }) readOnlyTemplateMail: TemplateRef<any>;
  @ViewChild('editTemplateMail', { static: false }) editTemplateMail: TemplateRef<any>;

  Mails: Parameter[];
  Phones: Parameter[];
  EditedPhone: Parameter;
  EditedMail: Parameter;
  isNewPhone: boolean;
  isNewMail: boolean;


  displayedColumns: string[] = ['Name', 'b1'];
  constructor(private auth: AuthorizationService, private adsServ: AdsServise,
    private snackBar: MatSnackBar, private router: Router, public dialog: MatDialog) {

  }

  ngOnInit() {
    if (this.auth.UserName == '') {
      this.router.navigate(['/']);
    }
    this.Mails = new Array<Parameter>();
    this.Phones = new Array<Parameter>();
    this.LoadMails();
    this.LoadPhones();
  }

  private LoadMails() {
    this.adsServ.GetMails().subscribe((response: Parameter[]) => {
      this.Mails = response;
    });
  }

  private LoadPhones() {
    this.adsServ.GetPhones().subscribe((response: Parameter[]) => {
      this.Phones = response;
    });
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Добавить') {
        this.addRowPhone(result.data);
      } else if (result.event == 'Изменить') {
        this.updateRowPhone(result.data);
      } else if (result.event == 'Удалить') {
        this.deleteRowPhone(result.data);
      }
    });
  }
  openDialogMails(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Добавить') {
        this.addRowMail(result.data);
      } else if (result.event == 'Изменить') {
        this.updateRowMail(result.data);
      } else if (result.event == 'Удалить') {
        this.deleteRowMail(result.data);
      }
    });
  }
  addRowPhone(rowObj) {
    const body = { Phones: [{ Name: rowObj.Name }] };
    this.adsServ.AddPhone(body).subscribe(result => {
      this.snackBar.open('Добавлено', 'Угу', { duration: 2000 });
      this.LoadPhones();
    },
      err => {
        this.snackBar.open('Не добавлено', 'Угу', { duration: 2000 });
      }
    );
  }
  updateRowPhone(rowObj) {
    const body = { Phones: [{ Name: rowObj.Name, Id: rowObj.Id }] };
    this.adsServ.ChangePhone(body).subscribe(data => {
      this.snackBar.open('Изменено', 'Угу', { duration: 2000 });
      this.LoadPhones();
    },
      err => {
        this.snackBar.open('Не изменено', 'Угу', { duration: 2000 });
      });
  }
  deleteRowPhone(rowObj) {
    this.adsServ.DeletePhone(rowObj.Id).subscribe(result => {
      this.LoadPhones();
      this.snackBar.open('Удалено', 'Угу', { duration: 2000 });
    },
      err => {
        this.snackBar.open('Не удалено', 'Угу', { duration: 2000 });
      });
  }

  addRowMail(rowObj) {
    const body = { Mails: [{ Name: rowObj.Name }] };
    this.adsServ.AddMail(body).subscribe(result => {
      this.snackBar.open('Добавлено', 'Угу', { duration: 2000 });
      this.LoadMails();
    },
      err => {
        this.snackBar.open('Не добавлено', 'Угу', { duration: 2000 });
      }
    );
  }
  updateRowMail(rowObj) {
    const body = { Mails: [{ Name: rowObj.Name, Id: rowObj.Id }] };
    this.adsServ.ChangeMail(body).subscribe(data => {
      this.snackBar.open('Изменено', 'Угу', { duration: 2000 });
      this.LoadMails();
    },
      err => {
        this.snackBar.open('Не изменено', 'Угу', { duration: 2000 });
      });
  }
  deleteRowMail(rowObj) {
    this.adsServ.DeleteMail(rowObj.Id).subscribe(result => {
      this.LoadMails();
      this.snackBar.open('Удалено', 'Угу', { duration: 2000 });
    },
      err => {
        this.snackBar.open('Не удалено', 'Угу', { duration: 2000 });
      });
  }
}
