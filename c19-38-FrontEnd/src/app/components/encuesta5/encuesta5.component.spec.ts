import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Encuesta5Component } from './encuesta5.component';

describe('Encuesta5Component', () => {
  let component: Encuesta5Component;
  let fixture: ComponentFixture<Encuesta5Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Encuesta5Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Encuesta5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
