import { Component, OnInit } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { Router } from "@angular/router";
@Component({
  selector: 'app-contact-ads-component',
  templateUrl: './contact-ads-component.component.html',
  styleUrls: ['./contact-ads-component.component.css']
})
export class ContactAdsComponentComponent implements OnInit {

  sortAd: string = '';
  ads: any;
  response: any = [];

  constructor(private adsService: AdsServise, private auth: AuthorizationService, private router: Router) { }

  ngOnInit() {

    if (this.auth.UserName == '') {
      this.router.navigate(['/']);
    }
    this.adsService.GetContactAd()
      .subscribe((responces: any) => {
        this.response = (responces);
      })
  }
}

