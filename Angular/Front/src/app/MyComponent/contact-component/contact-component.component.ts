import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { AdsServise } from 'src/app/MyServises/ads.servise';

@Component({
  selector: 'app-contact-component',
  templateUrl: './contact-component.component.html',
  styleUrls: ['./contact-component.component.css']
})
export class ContactComponentComponent implements OnInit {
  Mails: [];
  Phones: [];
  constructor(private auth: AuthorizationService, private adsServ: AdsServise) { }

  ngOnInit() {
    this.adsServ.GetContactRelations("http://localhost:61988/api/User/" + this.auth.Id)
      .subscribe((responce: any) => {

      });
  }
  public AddTelephone() {
    var body = ["3", "dsdsd"];

    console.log(body);
    this.adsServ.AddTelephone("http://localhost:61988/api/User/AddTelephone", body)
      .subscribe();
  }
}
