import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AuthorizationService } from './authorization.service';
import { AdsPageComponent } from '../MyComponent/ads-page/ads-page.component';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';





@Injectable()
export class AdsServise {
  response: any;
  constructor(private httpClient: HttpClient, private auth: AuthorizationService) { }
  public GetAd(url: string, params:any = "") {
    return this.httpClient.get(url,{params});
  }


  public GetContactAd(url: string) {
    const params = {
      Login: this.auth.Login,
      Password: this.auth.Password,
    }
    return this.httpClient.get(url, { params });
  }
  public DeleteContactAd(url: string, ) {
    console.log(url);
    const params = {
      Login: this.auth.Login,
      Password: this.auth.Password,
    }
    return this.httpClient.delete(url, { params });
  }
  public GetCategories(url: string) {
    return this.httpClient.get(url);
  }
  public AddAd(url: string, body: any) {
    body.Contact.Login = this.auth.Login;
    body.Contact.Password = this.auth.Password;
    return this.httpClient.post(url, body);
  }
  public ChangeAd(url: string, body: any) {
    body.Contact.Login = this.auth.Login;
    body.Contact.Password = this.auth.Password;
    return this.httpClient.put(url, body);
  }
  public GetContactRelations(url: string) {
    return this.httpClient.get(url);
  }
  public AddTelephone(url: string, body: any) {
    return this.httpClient.post(url, body);
  }
}
