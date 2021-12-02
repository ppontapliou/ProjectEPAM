import { Parameter } from './../../Interfaces/IParameter';
import { Component, OnInit, Input } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';

@Component({
  selector: 'app-image-slider',
  templateUrl: './image-slider.component.html',
  styleUrls: ['./image-slider.component.css']
})
export class ImageSliderComponent implements OnInit {

  @Input() Images: string[] = [];
  @Input() sImg = '';
  _Id: number = 0;

  @Input()
  set Id(age: number) {
    console.log(age);
    this._Id = age;
    this.adsService.GetImages(age).subscribe((responce:Parameter[]) => {
      responce.forEach(element => {
        this.Images= [...this.Images, element.Name]
      });
      console.log(this.Images,age);
    });
  }
  get Id() { return this._Id; }
  constructor(private adsService: AdsServise) { }

  ngOnInit(): void { }
  selectImg(src: string) {
    this.sImg = src;
  }
}
