export interface User {
    userId?: number;
    username: string;
    passwordHash: string;
    role: string;
    email: string;
    phoneNumber?: string;
    dateCreated?: string;
    lastLogin?: string;
  }
  