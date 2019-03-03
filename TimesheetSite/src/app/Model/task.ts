import { TaskType } from './taskType';

export class Task {
        id: number;

        projectId: number;

        employeeId: number;

        name: string;

        type: TaskType;

        description: string;

        estimatedTime: Date | string | null;

        startDate: Date | string;

        endDate: Date | string | null;

        spentTime: Date | string | null;

        progress: number;
    }
