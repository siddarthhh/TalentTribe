import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { RegistrationComponent } from './Pages/registration/registration.component';

import { NgModule } from '@angular/core';
import { CreateNewJobComponent } from './Pages/create-new-job/create-new-job.component';
import { authGuard } from './auth.guard';
import { JobListComponent } from './Pages/job-list/job-list.component';
import { JobDetailComponent } from './Pages/job-detail/job-detail.component';
import { ApplicationComponent } from './Pages/application/application.component';
import {  JobSeekerApplicationComponent } from './Pages/job-seeker-application/job-seeker-application.component';
import { EditJobSeekerProfileComponent } from './Pages/edit-job-seeker-profile/edit-job-seeker-profile.component';
import { AddCredentialComponent } from './Pages/add-credential/add-credential.component';
import { CredentialsDashboardComponent } from './Pages/credentials-dashboard/credentials-dashboard.component';
import { JobSeekerProfileComponent } from './Pages/job-seeker-profile/job-seeker-profile.component';
import { EditEmployerProfileComponent } from './Pages/edit-employer-profile/edit-employer-profile.component';
import { EmployeeDashboardComponent } from './Pages/employee-dashboard/employee-dashboard.component';
import { CompanyDashboardComponent } from './Pages/company-dashboard/company-dashboard.component';
import { EmployerApplicationDashboardComponent } from './Pages/employer-application-dashboard/employer-application-dashboard.component';
import { AppliedjobseekerdetailsComponent } from './Pages/appliedjobseekerdetails/appliedjobseekerdetails.component';
import { EmployerJobComponent } from './Pages/employer-job/employer-job.component';
import { InterviewDetailsComponent } from './Pages/interview-details/interview-details.component';
import { EditJobComponent } from './Pages/edit-job/edit-job.component';
import { roleGuard } from './role.guard';
import { ManageEmployerComponent } from './Pages/manage-employer/manage-employer.component';
import { ManageJobseekerComponent } from './Pages/manage-jobseeker/manage-jobseeker.component';
import { ManageApplicationsComponent } from './Pages/manage-applications/manage-applications.component';
import { ManageJobComponent } from './Pages/manage-job/manage-job.component';
import { AdminMetricsComponent } from './Pages/admin-metrics/admin-metrics.component';



export const routes: Routes = [

    {path:'',redirectTo:'login',pathMatch:'full'},

    
    {path:'login',component: LoginComponent},
    {path:'register',component: RegistrationComponent},
    {path:'job-list',component: JobListComponent},
    {path:'job-detail',component: JobDetailComponent},
    {path:'jobseeker-app',component: JobSeekerApplicationComponent, canActivate:[roleGuard],data:{requiredRole:"JobSeeker"}},
    { path: 'jobseeker-profile', component: JobSeekerProfileComponent , canActivate:[roleGuard],data:{requiredRole:"JobSeeker"}},
    { path: 'appliedjobseekerdetails/:ApplicationId/:jobSeekerProfileId', component: AppliedjobseekerdetailsComponent , canActivate:[roleGuard],data:{requiredRole:"Employer" }},
    { path: 'edit-jobseekerProfile', component: EditJobSeekerProfileComponent, canActivate:[roleGuard],data:{requiredRole:"JobSeeker"} },
    { path: 'add-credential', component: AddCredentialComponent, canActivate:[roleGuard],data:{requiredRole:"JobSeeker"} },
    { path: 'credentials-dashboard', component: CredentialsDashboardComponent, canActivate:[roleGuard],data:{requiredRole:"JobSeeker"} },
    {path:'job-detail/:jobId/:employerProfileId',component: JobDetailComponent, canActivate:[roleGuard],data:{requiredRole:"JobSeeker"}},
    {path:'application/:jobId',component:ApplicationComponent , canActivate:[roleGuard],data:{requiredRole:"JobSeeker"}},
    {path:'new-job',component: CreateNewJobComponent,canActivate:[roleGuard],data:{requiredRole:"Employer"}},
    {path:'employer-job',component: EmployerJobComponent,canActivate:[roleGuard],data:{requiredRole:"Employer"}},
    { path: 'edit-employer-profile', component: EditEmployerProfileComponent ,canActivate:[roleGuard],data:{requiredRole:"Employer"}},
    { path: 'employer-dashboard', component: EmployeeDashboardComponent ,canActivate:[roleGuard],data:{requiredRole:"Employer"}},
    { path: 'company-dashboard', component: CompanyDashboardComponent,canActivate:[roleGuard],data:{requiredRole:"Employer"} },
    { path: 'manage-employer', component: ManageEmployerComponent, canActivate:[roleGuard],data:{requiredRole:"admin"}},
    { path: 'manage-jobseeker', component: ManageJobseekerComponent , canActivate:[roleGuard],data:{requiredRole:"admin"}},
    { path: 'manage-applications', component: ManageApplicationsComponent , canActivate:[roleGuard],data:{requiredRole:"admin"}},
    { path: 'manage-jobs', component: ManageJobComponent , canActivate:[roleGuard],data:{requiredRole:"admin"}},
    { path: 'admin-metrics', component: AdminMetricsComponent , canActivate:[roleGuard],data:{requiredRole:"admin"}},


    { path: 'application-dashboard', component: EmployerApplicationDashboardComponent,canActivate:[roleGuard],data:{requiredRole:"Employer"} },
    { path: 'interview-details/:ApplicationId', component: InterviewDetailsComponent,canActivate:[roleGuard],data:{requiredRole:"Employer"} },
    {
        path: 'edit-job/:id',
        component: EditJobComponent
        ,canActivate:[roleGuard],data:{requiredRole:"Employer"}
      }
];

