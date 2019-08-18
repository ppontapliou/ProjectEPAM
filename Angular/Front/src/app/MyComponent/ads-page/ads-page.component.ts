import { Component, OnInit } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Router } from '@angular/router';


@Component({
  selector: 'app-ads-page',
  templateUrl: './ads-page.component.html',
  styleUrls: ['./ads-page.component.css']
})
export class AdsPageComponent implements OnInit {
  sortAd: string = '';
  ads: any;
  response: any = [];
  adstmp: [];

  constructor(private adsService: AdsServise,private router: Router) { }

  ngOnInit() {

    this.adsService.GetAd('http://localhost:62976/api/ads')

      .subscribe((responces: any) => {
        this.adsService.response = (responces);
        this.response = responces;
        console.log(this.response);
      },
      err=>{
        console.log(err);
        this.router.navigate(['/servereception']);
      })
  }
}
