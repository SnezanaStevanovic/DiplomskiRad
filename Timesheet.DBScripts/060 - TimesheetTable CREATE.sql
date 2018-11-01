CREATE TABLE [Timesheet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Overtime] [datetime] NOT NULL,
	[StartPeriodDate] [datetime] NOT NULL,
	[EndPeriodDate] [datetime] NOT NULL,
	[PauseTime] [datetime] NULL,
	[VacationTime] [datetime] NULL,
	[WorkTime] [datetime] NOT NULL,
	[EmployeeId] [int] NOT NULL
 CONSTRAINT [PK_Timesheet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [Employee] ([Id])
GO