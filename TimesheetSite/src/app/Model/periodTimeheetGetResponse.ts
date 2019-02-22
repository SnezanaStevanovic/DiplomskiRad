import { BaseResponse } from './baseResponse';
import { Timesheet } from './timesheet';

export interface PeriodTimeheetGetResponse extends BaseResponse {
    allTimesheetsForPeriod: Timesheet[];
}
