import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminMetricsComponent } from './admin-metrics.component';

describe('AdminMetricsComponent', () => {
  let component: AdminMetricsComponent;
  let fixture: ComponentFixture<AdminMetricsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminMetricsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminMetricsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
