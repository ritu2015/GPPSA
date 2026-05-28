export interface Employee {
  id: string;
  employeeCode: string;
  fullName: string;
  email: string;
  salary: number;
  dateOfJoining: string;   // IMPORTANT: string, not Date
  departmentId: string;
  departmentName: string;  // MUST exist
}