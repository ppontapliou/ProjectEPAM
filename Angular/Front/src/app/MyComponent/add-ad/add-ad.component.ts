import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/MyServises/authorization.service';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Parameter } from 'src/app/Interfaces/IParameter';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-ad',
  templateUrl: './add-ad.component.html',
  styleUrls: ['./add-ad.component.css']
})
export class AddAdComponent implements OnInit {

  filesToUpload: File[] = null;

  Categories: Parameter[] = [];
  isLoadingCategories: boolean = false;
  Types: Parameter[] = [];
  isLoadingTypes: boolean = false;
  States: Parameter[] = [];
  isLoadingStates: boolean = false;
  AllCorrect: boolean = true;

  isLinear = true;
  nameFormGroup: FormGroup;
  titleFormGroup: FormGroup;
  pictureFormGroup: FormGroup;
  categoryFormGroup: FormGroup;
  adressFormGroup: FormGroup;
  typeFormGroup: FormGroup;
  stateFormGroup: FormGroup;

  // tslint:disable-next-line: max-line-length
  constructor(private adsService: AdsServise, private auth: AuthorizationService, private router: Router, private _formBuilder: FormBuilder, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    if (this.auth.UserName == '') {
      this.router.navigate(['/']);
    }
    this.isLoadingStates = true;
    this.isLoadingCategories = true;
    this.isLoadingTypes = true;

    this.nameFormGroup = this._formBuilder.group({
      nameCtrl: ['', [Validators.minLength(5), Validators.maxLength(50)]]
    });
    this.titleFormGroup = this._formBuilder.group({
      titleCtrl: ['', [Validators.minLength(5), Validators.maxLength(3000)]]
    });
    this.pictureFormGroup = this._formBuilder.group({
      pictureCtrl: ['', [Validators.minLength(2), Validators.maxLength(1000)]]
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

  }
  AddAd() {
    var body = {
      Name: this.nameFormGroup.value.nameCtrl,
      Title: this.titleFormGroup.value.titleCtrl,
      Adress: this.adressFormGroup.value.adressCtrl,
      Category: this.categoryFormGroup.value.categoryCtrl,
      Type: this.typeFormGroup.value.typeCtrl,
      State: this.stateFormGroup.value.stateCtrl,
    };
    if(this.filesToUpload==null){
      return;
    }
    this.adsService.AddAd(this.filesToUpload, body).subscribe((resp: any) => {
      this._snackBar.open('Добавлено', 'Угу', { duration: 2000 });
      this.router.navigate(['/myads/']);
    },
      err => {
        this._snackBar.open('Упс, косяк', 'Угу', { duration: 2000 });
      });
  }
  OnFileSelected(event) {
    if (event.target.files.length < 10) {
      this.filesToUpload = event.target.files;
      this._snackBar.open('Изображения выбраны [' + this.filesToUpload.length + ']', 'Угу', { duration: 2000 });
    } else {
      this._snackBar.open('Изображений должно быть не больше 10', 'Угу', { duration: 2000 });
    }
  }
}
