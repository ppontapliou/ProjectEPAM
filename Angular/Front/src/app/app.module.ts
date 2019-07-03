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

const routes = [
  { path: '', component: AdsPageComponent },
  { path: 'ad/:id', component: BigAdComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    AdComponent,
    AdsPageComponent,
    BigAdComponent,
    LogComponent,
    SortPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ScrollingModule,
    RouterModule.forRoot(routes),
    FormsModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
