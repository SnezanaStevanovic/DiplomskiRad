import { BaseResponse } from './baseResponse';
import { HoursPerDay } from './hoursPerDay';

export interface GetWorkingHoursForPeriodResponse extends BaseResponse {
    hoursPerDay: HoursPerDay[];
}
