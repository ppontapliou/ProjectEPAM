import { Component, OnInit } from '@angular/core';
import { AdsServise } from '../ads.servise';

@Component({
  selector: 'app-ads-page',
  templateUrl: './ads-page.component.html',
  styleUrls: ['./ads-page.component.css']
})
export class AdsPageComponent implements OnInit {

  ads: any;
  response: any;
  adstmp: [];

  constructor(private adsService: AdsServise) { }

  ngOnInit() {
    this.ads = this.adsService.ads
    console.log(this.ads);
    //this.adsService.get('https://api.github.com/users')
    
    this.adsService.get('http://localhost:61988/api/ads')
    .subscribe((responces: any) => {
      this.response = (responces);
      console.log(this.response);
      })
  }
}
