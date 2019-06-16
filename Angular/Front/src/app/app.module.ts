import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdComponent } from './Ad/ad.component';
import { HttpClientModule } from '@angular/common/http';
import { AdsPageComponent } from './ads-page/ads-page.component';
import { BigAdComponent } from './big-ad/big-ad.component';
import { RouterModule } from '@angular/router';

const routes = [
  { path: '', component: AdsPageComponent },
  { path: 'ad/:id', component: BigAdComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    AdComponent,
    AdsPageComponent,
    BigAdComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
