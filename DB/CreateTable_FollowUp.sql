USE [NidanProd]
GO

/****** Object:  Table [dbo].[FollowUp]    Script Date: 16/03/2017 7:14:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[FollowUp](
	[FollowUpId] [int] IDENTITY(1,1) NOT NULL,
	[FollowUpDateTime] [datetime] NOT NULL CONSTRAINT [DF_FollowUp_FollowUpDateTime]  DEFAULT (((1900)-(1))-(1)),
	[MobilizationId] [int] NULL,
	[EnquiryId] [int] NULL,
	[Remark] [nvarchar](max) NULL,
	[Closed] [bit] NULL,
	[ReadDateTime] [datetime] NOT NULL CONSTRAINT [DF_FollowUp_ReadDateTime]  DEFAULT (((1900)-(1))-(1)),
	[CreatedDateTime] [datetime] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[Name] [varchar](500) NULL,
	[Mobile] [bigint] NULL,
	[IntrestedCourseId] [int] NOT NULL,
	[FollowUpType] [varchar](200) NULL,
	[AlternateMobile] [bigint] NULL,
	[FollowUpURL] [varchar](2000) NULL,
	[CounsellingId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[FollowUp] ADD [Close] [varchar](5) NULL
ALTER TABLE [dbo].[FollowUp] ADD [ClosingRemark] [varchar](max) NULL
ALTER TABLE [dbo].[FollowUp] ADD [RegistrationPaymentReceiptId] [int] NULL
 CONSTRAINT [PK_FollowUp] PRIMARY KEY CLUSTERED 
(
	[FollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FollowUp]  WITH CHECK ADD  CONSTRAINT [FK_FollowUp_FollowUp] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[FollowUp] CHECK CONSTRAINT [FK_FollowUp_FollowUp]
GO

ALTER TABLE [dbo].[FollowUp]  WITH CHECK ADD  CONSTRAINT [FK_FollowUp_FollowUp1] FOREIGN KEY([FollowUpId])
REFERENCES [dbo].[FollowUp] ([FollowUpId])
GO

ALTER TABLE [dbo].[FollowUp] CHECK CONSTRAINT [FK_FollowUp_FollowUp1]
GO

ALTER TABLE [dbo].[FollowUp]  WITH CHECK ADD  CONSTRAINT [FK_FollowUp_RegistrationPaymentReceipt] FOREIGN KEY([RegistrationPaymentReceiptId])
REFERENCES [dbo].[RegistrationPaymentReceipt] ([RegistrationPaymentReceiptId])
GO

ALTER TABLE [dbo].[FollowUp] CHECK CONSTRAINT [FK_FollowUp_RegistrationPaymentReceipt]
GO


