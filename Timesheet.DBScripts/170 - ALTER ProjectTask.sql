ALTER TABLE ProjectTask ADD StatusId int not null

ALTER TABLE ProjectTask     
ADD CONSTRAINT FK_ProjetTask_ProjetTaskStatus FOREIGN KEY (StatusId)     
    REFERENCES ProjectTaskStatus (Id) 