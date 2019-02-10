import { Role } from './role.enum';
import { Gender } from './gender.enum';

export class Employee {
    id: number;

    userId: number;

    role: Role;

    firstName: string;

    lastName: string;

    dateOfBirth: Date | string | null;

    adress: string;

    gender: Gender | null;
}
