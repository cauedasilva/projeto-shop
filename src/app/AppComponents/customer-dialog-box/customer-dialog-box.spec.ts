import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerDialogBox } from './customer-dialog-box';

describe('CustomerDialogBox', () => {
  let component: CustomerDialogBox;
  let fixture: ComponentFixture<CustomerDialogBox>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerDialogBox]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerDialogBox);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
