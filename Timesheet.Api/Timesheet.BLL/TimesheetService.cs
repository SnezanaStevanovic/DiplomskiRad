using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL
{
    public class TimesheetService : ITimesheetService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TimesheetService));

        private readonly ITimesheetDP _timesheetDP;

        public TimesheetService(ITimesheetDP timesheetDP)
        {
            _timesheetDP = timesheetDP;
        }

        public async Task<BaseResponse> EndTimeSet(TimesheetEndTimeRequest endTimeRequest)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                await _timesheetDP.UpdateEndTime(endTimeRequest.EmployeeId,
                                                 endTimeRequest.EndTime,
                                                 endTimeRequest.Pause,
                                                 endTimeRequest.Overtime);

                baseResponse.Success = true;
                baseResponse.Message = "Timesheet EndTime successfully updated";
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Timesheet EndTimeSet failed";
            }
            
            return baseResponse;
        }

        public async Task<PeriodTimeheetGetResponse> PeriodTimesheetGet(PeriodTimeheetGetRequest periodTimeheetGetRequest)
        {
            PeriodTimeheetGetResponse response = new PeriodTimeheetGetResponse();
            try
            {
                response.AllTimesheetsForPeriod = await _timesheetDP.PeriodTimeshetGet(periodTimeheetGetRequest.EmployeeId,
                                                                                       periodTimeheetGetRequest.StartDate,
                                                                                       periodTimeheetGetRequest.EndDate);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "PeriodTimesheetGet method failed.";

                Logger.Error($"PeriodTimesheetGet method failed. Details: {ex}");
            }

            return response;
        }

        public async Task<BaseResponse> StartTimeSet(TimesheetStartTimeRequest startTimeRequest)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
               await  _timesheetDP.InsertStartTime(startTimeRequest.EmployeeId,
                                                   startTimeRequest.StartTime);

                baseResponse.Success = true;
                baseResponse.Message = "Timesheet StartTime successfully changed";
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Timesheet StartTimeSet failed";
            }

            return baseResponse;
        }


    }
}
