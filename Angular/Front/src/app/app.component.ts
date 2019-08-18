import { Component, OnInit } from '@angular/core';
import { AdsServise } from './MyServises/ads.servise';
import { AuthorizationService } from './MyServises/authorization.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [AdsServise]
})

export class AppComponent implements OnInit {
  private router: Router;
  constructor(private adsService: AdsServise, private auth: AuthorizationService) { }
  //
  ngOnInit() {
    console.log(this.auth.UserLinks);
    //(this.auth.Auth('log1', "password1"));
  }
  RouteHome(str: string) {
    // console.log(str);
    // if(str =="GetMyAds()"){
    //   this.adsService.GetContactAd('http://localhost:61988/api/Ads/ContactAds',)
    //   .subscribe((responces: any) => {
    //     this.adsService.response = (responces);

    //   })
    // }
  }
}
