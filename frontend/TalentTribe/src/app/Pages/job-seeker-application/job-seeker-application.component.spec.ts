import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobSeekerApplicationComponent } from './job-seeker-application.component';

describe('JobSeekerApplicationComponent', () => {
  let component: JobSeekerApplicationComponent;
  let fixture: ComponentFixture<JobSeekerApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobSeekerApplicationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobSeekerApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
