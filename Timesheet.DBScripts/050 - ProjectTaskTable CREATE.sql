CREATE TABLE [ProjectTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Progress] [varchar] (10) NULL,
	[EstimatedTime] [datetime] NULL,
	[SpentTime] [datetime] NULL,
	[ProjectId] [int] NOT NULL
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_Project] FOREIGN KEY([ProjectId])
REFERENCES [Project] ([Id])
GO

ALTER TABLE [ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_ProjectTaskType] FOREIGN KEY([Type])
REFERENCES [ProjectTaskType] ([Id])
GO