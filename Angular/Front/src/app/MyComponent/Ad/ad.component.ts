import { Component, Input } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ad',
  templateUrl: './ad.component.html',
  styleUrls: ['./ad.component.css']
})

export class AdComponent {
  @Input() ad;
  @Input() changeVisible = false;
  changeImage: any = "assets/picture/change.png";
  deleteImage: any = "assets/picture/delete.png";

  constructor(private adServ: AdsServise, private router: Router) { }

  ngOnInit() { }

  Change(Id: number) {
    this.router.navigate(['/change/'+Id]);
  }

  Delete(Id: number) {
    console.log(Id, 'sdsds')
    this.adServ.DeleteContactAd('http://localhost:61988/api/Ads/' + Id).subscribe((responces: any) => {
      this.router.navigateByUrl('/RefrshComponent', {skipLocationChange: true}).then(()=>
      this.router.navigate(['myads']));
    });

  }

}
