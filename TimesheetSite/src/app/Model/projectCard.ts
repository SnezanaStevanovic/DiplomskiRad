import { Project } from './project';
import { Employee } from './employee';

export class ProjectCard  {
    cols: number;
    rows: number;
    project: Project;
    employees: Array<Employee>;
}
