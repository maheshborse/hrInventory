import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DispatchToEmployeeComponent } from './dispatch-to-employee.component';

describe('DispatchToEmployeeComponent', () => {
  let component: DispatchToEmployeeComponent;
  let fixture: ComponentFixture<DispatchToEmployeeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DispatchToEmployeeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DispatchToEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
