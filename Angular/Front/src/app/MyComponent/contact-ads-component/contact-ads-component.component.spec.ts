import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactAdsComponentComponent } from './contact-ads-component.component';

describe('ContactAdsComponentComponent', () => {
  let component: ContactAdsComponentComponent;
  let fixture: ComponentFixture<ContactAdsComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactAdsComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactAdsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
