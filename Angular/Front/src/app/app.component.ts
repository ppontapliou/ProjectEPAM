import { Component, OnInit } from '@angular/core';
import { AdsServise } from './ads.servise';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [AdsServise]
})

export class AppComponent implements OnInit {
  ads: any;
  response: any;
  adstmp: [];

  constructor(private adsService: AdsServise) { }

  ngOnInit() {
    this.ads = this.adsService.ads
    console.log(this.ads);
    //this.adsService.get('https://api.github.com/users')
    
    this.adsService.get('http://localhost:61988/api/ads')
    .subscribe((responces: string) => {
      this.response = JSON.parse(responces);
      console.log(this.response);
      })
  }
}
