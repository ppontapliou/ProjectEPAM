import { Component, Input } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-ad',
  templateUrl: './ad.component.html',
  styleUrls: ['./ad.component.css']
})

export class AdComponent {
  @Input() ad;
  @Input() changeVisible = false;
  changeImage: any = 'assets/picture/change.png';
  deleteImage: any = 'assets/picture/delete.png';

  constructor(private adServ: AdsServise, private router: Router, private _snackBar: MatSnackBar) { }

  ngOnInit() { }

  Change(Id: number) {
    localStorage.setItem('changeValue',Id.toString());
    this.router.navigate(['/change/']);
  }

  Delete(Id: number) {
    this.adServ.DeleteContactAd(Id).subscribe((responces: any) => {
      this._snackBar.open('Удалено', 'Угу', { duration: 2000 });
      this.router.navigateByUrl('/RefrshComponent', {skipLocationChange: true}).then(()=>
      this.router.navigate(['myads']));
    },
    err=>{
      this._snackBar.open('Упс, косяк', 'Угу', { duration: 2000 });
    });

  }

}
