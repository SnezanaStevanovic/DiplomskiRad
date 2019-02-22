CREATE TABLE [dbo].[EmployeeTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTask_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[EmployeeTask] CHECK CONSTRAINT [FK_EmployeeTask_Employee]
GO

ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjectTask_ProjectTask] FOREIGN KEY([TaskId])
REFERENCES [dbo].[ProjectTask] ([Id])
GO

ALTER TABLE [dbo].[EmployeeTask] CHECK CONSTRAINT [FK_EmployeeProjectTask_ProjectTask]
GO


