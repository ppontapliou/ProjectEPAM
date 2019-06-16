import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AdsServise } from '../ads.servise';

@Component({
  selector: 'app-big-ad',
  templateUrl: './big-ad.component.html',
  styleUrls: ['./big-ad.component.css']
})
export class BigAdComponent implements OnInit {

    private id: number;
    private subscription: Subscription;
    response: any;
    constructor(private activateRoute: ActivatedRoute,private adsService: AdsServise){
        this.subscription = activateRoute.params.subscribe(params=>this.id=params['id']);
    }
  ngOnInit() {
    this.adsService.get('http://localhost:61988/api/ads/'+this.id)
    .subscribe((responces: string) => {
      this.response = JSON.parse(responces);
      console.log(this.response);
      })
  }

}
