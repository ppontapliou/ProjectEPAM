import { AuthorizationService } from './../MyServises/authorization.service';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpRequest, HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { HttpInterceptor } from '@angular/common/http';
import { Observable, throwError, empty } from 'rxjs';
import { tap, catchError, switchMap } from 'rxjs/operators';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  refreshingAccessToken = false;
  constructor(private auth: AuthorizationService, private http: HttpClient) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<any> {
    console.log('Ловим');
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        console.log(error);

        if (error.status === 401) {
          return this.refreshAccessToken()
            .pipe(
              switchMap(() => {
                req = this.addAuthHeader(req);
                return next.handle(req);
              }),
              catchError((err: any) => {
                console.log(err);
                this.auth.LogOut();
                return empty();
              })
            )

        }
        return throwError(error);
      })
    )
  }
  refreshAccessToken() {
    if (this.refreshingAccessToken) { } else {
      this.refreshingAccessToken = true;
      // we want to call a method in the auth service to send a request to refresh the access token
      const userData = 'grant_type=refresh_token&refresh_token=' + localStorage.getItem('refresh-token') + '&client_id=';
      const reqGeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded', 'No-Auth': 'True' });
      return this.http.post('http://localhost:62976/token', userData, {
        headers: reqGeader
      }).pipe(tap((responces: any) => {
        console.log('Access Token Refreshed!');
        localStorage.setItem('token', responces.access_token);
        localStorage.setItem('refresh-token', responces.refresh_token);
        this.auth.Init();
      }));
    }
  }
  addAuthHeader(request: HttpRequest<any>) {
    // get the access token
    const authReq = request.clone({
      headers: request.headers.set('Authorization', 'bearer ' + localStorage.getItem('token'))
    })
    return authReq;
  }
}
