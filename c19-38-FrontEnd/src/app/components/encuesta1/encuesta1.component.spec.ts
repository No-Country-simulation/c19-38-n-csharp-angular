import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Encuesta1Component } from './encuesta1.component';

describe('Encuesta1Component', () => {
  let component: Encuesta1Component;
  let fixture: ComponentFixture<Encuesta1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Encuesta1Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Encuesta1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
