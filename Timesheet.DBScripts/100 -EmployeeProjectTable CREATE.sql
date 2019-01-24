CREATE TABLE EmployeeProject(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL 
	CONSTRAINT [PK_EmployeeProject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [EmployeeProject]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProject_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [Employee] ([Id])
GO

ALTER TABLE [EmployeeProject]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProject_Project] FOREIGN KEY([ProjectId])
REFERENCES [Project] ([Id])
GO
