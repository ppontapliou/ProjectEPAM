
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  readonly BaseUrl = 'http://localhost:62976/';
  readonly reqHeader = { headers: new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') }) };
  Id: string;
  UserName = '';
  Login = '';
  Password = '';
  Loggined = false;
  Status = '';
  public UserLinks = [
    { path: '', status: 'Guest', name: 'Домой', buttonMethod: 'RouteHome()', },
  ];
  constructor(private http: HttpClient, private router: Router, private _snackBar: MatSnackBar) { }
  private Links = [
    { path: '', status: 'Guest', name: 'Домой', buttonMethod: 'RouteHome()', },
    { path: 'myads/', status: 'User', name: 'Мои заказы', buttonMethod: 'GetMyAds()', },
    { path: '/adminbar', status: 'Admin', name: 'Панель администрации', buttonMethod: '', },
  ];

  public Auth(login: string, password: string) {
    const userData = 'username=' + login + '&password=' + password + '&grant_type=password';

    const reqGeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded', 'No-Auth': 'True' });
    this.http.post('http://localhost:62976/token', userData, {
      headers: reqGeader
    })
      .subscribe((responces: any) => {
        console.log(responces);
        localStorage.setItem('token', responces.access_token);
        localStorage.setItem('refresh-token', responces.refresh_token);
        this.Init();
      });
  }
  public Init() {
    const token = localStorage.getItem('token');
    if (token != null) {
      const userInfo = jwt_decode(token);
      if (Date.now() >= userInfo.exp * 1000) {
        this.refreshToken();
      }
      this.UserName = userInfo.name;
      this.Status = userInfo.role;
      this.Loggined = true;
      this.GetLinks();
    }
  }
  public LogOut() {
    this.UserName = '';
    this.Loggined = false;
    this.Login = '';
    this.Password = '';
    this.Status = '';
    this.GetLinks();
    localStorage.clear();
    this.router.navigate(['/']);
  }
  public GetLinks() {
    this.UserLinks = [];

    if (this.Status == '') {
      this.UserLinks.push(this.Links[0]);
      this.UserLinks.push({ path: '/reg', status: 'Guest', name: 'Регистрация', buttonMethod: 'RouteHome()', });
      return;
    }
    if (this.Status == 'User') {
      this.UserLinks.push(this.Links[0]);
      this.UserLinks.push(this.Links[1]);
      return;
    } if (this.Status == 'Editor' || this.Status == 'Admin') {
      this.UserLinks.push(this.Links[0]);
      this.UserLinks.push(this.Links[1]);
      this.UserLinks.push(this.Links[2]);
      return;
    }
  }
  getToken() {
    return localStorage.getItem('token');
  }
  refreshToken() {
    const userData = 'grant_type=refresh_token&refresh_token=' + localStorage.getItem('refresh-token') + '&client_id=';
    const reqGeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded', 'No-Auth': 'True' });
    return this.http.post('http://localhost:62976/token', userData, {
      headers: reqGeader
    }).pipe(tap((responces: any) => {
      console.log('Access Token Refreshed!');
      localStorage.setItem('token', responces.access_token);
      localStorage.setItem('refresh-token', responces.refresh_token);
      this.Init();
    }),
    );
  }
  RegistrationUser(body) {
    return this.http.post('http://localhost:62976/api/Users/Registration',body);
  }
}
