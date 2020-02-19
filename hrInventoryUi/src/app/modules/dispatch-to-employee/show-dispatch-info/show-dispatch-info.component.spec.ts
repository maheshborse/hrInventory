import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowDispatchInfoComponent } from './show-dispatch-info.component';

describe('ShowDispatchInfoComponent', () => {
  let component: ShowDispatchInfoComponent;
  let fixture: ComponentFixture<ShowDispatchInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowDispatchInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowDispatchInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
