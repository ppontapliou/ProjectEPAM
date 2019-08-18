import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { AuthorizationService } from './authorization.service';


@Injectable({
  providedIn: 'root'
})
export class TokenInteseptorService implements HttpInterceptor {

  constructor(private injector:Injector) { }

  intercept(req, next){
    let authServise = this.injector.get(AuthorizationService);
    let tokenizedReq = req.clone({
      setHeaders:{
        Authorization: `Bearer ${authServise.getToken()}`
      }
    })
    return next.handle(tokenizedReq);
  }
}
