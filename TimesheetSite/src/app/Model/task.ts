import { TaskType } from './taskType';
import { TaskStatus } from './taskStatus.enum';

export class Task {
        id: number;

        projectId: number;

        projectName: string;

        employeeId: number;

        employeeName: string;

        name: string;

        type: TaskType;

        description: string;

        estimatedTime: Date | string | null;

        startDate: Date | string;

        endDate: Date | string | null;

        spentTime: number | null;

        progress: number;

        status: TaskStatus;
    }
