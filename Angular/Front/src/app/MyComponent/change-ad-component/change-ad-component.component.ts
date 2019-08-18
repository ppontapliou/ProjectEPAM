import { Component, OnInit } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';

@Component({
  selector: 'app-change-ad-component',
  templateUrl: './change-ad-component.component.html',
  styleUrls: ['./change-ad-component.component.css']
})
export class ChangeAdComponentComponent implements OnInit {
  category: string = "";
  status: string = "";
  type: string = "";
  Name: string = "";
  src: string = "";
  Place: string = "";
  Title: string = "";

  private id: number;

  Categories: any;
  AllCorrect: boolean = true;
  response: any;
  private subscription: Subscription;

  constructor(private activateRoute: ActivatedRoute, private adsService: AdsServise, private auth: AuthorizationService,private router: Router) {
    this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
  }

  ngOnInit() {
    if (this.auth.UserName == "") {
      this.router.navigate(['/']);
    }
    this.adsService.GetCategories("http://localhost:61988/api/Ads/Categories")
      .subscribe((responce: any) => {
        console.log(responce);
        this.Categories = responce.ListCategories;
      });
    this.adsService.GetAd('http://localhost:62976//api/ads/' + this.id)
      .subscribe((responces: any) => {
        this.response = (responces);
        console.log(this.response);
        this.category = this.response.Category;
        this.Name = this.response.NameAd;
        this.status = this.response.State;
        this.type = this.response.Type;
        this.src = this.response.Picture;
        this.Place = this.response.Adress;
        this.Title = this.response.Title;
      })
  }
  check() {
    var Id: any = 1;
    var type: any;
    var state: any;
    switch (this.type) {
      case "Товар":
        {
          type = 1;
          break;
        }
      case "Услуга":
        {
          type = 2;
          break;
        }
    }
    switch (this.status) {
      case "Новое":
        {
          state = 1;
          break;
        }
      case "бу":
        {
          state = 2;
          break;
        }
      case "нет":
        {
          state = 2;
          break;
        }
    }
    if (
      this.category == ""
      || this.status == ""
      || this.type == ""
      || this.Name == ""
      || this.src == ""
      || this.Place == ""
      || this.Title == ""
    ) {
      this.AllCorrect = false;
      return;
    }
    else {
      this.AllCorrect = true;
    }
    this.Categories.forEach(element => {
      if (element.NameCategory == this.category) {
        Id = element.Id;
      }
    });
    this.response.Category = Id;
    this.response.NameAd =
      this.Name; this.response.State = state;
    this.response.Type = type;
    this.response.Picture =this.src;
    this.response.Adress =this.Place;
    this.response.Title =this.Title;
    // var body = {
    //   NameAd: this.Name,
    //   Title: this.Title,
    //   Picture: this.src,
    //   Adress: this.Place,
    //   Category: Id,
    //   Type: type,
    //   State: state,
    //   Contact: {
    //     Login: "",
    //     Password: "",
    //   },
    // };
    this.adsService.ChangeAd('http://localhost:61988/api/Ads', this.response)
    .subscribe((response:any)=>{
      this.router.navigate(['/myads/']);
    });
    //console.log(Id, this.status, this.type, this.Name, this.src, this.Place, this.Title);
  }
}
