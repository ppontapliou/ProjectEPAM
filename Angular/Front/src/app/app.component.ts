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
    this.auth.Init();
  }

}
