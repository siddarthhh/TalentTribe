export interface Company {
    companyId: number;
    employerProfileId: number;
    companyName: string;
    companyDescription?: string;
    industry?: string;
    address?: string;
    city?: string;
    state?: string;
    country?: string;
    postalCode?: string;
    websiteUrl?: string;
    contactEmail?: string;
    contactPhone?: string;
  }