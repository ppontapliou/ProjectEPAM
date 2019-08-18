import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { ɵELEMENT_PROBE_PROVIDERS } from '@angular/platform-browser';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-ad',
  templateUrl: './add-ad.component.html',
  styleUrls: ['./add-ad.component.css']
})
export class AddAdComponent implements OnInit {
  category: string = "";
  status: string = "";
  type: string = "";
  Name: string = "";
  src: string = "";
  Place: string = "";
  Title: string = "";

  Categories: any;
  AllCorrect: boolean = true;

  constructor(private adsService: AdsServise, private auth: AuthorizationService,private router: Router) { }

  ngOnInit() {
    this.adsService.GetCategories("http://localhost:61988/api/Ads/Categories")
      .subscribe((responce: any) => {
        console.log(responce);
        this.Categories = responce.ListCategories;
      });
      if (this.auth.UserName == "") {
        this.router.navigate(['/']);
      }
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

      this.status == ""
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
    var body = {
      NameAd: this.Name,
      Title: this.Title,
      Picture: this.src,
      Adress: this.Place,
      Category: Id,
      Type: type,
      State: state,
      Contact:{
        Login:"",
        Password:"",
      },
    };
    this.adsService.AddAd("http://localhost:61988/api/Ads", body).subscribe((resp:any)=>{
      this.router.navigate(['/myads/']);
    });

    //console.log(Id, this.status, this.type, this.Name, this.src, this.Place, this.Title);
  }
}
