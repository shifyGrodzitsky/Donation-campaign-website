import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftManagementComponent } from './gift-management.component';

describe('GiftManagementComponent', () => {
  let component: GiftManagementComponent;
  let fixture: ComponentFixture<GiftManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GiftManagementComponent]
    });
    fixture = TestBed.createComponent(GiftManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
