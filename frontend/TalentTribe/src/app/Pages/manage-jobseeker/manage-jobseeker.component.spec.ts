import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageJobseekerComponent } from './manage-jobseeker.component';

describe('ManageJobseekerComponent', () => {
  let component: ManageJobseekerComponent;
  let fixture: ComponentFixture<ManageJobseekerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageJobseekerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageJobseekerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
