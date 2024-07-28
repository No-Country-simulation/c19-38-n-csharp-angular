import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Encuesta3Component } from './encuesta3.component';

describe('Encuesta3Component', () => {
  let component: Encuesta3Component;
  let fixture: ComponentFixture<Encuesta3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Encuesta3Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Encuesta3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
