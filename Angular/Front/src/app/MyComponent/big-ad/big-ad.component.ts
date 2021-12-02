import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Ad } from 'src/app/Interfaces/IAd';
import { MatDialog } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';


@Component({
  selector: 'app-big-ad',
  templateUrl: './big-ad.component.html',
  styleUrls: ['./big-ad.component.css']
})
export class BigAdComponent implements OnInit {
  private id: number;
  private subscription: Subscription;
  response: Ad = new Ad();

  constructor(private activateRoute: ActivatedRoute, private adsService: AdsServise, private router: Router, public dialog: MatDialog, private auth: AuthorizationService) {
    this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
    console.log(this.response.Contact.Phones.length == 0);
  }
  ngOnInit() {
    this.adsService.GetAd('http://localhost:62976/api/ads/' + this.id)
      .subscribe((responces: Ad) => {
        this.response = (responces);
      })
  }
  Delete(action, obj, explanation) {
    obj.explanation = explanation;
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Удалить') {
        this.adsService.DeleteContactAd(this.id)
          .subscribe((responce) => {
            this.router.navigate(['/']);
          });
      }
    });
  }
  Change() {
    localStorage.setItem('changeValue', this.id.toString());
    this.router.navigate(['/change/']);
  }

}
