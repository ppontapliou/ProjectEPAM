import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-ad',
    templateUrl: './ad.component.html',
    styleUrls: ['./ad.component.css']
})

export class AdComponent {
    @Input() ad;
    Id: number;
    ngOnInit() {
        this.Id = this.ad.Id
        
    }
}