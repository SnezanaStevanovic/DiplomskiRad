export interface Timesheet {
    id: number;
    employeeId: number;
    overtime: Date | string;
    pause: Date | string;
    startTime: Date | string;
    endTime: Date | string;
}