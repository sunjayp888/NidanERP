USE [NidanProd]
GO

/****** Object:  Table [dbo].[SubjectTrainer]    Script Date: 28/03/2017 06:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubjectTrainer](
	[SubjectTrainerId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_SubjectTrainer] PRIMARY KEY CLUSTERED 
(
	[SubjectTrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubjectTrainer]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTrainer_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[SubjectTrainer] CHECK CONSTRAINT [FK_SubjectTrainer_Organisation]
GO

ALTER TABLE [dbo].[SubjectTrainer]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTrainer_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO

ALTER TABLE [dbo].[SubjectTrainer] CHECK CONSTRAINT [FK_SubjectTrainer_Subject]
GO

ALTER TABLE [dbo].[SubjectTrainer]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTrainer_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainer] ([TrainerId])
GO

ALTER TABLE [dbo].[SubjectTrainer] CHECK CONSTRAINT [FK_SubjectTrainer_Trainer]
GO


