import { AuthInterceptor } from './Inteceptors/api.interceptor';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { RouterModule } from '@angular/router';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { LogComponent } from './MyComponent/log/log.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { AdsPageComponent } from './MyComponent/ads-page/ads-page.component';
import { BigAdComponent } from './MyComponent/big-ad/big-ad.component';
import { AdComponent } from './MyComponent/Ad/ad.component';
import { SortPipe, SortPipeCategory, SortPipeType } from './MyComponent/ads-page/sort.pipe';
import { NotFoundComponentComponent } from './MyComponent/not-found-component/not-found-component.component';
import { ChangeAdComponentComponent } from './MyComponent/change-ad-component/change-ad-component.component';
import { ContactComponentComponent } from './MyComponent/contact-component/contact-component.component';
import { ContactAdsComponentComponent } from './MyComponent/contact-ads-component/contact-ads-component.component';
import { AddAdComponent } from './MyComponent/add-ad/add-ad.component';
import { ServerExceptionComponent } from './MyComponent/server-exception/server-exception.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import {MatPaginatorModule} from '@angular/material/paginator';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AddContactComponent } from './MyComponent/AdContact/AdContact.component';
import { DialogBoxComponent } from './MyComponent/dialog-box/dialog-box.component';
import { ImageSliderComponent } from './MyComponent/image-slider/image-slider.component';
import { RegistrationComponent } from './MyComponent/registration/registration.component';


const routes = [
  { path: '', component: AdsPageComponent },
  { path: 'ad/:id', component: BigAdComponent },
  { path: 'myads', component: ContactAdsComponentComponent },
  { path: 'contact', component: ContactComponentComponent },
  { path: 'add', component: AddAdComponent },
  { path: 'change', component: ChangeAdComponentComponent },
  { path: 'servereception', component: ServerExceptionComponent },
  { path: 'img', component: ImageSliderComponent },
  { path: 'adminbar', component: AddContactComponent },
  { path: 'reg', component: RegistrationComponent },
  { path: '**', component: NotFoundComponentComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    AdComponent,
    AdsPageComponent,
    BigAdComponent,
    LogComponent,
    SortPipe, SortPipeCategory, SortPipeType,
    NotFoundComponentComponent,
    ChangeAdComponentComponent,
    ContactComponentComponent,
    ContactAdsComponentComponent,
    AddAdComponent,
    ServerExceptionComponent,
    AddContactComponent,
    DialogBoxComponent,
    ImageSliderComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ScrollingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatStepperModule,
    MatIconModule,
    MatSnackBarModule,
    MatMenuModule,
    MatGridListModule,
    MatListModule,
    MatSlideToggleModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatRadioModule,
    MatPaginatorModule,
  ],
  exports: [],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  entryComponents: [
    DialogBoxComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
