import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThoughtPageComponent } from './thought-page.component';

describe('ThoughtPageComponent', () => {
  let component: ThoughtPageComponent;
  let fixture: ComponentFixture<ThoughtPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThoughtPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ThoughtPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
