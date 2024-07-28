import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Encuesta2Component } from './encuesta2.component';

describe('Encuesta2Component', () => {
  let component: Encuesta2Component;
  let fixture: ComponentFixture<Encuesta2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Encuesta2Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Encuesta2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
