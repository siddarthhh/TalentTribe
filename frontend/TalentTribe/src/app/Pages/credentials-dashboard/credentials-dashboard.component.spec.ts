import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CredentialsDashboardComponent } from './credentials-dashboard.component';

describe('CredentialsDashboardComponent', () => {
  let component: CredentialsDashboardComponent;
  let fixture: ComponentFixture<CredentialsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CredentialsDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CredentialsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
