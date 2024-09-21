import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerJobComponent } from './employer-job.component';

describe('EmployerJobComponent', () => {
  let component: EmployerJobComponent;
  let fixture: ComponentFixture<EmployerJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployerJobComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployerJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
