import { Component, OnInit } from '@angular/core';
import { AdsServise } from './MyServises/ads.servise';


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
    //  })
  }
}
