import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AuthorizationService } from './authorization.service';


@Injectable()
export class AdsServise {

  response: any;

  constructor(private httpClient: HttpClient, private auth: AuthorizationService) { }

  public GetAd(url: string, params: any = '') {
    return this.httpClient.get(url, { params });
  }

  public GetImages(id: number) {
    const path = 'api/Image/GetImages/';
    return this.httpClient.get(this.auth.BaseUrl + path + id);
  }

  public GetContactAd() {
    const path = 'api/Ads/UserAds';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.get(this.auth.BaseUrl + path, {
      headers: reqHeader
    });
  }

  public AddContact(body: any) {
    const path = 'api/Users/CreateUser';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }
  public ChangeContact(body: any) {
    const path = 'api/Users/ChangeUser';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.put(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }

  public DeleteContactAd(Id: number) {
    const path = 'api/Ads/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + Id, {
      headers: reqHeader
    });
  }

  public GetPartAds(id: number, category: number, state: number, name: string, type: number) {
    if (name == '') {
      name = '"""'
    }
    const path = 'api/Ads/PartAds/' + id + '/name/' + name + '/category/' + category + '/state/' + state + '/type/' + type;
    return this.httpClient.get(this.auth.BaseUrl + path);
  }

  public GetCategories() {
    const path = 'api/AdsData/GetCategories';
    return this.httpClient.get(this.auth.BaseUrl + path);
  }

  public GetStates() {
    const path = 'api/AdsData/GetStates';
    return this.httpClient.get(this.auth.BaseUrl + path);
  }

  public GetTypes() {
    const path = 'api/AdsData/GetTypes';
    return this.httpClient.get(this.auth.BaseUrl + path);
  }

  public AddAd(picture: File[], body: any) {
    // const path = 'api/Ads';
    const path = 'api/Ads/Image';
    const formData: FormData = new FormData();
    // formData.append('Images', , 'Name');
    for (let i = 0; i < picture.length; i++) {
      formData.append('Images', picture[i], i + picture[i].name);
    }

    formData.append('Name', body.Name);
    formData.append('Adress', body.Adress);
    formData.append('Title', body.Title);
    formData.append('Type', body.Type);
    formData.append('State', body.State);
    formData.append('Category', body.Category);

    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, formData, {
      headers: reqHeader
    });
  }

  public AddImage(body: File, id: number) {
    const path = 'api/Image/AddImage';
    const formData: FormData = new FormData();
    // formData.append('Images', , 'Name');

    formData.append('Image', body, body.name);
    formData.append('Id', id.toString());
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, formData, {
      headers: reqHeader
    });
  }
  public DeleteImage(id: number, idAd: number) {
    const path = 'api/Image/DeleteImage/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + id + '/' + idAd, {
      headers: reqHeader
    });
  }
  public ChangeAd(picture: File, body: any) {
    const path = 'api/Ads';
    const formData: FormData = new FormData();
    if (picture != null)
      formData.append('Images', picture, picture.name);
    formData.append('Name', body.Name);
    formData.append('Adress', body.Adress);
    formData.append('Title', body.Title);
    formData.append('Type', body.Type);
    formData.append('State', body.State);
    formData.append('Category', body.Category);
    formData.append('Picture', body.Picture);
    formData.append('Id', body.Id);
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.put(this.auth.BaseUrl + path, formData, {
      headers: reqHeader
    });
  }

  public AddPhone(body: any) {
    const path = 'api/Users/AddUserPhones';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }
  public AddMail(body: any) {
    const path = 'api/Users/AddUserMails';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }

  public DeletePhone(id: number) {
    const path = 'api/Users/DeleteUserPhones/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + id, {
      headers: reqHeader
    });
  }

  public DeleteMail(id: number) {
    const path = 'api/Users/DeleteUserMails/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + id, {
      headers: reqHeader
    });
  }

  public ChangePhone(body: any) {
    const path = 'api/Users/ChangeUserPhones';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.put(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }

  public ChangeMail(body: any) {
    const path = 'api/Users/ChangeUserMails';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.put(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }

  public GetPhones() {
    const path = 'api/Users/GetPhones';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.get(this.auth.BaseUrl + path, {
      headers: reqHeader
    });
  }

  public GetMails() {
    const path = 'api/Users/GetMails';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.get(this.auth.BaseUrl + path, {
      headers: reqHeader
    });
  }

  public GetUsers(id: number, name: string) {
    const path = 'api/Users/GetUsers/' + id + '/name/' + name;
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.get(this.auth.BaseUrl + path, {
      headers: reqHeader
    });
  }

  public AddCategory(body: any) {
    const path = 'api/AdsData/AddCategory';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }
  public ChangeCategory(body: any) {
    const path = 'api/AdsData/ChangeCategory';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.put(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }
  public DeleteCategory(body: any) {
    const path = 'api/AdsData/DeleteCategory/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + body.Id, {
      headers: reqHeader
    });
  }

  public AddState(body: any) {
    const path = 'api/AdsData/AddState';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.post(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }
  public ChangeState(body: any) {
    const path = 'api/AdsData/ChangeState';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.put(this.auth.BaseUrl + path, body, {
      headers: reqHeader
    });
  }
  public DeleteState(body: any) {
    const path = 'api/AdsData/DeleteState/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + body.Id, {
      headers: reqHeader
    });
  }
  public DeleteContact(id: number){
    const path = 'api/Users/DeleteUser/';
    const reqHeader = new HttpHeaders({ Authorization: 'bearer ' + localStorage.getItem('token') });
    return this.httpClient.delete(this.auth.BaseUrl + path + id, {
      headers: reqHeader
    });
  }
}
