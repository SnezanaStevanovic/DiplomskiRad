ALTER TABLE ProjectTask ADD EmployeeId INT NULL

ALTER TABLE ProjectTask     
ADD CONSTRAINT FK_ProjectTask_Employee FOREIGN KEY (EmployeeId)     
    REFERENCES Employee (Id)  