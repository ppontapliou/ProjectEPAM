import { element } from 'protractor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AdsServise } from 'src/app/MyServises/ads.servise';
import { Router } from '@angular/router';
import { Parameter } from 'src/app/Interfaces/IParameter';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-ads-page',
  templateUrl: './ads-page.component.html',
  styleUrls: ['./ads-page.component.css']
})
export class AdsPageComponent implements OnInit {

  @ViewChild(CdkVirtualScrollViewport)
  viewport: CdkVirtualScrollViewport;

  sortAd = '';
  response: any = [];
  isLoadingResults = false;
  Categories: Parameter[] = [new Parameter(0, 'Все категории')];
  States: Parameter[] = [new Parameter(0, 'Любое состояние')];
  StateValue = 0;
  CategoryValue = 0;
  TypeValue = 0;
  SearchLine = '';
  IsEnd = false;

  checked = false;

  constructor(private adsService: AdsServise, private router: Router) { }

  ngOnInit() {
    this.isLoadingResults = true;
    this.adsService.GetCategories()
      .subscribe((responce: Parameter[]) => {
        this.Categories = [...this.Categories, ...responce];
      },
        err => {
          this.router.navigate(['/servereception']);
        });
    this.adsService.GetStates()
      .subscribe((responce: Parameter[]) => {
        this.States = [...this.States, ...responce];
      },
        err => {
          this.router.navigate(['/servereception']);
        });
  }

  Find() {
    this.SearchLine = this.sortAd;
    this.response = [];
    this.GetAds(0);
  }

  ChangeCategory(id) {
    this.CategoryValue = id;
    console.log(this.CategoryValue);
    this.response = [];
    this.GetAds(0);
  }

  ChangeType(id) {
    this.TypeValue = id;
    this.response = [];
    this.GetAds(0);
  }

  ChangeState(id) {
    this.StateValue = id;
    console.log(this.StateValue);
    this.response = [];
    this.GetAds(0);
  }

  GetAds(id: number) {
    if (id > 0) {
      id = this.response[id - 1].Id;
    }
    console.log(id, 'query')
    this.IsEnd = false;
    this.isLoadingResults = true;
    this.adsService.GetPartAds(id, this.CategoryValue, this.StateValue, this.SearchLine, this.TypeValue)
      .subscribe((responces: any) => {
        console.log(responces);
        if (responces.length < 5) {
          this.IsEnd = true;
        }
        //this.adsService.response = (responces);
        this.response = [...this.response, ...responces];
        this.isLoadingResults = false;
      },
        err => {
          this.router.navigate(['/servereception']);
        });
  }


  Reload(id) {
    console.log('reload', id)
    if (this.IsEnd) {
      return;
    }
    const end = this.viewport.getRenderedRange().end;
    const total = this.viewport.getDataLength();
    console.log(`${end}, '>=', ${total}`);
    if (end == total) {
      this.GetAds(id);
    }
  }

  generatePdf() {
    // let docDefinition = {
    //   content: [
    //     { text: 'Объявления', style: 'header' },
    //   ],
    //   styles: {
    //     header: {
    //       fontSize: 18,
    //       bold: true,
    //     },
    //     subheader: {
    //       fontSize: 14,
    //       bold: true,
    //       margin: [0, 15, 0, 0]
    //     },
    //   }
    // };

    // this.response.forEach((element: any) => {
    //   docDefinition.content.push({ text: 'Название', style: 'subheader' });
    //   docDefinition.content.push({ text: element.Name, style: '' });

    //   docDefinition.content.push({ text: 'Адрес', style: 'subheader' });
    //   docDefinition.content.push({ text: element.Adress, style: '' });

    //   docDefinition.content.push({ text: 'Создатель', style: 'subheader' });
    //   docDefinition.content.push({ text: element.Contact.Name, style: '' });

    //   docDefinition.content.push({ text: 'Описание', style: 'subheader' });
    //   docDefinition.content.push({ text: element.Title, style: '' });

    //   docDefinition.content.push({ text: '__________________________', style: '' });
    // });
    var docDefinition = {
      content: [
        {
          layout: 'lightHorizontalLines', // optional
          table: {
            // headers are automatically repeated if the table spans over multiple pages
            // you can declare how many rows should be treated as headers
            headerRows: 1,
            widths: ['*', 'auto', 100, '*'],

            body: [
              ['Название', 'Адрес', 'Создатель', 'Категория'],
            ]
          }
        }
      ]
    };
    this.response.forEach((element: any) => {
      docDefinition.content[0].table.body.push([element.Name, element.Adress, element.Contact.Name, element.Category]);
    });
    pdfMake.createPdf(docDefinition).download('Список объявлений.pdf');
  }
}
