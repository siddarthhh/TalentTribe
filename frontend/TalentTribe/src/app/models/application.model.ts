export interface Application {
    applicationId?: number;
    jobSeekerProfileId: number;
    jobId: number;
    coverLetter: string;
    status: string;
    resumeUrl?: string;
    applicationDate: Date;
    submittedDate: Date;
    interviewDate?: Date;
    feedback?: string;
  }


  export interface ApplicationUpdatePayload {
    status: string;
    feedback: string;
   
  }


  export interface JobApplication {
    applicationId?: number;
    jobSeekerProfileId?: number;
    jobId?: number;
    status: string;
    coverLetter?: string;
    resumeUrl?: string;
    submittedDate: Date;
    feedback?: string;
    jobDetails?: {
      jobTitle: string;
      companyName:string,
      jobDescription?: string;
      requiredSkills?: string;
      employmentType?: string;
      salaryRange?: string;
      location?: string;
      experienceLevel?: string;
    };
    interviews?: Interview[];
  }
  
  export interface Interview {
    interviewDate?: Date;
    interviewFeedback?: string;
    interviewType?: string;
    interviewLink?: string;
    interviewLocation?:string;
  }
  export interface EmpApplication {
    applicationId: number;
    jobId: number;
    jobSeekerProfileId: number;
    applicationDate: Date;
    status: string;
    resumeUrl: string;
    coverLetter: string;
    job?: {
      jobTitle: string; // Adjust according to Job model
    };
    jobSeekerProfile?: {
      fullName: string; // Adjust according to JobSeekerProfile model
    };
  }
  
  