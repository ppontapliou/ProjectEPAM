import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs";


@Injectable()
export class AdsServise {
    constructor(private httpClient: HttpClient) { }
    public get(url: string){
        return this.httpClient.get(url);
    }

    ads = [
        { Name: 'name1' },
        { Name: 'name2' },
        { Name: 'name3' },
        { Name: 'name4' },
        { Name: 'name5' },
        { Name: 'name6' },
        { Name: 'name7' },
        { Name: 'name8' },
    ]
}