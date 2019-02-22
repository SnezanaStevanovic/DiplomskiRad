import { BaseResponse } from './baseResponse';
import { Employee } from './employee';

export interface AllEmployeesResponse extends BaseResponse {
    employees: Array<Employee>;
}
