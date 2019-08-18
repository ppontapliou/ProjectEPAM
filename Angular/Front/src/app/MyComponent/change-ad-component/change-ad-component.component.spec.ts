import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeAdComponentComponent } from './change-ad-component.component';

describe('ChangeAdComponentComponent', () => {
  let component: ChangeAdComponentComponent;
  let fixture: ComponentFixture<ChangeAdComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeAdComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeAdComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
