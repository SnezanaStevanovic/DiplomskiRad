export class Project {
    id: number;
    name: string;
    estimatedTime: Date | string | null;
    spentTime: Date | string | null;
    dateCreated: Date | string;
    endDate: Date | string | null;
    progress: number;
}
