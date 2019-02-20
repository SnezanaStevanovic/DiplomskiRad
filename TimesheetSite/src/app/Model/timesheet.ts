export interface Timesheet {
    id: number;
    employeeId: number;
    overtime: Date | string;
    pause: Date | string;
    startTime: Date;
    endTime: Date;
}
