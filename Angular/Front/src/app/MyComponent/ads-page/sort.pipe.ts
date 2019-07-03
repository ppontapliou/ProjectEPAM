import { Pipe } from '@angular/core';
import { PipeTransform } from '@angular/core';

@Pipe({
    name:'sort'
})
export class SortPipe implements PipeTransform{
    transform(Ads, sort) {
        return Ads.filter(Ad=>{
            return Ad.NameAd.toLowerCase().includes(sort.toLowerCase())
        })
    }

}