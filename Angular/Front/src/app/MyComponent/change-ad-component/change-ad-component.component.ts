import { Component, OnInit } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Router } from '@angular/router';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Parameter } from 'src/app/Interfaces/IParameter';

@Component({
  selector: 'app-change-ad-component',
  templateUrl: './change-ad-component.component.html',
  styleUrls: ['./change-ad-component.component.css']
})
export class ChangeAdComponentComponent implements OnInit {

  filesToUpload: File = null;
  status: string = "";
  type: string = "";

  Categories: Parameter[] = [];
  isLoadingCategories: boolean = true;
  Types: Parameter[] = [];
  isLoadingTypes: boolean = true;
  States: Parameter[] = [];
  isLoadingStates: boolean = true;
  isLoadingAdd: boolean = true;

  isLinear = true;
  nameFormGroup: FormGroup;
  titleFormGroup: FormGroup;
  pictureFormGroup: FormGroup;
  categoryFormGroup: FormGroup;
  adressFormGroup: FormGroup;
  typeFormGroup: FormGroup;
  stateFormGroup: FormGroup;

  adHidden = true;
  imageHidden = false;

  private id: number;

  response: any;

  Images: Parameter[] = [];

  // tslint:disable-next-line: max-line-length
  constructor(private adsService: AdsServise, private auth: AuthorizationService, private router: Router, private _formBuilder: FormBuilder, private _snackBar: MatSnackBar) {
    if (this.auth.UserName == "") {
      this.router.navigate(['/']);
    }
    this.id = +localStorage.getItem('changeValue');
    localStorage.removeItem('changeValue');
    if (this.id == 0) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.LoadImages();
    this.nameFormGroup = this._formBuilder.group({
      nameCtrl: ['', [Validators.minLength(5), Validators.maxLength(50)]]
    });
    this.titleFormGroup = this._formBuilder.group({
      titleCtrl: ['', [Validators.minLength(5), Validators.maxLength(3000)]]
    });

    this.categoryFormGroup = this._formBuilder.group({
      categoryCtrl: ['', Validators.required]
    });
    this.adressFormGroup = this._formBuilder.group({
      adressCtrl: ['', [Validators.minLength(5), Validators.maxLength(100)]]
    });
    this.typeFormGroup = this._formBuilder.group({
      typeCtrl: ['', Validators.required]
    });
    this.stateFormGroup = this._formBuilder.group({
      stateCtrl: ['', Validators.required]
    });

    this.adsService.GetCategories()
      .subscribe((responce: Parameter[]) => {
        this.Categories = responce;
        this.isLoadingCategories = false;
      });

    this.adsService.GetTypes()
      .subscribe((responce: Parameter[]) => {
        this.Types = responce;
        this.isLoadingTypes = false;
      });

    this.adsService.GetStates()
      .subscribe((responce: Parameter[]) => {
        this.States = responce;
        this.isLoadingStates = false;
      });
    /* * */

    this.adsService.GetAd('http://localhost:62976//api/ads/' + this.id)
      .subscribe((responces: any) => {
        this.response = (responces);
        this.categoryFormGroup.setValue({ categoryCtrl: this.response.Category });
        this.nameFormGroup.setValue({ nameCtrl: this.response.Name });
        this.stateFormGroup.setValue({ stateCtrl: this.response.State });
        this.typeFormGroup.setValue({ typeCtrl: this.response.Type });

        this.adressFormGroup.setValue({ adressCtrl: this.response.Adress });
        this.titleFormGroup.setValue({ titleCtrl: this.response.Title });
        this.isLoadingAdd = false;
      })
  }

  LoadImages(){
    this.adsService.GetImages(this.id).subscribe((images:Parameter[])=>{
      this.Images = images;
    });
  }
  UpdateAd() {

    this.response.Name = this.nameFormGroup.value.nameCtrl;
    this.response.Title = this.titleFormGroup.value.titleCtrl;
    this.response.Adress = this.adressFormGroup.value.adressCtrl;
    this.response.Category = this.categoryFormGroup.value.categoryCtrl;
    this.response.Type = this.typeFormGroup.value.typeCtrl;
    this.response.State = this.stateFormGroup.value.stateCtrl;
    this.adsService.ChangeAd(this.filesToUpload, this.response)
      .subscribe((response: any) => {
        this._snackBar.open("Изменено", "Угу", { duration: 2000 });
        this.router.navigate(['/myads/']);
      },
        err => {
          this._snackBar.open("Упс, косяк", "Угу", { duration: 2000 });
        });
    console.log(this.response);
  }

  OnFileSelected(event) {
    this.filesToUpload = event.target.files[0];
    this._snackBar.open('Изображение выбрано', 'Угу', { duration: 2000 });
    this.response.Picture = '';
  }

  DeleteImage (id){
    this.adsService.DeleteImage(id, this.id).subscribe(()=>{
      this._snackBar.open('Удалено', 'Угу', { duration: 2000 });
      this.LoadImages();
    },
    err=>{
      this.router.navigate(['/servereception/']);
    });
  }
  AddImage(event){
    this.adsService.AddImage(event.target.files[0], this.id).subscribe(()=>{
      this._snackBar.open('Добавлен', 'Угу', { duration: 2000 });
      this.LoadImages();
    });
  }
}
