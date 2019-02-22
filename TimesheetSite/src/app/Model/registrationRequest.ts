import { Employee } from './employee';
import { User } from './user';

export class RegistrationRequest {
    newEmployee: Employee;
    newUser: User;
    projectIds: Array<number>;

}
