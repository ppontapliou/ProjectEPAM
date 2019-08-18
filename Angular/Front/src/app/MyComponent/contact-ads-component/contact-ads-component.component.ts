import { Component, OnInit } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import {Router} from "@angular/router"
import { identifierModuleUrl } from '@angular/compiler';

@Component({
  selector: 'app-contact-ads-component',
  templateUrl: './contact-ads-component.component.html',
  styleUrls: ['./contact-ads-component.component.css']
})
export class ContactAdsComponentComponent implements OnInit {

  sortAd: string = '';
  ads: any;
  response: any;
  adstmp: [];

  constructor(private adsService: AdsServise, private auth: AuthorizationService,private router: Router) { }

  ngOnInit() {
    
    if (this.auth.UserName == "") {
      this.router.navigate(['/']);
    }
    //this.adsService.get('https://api.github.com/users')
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let params = new URLSearchParams();
    params.append("someParamKey", "some value")
    this.adsService.GetContactAd('http://localhost:61988/api/Ads/ContactAds')
      .subscribe((responces: any) => {
        this.response = (responces);
        console.log(this.response);
      })
  }
}
