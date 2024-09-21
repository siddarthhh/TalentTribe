import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppliedjobseekerdetailsComponent } from './appliedjobseekerdetails.component';

describe('AppliedjobseekerdetailsComponent', () => {
  let component: AppliedjobseekerdetailsComponent;
  let fixture: ComponentFixture<AppliedjobseekerdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppliedjobseekerdetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppliedjobseekerdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
