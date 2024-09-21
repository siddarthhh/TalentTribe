import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmployerProfileComponent } from './edit-employer-profile.component';

describe('EditEmployerProfileComponent', () => {
  let component: EditEmployerProfileComponent;
  let fixture: ComponentFixture<EditEmployerProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditEmployerProfileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditEmployerProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
