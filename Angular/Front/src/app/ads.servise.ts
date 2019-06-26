import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs";


@Injectable()
export class AdsServise {
    constructor(private httpClient: HttpClient) { }
    public get(url: string){
        return this.httpClient.get(url);
    }
    
}