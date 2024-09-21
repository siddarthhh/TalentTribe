import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerApplicationDashboardComponent } from './employer-application-dashboard.component';

describe('EmployerApplicationDashboardComponent', () => {
  let component: EmployerApplicationDashboardComponent;
  let fixture: ComponentFixture<EmployerApplicationDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployerApplicationDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployerApplicationDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
