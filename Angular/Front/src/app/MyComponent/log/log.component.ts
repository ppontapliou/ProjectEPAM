import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {
  Visible: boolean;

  constructor(private auth: AuthorizationService, private router: Router) { }
  log: string;
  pass: string;
  ngOnInit() {
    this.Visible = false;
    this.auth.GetLinks();
  }
  change() {
    this.Visible = !this.Visible;

  }
  LogIn(login: string, password: string) {
    (this.auth.Auth(this.log, this.pass));
    //location.reload();
  }
  LogOut() {
    this.auth.LogOut();
    this.log = '';
    this.pass = '';
    this.router.navigate(['/']);
    this.change();
    //location.reload();
  }

}
