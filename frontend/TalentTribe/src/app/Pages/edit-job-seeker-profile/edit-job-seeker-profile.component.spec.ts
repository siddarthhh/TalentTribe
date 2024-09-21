import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJobSeekerProfileComponent } from './edit-job-seeker-profile.component';

describe('EditJobSeekerProfileComponent', () => {
  let component: EditJobSeekerProfileComponent;
  let fixture: ComponentFixture<EditJobSeekerProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditJobSeekerProfileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditJobSeekerProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
