import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Encuesta6Component } from './encuesta6.component';

describe('Encuesta6Component', () => {
  let component: Encuesta6Component;
  let fixture: ComponentFixture<Encuesta6Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Encuesta6Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Encuesta6Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
