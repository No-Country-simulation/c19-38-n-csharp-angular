import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeDeportistaComponent } from './home-deportista.component';

describe('HomeDeportistaComponent', () => {
  let component: HomeDeportistaComponent;
  let fixture: ComponentFixture<HomeDeportistaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeDeportistaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeDeportistaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
