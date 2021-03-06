import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AdsServise } from 'src/app/MyServises/ads.servise';


@Component({
  selector: 'app-big-ad',
  templateUrl: './big-ad.component.html',
  styleUrls: ['./big-ad.component.css']
})
export class BigAdComponent implements OnInit {
    public str =  "classOne";
    ngStyle: { [klass: string]: any; }
    private id: number;
    private subscription: Subscription;
    response: any;
    constructor(private activateRoute: ActivatedRoute,private adsService: AdsServise){
        this.subscription = activateRoute.params.subscribe(params=>this.id=params['id']);
    }
  ngOnInit() {
    this.adsService.GetAd('http://localhost:62976/api/ads/'+this.id)
    .subscribe((responces: any) => {
      this.response = (responces);
      console.log(this.response);
      })
  }

}
