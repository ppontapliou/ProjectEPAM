import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  UserName: string = "";
  Login: string = "";
  Password: string = "";
  Loggined: boolean = false;
  Status: string = "";
  constructor(private http: HttpClient) { }
  Links: [
    {path:"/",status:"guest",name:"Начальная страница"},
    {path:"/",status:"user",name:"Мои заказы"},
    {path:"/adminbar",status:"admin",name:"Панель администрации"}
  ];
  public Auth(login: string, password: string) {
    const body = { Login: login, Password: password };
    this.http.post('http://localhost:61988/api/Authorization', body)
      .subscribe((responces: string) => {
        if (responces != null) {
          this.UserName = responces;
          console.log(this.UserName)
          this.Loggined = true;
          this.Login = login;
          this.Password = password;
          //this.UserName = responces.UserName;
          //this.UserName = responces.Status;
        }
      });
  }
  public LogOut() {
    this.UserName = "";
    this.Loggined = false;
    this.Login = "";
    this.Password = "";
  }
}
