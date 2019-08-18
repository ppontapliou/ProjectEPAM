import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';

import { RouterModule } from '@angular/router';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { LogComponent } from './MyComponent/log/log.component';

import { FormsModule } from '@angular/forms'
import { AdsPageComponent } from './MyComponent/ads-page/ads-page.component';
import { BigAdComponent } from './MyComponent/big-ad/big-ad.component';
import { AdComponent } from './MyComponent/Ad/ad.component';
import { SortPipe } from './MyComponent/ads-page/sort.pipe';
import { NotFoundComponentComponent } from './MyComponent/not-found-component/not-found-component.component';
import { ChangeAdComponentComponent } from './MyComponent/change-ad-component/change-ad-component.component';
import { ContactComponentComponent } from './MyComponent/contact-component/contact-component.component';
import { ContactAdsComponentComponent } from './MyComponent/contact-ads-component/contact-ads-component.component';
import { AddAdComponent } from './MyComponent/add-ad/add-ad.component';
import { ServerExceptionComponent } from './MyComponent/server-exception/server-exception.component';


const routes = [
  { path: '', component: AdsPageComponent },
  { path: 'ad/:id', component: BigAdComponent },
  { path: 'myads', component: ContactAdsComponentComponent },
  { path: 'contact', component: ContactComponentComponent },
  { path: 'add', component: AddAdComponent },
  { path: 'change/:id', component: ChangeAdComponentComponent },
  { path: 'servereception', component: ServerExceptionComponent},
  { path: '**', component: NotFoundComponentComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    AdComponent,
    AdsPageComponent,
    BigAdComponent,
    LogComponent,
    SortPipe,
    NotFoundComponentComponent,
    ChangeAdComponentComponent,
    ContactComponentComponent,
    ContactAdsComponentComponent,
    AddAdComponent,
    ServerExceptionComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ScrollingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    //ScrollDispatchModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
