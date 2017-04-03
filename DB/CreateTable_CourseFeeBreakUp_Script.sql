USE [NidanProd]
GO

/****** Object:  Table [dbo].[CourseFeeBreakUp]    Script Date: 28/03/2017 06:19:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CourseFeeBreakUp](
	[CourseFeeBreakUpId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CourseFeeBreakUp] PRIMARY KEY CLUSTERED 
(
	[CourseFeeBreakUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CourseFeeBreakUp]  WITH CHECK ADD  CONSTRAINT [FK_CourseFeeBreakUp_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CourseFeeBreakUp] CHECK CONSTRAINT [FK_CourseFeeBreakUp_Organisation]
GO


