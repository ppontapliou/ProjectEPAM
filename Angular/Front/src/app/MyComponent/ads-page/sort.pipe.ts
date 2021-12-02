import { Pipe } from '@angular/core';
import { PipeTransform } from '@angular/core';

@Pipe({
  name: 'sort'
})
export class SortPipe implements PipeTransform {
  transform(Ads, sort) {
    return Ads.filter(Ad => {
      return Ad.Name.toLowerCase().includes(sort.toLowerCase());
    })
  }
}
@Pipe({
  name: 'sortCategory'
})
export class SortPipeCategory implements PipeTransform {
  transform(Ads, c1, c2, c3, c4, c5) {
    return Ads.filter(Ad => {
      let sort = "";
      if (c1) {
        sort = "Животные";
      }
      if (c2) {
        sort = "Учеба";
      }
      if (c3) {
        sort = "Развлечение";
      }
      if (c4) {
        sort = "Технологии";
      }
      if (c5) {
        sort = "Дом";
      }
      return Ad.Category.toLowerCase().includes(sort.toLowerCase());
    })
  }
}
@Pipe({
  name: 'sortType'
})
export class SortPipeType implements PipeTransform {
  transform(Ads, c1, c2) {
    return Ads.filter(Ad => {
      let sort = "";
      if (c1) {
        sort = "Товар";
      }
      if (c2) {
        sort = "Услуга";
      }
      return Ad.Type.toLowerCase().includes(sort.toLowerCase());
    })
  }
}
