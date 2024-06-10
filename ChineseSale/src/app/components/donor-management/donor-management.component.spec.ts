import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorManagementComponent } from './donor-management.component';

describe('DonorManagementComponent', () => {
  let component: DonorManagementComponent;
  let fixture: ComponentFixture<DonorManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DonorManagementComponent]
    });
    fixture = TestBed.createComponent(DonorManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
