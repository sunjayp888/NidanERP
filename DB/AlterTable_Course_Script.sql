
-- Add SchemeId [CourseTypeId] & [Description] To Course Table
alter table Course add [CourseTypeId] [int] NULL

ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_CourseType] FOREIGN KEY([CourseTypeId])
REFERENCES [dbo].[CourseType] ([CourseTypeId])
GO

alter table Course add [Description] [varchar](1000) NULL
GO