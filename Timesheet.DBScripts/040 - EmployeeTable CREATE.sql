CREATE TABLE Employee(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Adress] [nvarchar](50) NULL,
	[Gender] [nvarchar] (10) NULL,
	[DateOfBirth] [datetime] NULL,
	[ProjectId] [int] NOT NULL
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User] FOREIGN KEY([UserId])
REFERENCES [UserLogin] ([Id])
GO
ALTER TABLE [Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Role] FOREIGN KEY([RoleId])
REFERENCES [Role] ([Id])
GO
ALTER TABLE [Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Project] FOREIGN KEY([ProjectId])
REFERENCES [Project] ([Id])
GO