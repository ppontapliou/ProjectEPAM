import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private auth: AuthorizationService, private _snack: MatSnackBar, private router: Router) { }

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  NameFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(50),
    Validators.minLength(5)
  ]);
  PasswordFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(40),
    Validators.minLength(5)
  ]);
  LoginFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(20),
    Validators.minLength(5)
  ]);

  matcher = new MyErrorStateMatcher();

  ngOnInit(): void {
  }
  registracion() {


    if (this.NameFormControl.valid && this.LoginFormControl.valid && this.PasswordFormControl.valid && this.emailFormControl.valid) {
      const body = {
        Login: this.LoginFormControl.value,
        Password: this.PasswordFormControl.value,
        Name: this.NameFormControl.value,
        Mails: [{ Name: this.emailFormControl.value }],
      };
      this.auth.RegistrationUser(body).subscribe(() => {
        this._snack.open('А теперь в можете авторизироваться', 'Угу', { duration: 2000 });
        this.router.navigate(['/']);
      },
        (err: HttpErrorResponse) => {
          if (err.status == 400) {
            this._snack.open(err.message, 'Угу', { duration: 2000 });
          }
          else {
            this.router.navigate(['/servereception']);
          }
        });
    }

  }
}
