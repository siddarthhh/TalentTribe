

  export interface EmployerProfile {
    employerProfileId: number;
    fullName: string;
    positionTitle: string;
    department: string;
    workEmail: string;
    workPhone: string;
    dateJoined: string;  // Use 'string' type to match the format in API response
  }
  