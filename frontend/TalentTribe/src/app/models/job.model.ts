export interface Job {
    jobId: number;
    employerProfileId: number;
    companyName:string,
    jobTitle: string;
    jobDescription: string;
    requiredSkills: string;
    employmentType: string;
    salaryRange: string;
    location: string;
    jobStatus: string;
    datePosted: Date;
    applicationDeadline?: Date;
    isActive: boolean;
    experienceLevel: string;
    CompanyPictureUrl?:string;
  }