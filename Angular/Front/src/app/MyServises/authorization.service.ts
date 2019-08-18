import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  Id: string;
  UserName: string = "";
  Login: string = "";
  Password: string = "";
  Loggined: boolean = false;
  Status: string = "";
  public UserLinks = [
    { path: "", status: "Guest", name: "Домой", buttonMethod: "RouteHome()", },
  ];
  constructor(private http: HttpClient) { }
  private Links = [
    { path: "", status: "Guest", name: "Домой", buttonMethod: "RouteHome()", },
    { path: "myads/", status: "User", name: "Мои заказы", buttonMethod: "GetMyAds()", },
    { path: "/adminbar", status: "Admin", name: "Панель администрации", buttonMethod: "", },
  ];

  public Auth(login: string, password: string) {

    var userData = "username=" + login + "1&password=" + password + "&grant_type=password";

    var reqGeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded', 'No-Auth': 'True' });
    this.http.post('http://localhost:62976/token', userData, {
      headers: reqGeader
    })
      .subscribe(responces => {
        console.log(responces);
      });

    this.GetLinks();
  }
  public LogOut() {
    this.UserName = "";
    this.Loggined = false;
    this.Login = "";
    this.Password = "";
    this.Status = "";
    this.GetLinks();
  }
  public GetLinks() {
    this.UserLinks = [];

    if (this.Status == "") { this.UserLinks.push(this.Links[0]); return; }

    for (let i = 0; i < this.Links.length; i++) {
      const element = this.Links[i];
      this.UserLinks.push(element);

      if (this.Status == element.status) {
        break;
      }
    }
  }
  getToken() {
    return localStorage.getItem('token');
  }
}
