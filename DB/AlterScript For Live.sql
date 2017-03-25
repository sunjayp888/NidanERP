
-- Drop SchemeType Table
drop table SchemeType
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Delete SchemeTypeId Column From Enquiry Table
alter table Enquiry drop SchemeType
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Delete SchemeTypeId Column From Scheme Table
alter table Scheme drop SchemeType
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Add SchemeId Column To Sector Table
alter table Sector add [SchemeId] [int] NOT NULL
GO

ALTER TABLE [dbo].[Sector]  WITH CHECK ADD  CONSTRAINT [FK_Sector_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO

ALTER TABLE [dbo].[Sector] CHECK CONSTRAINT [FK_Sector_Scheme]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Add SchemeId Column To Course Table
alter table Course add [SchemeId] [int] NULL

ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO

ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Scheme]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Add Registered Column to Enquiry Table
alter table Enquiry add [Registered] [bit] NULL
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table PaymentMode
CREATE TABLE [dbo].[PaymentMode](
	[PaymentModeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_PaymentMode] PRIMARY KEY CLUSTERED 
(
	[PaymentModeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PaymentMode]  WITH CHECK ADD  CONSTRAINT [FK_PaymentMode_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[PaymentMode] CHECK CONSTRAINT [FK_PaymentMode_Organisation]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table RegistrationPaymentReceipt
CREATE TABLE [dbo].[RegistrationPaymentReceipt](
	[RegistrationPaymentReceiptId] [int] IDENTITY(1,1) NOT NULL,
	[CentreId] [int] NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Fees] [int] NOT NULL,
	[ChequeNo] [varchar](100) NOT NULL,
	[ChequeDate] [date] NOT NULL,
	[BankName] [varchar](500) NOT NULL,
	[Particulars] [varchar](500) NOT NULL,
	[PaymentModeId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[FollowUpDate] [date] NULL,
	[Remarks] [varchar](max) NULL,
 CONSTRAINT [PK_RegistrationPaymentReceipt] PRIMARY KEY CLUSTERED 
(
	[RegistrationPaymentReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Centre]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Course]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Enquiry]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Organisation]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_PaymentMode] FOREIGN KEY([PaymentModeId])
REFERENCES [dbo].[PaymentMode] ([PaymentModeId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_PaymentMode]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table Registration
CREATE TABLE [dbo].[Registration](
	[RegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationPaymentReceiptId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Registration]  WITH CHECK ADD  CONSTRAINT [FK_Registration_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Registration] CHECK CONSTRAINT [FK_Registration_Organisation]
GO

ALTER TABLE [dbo].[Registration]  WITH CHECK ADD  CONSTRAINT [FK_Registration_RegistrationPaymentReceipt] FOREIGN KEY([RegistrationPaymentReceiptId])
REFERENCES [dbo].[RegistrationPaymentReceipt] ([RegistrationPaymentReceiptId])
GO

ALTER TABLE [dbo].[Registration] CHECK CONSTRAINT [FK_Registration_RegistrationPaymentReceipt]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Add RegistrationPaymentReceiptId Column to FollowUp Table
alter table FollowUp add [RegistrationPaymentReceiptId] [int] NULL

ALTER TABLE [dbo].[FollowUp]  WITH CHECK ADD  CONSTRAINT [FK_FollowUp_RegistrationPaymentReceipt] FOREIGN KEY([RegistrationPaymentReceiptId])
REFERENCES [dbo].[RegistrationPaymentReceipt] ([RegistrationPaymentReceiptId])
GO

ALTER TABLE [dbo].[FollowUp] CHECK CONSTRAINT [FK_FollowUp_RegistrationPaymentReceipt]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table EventFunctionType
CREATE TABLE [dbo].[EventFunctionType](
	[EventFunctionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EventFunctionType] PRIMARY KEY CLUSTERED 
(
	[EventFunctionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventFunctionType]  WITH CHECK ADD  CONSTRAINT [FK_EventFunctionType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[EventFunctionType] CHECK CONSTRAINT [FK_EventFunctionType_Organisation]
GO

------------------------------------------------------------------------------------------------------------------------------------------

--Create Table Question
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[EventFunctionTypeId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_EventFunctionType] FOREIGN KEY([EventFunctionTypeId])
REFERENCES [dbo].[EventFunctionType] ([EventFunctionTypeId])
GO

ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_EventFunctionType]
GO

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Organisation]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table Brainstorming
CREATE TABLE [dbo].[Brainstorming](
	[BrainstormingId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Completed] [varchar](10) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Brainstorming] PRIMARY KEY CLUSTERED 
(
	[BrainstormingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Brainstorming]  WITH CHECK ADD  CONSTRAINT [FK_Brainstorming_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Brainstorming] CHECK CONSTRAINT [FK_Brainstorming_Centre]
GO

ALTER TABLE [dbo].[Brainstorming]  WITH CHECK ADD  CONSTRAINT [FK_Brainstorming_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[Brainstorming] CHECK CONSTRAINT [FK_Brainstorming_Event]
GO

ALTER TABLE [dbo].[Brainstorming]  WITH CHECK ADD  CONSTRAINT [FK_Brainstorming_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Brainstorming] CHECK CONSTRAINT [FK_Brainstorming_Organisation]
GO

ALTER TABLE [dbo].[Brainstorming]  WITH CHECK ADD  CONSTRAINT [FK_Brainstorming_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[Brainstorming] CHECK CONSTRAINT [FK_Brainstorming_Question]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table Planning
CREATE TABLE [dbo].[Planning](
	[PlanningId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Completed] [varchar](10) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Planning] PRIMARY KEY CLUSTERED 
(
	[PlanningId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Centre]
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Event]
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Organisation]
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Question]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table Budget
CREATE TABLE [dbo].[Budget](
	[BudgetId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Completed] [varchar](10) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Centre]
GO

ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Event]
GO

ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Organisation]
GO

ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Question]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table Eventday
CREATE TABLE [dbo].[Eventday](
	[EventdayId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Completed] [varchar](10) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Eventday] PRIMARY KEY CLUSTERED 
(
	[EventdayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Eventday]  WITH CHECK ADD  CONSTRAINT [FK_Eventday_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Eventday] CHECK CONSTRAINT [FK_Eventday_Centre]
GO

ALTER TABLE [dbo].[Eventday]  WITH CHECK ADD  CONSTRAINT [FK_Eventday_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[Eventday] CHECK CONSTRAINT [FK_Eventday_Event]
GO

ALTER TABLE [dbo].[Eventday]  WITH CHECK ADD  CONSTRAINT [FK_Eventday_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Eventday] CHECK CONSTRAINT [FK_Eventday_Organisation]
GO

ALTER TABLE [dbo].[Eventday]  WITH CHECK ADD  CONSTRAINT [FK_Eventday_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[Eventday] CHECK CONSTRAINT [FK_Eventday_Question]
GO

------------------------------------------------------------------------------------------------------------------------------------------

-- Create Table Postevent
CREATE TABLE [dbo].[Postevent](
	[PosteventId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Completed] [varchar](10) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Postevent] PRIMARY KEY CLUSTERED 
(
	[PosteventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Postevent]  WITH CHECK ADD  CONSTRAINT [FK_Postevent_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Postevent] CHECK CONSTRAINT [FK_Postevent_Centre]
GO

ALTER TABLE [dbo].[Postevent]  WITH CHECK ADD  CONSTRAINT [FK_Postevent_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[Postevent] CHECK CONSTRAINT [FK_Postevent_Event]
GO

ALTER TABLE [dbo].[Postevent]  WITH CHECK ADD  CONSTRAINT [FK_Postevent_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Postevent] CHECK CONSTRAINT [FK_Postevent_Organisation]
GO

ALTER TABLE [dbo].[Postevent]  WITH CHECK ADD  CONSTRAINT [FK_Postevent_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[Postevent] CHECK CONSTRAINT [FK_Postevent_Question]
GO

------------------------------------------------------------------------------------------------------------------------------------------

