CREATE TABLE ProjectTaskStatus
(  
   Id int IDENTITY (1,1) NOT NULL,  
   Name varchar(50) NOT NULL
   CONSTRAINT PK_ProjectTaskStatus_Id PRIMARY KEY CLUSTERED (Id)  
);  