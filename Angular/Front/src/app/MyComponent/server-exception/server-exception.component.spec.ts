import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerExceptionComponent } from './server-exception.component';

describe('ServerExceptionComponent', () => {
  let component: ServerExceptionComponent;
  let fixture: ComponentFixture<ServerExceptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServerExceptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServerExceptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
