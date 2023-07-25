import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditThexpressionComponent } from './edit-thexpression.component';

describe('EditThexpressionComponent', () => {
  let component: EditThexpressionComponent;
  let fixture: ComponentFixture<EditThexpressionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditThexpressionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditThexpressionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
