import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RandomManagementComponent } from './random-management.component';

describe('RandomManagementComponent', () => {
  let component: RandomManagementComponent;
  let fixture: ComponentFixture<RandomManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RandomManagementComponent]
    });
    fixture = TestBed.createComponent(RandomManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
