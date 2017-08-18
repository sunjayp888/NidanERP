USE [master]
GO
/****** Object:  Database [ServerNidanERP]    Script Date: 18/08/2017 06:04:03 PM ******/
CREATE DATABASE [ServerNidanERP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ServerNidanERP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ServerNidanERP.mdf' , SIZE = 11264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ServerNidanERP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ServerNidanERP_log.ldf' , SIZE = 2304KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ServerNidanERP] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ServerNidanERP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ServerNidanERP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ServerNidanERP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ServerNidanERP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ServerNidanERP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ServerNidanERP] SET ARITHABORT OFF 
GO
ALTER DATABASE [ServerNidanERP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ServerNidanERP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ServerNidanERP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ServerNidanERP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ServerNidanERP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ServerNidanERP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ServerNidanERP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ServerNidanERP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ServerNidanERP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ServerNidanERP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ServerNidanERP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ServerNidanERP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ServerNidanERP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ServerNidanERP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ServerNidanERP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ServerNidanERP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ServerNidanERP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ServerNidanERP] SET RECOVERY FULL 
GO
ALTER DATABASE [ServerNidanERP] SET  MULTI_USER 
GO
ALTER DATABASE [ServerNidanERP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ServerNidanERP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ServerNidanERP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ServerNidanERP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ServerNidanERP] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ServerNidanERP', N'ON'
GO
USE [ServerNidanERP]
GO
/****** Object:  Table [dbo].[Absence]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absence](
	[AbsenceId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelAbsenceEntitlementId] [int] NOT NULL,
	[AbsenceTypeId] [int] NOT NULL,
	[AbsenceStatusId] [int] NOT NULL,
	[AbsenceStatusByUser] [nvarchar](128) NULL,
	[AbsenceStatusDateTimeUtc] [datetime2](7) NULL,
	[Description] [nvarchar](max) NULL,
	[ReturnToWorkCompleted] [bit] NULL,
	[DoctorsNoteSupplied] [bit] NULL,
 CONSTRAINT [PK_Absence] PRIMARY KEY CLUSTERED 
(
	[AbsenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbsenceDay]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsenceDay](
	[AbsenceDayId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[AbsenceId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[AM] [bit] NOT NULL CONSTRAINT [DF_AbsenceDay_AM]  DEFAULT ((0)),
	[PM] [bit] NOT NULL CONSTRAINT [DF_AbsenceDay_PM]  DEFAULT ((0)),
	[Duration] [float] NOT NULL,
 CONSTRAINT [PK_AbsenceDay] PRIMARY KEY CLUSTERED 
(
	[AbsenceDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbsencePeriod]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsencePeriod](
	[AbsencePeriodId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_AbsencePeriod] PRIMARY KEY CLUSTERED 
(
	[AbsencePeriodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbsencePolicy]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsencePolicy](
	[AbsencePolicyId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[WorkingPatternId] [int] NULL,
 CONSTRAINT [PK_AbsencePolicy] PRIMARY KEY CLUSTERED 
(
	[AbsencePolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbsencePolicyEntitlement]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsencePolicyEntitlement](
	[AbsencePolicyEntitlementId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[AbsenceTypeId] [int] NOT NULL,
	[FrequencyId] [int] NOT NULL,
	[Entitlement] [float] NOT NULL,
	[MaximumCarryForward] [float] NOT NULL,
	[IsUnplanned] [bit] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[AbsencePolicyId] [int] NOT NULL,
	[HasEntitlement] [bit] NOT NULL,
 CONSTRAINT [PK_AbsencePolicyEntitlement] PRIMARY KEY CLUSTERED 
(
	[AbsencePolicyEntitlementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbsencePolicyPeriod]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsencePolicyPeriod](
	[AbsencePolicyPeriodId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[AbsencePolicyId] [int] NOT NULL,
	[AbsencePeriodId] [int] NOT NULL,
 CONSTRAINT [PK_AbsencePolicyPeriod] PRIMARY KEY CLUSTERED 
(
	[AbsencePolicyPeriodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbsenceStatus]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AbsenceStatus](
	[AbsenceStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AbsenceStatus] PRIMARY KEY CLUSTERED 
(
	[AbsenceStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AbsenceType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsenceType](
	[AbsenceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ColourId] [int] NOT NULL,
 CONSTRAINT [PK_AbsenceType] PRIMARY KEY CLUSTERED 
(
	[AbsenceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Admission]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Admission](
	[AdmissionId] [int] IDENTITY(1,1) NOT NULL,
	[EnrollmentNumber] [varchar](500) NULL,
	[RegistrationId] [int] NOT NULL,
	[BatchId] [int] NULL,
	[CentreId] [int] NOT NULL,
	[AdmissionDate] [date] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NULL,
 CONSTRAINT [PK_Admission] PRIMARY KEY CLUSTERED 
(
	[AdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Alert]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alert](
	[AlertId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Alert] PRIMARY KEY CLUSTERED 
(
	[AlertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AreaOfInterest]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AreaOfInterest](
	[AreaOfInterestId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_AreaOfInterest] PRIMARY KEY CLUSTERED 
(
	[AreaOfInterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[OrganisationId] [int] NULL,
	[PersonnelId] [int] NULL,
	[CentreId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsersAlertSchedule]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsersAlertSchedule](
	[AspnetUsersAlertScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[AspNetUsersId] [nvarchar](128) NOT NULL,
	[AlertId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsersAlertSchedule] PRIMARY KEY CLUSTERED 
(
	[AspnetUsersAlertScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attendance](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[PersonnelId] [int] NULL,
	[StudentCode] [varchar](100) NULL,
	[IsPresent] [bit] NULL,
	[AttendanceDate] [date] NULL,
	[AttendanceDateTime] [datetime] NULL,
	[CentreId] [int] NULL,
	[OrganisationId] [int] NULL,
	[BioMetricLogTime] [varchar](50) NULL,
	[Direction] [varchar](2) NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Batch](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Intake] [int] NOT NULL,
	[CourseInstallmentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[BatchStartDate] [date] NOT NULL,
	[BatchEndDate] [date] NOT NULL,
	[NumberOfHolidays] [int] NOT NULL,
	[NumberOfHoursDaily] [int] NOT NULL,
	[BatchStartTimeHours] [int] NOT NULL,
	[BatchStartTimeMinutes] [int] NOT NULL,
	[BatchStartTimeSpan] [varchar](10) NOT NULL,
	[BatchEndTimeHours] [int] NOT NULL,
	[BatchEndTimeMinutes] [int] NOT NULL,
	[BatchEndTimeSpan] [varchar](10) NOT NULL,
	[AssessmentDate] [date] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[Remarks] [varchar](1000) NULL,
	[Month] [int] NOT NULL,
	[NumberOfInstallment] [int] NOT NULL,
	[FirstInstallment] [int] NULL,
	[SecondInstallment] [int] NULL,
	[ThirdInstallment] [int] NULL,
	[FourthInstallment] [int] NULL,
	[FifthInstallment] [int] NULL,
	[SixthInstallment] [int] NULL,
	[SeventhInstallment] [int] NULL,
	[EighthInstallment] [int] NULL,
	[NinethInstallment] [int] NULL,
	[TenthInstallment] [int] NULL,
	[EleventhInstallment] [int] NULL,
	[TwelvethInstallment] [int] NULL,
	[RoomId] [int] NULL,
 CONSTRAINT [PK_Batch] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BatchAttendance]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BatchAttendance](
	[BatchAttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[AttendanceId] [int] NOT NULL,
	[BatchTrainerId] [int] NOT NULL,
	[StudentCode] [varchar](100) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BatchAttendance] PRIMARY KEY CLUSTERED 
(
	[BatchAttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BatchDay]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchDay](
	[BatchDayId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[IsMonday] [bit] NOT NULL,
	[IsTuesday] [bit] NOT NULL,
	[IsWednesday] [bit] NOT NULL,
	[IsThursday] [bit] NOT NULL,
	[IsFriday] [bit] NOT NULL,
	[IsSaturday] [bit] NOT NULL,
	[IsSunday] [bit] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_BatchDay] PRIMARY KEY CLUSTERED 
(
	[BatchDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BatchTimePrefer]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BatchTimePrefer](
	[BatchTimePreferId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_BatchTimePrefer] PRIMARY KEY CLUSTERED 
(
	[BatchTimePreferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BatchTrainer]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchTrainer](
	[BatchTrainerId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_BatchTrainer] PRIMARY KEY CLUSTERED 
(
	[BatchTrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BiometricAttendance]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BiometricAttendance](
	[StudentCode] [nvarchar](50) NOT NULL,
	[LogDateTime] [datetime] NOT NULL,
	[Direction] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Brainstorming]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Brainstorming](
	[BrainstormingId] [int] IDENTITY(1,1) NOT NULL,
	[BeforePlanningAnswerDiscussTheseQuestion] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Brainstorming] PRIMARY KEY CLUSTERED 
(
	[BrainstormingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Budget]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Budget](
	[BudgetId] [int] IDENTITY(1,1) NOT NULL,
	[MajorGroup] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Building]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[BuildingId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[OrganisationId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[Address1] [nvarchar](100) NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CandidateFee]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CandidateFee](
	[CandidateFeeId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDate] [datetime] NULL,
	[CandidateInstallmentId] [int] NULL,
	[PaidAmount] [decimal](18, 2) NULL,
	[PaymentModeId] [int] NOT NULL,
	[FeeTypeId] [int] NOT NULL,
	[ChequeNumber] [varchar](50) NULL,
	[ChequeDate] [datetime] NULL,
	[BankName] [varchar](1000) NULL,
	[Penalty] [decimal](18, 2) NULL,
	[InstallmentDate] [datetime] NULL,
	[StudentCode] [varchar](50) NULL,
	[InstallmentNumber] [int] NULL,
	[InstallmentAmount] [decimal](18, 2) NULL,
	[BalanceInstallmentAmount] [decimal](18, 2) NULL,
	[Particulars] [varchar](max) NULL,
	[FiscalYear] [varchar](50) NULL,
	[FollowUpDate] [datetime] NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[IsPaymentDone] [bit] NOT NULL,
	[PersonnelId] [int] NULL,
	[IsPaidAmountOverride] [bit] NULL,
	[AdvancedAmount] [decimal](18, 2) NULL,
	[ReferenceReceiptNumber] [varchar](100) NULL,
	[HaveReceipt] [bit] NULL,
	[ReceiptNumber] [varchar](500) NULL,
 CONSTRAINT [PK_CandidateFee] PRIMARY KEY CLUSTERED 
(
	[CandidateFeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CandidateFeeDetail]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateFeeDetail](
	[CandidateFeeDetailId] [int] IDENTITY(1,1) NOT NULL,
	[CandidateInstallmentId] [int] NOT NULL,
	[AdmissionId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[TotalFee] [int] NOT NULL,
	[InstallmentNumber] [int] NOT NULL,
 CONSTRAINT [PK_CandidateFeeDetail] PRIMARY KEY CLUSTERED 
(
	[CandidateFeeDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CandidateInstallment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CandidateInstallment](
	[CandidateInstallmentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentCode] [varchar](50) NULL,
	[CourseFee] [decimal](18, 2) NULL,
	[DownPayment] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[NumberOfInstallment] [int] NULL,
	[LumpsumAmount] [decimal](18, 2) NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CourseInstallmentId] [int] NOT NULL,
	[IsPercentageDiscount] [bit] NOT NULL,
	[IsTotalAmountDiscount] [bit] NOT NULL,
	[PaymentMethod] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_CandidateInstallment] PRIMARY KEY CLUSTERED 
(
	[CandidateInstallmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CasteCategory]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CasteCategory](
	[CasteCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Caste] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CasteCategory] PRIMARY KEY CLUSTERED 
(
	[CasteCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Centre]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Centre](
	[CentreId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [varchar](100) NULL,
	[Name] [varchar](500) NOT NULL,
	[Address1] [varchar](500) NOT NULL,
	[Address2] [varchar](500) NULL,
	[Address3] [varchar](500) NULL,
	[Address4] [varchar](500) NULL,
	[TalukaId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[PinCode] [int] NOT NULL,
	[EmailId] [varchar](500) NOT NULL,
	[Telephone] [bigint] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Centre] PRIMARY KEY CLUSTERED 
(
	[CentreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CentreCourse]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentreCourse](
	[CentreCourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreCourse] PRIMARY KEY CLUSTERED 
(
	[CentreCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CentreCourseInstallment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentreCourseInstallment](
	[CentreCourseInstallmentId] [int] IDENTITY(1,1) NOT NULL,
	[CourseInstallmentId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreCourseInstallment] PRIMARY KEY CLUSTERED 
(
	[CentreCourseInstallmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CentreEnrollmentReceiptSetting]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CentreEnrollmentReceiptSetting](
	[CentreEnrollmentReceiptSettingId] [int] IDENTITY(1,1) NOT NULL,
	[EnrollmentNumber] [int] NOT NULL,
	[TaxYear] [varchar](50) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreEnrollmentReceiptSetting] PRIMARY KEY CLUSTERED 
(
	[CentreEnrollmentReceiptSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CentrePettyCash]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CentrePettyCash](
	[CentrePettyCashId] [int] IDENTITY(1,1) NOT NULL,
	[CentreId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Particulars] [varchar](500) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentrePettyCash] PRIMARY KEY CLUSTERED 
(
	[CentrePettyCashId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CentreReceiptSetting]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CentreReceiptSetting](
	[CentreReceiptSettingId] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptNumber] [int] NOT NULL,
	[TaxYear] [varchar](50) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreReceiptSetting] PRIMARY KEY CLUSTERED 
(
	[CentreReceiptSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CentreScheme]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentreScheme](
	[CentreSchemeId] [int] IDENTITY(1,1) NOT NULL,
	[SchemeId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreScheme] PRIMARY KEY CLUSTERED 
(
	[CentreSchemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CentreSector]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentreSector](
	[CentreSectorId] [int] IDENTITY(1,1) NOT NULL,
	[SectorId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreSector] PRIMARY KEY CLUSTERED 
(
	[CentreSectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CentreVoucherNumber]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentreVoucherNumber](
	[CentreVoucherNumberId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreVoucherNumber] PRIMARY KEY CLUSTERED 
(
	[CentreVoucherNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Colour]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Colour](
	[ColourId] [int] NOT NULL,
	[Hex] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ColourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[ColourId] [int] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CompanyBuilding]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyBuilding](
	[CompanyBuildingId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CompanyBuilding] PRIMARY KEY CLUSTERED 
(
	[CompanyBuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Counselling]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Counselling](
	[CounsellingId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[Title] [varchar](50) NULL,
	[FirstName] [varchar](100) NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[CourseOfferedId] [int] NOT NULL,
	[PreferTiming] [varchar](500) NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedDate] [date] NOT NULL,
	[FollowUpDate] [date] NULL,
	[RemarkByBranchManager] [varchar](max) NULL,
	[SectorId] [int] NULL,
	[PsychomatricTest] [varchar](100) NULL,
	[ConversionProspect] [int] NULL,
	[Close] [varchar](5) NULL,
	[ClosingRemark] [varchar](max) NULL,
	[RemarkByBm] [varchar](max) NULL,
	[IsRegistrationDone] [bit] NOT NULL,
 CONSTRAINT [PK_Counselling] PRIMARY KEY CLUSTERED 
(
	[CounsellingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CounsellingTest]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CounsellingTest](
	[CounsellingId] [float] NULL,
	[EnquiryId] [float] NULL,
	[CentreId] [float] NULL,
	[OrganisationId] [float] NULL,
	[PersonnelId] [float] NULL,
	[CourseOfferedId] [float] NULL,
	[PreferTiming] [nvarchar](255) NULL,
	[Remarks] [nvarchar](255) NULL,
	[FollowUpDate] [datetime] NULL,
	[RemarkByBranchManager] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[SectorId] [float] NULL,
	[PsychomatricTest] [nvarchar](255) NULL,
	[ConversionProspect] [float] NULL,
	[Close] [nvarchar](255) NULL,
	[ClosingRemark] [nvarchar](255) NULL,
	[RemarkByBm] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Course]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[SectorId] [int] NOT NULL,
	[SchemeId] [int] NULL,
	[CourseTypeId] [int] NULL,
	[Description] [varchar](1000) NULL,
	[Duration] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseInstallment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseInstallment](
	[CourseInstallmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[CourseId] [int] NOT NULL,
	[Fee] [int] NOT NULL,
	[DownPayment] [int] NOT NULL,
	[LumpsumAmount] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[NumberOfInstallment] [int] NULL,
	[CentreId] [int] NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_CourseInstallment] PRIMARY KEY CLUSTERED 
(
	[CourseInstallmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseSubject]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSubject](
	[CourseSubjectId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CourseSubject] PRIMARY KEY CLUSTERED 
(
	[CourseSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseType](
	[CourseTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[CourseTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CousellingTest]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CousellingTest](
	[CounsellingId] [float] NULL,
	[EnquiryId] [int] NULL,
	[CentreId] [float] NULL,
	[OrganisationId] [float] NULL,
	[PersonnelId] [float] NULL,
	[CourseOfferedId] [float] NULL,
	[PreferTiming] [nvarchar](255) NULL,
	[Remarks] [nvarchar](255) NULL,
	[FollowUpDate] [datetime] NULL,
	[RemarkByBranchManager] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[SectorId] [float] NULL,
	[PsychomatricTest] [nvarchar](255) NULL,
	[ConversionProspect] [float] NULL,
	[Close] [nvarchar](255) NULL,
	[ClosingRemark] [nvarchar](255) NULL,
	[RemarkByBm] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[ColourId] [int] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[District]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[District](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[StateId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Document]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentCode] [varchar](100) NOT NULL,
	[StudentName] [varchar](500) NULL,
	[FileName] [varchar](4000) NOT NULL,
	[Description] [varchar](1000) NULL,
	[Location] [varchar](max) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[DownloadedDateTime] [datetime] NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DocumentType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[BasePath] [varchar](1000) NOT NULL,
	[IsEnquiry] [bit] NOT NULL,
	[IsCounselling] [bit] NOT NULL,
	[IsAdmission] [bit] NOT NULL,
	[IsExpense] [bit] NOT NULL,
	[IsTrainer] [bit] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmergencyContact]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmergencyContact](
	[EmergencyContactId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[Relationship] [varchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Forenames] [nvarchar](100) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[CountryId] [int] NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Address4] [nvarchar](100) NULL,
	[Postcode] [nvarchar](12) NULL,
	[Telephone] [varchar](15) NOT NULL,
	[Mobile] [varchar](15) NULL,
 CONSTRAINT [PK_EmergencyContact] PRIMARY KEY CLUSTERED 
(
	[EmergencyContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employment](
	[EmploymentId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NULL,
	[TerminationDate] [datetime2](7) NULL,
	[BuildingId] [int] NOT NULL,
	[ReportsToPersonnelId] [int] NULL,
	[JobTitle] [nvarchar](100) NULL,
	[JobDescription] [nvarchar](max) NULL,
	[EndEmploymentReasonId] [int] NULL,
	[WorkingPatternId] [int] NULL,
	[PublicHolidayPolicyId] [int] NOT NULL,
	[AbsencePolicyId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Employment] PRIMARY KEY CLUSTERED 
(
	[EmploymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmploymentDepartment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentDepartment](
	[EmploymentDepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[EmploymentId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EmploymentDepartment] PRIMARY KEY CLUSTERED 
(
	[EmploymentDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmploymentTeam]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentTeam](
	[EmploymentTeamId] [int] IDENTITY(1,1) NOT NULL,
	[EmploymentId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EmploymentTeam] PRIMARY KEY CLUSTERED 
(
	[EmploymentTeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Enquiry]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Enquiry](
	[EnquiryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[AlternateMobile] [bigint] NULL,
	[EmailId] [varchar](500) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Age] [int] NOT NULL,
	[Address1] [varchar](500) NOT NULL,
	[Address2] [varchar](500) NULL,
	[Address3] [varchar](500) NULL,
	[Address4] [varchar](500) NULL,
	[TalukaId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[PinCode] [int] NOT NULL,
	[GuardianName] [varchar](500) NULL,
	[GuardianContactNo] [bigint] NULL,
	[OccupationId] [int] NOT NULL,
	[ReligionId] [int] NOT NULL,
	[CasteCategoryId] [int] NOT NULL,
	[Gender] [varchar](100) NOT NULL,
	[EducationalQualificationId] [int] NOT NULL,
	[YearOfPassOut] [varchar](100) NULL,
	[Marks] [varchar](100) NULL,
	[IntrestedCourseId] [int] NOT NULL,
	[HowDidYouKnowAboutId] [int] NOT NULL,
	[PreTrainingStatus] [varchar](100) NULL,
	[EmploymentStatus] [varchar](100) NULL,
	[Promotional] [varchar](100) NULL,
	[EnquiryDate] [date] NULL,
	[StudentCode] [varchar](100) NULL,
	[EnquiryStatus] [varchar](100) NULL,
	[EmployerName] [varchar](500) NULL,
	[EmployerContactNo] [varchar](50) NULL,
	[EmployerAddress] [varchar](max) NULL,
	[AnnualIncome] [int] NULL,
	[SchemeId] [int] NULL,
	[EnquiryTypeId] [int] NOT NULL,
	[StudentTypeId] [int] NOT NULL,
	[SectorId] [int] NOT NULL,
	[BatchTimePreferId] [int] NOT NULL,
	[AppearingQualification] [varchar](500) NULL,
	[YearOfExperience] [int] NULL,
	[PlacementNeeded] [varchar](100) NULL,
	[Remarks] [varchar](max) NULL,
	[FollowUpDate] [datetime] NULL,
	[PreferredMonthForJoining] [int] NULL,
	[Close] [varchar](5) NULL,
	[ClosingRemark] [varchar](max) NULL,
	[ConversionProspect] [int] NULL,
	[OtherInterestedCourse] [varchar](500) NULL,
	[RemarkByBranchManager] [varchar](max) NULL,
	[IsCounsellingDone] [bit] NOT NULL,
	[IsRegistrationDone] [bit] NOT NULL,
	[IsAdmissionDone] [bit] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Enquiry] PRIMARY KEY CLUSTERED 
(
	[EnquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EnquiryCourse]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnquiryCourse](
	[EnquiryCourseId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EnquiryCourse] PRIMARY KEY CLUSTERED 
(
	[EnquiryCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EnquiryTest]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnquiryTest](
	[EnquiryId] [float] NULL,
	[CandidateName] [nvarchar](255) NULL,
	[Mobile] [float] NULL,
	[AlternateMobile] [nvarchar](255) NULL,
	[EmailId] [nvarchar](255) NULL,
	[Age] [float] NULL,
	[Address] [nvarchar](255) NULL,
	[GuardianName] [nvarchar](255) NULL,
	[GuardianContactNo] [float] NULL,
	[OccupationId] [float] NULL,
	[ReligionId] [float] NULL,
	[CasteCategoryId] [float] NULL,
	[Gender] [nvarchar](255) NULL,
	[EducationalQualificationId] [float] NULL,
	[YearOfPassOut] [float] NULL,
	[Marks] [float] NULL,
	[IntrestedCourseId] [float] NULL,
	[HowDidYouKnowAboutId] [float] NULL,
	[PreTrainingStatus] [nvarchar](255) NULL,
	[EmploymentStatus] [nvarchar](255) NULL,
	[Promotional] [nvarchar](255) NULL,
	[EnquiryDate] [datetime] NULL,
	[CentreId] [float] NULL,
	[OrganisationId] [float] NULL,
	[StudentCode] [float] NULL,
	[EnquiryStatus] [nvarchar](255) NULL,
	[EmployerName] [nvarchar](255) NULL,
	[EmployerContactNo] [nvarchar](255) NULL,
	[EmployerAddress] [nvarchar](255) NULL,
	[AnnualIncome] [float] NULL,
	[SchemeId] [float] NULL,
	[EnquiryTypeId] [float] NULL,
	[StudentTypeId] [float] NULL,
	[SectorId] [float] NULL,
	[BatchTimePreferId] [float] NULL,
	[AppearingQualification] [nvarchar](255) NULL,
	[YearOfExperience] [float] NULL,
	[PlacementNeeded] [nvarchar](255) NULL,
	[Remarks] [nvarchar](255) NULL,
	[FollowUpDate] [datetime] NULL,
	[PreferredMonthForJoining] [float] NULL,
	[Close] [nvarchar](255) NULL,
	[ClosingRemark] [nvarchar](255) NULL,
	[ConversionProspect] [float] NULL,
	[OtherInterestedCourse] [nvarchar](255) NULL,
	[RemarkByBm] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EnquiryType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EnquiryType](
	[EnquiryTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EnquiryType] PRIMARY KEY CLUSTERED 
(
	[EnquiryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[EventStartDate] [date] NULL,
	[EventEndDate] [date] NULL,
	[ApprovedBy] [int] NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Event1] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventBrainstorming]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventBrainstorming](
	[EventBrainstormingId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[BrainstormingId] [int] NOT NULL,
	[DisscussionCompletedYesNo] [varchar](10) NOT NULL,
	[RefernceDetailDocument] [varchar](max) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EventBrainstorming] PRIMARY KEY CLUSTERED 
(
	[EventBrainstormingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventBudget]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventBudget](
	[EventBudgetId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[BudgetId] [int] NOT NULL,
	[SpecificHead] [varchar](500) NULL,
	[EstimatedQuantity] [decimal](18, 2) NULL,
	[EstimatedCostPerUnit] [decimal](18, 2) NULL,
	[EstimatedSubtotal] [nchar](10) NULL,
	[Notes] [nvarchar](max) NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EventBudget] PRIMARY KEY CLUSTERED 
(
	[EventBudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Eventday]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Eventday](
	[EventDayId] [int] IDENTITY(1,1) NOT NULL,
	[Activity] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Eventday] PRIMARY KEY CLUSTERED 
(
	[EventDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventFunctionType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[EventPlanning]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventPlanning](
	[EventPlanningId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[PlanningId] [int] NOT NULL,
	[Input] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EventPlanning] PRIMARY KEY CLUSTERED 
(
	[EventPlanningId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventPostEvent]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventPostEvent](
	[EventPostEventId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[PostEventId] [int] NOT NULL,
	[Completed] [varchar](50) NULL,
	[RefernceDetail] [varchar](max) NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_EventPostEvent] PRIMARY KEY CLUSTERED 
(
	[EventPostEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Expense](
	[ExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNumber] [varchar](500) NULL,
	[ExpenseHeaderId] [int] NOT NULL,
	[CashMemoNumbers] [varchar](500) NOT NULL,
	[DebitAmount] [decimal](18, 2) NOT NULL,
	[RupeesInWord] [varchar](500) NOT NULL,
	[PaidTo] [varchar](500) NOT NULL,
	[Particulars] [varchar](max) NOT NULL,
	[PaymentModeId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExpenseHeader]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExpenseHeader](
	[ExpenseHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_ExpenseHeader] PRIMARY KEY CLUSTERED 
(
	[ExpenseHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExpenseProject]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseProject](
	[ExpenseProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_ExpenseProject] PRIMARY KEY CLUSTERED 
(
	[ExpenseProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeeType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FeeType](
	[FeeTypeId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FeeType] PRIMARY KEY CLUSTERED 
(
	[FeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FollowUp]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FollowUp](
	[FollowUpId] [int] IDENTITY(1,1) NOT NULL,
	[FollowUpDateTime] [datetime] NULL CONSTRAINT [DF_FollowUp_FollowUpDateTime]  DEFAULT (((1900)-(1))-(1)),
	[MobilizationId] [int] NULL,
	[EnquiryId] [int] NULL,
	[CounsellingId] [int] NULL,
	[RegistrationId] [int] NULL,
	[Title] [varchar](50) NULL,
	[FirstName] [varchar](100) NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Remark] [nvarchar](max) NULL,
	[Closed] [bit] NULL,
	[ReadDateTime] [datetime] NOT NULL CONSTRAINT [DF_FollowUp_ReadDateTime]  DEFAULT (((1900)-(1))-(1)),
	[CreatedDateTime] [datetime] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[Mobile] [bigint] NULL,
	[IntrestedCourseId] [int] NOT NULL,
	[FollowUpType] [varchar](200) NULL,
	[AlternateMobile] [bigint] NULL,
	[FollowUpURL] [varchar](2000) NULL,
	[Close] [varchar](5) NULL,
	[ClosingRemark] [varchar](max) NULL,
	[AdmissionId] [int] NULL,
 CONSTRAINT [PK_FollowUp] PRIMARY KEY CLUSTERED 
(
	[FollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FollowUpHistory]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FollowUpHistory](
	[FollowUpHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[FollowUpId] [int] NOT NULL,
	[FollowUpType] [varchar](200) NOT NULL,
	[Remarks] [varchar](max) NULL,
	[Close] [varchar](5) NULL,
	[ClosingRemarks] [varchar](max) NULL,
	[FollowUpDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_FollowUpHistory] PRIMARY KEY CLUSTERED 
(
	[FollowUpHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FollowUpSheet]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowUpSheet](
	[FollowUpId] [float] NULL,
	[FollowUpDateTime] [datetime] NULL,
	[MobilizationId] [float] NULL,
	[EnquiryId] [nvarchar](255) NULL,
	[Remark] [nvarchar](255) NULL,
	[Closed] [nvarchar](255) NULL,
	[ReadDateTime] [datetime] NULL,
	[CreatedDateTime] [datetime] NULL,
	[OrganisationId] [float] NULL,
	[CentreId] [float] NULL,
	[Name] [nvarchar](255) NULL,
	[Mobile] [float] NULL,
	[IntrestedCourseId] [float] NULL,
	[FollowUpType] [nvarchar](255) NULL,
	[AlternateMobile] [nvarchar](255) NULL,
	[FollowUpURL] [nvarchar](255) NULL,
	[CounsellingId] [nvarchar](255) NULL,
	[Close] [nvarchar](255) NULL,
	[ClosingRemark] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FollowUpTest]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowUpTest](
	[FollowUpId] [float] NULL,
	[FollowUpDateTime] [datetime] NULL,
	[MobilizationId] [float] NULL,
	[EnquiryId] [nvarchar](255) NULL,
	[Remark] [nvarchar](255) NULL,
	[Closed] [nvarchar](255) NULL,
	[ReadDateTime] [datetime] NULL,
	[CreatedDateTime] [datetime] NULL,
	[OrganisationId] [float] NULL,
	[CentreId] [float] NULL,
	[Name] [nvarchar](255) NULL,
	[Mobile] [float] NULL,
	[IntrestedCourseId] [float] NULL,
	[FollowUpType] [nvarchar](255) NULL,
	[AlternateMobile] [nvarchar](255) NULL,
	[FollowUpURL] [nvarchar](255) NULL,
	[CounsellingId] [nvarchar](255) NULL,
	[Close] [nvarchar](255) NULL,
	[ClosingRemark] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Frequency]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Frequency](
	[FrequencyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Periods] [int] NOT NULL,
 CONSTRAINT [PK_Frequency] PRIMARY KEY CLUSTERED 
(
	[FrequencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gst]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gst](
	[GstId] [int] IDENTITY(1,1) NOT NULL,
	[GstNumber] [varchar](50) NOT NULL,
	[StateId] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_StateGst] PRIMARY KEY CLUSTERED 
(
	[GstId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Holiday]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Holiday](
	[HolidayId] [int] IDENTITY(1,1) NOT NULL,
	[HolidayDate] [date] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[WeekDay] [varchar](100) NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
(
	[HolidayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Host]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Host](
	[HostId] [smallint] IDENTITY(1,1) NOT NULL,
	[HostName] [nvarchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Host] PRIMARY KEY CLUSTERED 
(
	[HostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HowDidYouKnowAbout]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HowDidYouKnowAbout](
	[HowDidYouKnowAboutId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_HowDidYouKnowAboutUs] PRIMARY KEY CLUSTERED 
(
	[HowDidYouKnowAboutId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mobilization]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mobilization](
	[MobilizationId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NOT NULL,
	[CentreId] [int] NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[AlternateMobile] [bigint] NULL,
	[InterestedCourseId] [int] NOT NULL,
	[QualificationId] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[FollowUpDate] [date] NULL,
	[Remark] [varchar](max) NULL,
	[MobilizerStatus] [varchar](100) NULL,
	[StudentLocation] [varchar](500) NULL,
	[OtherInterestedCourse] [varchar](1000) NULL,
	[GeneratedDate] [date] NOT NULL,
	[MobilizationTypeId] [int] NOT NULL,
	[PersonnelId] [int] NULL,
	[Close] [varchar](5) NULL,
	[ClosingRemark] [varchar](max) NULL,
 CONSTRAINT [PK_Mobilization] PRIMARY KEY CLUSTERED 
(
	[MobilizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MobilizationType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MobilizationType](
	[MobilizationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_MobilizationType] PRIMARY KEY CLUSTERED 
(
	[MobilizationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MobiTest]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MobiTest](
	[MobilizationId] [int] NULL,
	[EventId] [float] NULL,
	[OrganisationId] [float] NULL,
	[CentreId] [float] NULL,
	[Name] [nvarchar](255) NULL,
	[Mobile] [float] NULL,
	[AlternateMobile] [nvarchar](255) NULL,
	[InterestedCourseId] [float] NULL,
	[QualificationId] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[FollowUpDate] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[MobilizerStatus] [nvarchar](255) NULL,
	[StudentLocation] [nvarchar](255) NULL,
	[OtherInterestedCourse] [nvarchar](255) NULL,
	[GeneratedDate] [datetime] NULL,
	[MobilizationTypeId] [float] NULL,
	[PersonnelId] [float] NULL,
	[Close] [nvarchar](255) NULL,
	[ClosingRemark] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Occupation]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Occupation](
	[OccupationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Occupation] PRIMARY KEY CLUSTERED 
(
	[OccupationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[OrganisationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Organisation_1] PRIMARY KEY CLUSTERED 
(
	[OrganisationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OtherFee]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OtherFee](
	[OtherFeeId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ProjectId] [int] NULL,
	[ExpenseHeaderId] [int] NOT NULL,
	[CashMemo] [varchar](500) NOT NULL,
	[Unit] [int] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[Description] [varchar](max) NULL,
	[DebitAmount] [decimal](18, 2) NOT NULL,
	[RupeesInWord] [varchar](max) NOT NULL,
	[PaidTo] [varchar](500) NOT NULL,
	[Particulars] [varchar](max) NOT NULL,
	[PaymentModeId] [int] NOT NULL,
	[PrintDate] [datetime] NULL,
	[Approved] [bit] NULL,
	[ApprovedBy] [int] NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
 CONSTRAINT [PK_OtherFee] PRIMARY KEY CLUSTERED 
(
	[OtherFeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentMode]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[Personnel]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personnel](
	[PersonnelId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Forenames] [nvarchar](100) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Address4] [nvarchar](100) NULL,
	[Postcode] [nvarchar](12) NOT NULL,
	[Telephone] [varchar](15) NULL,
	[Mobile] [varchar](15) NULL,
	[NINumber] [varchar](10) NULL,
	[BankAccountNumber] [varchar](10) NULL,
	[BankSortCode] [varchar](6) NULL,
	[BankAccountName] [nvarchar](100) NULL,
	[BankAddress1] [nvarchar](100) NULL,
	[BankAddress2] [nvarchar](100) NULL,
	[BankAddress3] [nvarchar](100) NULL,
	[BankAddress4] [nvarchar](100) NULL,
	[BankPostcode] [nvarchar](12) NULL,
	[BankTelephone] [varchar](15) NULL,
	[Email] [varchar](256) NOT NULL,
	[CurrentEmploymentId] [int] NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Personnel] PRIMARY KEY CLUSTERED 
(
	[PersonnelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonnelAbsenceEntitlement]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonnelAbsenceEntitlement](
	[PersonnelAbsenceEntitlementId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[AbsencePolicyPeriodId] [int] NOT NULL,
	[AbsenceTypeId] [int] NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Entitlement] [float] NULL,
	[CarriedOver] [float] NULL,
	[Used] [float] NULL,
	[Remaining] [float] NULL,
	[MaximumCarryForward] [float] NULL,
	[FrequencyId] [int] NOT NULL,
	[EmploymentId] [int] NOT NULL,
 CONSTRAINT [PK_PersonnelAbsenceEntitlement] PRIMARY KEY CLUSTERED 
(
	[PersonnelAbsenceEntitlementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Planning]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Planning](
	[PlanningId] [int] IDENTITY(1,1) NOT NULL,
	[MajorPoint] [varchar](500) NOT NULL,
	[Point] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Planning] PRIMARY KEY CLUSTERED 
(
	[PlanningId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PostEvent]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PostEvent](
	[PostEventId] [int] IDENTITY(1,1) NOT NULL,
	[Activity] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_Postevent] PRIMARY KEY CLUSTERED 
(
	[PostEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CentreId] [int] NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PublicHoliday]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublicHoliday](
	[PublicHolidayId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PublicHolidayPolicyId] [int] NOT NULL,
 CONSTRAINT [PK_PublicHoliday] PRIMARY KEY CLUSTERED 
(
	[PublicHolidayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PublicHolidayPolicy]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublicHolidayPolicy](
	[PublicHolidayPolicyId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PublicHolidayPolicy] PRIMARY KEY CLUSTERED 
(
	[PublicHolidayPolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Qualification]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Qualification](
	[QualificationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Qualification] PRIMARY KEY CLUSTERED 
(
	[QualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Question]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Registration]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Registration](
	[RegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[StudentCode] [varchar](50) NULL,
	[EnquiryId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[CourseInstallmentId] [int] NOT NULL,
	[CandidateFeeId] [int] NOT NULL,
	[CandidateInstallmentId] [int] NOT NULL,
	[Remarks] [varchar](max) NULL,
	[FollowupDate] [datetime] NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[IsAdmissionDone] [bit] NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrationPaymentReceipt]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrationPaymentReceipt](
	[RegistrationPaymentReceiptId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CounsellingId] [int] NOT NULL,
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
	[FinancialYear] [varchar](100) NOT NULL,
 CONSTRAINT [PK_RegistrationPaymentReceipt] PRIMARY KEY CLUSTERED 
(
	[RegistrationPaymentReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Religion]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Religion](
	[ReligionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Religion] PRIMARY KEY CLUSTERED 
(
	[ReligionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Room]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[Floor] [int] NOT NULL,
	[OccupiedStartDate] [date] NULL,
	[OccupiedEndDate] [date] NULL,
	[OccupiedStartTime] [varchar](50) NULL,
	[OccupiedEndTime] [varchar](50) NULL,
	[RoomTypeId] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[SquareFeet] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomAvailable]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomAvailable](
	[RoomAvailableId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[Day] [varchar](10) NOT NULL,
	[StartTimeHours] [int] NOT NULL,
	[StartTimeMinutes] [int] NOT NULL,
	[StartTimeSpan] [varchar](10) NOT NULL,
	[EndTimeHours] [int] NOT NULL,
	[EndTimeMinutes] [int] NOT NULL,
	[EndTimeSpan] [varchar](10) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[RoomTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Scheme]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Scheme](
	[SchemeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Scheme] PRIMARY KEY CLUSTERED 
(
	[SchemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sector]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sector](
	[SectorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[SchemeId] [int] NOT NULL,
 CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
(
	[SectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Session]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Duration] [varchar](500) NOT NULL,
	[CourseTypeId] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Site]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[SiteId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CountryId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[State]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[GstStateCode] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentType]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentType](
	[StudentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_StudentType] PRIMARY KEY CLUSTERED 
(
	[StudentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[CourseId] [int] NULL,
	[TrainerId] [int] NULL,
	[CourseTypeId] [int] NOT NULL,
	[TotalMarks] [int] NOT NULL,
	[PassingMarks] [int] NOT NULL,
	[NoOfAttemptsAllowed] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubjectCourse]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectCourse](
	[SubjectCourseId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_SubjectCourse] PRIMARY KEY CLUSTERED 
(
	[SubjectCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubjectTrainer]    Script Date: 18/08/2017 06:04:03 PM ******/
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
/****** Object:  Table [dbo].[Taluka]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Taluka](
	[TalukaId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DistrictId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Taluka] PRIMARY KEY CLUSTERED 
(
	[TalukaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Team]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[TeamId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[ColourId] [int] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Template]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Template](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Template] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trainer](
	[TrainerId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Address1] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NULL,
	[Address3] [varchar](100) NULL,
	[Address4] [varchar](100) NULL,
	[TalukaId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[PinCode] [nvarchar](12) NOT NULL,
	[Gender] [varchar](100) NULL,
	[AadharNo] [bigint] NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[EmailId] [varchar](500) NOT NULL,
	[Certified] [varchar](100) NULL,
	[CertificationNo] [varchar](500) NULL,
	[SectorId] [int] NOT NULL,
	[CourseId] [int] NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_Trainer] PRIMARY KEY CLUSTERED 
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrainerAvailable]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrainerAvailable](
	[TrainerAvailableId] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[Day] [varchar](10) NOT NULL,
	[StartTimeHours] [int] NOT NULL,
	[StartTimeMinutes] [int] NOT NULL,
	[StartTimeSpan] [varchar](10) NOT NULL,
	[EndTimeHours] [int] NOT NULL,
	[EndTimeMinutes] [int] NOT NULL,
	[EndTimeSpan] [varchar](10) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_TrainerAvailable] PRIMARY KEY CLUSTERED 
(
	[TrainerAvailableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrainerDocument]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrainerDocument](
	[TrainerDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NOT NULL,
	[TrainerName] [varchar](500) NULL,
	[FileName] [varchar](4000) NOT NULL,
	[Description] [varchar](1000) NULL,
	[Location] [varchar](max) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[DownloadedDateTime] [datetime] NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NULL,
 CONSTRAINT [PK_TrainerDocument] PRIMARY KEY CLUSTERED 
(
	[TrainerDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Voucher](
	[VoucherId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNumber] [varchar](100) NULL,
	[CashMemo] [varchar](500) NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkingPattern]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingPattern](
	[WorkingPatternId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_WorkingPattern] PRIMARY KEY CLUSTERED 
(
	[WorkingPatternId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkingPatternDay]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingPatternDay](
	[WorkingPatternDayId] [int] IDENTITY(1,1) NOT NULL,
	[WorkingPatternId] [int] NOT NULL,
	[DayOfWeek] [smallint] NOT NULL,
	[AM] [bit] NOT NULL,
	[PM] [bit] NOT NULL,
 CONSTRAINT [PK_WorkingPatternDay] PRIMARY KEY CLUSTERED 
(
	[WorkingPatternDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[MobilizationCentreReport]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create view [dbo].[MobilizationCentreReport]
as
Select 
[Date],
CentreId = T.CentreId,
CentreName = C.Name, 
MobilizationCount = Count(M.MobilizationId),
EnquiryCount = (case when E.EnquiryCount IS NULL THEN 0 ELSE E.EnquiryCount end),
CounsellingCount = (case when Ce.CounsellingCount IS NULL THEN 0 ELSE Ce.CounsellingCount end),
RegistrationCount = (case when R.RegistrationCount IS NULL THEN 0 ELSE R.RegistrationCount end),
AdmissionCount = (case when A.AdmissionCount IS NULL THEN 0 ELSE A.AdmissionCount end),
CourseBooking = (case when CI.CourseBooking IS NULL THEN 0 ELSE CI.CourseBooking end),
FeeCollected = (case when CF.FeeCollected IS NULL THEN 0 ELSE CF.FeeCollected end)
From
(
select 
[Date],
temp.CentreId
From
(
	select GeneratedDate[Date],CentreId from Mobilization
	union all
	select Enquirydate,CentreId  from Enquiry
	union all
	select CreatedDate,CentreId  from Counselling
	union all 
	select cast( RegistrationDate as date),CentreId   from Registration
	union all
	select AdmissionDate,CentreId  from Admission
	union all
	select CreatedDate,CentreId  from [CandidateInstallment]
	union all
	select cast( PaymentDate as date),CentreId  from [CandidateFee] where PaymentDate is not null
) temp group by [date],temp.CentreId) T 
Left Join Mobilization M on T.date = M.Generateddate and T.CentreId = M.CentreId
Left Join (select count(*)EnquiryCount,CentreId, EnquiryDate from enquiry group by EnquiryDate,CentreId) E on T.date = E.EnquiryDate and T.CentreId = E.CentreId
Left Join (select count(*)CounsellingCount,CentreId, CreatedDate from Counselling group by CreatedDate,CentreId) Ce on T.date = Ce.CreatedDate and T.CentreId = Ce.CentreId
Left Join (select count(*)RegistrationCount,CentreId,cast(RegistrationDate as date)RegistrationDate from Registration group by cast( RegistrationDate as date),CentreId) R on T.date = R.RegistrationDate and T.CentreId = R.CentreId
Left Join (select count(*)AdmissionCount,CentreId,AdmissionDate from Admission group by AdmissionDate,CentreId) A on T.date = A.AdmissionDate and T.CentreId = A.CentreId
Left Join (select Sum(CourseFee)CourseBooking,CentreId,CreatedDate from CandidateInstallment group by CreatedDate,CentreId) CI on T.date = CI.CreatedDate and T.CentreId = CI.CentreId
Left Join (select Sum(PaidAmount)FeeCollected,CentreId,cast(PaymentDate as date)PaymentDate from CandidateFee where PaymentDate IS NOT NULL and IsPaymentDone = 1 group by cast(PaymentDate as date),CentreId) CF on T.date = CF.PaymentDate and T.CentreId = CF.CentreId
left join Centre C on C.CentreId = T.CentreId

group by T.Date,T.CentreId,EnquiryCount,CounsellingCount,RegistrationCount,AdmissionCount,CourseBooking,FeeCollected,C.Name







GO
/****** Object:  View [dbo].[MobilizationCentreReportMonthWise]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[MobilizationCentreReportMonthWise]
as
select 
DATEPART(month, [Date])[Month],
DATEPART(YEAR, [Date])[Year],
[MonthName]=DATENAME(month, [Date]), 
CentreId,
CentreName,
MobilizationCount=SUM(MobilizationCount),
EnquiryCount=SUM(EnquiryCount),
CounsellingCount=SUM(CounsellingCount),
RegistrationCount=SUM(RegistrationCount),
AdmissionCount=SUM(AdmissionCount),
CourseBooking=SUM(CourseBooking),
FeeCollected=SUM(FeeCollected)

from [MobilizationCentreReport]

group by DATEPART(month, [Date]),
DATEPART(YEAR, [date]),
CentreId,
CentreName,
DATENAME(month, [Date])
GO
/****** Object:  View [dbo].[AdmissionGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[AdmissionGrid]
AS 
Select
CandidateName = E.Title+' '+E.FirstName+' '+Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end+' '+E.Lastname,
BatchName=B.Name,
TotalFee=Case when CI.PaymentMethod='MonthlyInstallment' then CI.CourseFee else CI.LumpsumAmount end,
PaidAmount=sum(CF.PaidAmount),
PendingAmount = Case when CI.PaymentMethod='MonthlyInstallment' then CI.CourseFee - sum(PaidAmount) else CI.LumpsumAmount - sum(PaidAmount) end,
A.AdmissionDate,
A.AdmissionId,
CentreName=C.Name,
C.CentreId
from Admission A
INNER JOIN REGISTRATION R ON R.RegistrationId =  A.RegistrationId
INNER JOIN ENQUIRY E ON E.EnquiryId = R.EnquiryId
left JOIN BATCH B ON B.BatchId = A.BatchId
INNER JOIN CandidateInstallment CI on CI.StudentCode = E.StudentCode
INNER JOIN CandidateFee CF on CF.CandidateInstallmentId = CI.CandidateInstallmentId
INNER JOIN Centre C on C.CentreId = A.CentreId
group by
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
B.Name,
CI.CourseFee,
A.AdmissionDate,
C.CentreId,
C.Name,
A.AdmissionId,
CI.PaymentMethod,
CI.LumpsumAmount







GO
/****** Object:  View [dbo].[AdmissionSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[AdmissionSearchField]
AS 
SELECT 

	 CandidateName = E.Title+' '+E.FirstName+' '+E.MiddleName+' '+E.Lastname,
BatchName=B.Name,
TotalFee=CI.CourseFee,
PaidAmount=sum(CF.PaidAmount),
PendingAmount = CI.CourseFee - sum(CF.PaidAmount),
A.AdmissionDate,
A.AdmissionId,
CentreName=C.Name,
C.CentreId,

 ISNULL(E.Title, '')+ISNULL(E.FirstName, '')+ISNULL(E.MiddleName, '')+ISNULL(E.LastName, '')
	  +ISNULL(C.Name, '')+ISNULL(B.Name, '')
	  + ISNULL(CONVERT(varchar,AdmissionDate, 101), '') 
	  + ISNULL(CONVERT(varchar,AdmissionDate, 103), '') 
	  + ISNULL(CONVERT(varchar,AdmissionDate, 105), '') 
	  + ISNULL(CONVERT(varchar,AdmissionDate, 126), '') AS SearchField

from Admission A
INNER JOIN REGISTRATION R ON R.RegistrationId =  A.RegistrationId
INNER JOIN ENQUIRY E ON E.EnquiryId = R.EnquiryId
INNER JOIN BATCH B ON B.BatchId = A.BatchId
INNER JOIN CandidateInstallment CI on CI.StudentCode = E.StudentCode
INNER JOIN CandidateFee CF on CF.CandidateInstallmentId = CI.CandidateInstallmentId
INNER JOIN Centre C on C.CentreId = A.CentreId
group by
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
B.Name,
CI.CourseFee,
A.AdmissionDate,
C.CentreId,
C.Name,
A.AdmissionId

  



GO
/****** Object:  View [dbo].[AttendanceGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE VIEW [dbo].[AttendanceGrid]
AS 
Select
StudentCode=E.StudentCode,
A.PersonnelId,
CandidateName = E.Title+' '+E.FirstName+' '+Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end+' '+E.Lastname,
B.BatchId,
AT.BioMetricLogTime,
AT.AttendanceDate,
AT.IsPresent,
AT.Direction,
CentreName=C.Name,
C.CentreId
from Admission A 
INNER JOIN Batch B on A.BatchId=b.BatchId
INNER JOIN Registration R on R.RegistrationId=A.RegistrationId
INNER JOIN Enquiry E on E.StudentCode=R.StudentCode
left JOIN Attendance AT on AT.StudentCode=E.StudentCode
left JOIN BatchAttendance BA on BA.AttendanceId=AT.AttendanceId
INNER JOIN Centre C on C.CentreId = A.CentreId


group by
E.StudentCode,
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
B.BatchId,
C.CentreId,
C.Name,
AT.AttendanceDate,
AT.IsPresent,
AT.BioMetricLogTime,
AT.AttendanceDate,
AT.IsPresent,
AT.Direction,
A.PersonnelId





GO
/****** Object:  View [dbo].[BiometricAttendanceGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE VIEW [dbo].[BiometricAttendanceGrid]
AS 

select
B.StudentCode,
B.LogDateTime,
CandidateName = E.Title+' '+E.FirstName+' '+Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end+' '+E.Lastname,
B.Direction,
R.CentreId,
R.OrganisationId
from 
BiometricAttendance B
Right join Registration R on R.StudentCode=B.StudentCode
INNER JOIN ENQUIRY E ON E.EnquiryId = R.EnquiryId




GO
/****** Object:  View [dbo].[CandidateFeeGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CandidateFeeGrid]
AS 
Select
CandidateFeeId,
PaymentDate,
candidatefee. CandidateInstallmentId,
PaidAmount,
PaymentMode = PM.Name,
FeeType = case  When InstallmentNumber is null then FT.Name else CONVERT(varchar(100), FT.Name +'-'+ convert(varchar(100), ISNULL(InstallmentNumber,''))) end,
ChequeNumber,
ChequeDate,
BankName,
Penalty,
InstallmentDate,
StudentCode,
InstallmentNumber,
InstallmentAmount,
BalanceInstallmentAmount,
Particulars,
FiscalYear,
FollowUpDate,
CentreId,
Organisation=O.Name,
IsPaymentDone,
PersonnelId,
IsPaidAmountOverride,
AdvancedAmount,
CF.NextInstallmentDate
from [dbo].[CandidateFee] candidatefee



Left join (
select  NextInstallmentDate = min(installmentdate),CandidateInstallmentId  
from [dbo].[CandidateFee]
where IsPaymentDone = 0 group by CandidateInstallmentId 
) CF on candidatefee.CandidateInstallmentId = CF.CandidateInstallmentId 
inner join FeeType FT on FT.FeeTypeId=candidatefee.FeeTypeId
join PaymentMode PM on PM.PaymentModeId=candidatefee.PaymentModeId
join Organisation O on O.OrganisationId=candidatefee.OrganisationId





GO
/****** Object:  View [dbo].[CandidateFeeSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[CandidateFeeSearchField]
AS 


	  Select
C.CandidateInstallmentId,
CandidateName = E.Title+' '+E.FirstName+' '+Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end+' '+E.Lastname,
BatchName=B.Name,
TotalFee=CI.CourseFee,
PaidAmount=sum(PaidAmount),
PendingAmount = CI.CourseFee - sum(PaidAmount),
A.AdmissionDate,
A.AdmissionId,
CentreName=CE.Name,
CE.CentreId,

 ISNULL(E.Title, '')+ISNULL(E.FirstName, '')+ISNULL(E.MiddleName, '')+ISNULL(E.LastName, '')
	  AS SearchField

from CandidateFee C
INNER JOIN CandidateInstallment CI ON CI.CandidateInstallmentId =  C.CandidateInstallmentId
INNER JOIN Enquiry E ON E.StudentCode = CI.StudentCode
INNER JOIN Registration R ON R.StudentCode = CI.StudentCode
INNER JOIN Admission A ON A.RegistrationId = R.RegistrationId
INNER JOIN BATCH B ON B.BatchId = A.BatchId
INNER JOIN Centre CE on CE.CentreId = A.CentreId
group by
C.CandidateInstallmentId,
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
B.Name,
CI.CourseFee,
A.AdmissionDate,
CE.CentreId,
CE.Name,
A.AdmissionId

     





GO
/****** Object:  View [dbo].[CandidateInstallmentGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[CandidateInstallmentGrid]
AS 
Select
C.CandidateInstallmentId,
CandidateName = E.Title+' '+E.FirstName+' '+ Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end +' '+E.Lastname,
BatchName=B.Name,
TotalFee=Case when CI.PaymentMethod='MonthlyInstallment' then CI.CourseFee else CI.LumpsumAmount end,
PaidAmount=sum(PaidAmount),
PendingAmount = Case when CI.PaymentMethod='MonthlyInstallment' then CI.CourseFee - sum(PaidAmount) else CI.LumpsumAmount - sum(PaidAmount) end,
A.AdmissionDate,
A.AdmissionId,
CentreName=CE.Name,
CE.CentreId

from CandidateFee C
INNER JOIN CandidateInstallment CI ON CI.CandidateInstallmentId =  C.CandidateInstallmentId
INNER JOIN Enquiry E ON E.StudentCode = CI.StudentCode
INNER JOIN Registration R ON R.StudentCode = CI.StudentCode
INNER JOIN Admission A ON A.RegistrationId = R.RegistrationId
left JOIN BATCH B ON B.BatchId = A.BatchId
INNER JOIN Centre CE on CE.CentreId = A.CentreId
group by
C.CandidateInstallmentId,
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
B.Name,
CI.CourseFee,
A.AdmissionDate,
CE.CentreId,
CE.Name,
A.AdmissionId,
CI.PaymentMethod,
CI.LumpsumAmount





GO
/****** Object:  View [dbo].[CandidateInstallmentSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[CandidateInstallmentSearchField]
AS 
Select
C.CandidateInstallmentId,
CandidateName = E.Title+' '+E.FirstName+' '+Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end+' '+E.Lastname,
BatchName=B.Name,
TotalFee=CI.CourseFee,
PaidAmount=sum(PaidAmount),
PendingAmount = CI.CourseFee - sum(PaidAmount),
A.AdmissionDate,
A.AdmissionId,
CentreName=CE.Name,
CE.CentreId,

 ISNULL(E.Title, '')+ISNULL(E.FirstName, '')+ISNULL(E.MiddleName, '')+ISNULL(E.LastName, '')
	  AS SearchField

from CandidateFee C
INNER JOIN CandidateInstallment CI ON CI.CandidateInstallmentId =  C.CandidateInstallmentId
INNER JOIN Enquiry E ON E.StudentCode = CI.StudentCode
INNER JOIN Registration R ON R.StudentCode = CI.StudentCode
INNER JOIN Admission A ON A.RegistrationId = R.RegistrationId
INNER JOIN BATCH B ON B.BatchId = A.BatchId
INNER JOIN Centre CE on CE.CentreId = A.CentreId
group by
C.CandidateInstallmentId,
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
B.Name,
CI.CourseFee,
A.AdmissionDate,
CE.CentreId,
CE.Name,
A.AdmissionId

     






GO
/****** Object:  View [dbo].[CounsellingDataGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[CounsellingDataGrid]
AS 

SELECT
      C.CounsellingId,
      C.EnquiryId,
      CandidateName=C.Title+' '+C.FirstName+' '+Case when C.MiddleName IS NULL  then ' ' else C.MiddleName end+' '+C.LastName,
      C.CentreId,
	  CentreName=CE.Name,
      CounselledBy=P.Title + ' ' + P.Forenames + ' ' + P.Surname,
      CourseOffered=CO.Name,
      C.PreferTiming ,
      C.Remarks,
      C.CreatedDate,
      C.FollowUpDate,
      C.RemarkByBranchManager,
      C.PsychomatricTest,
      C.ConversionProspect,
      C.[Close],
      C.ClosingRemark,
      C.RemarkByBm,
      IsRegistrationDone=case  When C.IsRegistrationDone = '0' then 'NO' else 'Yes' end

From Counselling C join Centre CE WITH(NOLOCk)
	on C.CentreId=CE.CentreId join Personnel P WITH(NOLOCk)
	on C.PersonnelId=P.PersonnelId join Course CO WITH(NOLOCk)
	on C.CourseOfferedId=CO.CourseId







GO
/****** Object:  View [dbo].[CounsellingSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE VIEW [dbo].[CounsellingSearchField]
AS 
SELECT 

       C.CounsellingId,
      C.EnquiryId,
      C.Title,
      C.FirstName,
      C.MiddleName,
      C.LastName,
      C.CentreId,
      C.OrganisationId,
      C.PersonnelId,
      C.CourseOfferedId,
      C.PreferTiming,
      C.Remarks,
      C.FollowUpDate,
      C.RemarkByBranchManager,
      C.SectorId,
      C.PsychomatricTest,
      C.ConversionProspect,
      C.[Close],
      C.ClosingRemark,
      C.RemarkByBm,
      C.IsRegistrationDone,

	  ISNULL(C.Title, '')+ISNULL(C.FirstName, '')+ISNULL(C.MiddleName, '')+ISNULL(C.LastName, '')
	  +ISNULL(CO.Name, '')
	  + ISNULL(CONVERT(varchar,C.FollowUpDate, 101), '') 
	  + ISNULL(CONVERT(varchar,C.FollowUpDate, 103), '') 
	  + ISNULL(CONVERT(varchar,C.FollowUpDate, 105), '') 
	  + ISNULL(CONVERT(varchar,C.FollowUpDate, 126), '') as SearchField
		

	  from Counselling C WITH (NOLOCK) left join Course CO WITH (NOLOCK)
	on C.CourseOfferedId = CO.CourseId
	where C.IsRegistrationDone=0





GO
/****** Object:  View [dbo].[CourseInstallmentSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[CourseInstallmentSearchField]
AS 
SELECT 

		C.CourseInstallmentId,
      C.Name,
      C.CourseId,
      C.Fee,
      C.DownPayment,
      C.LumpsumAmount,
      C.OrganisationId,
      C.NumberOfInstallment,
      C.CentreId,
      C.CreatedDate,

	  ISNULL(C.Name, '')+ISNULL(CO.Name, '')+CONVERT(varchar, C.Fee )+ISNULL(CE.Name, '')AS SearchField



	  FROM 
	CourseInstallment C  WITH (NOLOCK) left join Course CO WITH (NOLOCK)
	on C.CourseId = CO.CourseId join Centre CE WITH(NOLOCk)
	on C.CentreId=CE.CentreId




GO
/****** Object:  View [dbo].[CourseSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE VIEW [dbo].[CourseSearchField]
AS 
SELECT 

		C.CourseId,
		C.Name,
		C.OrganisationId,
		C.SectorId,
		C.SchemeId,
		C.CourseTypeId,
		C.Description,
		C.Duration,

ISNULL(C.Name, '')+ISNULL(S.Name, '')+ISNULL(SC.Name, '') AS SearchField
FROM 
	Course C  WITH (NOLOCK) left join Sector S WITH (NOLOCK)
	on C.SectorId = S.SectorId join Scheme SC WITH (NOLOCK)
	on C.SchemeId=SC.SchemeId













GO
/****** Object:  View [dbo].[EnquiryDataGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[EnquiryDataGrid]
AS 

SELECT 
	E.CentreId,
	  CE.Name as [Centre Name],
      E.EnquiryId,
      CandidateName= E.Title+' '+E.FirstName+' '+ Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end +' '+E.LastName,
      E.Mobile,
      E.EmailId,
	  E.DateOfBirth,
      E.Age,
      E.Address1,
	  E.Address2,
	  E.Address3,
	  E.Address4,
	  T.Name as [Taluka],
	  SE.Name as [State],
	  D.Name as [District],
	  E.PinCode,
      E.GuardianName,
      E.GuardianContactNo,
      O.Name as [Occupation],
      R.Name as [Religion],
      CC.Caste as [Caste Category],
      E.Gender,
      Q.Name as [Qualification],
      E.YearOfPassOut,
      E.Marks,
	  SC.Name as [Scheme],
	  S.Name as [Sector],
      H.Name as [How Did You Know About Us],
      E.PreTrainingStatus,
      E.EmploymentStatus,
      E.Promotional,
      E.EnquiryDate,
	  E.StudentCode,
	  E.EnquiryStatus,
      ET.Name as [Enquiry Type],
      ST.Name as [Student Type],
      BTP.Name as [Batch Time Prefer],
      E.AppearingQualification,
      E.PlacementNeeded,
	  E.ConversionProspect,
      E.Remarks,
      E.FollowUpDate,
      E.PreferredMonthForJoining,
	  E.OtherInterestedCourse,
	  E.[Close],
	  E.ClosingRemark,
	  IsCounsellingDone = case  When E.IsCounsellingDone = '0' then 'NO' else 'Yes' end,
      IsRegistrationDone = case  When E.IsRegistrationDone = '0' then 'NO' else 'Yes' end,
	  IsAdmissionDone = case  When E.IsAdmissionDone = '0' then 'NO' else 'Yes' end
		
FROM 
	Enquiry E WITH (NOLOCK) left join Religion R WITH (NOLOCK)
	on E.ReligionId=R.ReligionId join CasteCategory CC WITH (NOLOCK)
	ON E.CasteCategoryId=CC.CasteCategoryId join Qualification Q WITH (NOLOCK) 
	on E.EducationalQualificationId=Q.QualificationId join Taluka T WIth (NOLOCK)
	on E.TalukaId=T.TalukaId join District D WITH(NOLOCK)
	on E.DistrictId=D.DistrictId join State SE WITH(NOLOCK)
	on E.StateId=SE.StateId join Scheme SC WITH (NOLOCK)
	on E.SchemeId=SC.SchemeId join EnquiryType ET WITh(NOLOCK)
	on E.EnquiryTypeId=ET.EnquiryTypeId join Sector S WITH(NOLOCK)
	on E.SectorId=S.SectorId join HowDidYouKnowAbout H WITH(NOLOCk)
	on E.HowDidYouKnowAboutId=H.HowDidYouKnowAboutId join Occupation O WITH(NOLOCk)
	on E.OccupationId=O.OccupationId join Centre CE WITH(NOLOCk)
	on E.CentreId=CE.CentreId join StudentType ST WITH(NOLOCk)
	on E.StudentTypeId=ST.StudentTypeId join BatchTimePrefer BTP WITH(NOLOCk)
	On E.BatchTimePreferId=BTP.BatchTimePreferId
	



GO
/****** Object:  View [dbo].[EnquirySearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO












CREATE VIEW [dbo].[EnquirySearchField]
AS 
SELECT 

		E.EnquiryId,
      E.Title,
      E.FirstName,
      E.MiddleName,
      E.LastName,
      E.Mobile,
      E.AlternateMobile,
      E.EmailId,
      E.DateOfBirth,
      E.Age,
      E.Address1,
      E.Address2,
      E.Address3,
      E.Address4,
      E.TalukaId,
      E.StateId,
      E.DistrictId,
      E.PinCode,
      E.GuardianName,
      E.GuardianContactNo,
      E.OccupationId,
      E.ReligionId,
      E.CasteCategoryId,
      E.Gender,
      E.EducationalQualificationId,
      E.YearOfPassOut,
      E.Marks,
      E.IntrestedCourseId,
      E.HowDidYouKnowAboutId,
      E.PreTrainingStatus,
      E.EmploymentStatus,
      E.Promotional,
      E.EnquiryDate,
      E.StudentCode,
      E.EnquiryStatus,
      E.EmployerName,
      E.EmployerContactNo,
      E.EmployerAddress,
      E.AnnualIncome,
      E.SchemeId,
      E.EnquiryTypeId,
      E.StudentTypeId,
      E.SectorId,
      E.BatchTimePreferId,
      E.AppearingQualification,
      E.YearOfExperience,
      E.PlacementNeeded,
      E.Remarks,
      E.FollowUpDate,
      E.PreferredMonthForJoining,
      E.[Close],
      E.ClosingRemark,
      E.ConversionProspect,
      E.OtherInterestedCourse,
      E.RemarkByBranchManager,
      E.IsCounsellingDone,
      E.IsRegistrationDone,
      E.IsAdmissionDone,
      E.CentreId,
      E.OrganisationId,

	  ISNULL(E.Title, '')+ISNULL(E.FirstName, '')+ISNULL(E.MiddleName, '')+ISNULL(E.LastName, '')
		+CONVERT(varchar, E.Mobile )+ISNULL(T.Name, '')+ISNULL(S.Name, '')+ISNULL(D.Name, '')
		+ISNULL(R.Name, '')+ISNULL(CC.Caste, '')+ISNULL(E.Gender, '')+ISNULL(Q.Name, '')
		+ISNULL(H.Name, '')+ISNULL(SC.Name, '')+ISNULL(SE.Name, '')+ISNULL(CE.Name, '') AS SearchField

	  from Enquiry E left join Taluka T WITH (NOLOCK)
	on E.TalukaId = T.TalukaId join State S WITH (NOLOCK)
	on E.StateId=S.StateId join District D WITH (NOLOCK)
	on E.DistrictId=D.DistrictId join Religion R WITH (NOLOCK)
	on E.ReligionId=R.ReligionId join CasteCategory CC WITH(NOLOCK)
	on E.CasteCategoryId=CC.CasteCategoryId join HowDidYouKnowAbout H WITH(NOLOCK)
	on E.HowDidYouKnowAboutId=H.HowDidYouKnowAboutId join Scheme SC WITH(NOLOCK)
	on E.SchemeId=SC.SchemeId join Sector SE WITH(NOLOCK)
	on E.SectorId=SE.SectorId join Qualification Q WITH(NOLOCK)
	on E.EducationalQualificationId=Q.QualificationId join Centre CE WITH(NOLOCK)
	on E.CentreId=CE.CentreId
	where E.[Close]='No' and E.IsRegistrationDone=0 and E.IsAdmissionDone=0







GO
/****** Object:  View [dbo].[ExpenseDataGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[ExpenseDataGrid]
As
Select
E.ExpenseId, 
C.Name[CentreName],
CreatedDate,
VoucherNumber,
EH.Name[ExpenseHeader],
CashMemoNumbers,
DebitAmount,
PaidTo,
E.Particulars,
P.Forenames +' '+P.Surname[CreatedBy],
E.CentreId
from Expense E

Inner Join Centre C on E.CentreId = C.CentreId
Inner Join Personnel P on E.PersonnelId = P.PersonnelId
Inner Join ExpenseHeader EH on Eh.ExpenseHeaderId = E.ExpenseHeaderId



GO
/****** Object:  View [dbo].[FollowUpDataGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[FollowUpDataGrid]
AS 
select
	  F.CentreId,
	  CentreName=C.Name,
      F.FollowUpHistoryId,
      F.FollowUpId,
	  CandidateName=FU.Title+' '+FU.FirstName+' '+Case when FU.MiddleName IS NULL  then ' ' else FU.MiddleName end+' '+Fu.LastName,
      F.FollowUpType,
      COALESCE(F.Remarks,'')[Remarks],
      F.[Close],
      COALESCE(F.ClosingRemarks,'')[ClosingRemarks],
      F.FollowUpDate,
      F.CreatedDate, 
      F.OrganisationId

	  from FollowUpHistory F 
	  With(NOLOCK) left join FollowUp FU with (NOLOCK)
	  on F.FollowUpId = FU.FollowUpId join Centre C with(NOLOCK)
	  on F.CentreId=C.CentreId

	  






GO
/****** Object:  View [dbo].[FollowUpSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[FollowUpSearchField]
AS 
SELECT 

		F.FollowUpId,
      F.FollowUpDateTime,
      F.MobilizationId,
      F.EnquiryId,
      F.CounsellingId,
      F.RegistrationId,
      F.Title,
      F.FirstName,
      F.MiddleName,
      F.LastName,
      F.Remark,
      F.Closed,
      F.ReadDateTime,
      F.CreatedDateTime,
      F.OrganisationId,
      F.CentreId,
      F.Mobile,
      F.IntrestedCourseId,
      F.FollowUpType,
      F.AlternateMobile,
      F.FollowUpURL,
      F.[Close],
      F.ClosingRemark,

	  ISNULL(F.Title, '')+ISNULL(F.FirstName, '')+ISNULL(F.MiddleName, '')+ISNULL(F.LastName, '')
		+CONVERT(varchar, F.Mobile )+ISNULL(C.Name, '')+ISNULL(F.FollowUpType, '') AS SearchField

	  from 
	  FollowUp F WITH (NOLOCK) left join Course C WITH (NOLOCK)
	  on F.IntrestedCourseId = C.CourseId 
	  where F.[Close]='No'





GO
/****** Object:  View [dbo].[MobilizationCountReport]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[MobilizationCountReport]
as
select 
MobilizationCount,
MobilizationDate,
MobilizationCentreId,	
EnquiryCount,	
EnquiryDate,
EnquiryCentreId,	
AdmissionCount,	
AdmissionDate,	
AdmissionCentreId,
CounsellingCount,	
CreatedDate,
CounsellingCentreId,
RegistrationCount,	
RegistrationDate,
RegistrationCentreId,	
CandidateFeeAmount,	
PaymentDate,	
CandidateFeeCentreId,
CourseFee,	
CourseInstallmentCreatedDate,
CandidateInstallmentCentreId


from 
(select 
COUNT(MobilizationId)[MobilizationCount], GeneratedDate[MobilizationDate],CentreId[MobilizationCentreId]
from Mobilization group by GeneratedDate,CentreId) MobilizationCountTable
full outer join 
(select 
COUNT(EnquiryId)[EnquiryCount],EnquiryDate,CentreId[EnquiryCentreId] From Enquiry group by EnquiryDate,CentreId ) EnquiryCountTable

On MobilizationCountTable.MobilizationDate = EnquiryCountTable.EnquiryDate
full outer join 

(select 
COUNT(AdmissionId)[AdmissionCount],AdmissionDate,CentreId[AdmissionCentreId] From Admission group by AdmissionDate,CentreId ) AdmissionCountTable

on AdmissionCountTable.AdmissionDate = MobilizationCountTable.MobilizationDate

full outer join 

(select 
COUNT(CounsellingId)[CounsellingCount],CreatedDate,CentreId[CounsellingCentreId] From Counselling group by CreatedDate,CentreId ) CounsellingCountTable

on CounsellingCountTable.CreatedDate = MobilizationCountTable.MobilizationDate

full outer join 

(select 
COUNT(RegistrationId)[RegistrationCount],RegistrationDate,CentreId[RegistrationCentreId] From Registration group by RegistrationDate,CentreId ) RegistrationCountTable

on RegistrationCountTable.RegistrationDate = MobilizationCountTable.MobilizationDate

full outer join 

(select 
Sum(PaidAmount)CandidateFeeAmount, CAST(PaymentDate AS date)PaymentDate,CentreId[CandidateFeeCentreId] From CandidateFee group by  CAST(PaymentDate AS date),CentreId) 
CandidateFeeTable

on CandidateFeeTable.PaymentDate = MobilizationCountTable.MobilizationDate

full outer join 

(select 
Sum(CourseFee)CourseFee, CAST(CreatedDate AS date)CourseInstallmentCreatedDate,CentreId[CandidateInstallmentCentreId] From CandidateInstallment group by 
 CAST(CreatedDate AS date),CentreId) 
CourseInstallmentTable
on CourseInstallmentTable.CourseInstallmentCreatedDate = MobilizationCountTable.MobilizationDate

GO
/****** Object:  View [dbo].[MobilizationDataGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[MobilizationDataGrid]
AS 
SELECT 
		M.CentreId,
		CentreName = CE.Name,
		M.MobilizationId,
		EventName=E.Name,
		CandidateName = M.Title+' '+M.FirstName+' '+Case when M.MiddleName IS NULL  then ' ' else M.MiddleName end+' '+M.LastName,
		M.Mobile,
		InterestedCourse=C.Name,
		Qualification=Q.Name,
		M.CreatedDate,
		M.FollowUpDate,
		M.Remark,
		M.StudentLocation,
		M.OtherInterestedCourse,
		M.[Close],
		M.ClosingRemark,

		 ISNULL(CE.Name, '')+ISNULL(E.Name, '')+ISNULL(M.Title, '')+ISNULL(M.FirstName, '')
		 +ISNULL(M.MiddleName, '')+ISNULL(M.LastName, '')+ISNULL(C.Name, '')
		+CONVERT(varchar, M.Mobile )+ISNULL(Q.Name, '')+ISNULL(M.StudentLocation, '') AS SearchField


FROM 
	Mobilization M  WITH (NOLOCK) left join Event E WITH (NOLOCK)
	on M.Eventid = E.EventId join Course C WITH (NOLOCK)
	ON C.CourseId = m.InterestedCourseId join Qualification Q WITH (NOLOCK)
	ON Q.QualificationId = M.QualificationId join Personnel P WITH (NOLOCK)
	on m.PersonnelId=p.PersonnelId join Centre CE WITH (NOLOCK)
	on m.CentreId=CE.CentreId







GO
/****** Object:  View [dbo].[MobilizationSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO













CREATE VIEW [dbo].[MobilizationSearchField]
AS 
SELECT 
		M.MobilizationId,
		M.EventId,
		M.OrganisationId,
		M.CentreId,
		M.Title,
		M.FirstName,
		M.MiddleName,
		M.LastName,
		M.Mobile,
		M.AlternateMobile,
		M.InterestedCourseId,
		M.QualificationId,
		M.CreatedDate,
		M.FollowUpDate,
		M.Remark,
		M.MobilizerStatus,
		M.StudentLocation,
		M.OtherInterestedCourse,
		M.MobilizationTypeId,
		M.GeneratedDate,
		M.PersonnelId,
        M.[Close],
        M.ClosingRemark,
		ISNULL(M.Title, '')+ISNULL(M.FirstName, '')+ISNULL(M.MiddleName, '')+ISNULL(M.LastName, '')+ISNULL(E.Name, '')+ISNULL(C.Name, '')+ISNULL(M.StudentLocation, '')+ISNULL(Mo.Name, '') + ISNULL(CONVERT(varchar,M.GeneratedDate, 101), '') + ISNULL(CONVERT(varchar,GeneratedDate, 103), '') + ISNULL(CONVERT(varchar,GeneratedDate, 105), '') + ISNULL(CONVERT(varchar,GeneratedDate, 126), '') + ISNULL(StudentLocation, '') + ISNULL(Q.Name, '') + CONVERT(varchar, m.Mobile ) AS SearchField
FROM 
	Mobilization M  WITH (NOLOCK) left join Event E WITH (NOLOCK)
	on M.Eventid = E.EventId join Course C WITH (NOLOCK)
	ON C.CourseId = m.InterestedCourseId join Qualification Q WITH (NOLOCK)
	ON Q.QualificationId = M.QualificationId join Personnel P WITH (NOLOCK)
	on m.PersonnelId=p.PersonnelId join MobilizationType Mo WITH (NOLOCK)
	on M.MobilizationTypeId=Mo.MobilizationTypeId
	where M.[Close]='No'

	












GO
/****** Object:  View [dbo].[PersonnelSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[PersonnelSearchField]
AS 
SELECT 
		PersonnelId,
		OrganisationId,
		Title,
		Forenames,
		Surname,
		DOB,
		CountryId,
		Address1,
		Address2,
		Address3,
		Address4,
		Postcode,
		Telephone,
		Mobile,
		NINumber,
		BankAccountNumber,
		BankSortCode,
		BankAccountName,
		BankAddress1,
		BankAddress2,
		BankAddress3,
		BankAddress4,
		BankPostcode,
		BankTelephone,
		Email,
		CurrentEmploymentId,
      CentreId,
	
		ISNULL(Surname, '') + ISNULL(Forenames, '') + ISNULL(Surname, '') + ISNULL(CONVERT(varchar,DOB, 101), '') + ISNULL(CONVERT(varchar,DOB, 103), '') + ISNULL(CONVERT(varchar,DOB, 105), '') + ISNULL(CONVERT(varchar,DOB, 126), '') + ISNULL(EMail, '') + ISNULL(Postcode, '') + ISNULL(Mobile, '') AS SearchField
FROM 
	Personnel  WITH (NOLOCK)

















GO
/****** Object:  View [dbo].[PettyCashExpenseReport]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[PettyCashExpenseReport]
As
select
	PettyCashCreatedDate,
	PettyCashForCentreId,	
	PettyCashForCentreName,
	PettyCashAmount,	
	PettyCashParticulars,
	PettyCashCreatedBy,
	E.Particulars[ExpenseParticulars],	
	E.DebitAmount[ExpenseDebitAmount],
	E.CashMemoNumbers[ExpenseCashMemoNumbers],
	E.VoucherNumber[ExpenseVoucherNumber],
	E.PaidTo[ExpensePaidTo],
	[ExpenseCentreName],
	[ExpenseCreatedBy],
	[ExpenseCreatedDate],
	[ExpenseHeader] 
	from(
		SELECT CreatedDate
		ExpenseId,
		VoucherNumber,
		Ex.ExpenseHeaderId,
		Eh.Name[ExpenseHeader],
		CashMemoNumbers,
		DebitAmount,
		RupeesInWord,
		PaidTo,
		Particulars,
		PaymentModeId,
		CreatedDate[ExpenseCreatedDate],
		Ex.CentreId[ExpenseCentreId],
		C.Name[ExpenseCentreName],
		Ex.OrganisationId,
		P.Forenames+' '+P.Surname[ExpenseCreatedBy],
		row_number() over (order by CreatedDate) as ExpenseRowNumber
		FROM Expense Ex 
		INNER JOIN Personnel P on P.PersonnelId =  Ex.PersonnelId
		INNER JOIN Centre C on C.CentreId = Ex.CentreId 
		INNEr JOIN ExpenseHeader Eh on Eh.ExpenseHeaderId = Ex.ExpenseHeaderId
		) E
		left join
		(
			SELECT 
			CreatedDate[PettyCashCreatedDate],
			CentrePetty.CentreId[PettyCashForCentreId],
			C.Name[PettyCashForCentreName],
			Amount[PettyCashAmount],
			Particulars[PettyCashParticulars],
			P.Forenames+' '+P.Surname[PettyCashCreatedBy],
			row_number() over (order by CreatedDate) as CentrePettyCashRowNumber
			FROM CentrePettyCash CentrePetty
			INNER JOIN Personnel P on P.PersonnelId =  CentrePetty.CreatedBy
			INNER JOIN Centre C on C.CentreId = CentrePetty.CentreId
			)	CPC
	on  CPC.CentrePettyCashRowNumber=E.ExpenseRowNumber


 



GO
/****** Object:  View [dbo].[RegistrationGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[RegistrationGrid]
AS 
Select
CandidateName = E.Title+' '+E.FirstName+' '+Case when E.MiddleName IS NULL  then ' ' else E.MiddleName end+' '+E.Lastname,
R.RegistrationDate,
TotalFee=Case when CI.PaymentMethod='MonthlyInstallment' then CI.CourseFee else CI.LumpsumAmount end,
RegistrationAmount=CF.PaidAmount,
R.RegistrationId,
CentreName=C.Name,
PaymentMode=P.Name,
C.CentreId
from REGISTRATION R
INNER JOIN ENQUIRY E ON E.EnquiryId = R.EnquiryId
INNER JOIN CandidateInstallment CI on CI.StudentCode = E.StudentCode
INNER JOIN CandidateFee CF on CF.CandidateFeeId = R.CandidateFeeId
INNER JOIN PaymentMode P ON P.PaymentModeId=CF.PaymentModeId
INNER JOIN Centre C on C.CentreId = R.CentreId
group by
E.Title,
E.MiddleName,
E.FirstName,
E.Lastname,
CI.CourseFee,
C.CentreId,
C.Name,
CI.PaymentMethod,
CI.LumpsumAmount,
R.RegistrationDate,
CF.PaidAmount,
R.RegistrationId,
P.Name





GO
/****** Object:  View [dbo].[RegistrationSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE VIEW [dbo].[RegistrationSearchField]
AS 
SELECT

       R.RegistrationId,
      R.StudentCode,
      R.EnquiryId,
      R.CourseId,
      R.CourseInstallmentId,
      R.CandidateFeeId,
      R.CandidateInstallmentId,
      R.Remarks,
      R.FollowupDate,
      R.CentreId,
      R.OrganisationId,
      R.IsAdmissionDone,
      R.RegistrationDate,

	  ISNULL(E.Title, '')+ISNULL(E.FirstName, '')+ISNULL(E.MiddleName, '')+ISNULL(E.LastName, '')
	  +ISNULL(C.Name,'')+ ISNULL(CONVERT(varchar,R.RegistrationDate, 101), '') 
	  + ISNULL(CONVERT(varchar,R.RegistrationDate, 103), '') 
	  + ISNULL(CONVERT(varchar,R.RegistrationDate, 105), '') 
	  + ISNULL(CONVERT(varchar,R.RegistrationDate, 126), '')  AS SearchField

	  FROM 
	Registration R  WITH (NOLOCK) left join Course C WITH (NOLOCK)
	on R.CourseId = C.CourseId join Enquiry E WITH(NOLOCK)
	on R.EnquiryId=E.EnquiryId
	where R.IsAdmissionDone=0





GO
/****** Object:  View [dbo].[TrainerSearchField]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE VIEW [dbo].[TrainerSearchField]
AS 
SELECT 
		T.TrainerId,
		T.Title,
		T.FirstName,
		T.MiddleName,
		T.LastName,
		T.DateOfBirth,
		T.Address1,
		T.Address2,
		T.Address3,
		T.Address4,
		T.PinCode,
		T.TalukaId,
		T.DistrictId,
		T.StateId,
		T.Gender,
		T.AadharNo,
		T.Mobile,
		T.EmailId,
		T.Certified,
		T.CertificationNo,
		T.SectorId,
		T.CourseId,
		T.CentreId,
		T.OrganisationId,
		T.PersonnelId,
		T.CreatedDate,
		
		ISNULL(T.Title, '')+ISNULL(T.FirstName, '')+ISNULL(T.MiddleName, '')+ISNULL(T.LastName, '')
		+CONVERT(varchar, T.Mobile )+ISNULL(S.Name, '')
		+ ISNULL(CONVERT(varchar,T.DateOfBirth, 101), '') 
	  + ISNULL(CONVERT(varchar,DateOfBirth, 103), '') 
	  + ISNULL(CONVERT(varchar,DateOfBirth, 105), '') 
	  + ISNULL(CONVERT(varchar,DateOfBirth, 126), '')
	  +ISNULL(TL.Name, '')+ISNULL(ST.Name, '')+ISNULL(D.Name, '') AS SearchField
FROM 
	Trainer T  WITH (NOLOCK) left join Sector S WITH (NOLOCK)
	on T.SectorId = S.SectorId join Taluka TL WITH(NOLOCk)
	on T.TalukaId=TL.TalukaId join District D WITH(NOLOCk)
	on T.DistrictId=D.DistrictId join State ST WITH(NOLOCk)
	on T.StateId=ST.StateId















GO
/****** Object:  View [dbo].[UserAuthorisationFilter]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserAuthorisationFilter]
AS 
SELECT 
	ROW_NUMBER() OVER(ORDER BY p.PersonnelId, ur.RoleId) AS Id,
	p.PersonnelId,
	u.Id AS AspNetUsersId, 
	u.OrganisationId,
	ur.RoleId,
	r.Name AS RoleName
FROM
	AspNetUsers u
INNER JOIN
	AspNetUserRoles ur
		ON u.Id = ur.UserId
INNER JOIN
	AspNetRoles r 
		on ur.RoleId = r.Id
LEFT JOIN
	Personnel p 
		ON u.PersonnelId = p.PersonnelId















GO
/****** Object:  View [dbo].[VoucherGrid]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[VoucherGrid]
AS 
SELECT 
CentreId=V.CentreId,
CentreName=C.Name,
V.CreatedDate,
VoucherNumber=V.VoucherNumber,
CashMemo=V.CashMemo,
TotalDebitAmount=sum(O.DebitAmount),
PaidTo=O.PaidTo
From Voucher V inner join OtherFee O
on V.CashMemo=O.CashMemo inner join Centre C
on V.CentreId=C.CentreId
group by
V.CreatedDate,
VoucherNumber,
V.CashMemo,
O.PaidTo,
C.Name,
V.CentreId




GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD  CONSTRAINT [FK_Absence_AbsenceStatus] FOREIGN KEY([AbsenceStatusId])
REFERENCES [dbo].[AbsenceStatus] ([AbsenceStatusId])
GO
ALTER TABLE [dbo].[Absence] CHECK CONSTRAINT [FK_Absence_AbsenceStatus]
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD  CONSTRAINT [FK_Absence_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Absence] CHECK CONSTRAINT [FK_Absence_Organisation]
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD  CONSTRAINT [FK_Absence_PersonnelAbsenceEntitlement] FOREIGN KEY([PersonnelAbsenceEntitlementId])
REFERENCES [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId])
GO
ALTER TABLE [dbo].[Absence] CHECK CONSTRAINT [FK_Absence_PersonnelAbsenceEntitlement]
GO
ALTER TABLE [dbo].[AbsenceDay]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceDay_Absence] FOREIGN KEY([AbsenceId])
REFERENCES [dbo].[Absence] ([AbsenceId])
GO
ALTER TABLE [dbo].[AbsenceDay] CHECK CONSTRAINT [FK_AbsenceDay_Absence]
GO
ALTER TABLE [dbo].[AbsenceDay]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceDay_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AbsenceDay] CHECK CONSTRAINT [FK_AbsenceDay_Organisation]
GO
ALTER TABLE [dbo].[AbsencePeriod]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePeriod_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AbsencePeriod] CHECK CONSTRAINT [FK_AbsencePeriod_Organisation]
GO
ALTER TABLE [dbo].[AbsencePolicy]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicy_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AbsencePolicy] CHECK CONSTRAINT [FK_AbsencePolicy_Organisation]
GO
ALTER TABLE [dbo].[AbsencePolicy]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicy_WorkingPattern] FOREIGN KEY([WorkingPatternId])
REFERENCES [dbo].[WorkingPattern] ([WorkingPatternId])
GO
ALTER TABLE [dbo].[AbsencePolicy] CHECK CONSTRAINT [FK_AbsencePolicy_WorkingPattern]
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyEntitlement_AbsencePolicy] FOREIGN KEY([AbsencePolicyId])
REFERENCES [dbo].[AbsencePolicy] ([AbsencePolicyId])
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement] CHECK CONSTRAINT [FK_AbsencePolicyEntitlement_AbsencePolicy]
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyEntitlement_AbsenceType] FOREIGN KEY([AbsenceTypeId])
REFERENCES [dbo].[AbsenceType] ([AbsenceTypeId])
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement] CHECK CONSTRAINT [FK_AbsencePolicyEntitlement_AbsenceType]
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyEntitlement_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([FrequencyId])
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement] CHECK CONSTRAINT [FK_AbsencePolicyEntitlement_Frequency]
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyEntitlement_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AbsencePolicyEntitlement] CHECK CONSTRAINT [FK_AbsencePolicyEntitlement_Organisation]
GO
ALTER TABLE [dbo].[AbsencePolicyPeriod]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyPeriod_AbsencePeriod] FOREIGN KEY([AbsencePeriodId])
REFERENCES [dbo].[AbsencePeriod] ([AbsencePeriodId])
GO
ALTER TABLE [dbo].[AbsencePolicyPeriod] CHECK CONSTRAINT [FK_AbsencePolicyPeriod_AbsencePeriod]
GO
ALTER TABLE [dbo].[AbsencePolicyPeriod]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyPeriod_AbsencePolicy] FOREIGN KEY([AbsencePolicyId])
REFERENCES [dbo].[AbsencePolicy] ([AbsencePolicyId])
GO
ALTER TABLE [dbo].[AbsencePolicyPeriod] CHECK CONSTRAINT [FK_AbsencePolicyPeriod_AbsencePolicy]
GO
ALTER TABLE [dbo].[AbsencePolicyPeriod]  WITH CHECK ADD  CONSTRAINT [FK_AbsencePolicyPeriod_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AbsencePolicyPeriod] CHECK CONSTRAINT [FK_AbsencePolicyPeriod_Organisation]
GO
ALTER TABLE [dbo].[AbsenceType]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceType_Colour] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([ColourId])
GO
ALTER TABLE [dbo].[AbsenceType] CHECK CONSTRAINT [FK_AbsenceType_Colour]
GO
ALTER TABLE [dbo].[AbsenceType]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AbsenceType] CHECK CONSTRAINT [FK_AbsenceType_Organisation]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Batch]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Centre]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Organisation]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Personnel]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Registration] FOREIGN KEY([RegistrationId])
REFERENCES [dbo].[Registration] ([RegistrationId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Registration]
GO
ALTER TABLE [dbo].[AreaOfInterest]  WITH CHECK ADD  CONSTRAINT [FK_AreaOfInterest_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AreaOfInterest] CHECK CONSTRAINT [FK_AreaOfInterest_Organisation]
GO
ALTER TABLE [dbo].[AspNetUsersAlertSchedule]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsersAlertSchedule_Alert1] FOREIGN KEY([AlertId])
REFERENCES [dbo].[Alert] ([AlertId])
GO
ALTER TABLE [dbo].[AspNetUsersAlertSchedule] CHECK CONSTRAINT [FK_AspNetUsersAlertSchedule_Alert1]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Centre]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Organisation]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Personnel]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Centre]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Course]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_CourseInstallment] FOREIGN KEY([CourseInstallmentId])
REFERENCES [dbo].[CourseInstallment] ([CourseInstallmentId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_CourseInstallment]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Organisation]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Room]
GO
ALTER TABLE [dbo].[BatchAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BatchAttendance_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[BatchAttendance] CHECK CONSTRAINT [FK_BatchAttendance_Batch]
GO
ALTER TABLE [dbo].[BatchAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BatchAttendance_BatchTrainer] FOREIGN KEY([BatchTrainerId])
REFERENCES [dbo].[BatchTrainer] ([BatchTrainerId])
GO
ALTER TABLE [dbo].[BatchAttendance] CHECK CONSTRAINT [FK_BatchAttendance_BatchTrainer]
GO
ALTER TABLE [dbo].[BatchAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BatchAttendance_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[BatchAttendance] CHECK CONSTRAINT [FK_BatchAttendance_Centre]
GO
ALTER TABLE [dbo].[BatchAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BatchAttendance_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[BatchAttendance] CHECK CONSTRAINT [FK_BatchAttendance_Organisation]
GO
ALTER TABLE [dbo].[BatchDay]  WITH CHECK ADD  CONSTRAINT [FK_BatchDay_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[BatchDay] CHECK CONSTRAINT [FK_BatchDay_Batch]
GO
ALTER TABLE [dbo].[BatchDay]  WITH CHECK ADD  CONSTRAINT [FK_BatchDay_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[BatchDay] CHECK CONSTRAINT [FK_BatchDay_Centre]
GO
ALTER TABLE [dbo].[BatchDay]  WITH CHECK ADD  CONSTRAINT [FK_BatchDay_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[BatchDay] CHECK CONSTRAINT [FK_BatchDay_Organisation]
GO
ALTER TABLE [dbo].[BatchTimePrefer]  WITH CHECK ADD  CONSTRAINT [FK_BatchTimePrefer_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[BatchTimePrefer] CHECK CONSTRAINT [FK_BatchTimePrefer_Organisation]
GO
ALTER TABLE [dbo].[BatchTrainer]  WITH CHECK ADD  CONSTRAINT [FK_BatchTrainer_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[BatchTrainer] CHECK CONSTRAINT [FK_BatchTrainer_Batch]
GO
ALTER TABLE [dbo].[BatchTrainer]  WITH CHECK ADD  CONSTRAINT [FK_BatchTrainer_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[BatchTrainer] CHECK CONSTRAINT [FK_BatchTrainer_Centre]
GO
ALTER TABLE [dbo].[BatchTrainer]  WITH CHECK ADD  CONSTRAINT [FK_BatchTrainer_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[BatchTrainer] CHECK CONSTRAINT [FK_BatchTrainer_Organisation]
GO
ALTER TABLE [dbo].[BatchTrainer]  WITH CHECK ADD  CONSTRAINT [FK_BatchTrainer_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainer] ([TrainerId])
GO
ALTER TABLE [dbo].[BatchTrainer] CHECK CONSTRAINT [FK_BatchTrainer_Trainer]
GO
ALTER TABLE [dbo].[Brainstorming]  WITH CHECK ADD  CONSTRAINT [FK_Brainstorming_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Brainstorming] CHECK CONSTRAINT [FK_Brainstorming_Centre]
GO
ALTER TABLE [dbo].[Brainstorming]  WITH CHECK ADD  CONSTRAINT [FK_Brainstorming_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Brainstorming] CHECK CONSTRAINT [FK_Brainstorming_Organisation]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Centre]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Organisation]
GO
ALTER TABLE [dbo].[Building]  WITH CHECK ADD  CONSTRAINT [FK_Building_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Building] CHECK CONSTRAINT [FK_Building_Organisation]
GO
ALTER TABLE [dbo].[Building]  WITH CHECK ADD  CONSTRAINT [FK_Building_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([SiteId])
GO
ALTER TABLE [dbo].[Building] CHECK CONSTRAINT [FK_Building_Site]
GO
ALTER TABLE [dbo].[CandidateFee]  WITH CHECK ADD  CONSTRAINT [FK_CandidateFee_CandidateInstallment] FOREIGN KEY([CandidateInstallmentId])
REFERENCES [dbo].[CandidateInstallment] ([CandidateInstallmentId])
GO
ALTER TABLE [dbo].[CandidateFee] CHECK CONSTRAINT [FK_CandidateFee_CandidateInstallment]
GO
ALTER TABLE [dbo].[CandidateFee]  WITH CHECK ADD  CONSTRAINT [FK_CandidateFee_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CandidateFee] CHECK CONSTRAINT [FK_CandidateFee_Centre]
GO
ALTER TABLE [dbo].[CandidateFee]  WITH CHECK ADD  CONSTRAINT [FK_CandidateFee_FeeType] FOREIGN KEY([FeeTypeId])
REFERENCES [dbo].[FeeType] ([FeeTypeId])
GO
ALTER TABLE [dbo].[CandidateFee] CHECK CONSTRAINT [FK_CandidateFee_FeeType]
GO
ALTER TABLE [dbo].[CandidateFee]  WITH CHECK ADD  CONSTRAINT [FK_CandidateFee_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CandidateFee] CHECK CONSTRAINT [FK_CandidateFee_Organisation]
GO
ALTER TABLE [dbo].[CandidateFee]  WITH CHECK ADD  CONSTRAINT [FK_CandidateFee_PaymentMode] FOREIGN KEY([PaymentModeId])
REFERENCES [dbo].[PaymentMode] ([PaymentModeId])
GO
ALTER TABLE [dbo].[CandidateFee] CHECK CONSTRAINT [FK_CandidateFee_PaymentMode]
GO
ALTER TABLE [dbo].[CandidateFee]  WITH CHECK ADD  CONSTRAINT [FK_CandidateFee_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[CandidateFee] CHECK CONSTRAINT [FK_CandidateFee_Personnel]
GO
ALTER TABLE [dbo].[CandidateInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInstallment_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CandidateInstallment] CHECK CONSTRAINT [FK_CandidateInstallment_Centre]
GO
ALTER TABLE [dbo].[CandidateInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInstallment_CourseInstallment] FOREIGN KEY([CourseInstallmentId])
REFERENCES [dbo].[CourseInstallment] ([CourseInstallmentId])
GO
ALTER TABLE [dbo].[CandidateInstallment] CHECK CONSTRAINT [FK_CandidateInstallment_CourseInstallment]
GO
ALTER TABLE [dbo].[CandidateInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInstallment_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CandidateInstallment] CHECK CONSTRAINT [FK_CandidateInstallment_Organisation]
GO
ALTER TABLE [dbo].[CasteCategory]  WITH CHECK ADD  CONSTRAINT [FK_CasteCategory_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CasteCategory] CHECK CONSTRAINT [FK_CasteCategory_Organisation]
GO
ALTER TABLE [dbo].[Centre]  WITH CHECK ADD  CONSTRAINT [FK_Centre_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Centre] CHECK CONSTRAINT [FK_Centre_Organisation]
GO
ALTER TABLE [dbo].[CentreCourse]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourse_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreCourse] CHECK CONSTRAINT [FK_CentreCourse_Centre]
GO
ALTER TABLE [dbo].[CentreCourse]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourse_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreCourse] CHECK CONSTRAINT [FK_CentreCourse_Organisation]
GO
ALTER TABLE [dbo].[CentreCourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourseInstallment_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreCourseInstallment] CHECK CONSTRAINT [FK_CentreCourseInstallment_Centre]
GO
ALTER TABLE [dbo].[CentreCourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourseInstallment_CourseInstallment] FOREIGN KEY([CourseInstallmentId])
REFERENCES [dbo].[CourseInstallment] ([CourseInstallmentId])
GO
ALTER TABLE [dbo].[CentreCourseInstallment] CHECK CONSTRAINT [FK_CentreCourseInstallment_CourseInstallment]
GO
ALTER TABLE [dbo].[CentreCourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourseInstallment_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreCourseInstallment] CHECK CONSTRAINT [FK_CentreCourseInstallment_Organisation]
GO
ALTER TABLE [dbo].[CentreEnrollmentReceiptSetting]  WITH CHECK ADD  CONSTRAINT [FK_CentreEnrollmentReceiptSetting_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreEnrollmentReceiptSetting] CHECK CONSTRAINT [FK_CentreEnrollmentReceiptSetting_Centre]
GO
ALTER TABLE [dbo].[CentreEnrollmentReceiptSetting]  WITH CHECK ADD  CONSTRAINT [FK_CentreEnrollmentReceiptSetting_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreEnrollmentReceiptSetting] CHECK CONSTRAINT [FK_CentreEnrollmentReceiptSetting_Organisation]
GO
ALTER TABLE [dbo].[CentrePettyCash]  WITH CHECK ADD  CONSTRAINT [FK_CentrePettyCash_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentrePettyCash] CHECK CONSTRAINT [FK_CentrePettyCash_Centre]
GO
ALTER TABLE [dbo].[CentrePettyCash]  WITH CHECK ADD  CONSTRAINT [FK_CentrePettyCash_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentrePettyCash] CHECK CONSTRAINT [FK_CentrePettyCash_Organisation]
GO
ALTER TABLE [dbo].[CentreReceiptSetting]  WITH CHECK ADD  CONSTRAINT [FK_CentreReceiptSetting_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreReceiptSetting] CHECK CONSTRAINT [FK_CentreReceiptSetting_Centre]
GO
ALTER TABLE [dbo].[CentreReceiptSetting]  WITH CHECK ADD  CONSTRAINT [FK_CentreReceiptSetting_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreReceiptSetting] CHECK CONSTRAINT [FK_CentreReceiptSetting_Organisation]
GO
ALTER TABLE [dbo].[CentreScheme]  WITH CHECK ADD  CONSTRAINT [FK_CentreScheme_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreScheme] CHECK CONSTRAINT [FK_CentreScheme_Centre]
GO
ALTER TABLE [dbo].[CentreScheme]  WITH CHECK ADD  CONSTRAINT [FK_CentreScheme_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreScheme] CHECK CONSTRAINT [FK_CentreScheme_Organisation]
GO
ALTER TABLE [dbo].[CentreScheme]  WITH CHECK ADD  CONSTRAINT [FK_CentreScheme_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO
ALTER TABLE [dbo].[CentreScheme] CHECK CONSTRAINT [FK_CentreScheme_Scheme]
GO
ALTER TABLE [dbo].[CentreSector]  WITH CHECK ADD  CONSTRAINT [FK_CentreSector_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreSector] CHECK CONSTRAINT [FK_CentreSector_Centre]
GO
ALTER TABLE [dbo].[CentreSector]  WITH CHECK ADD  CONSTRAINT [FK_CentreSector_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreSector] CHECK CONSTRAINT [FK_CentreSector_Organisation]
GO
ALTER TABLE [dbo].[CentreSector]  WITH CHECK ADD  CONSTRAINT [FK_CentreSector_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[CentreSector] CHECK CONSTRAINT [FK_CentreSector_Sector]
GO
ALTER TABLE [dbo].[CentreVoucherNumber]  WITH CHECK ADD  CONSTRAINT [FK_CentreVoucherNumber_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[CentreVoucherNumber] CHECK CONSTRAINT [FK_CentreVoucherNumber_Centre]
GO
ALTER TABLE [dbo].[CentreVoucherNumber]  WITH CHECK ADD  CONSTRAINT [FK_CentreVoucherNumber_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CentreVoucherNumber] CHECK CONSTRAINT [FK_CentreVoucherNumber_Organisation]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Colour] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([ColourId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Colour]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Organisation]
GO
ALTER TABLE [dbo].[CompanyBuilding]  WITH CHECK ADD  CONSTRAINT [FK_CompanyBuilding_Building] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Building] ([BuildingId])
GO
ALTER TABLE [dbo].[CompanyBuilding] CHECK CONSTRAINT [FK_CompanyBuilding_Building]
GO
ALTER TABLE [dbo].[CompanyBuilding]  WITH CHECK ADD  CONSTRAINT [FK_CompanyBuilding_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[CompanyBuilding] CHECK CONSTRAINT [FK_CompanyBuilding_Company]
GO
ALTER TABLE [dbo].[CompanyBuilding]  WITH CHECK ADD  CONSTRAINT [FK_CompanyBuilding_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CompanyBuilding] CHECK CONSTRAINT [FK_CompanyBuilding_Organisation]
GO
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Centre]
GO
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Course] FOREIGN KEY([CourseOfferedId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Course]
GO
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Enquery] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Enquery]
GO
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Organisation]
GO
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Personnel]
GO
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Organisation]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_CourseType] FOREIGN KEY([CourseTypeId])
REFERENCES [dbo].[CourseType] ([CourseTypeId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_CourseType]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Organisation]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Scheme]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Sector]
GO
ALTER TABLE [dbo].[CourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CourseInstallment_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[CourseInstallment] CHECK CONSTRAINT [FK_CourseInstallment_Course]
GO
ALTER TABLE [dbo].[CourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CourseInstallment_CourseInstallment] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CourseInstallment] CHECK CONSTRAINT [FK_CourseInstallment_CourseInstallment]
GO
ALTER TABLE [dbo].[CourseSubject]  WITH CHECK ADD  CONSTRAINT [FK_CourseSubject_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[CourseSubject] CHECK CONSTRAINT [FK_CourseSubject_Course]
GO
ALTER TABLE [dbo].[CourseSubject]  WITH CHECK ADD  CONSTRAINT [FK_CourseSubject_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CourseSubject] CHECK CONSTRAINT [FK_CourseSubject_Organisation]
GO
ALTER TABLE [dbo].[CourseSubject]  WITH CHECK ADD  CONSTRAINT [FK_CourseSubject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[CourseSubject] CHECK CONSTRAINT [FK_CourseSubject_Subject]
GO
ALTER TABLE [dbo].[CourseType]  WITH CHECK ADD  CONSTRAINT [FK_CourseType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[CourseType] CHECK CONSTRAINT [FK_CourseType_Organisation]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK__Departmen__Colou__4F9CCB9E] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([ColourId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK__Departmen__Colou__4F9CCB9E]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Organisation]
GO
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_Organisation]
GO
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_State]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_DocumentType]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Organisation]
GO
ALTER TABLE [dbo].[EmergencyContact]  WITH CHECK ADD  CONSTRAINT [FK_EmergencyContact_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[EmergencyContact] CHECK CONSTRAINT [FK_EmergencyContact_Country]
GO
ALTER TABLE [dbo].[EmergencyContact]  WITH CHECK ADD  CONSTRAINT [FK_EmergencyContact_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EmergencyContact] CHECK CONSTRAINT [FK_EmergencyContact_Organisation]
GO
ALTER TABLE [dbo].[EmergencyContact]  WITH CHECK ADD  CONSTRAINT [FK_EmergencyContact_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[EmergencyContact] CHECK CONSTRAINT [FK_EmergencyContact_Personnel]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_AbsencePolicy] FOREIGN KEY([AbsencePolicyId])
REFERENCES [dbo].[AbsencePolicy] ([AbsencePolicyId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_AbsencePolicy]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_Building1] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Building] ([BuildingId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_Building1]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_Company]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_Organisation]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_Personnel]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_PublicHolidayPolicy] FOREIGN KEY([PublicHolidayPolicyId])
REFERENCES [dbo].[PublicHolidayPolicy] ([PublicHolidayPolicyId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_PublicHolidayPolicy]
GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [FK_Employment_WorkingPattern] FOREIGN KEY([WorkingPatternId])
REFERENCES [dbo].[WorkingPattern] ([WorkingPatternId])
GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [FK_Employment_WorkingPattern]
GO
ALTER TABLE [dbo].[EmploymentDepartment]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentDepartment_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[EmploymentDepartment] CHECK CONSTRAINT [FK_EmploymentDepartment_Department]
GO
ALTER TABLE [dbo].[EmploymentDepartment]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentDepartment_Employment] FOREIGN KEY([EmploymentId])
REFERENCES [dbo].[Employment] ([EmploymentId])
GO
ALTER TABLE [dbo].[EmploymentDepartment] CHECK CONSTRAINT [FK_EmploymentDepartment_Employment]
GO
ALTER TABLE [dbo].[EmploymentDepartment]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentDepartment_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EmploymentDepartment] CHECK CONSTRAINT [FK_EmploymentDepartment_Organisation]
GO
ALTER TABLE [dbo].[EmploymentTeam]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentTeam_Employment] FOREIGN KEY([EmploymentId])
REFERENCES [dbo].[Employment] ([EmploymentId])
GO
ALTER TABLE [dbo].[EmploymentTeam] CHECK CONSTRAINT [FK_EmploymentTeam_Employment]
GO
ALTER TABLE [dbo].[EventBrainstorming]  WITH CHECK ADD  CONSTRAINT [FK_EventBrainstorming_Brainstorming] FOREIGN KEY([BrainstormingId])
REFERENCES [dbo].[Brainstorming] ([BrainstormingId])
GO
ALTER TABLE [dbo].[EventBrainstorming] CHECK CONSTRAINT [FK_EventBrainstorming_Brainstorming]
GO
ALTER TABLE [dbo].[EventBrainstorming]  WITH CHECK ADD  CONSTRAINT [FK_EventBrainstorming_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[EventBrainstorming] CHECK CONSTRAINT [FK_EventBrainstorming_Centre]
GO
ALTER TABLE [dbo].[EventBrainstorming]  WITH CHECK ADD  CONSTRAINT [FK_EventBrainstorming_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventBrainstorming] CHECK CONSTRAINT [FK_EventBrainstorming_Event]
GO
ALTER TABLE [dbo].[EventBrainstorming]  WITH CHECK ADD  CONSTRAINT [FK_EventBrainstorming_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EventBrainstorming] CHECK CONSTRAINT [FK_EventBrainstorming_Organisation]
GO
ALTER TABLE [dbo].[EventBudget]  WITH CHECK ADD  CONSTRAINT [FK_EventBudget_Budget] FOREIGN KEY([BudgetId])
REFERENCES [dbo].[Budget] ([BudgetId])
GO
ALTER TABLE [dbo].[EventBudget] CHECK CONSTRAINT [FK_EventBudget_Budget]
GO
ALTER TABLE [dbo].[EventBudget]  WITH CHECK ADD  CONSTRAINT [FK_EventBudget_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[EventBudget] CHECK CONSTRAINT [FK_EventBudget_Centre]
GO
ALTER TABLE [dbo].[EventBudget]  WITH CHECK ADD  CONSTRAINT [FK_EventBudget_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventBudget] CHECK CONSTRAINT [FK_EventBudget_Event]
GO
ALTER TABLE [dbo].[EventBudget]  WITH CHECK ADD  CONSTRAINT [FK_EventBudget_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EventBudget] CHECK CONSTRAINT [FK_EventBudget_Organisation]
GO
ALTER TABLE [dbo].[Eventday]  WITH CHECK ADD  CONSTRAINT [FK_Eventday_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Eventday] CHECK CONSTRAINT [FK_Eventday_Centre]
GO
ALTER TABLE [dbo].[Eventday]  WITH CHECK ADD  CONSTRAINT [FK_Eventday_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Eventday] CHECK CONSTRAINT [FK_Eventday_Organisation]
GO
ALTER TABLE [dbo].[EventPlanning]  WITH CHECK ADD  CONSTRAINT [FK_EventPlanning_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[EventPlanning] CHECK CONSTRAINT [FK_EventPlanning_Centre]
GO
ALTER TABLE [dbo].[EventPlanning]  WITH CHECK ADD  CONSTRAINT [FK_EventPlanning_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventPlanning] CHECK CONSTRAINT [FK_EventPlanning_Event]
GO
ALTER TABLE [dbo].[EventPlanning]  WITH CHECK ADD  CONSTRAINT [FK_EventPlanning_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EventPlanning] CHECK CONSTRAINT [FK_EventPlanning_Organisation]
GO
ALTER TABLE [dbo].[EventPlanning]  WITH CHECK ADD  CONSTRAINT [FK_EventPlanning_Planning] FOREIGN KEY([PlanningId])
REFERENCES [dbo].[Planning] ([PlanningId])
GO
ALTER TABLE [dbo].[EventPlanning] CHECK CONSTRAINT [FK_EventPlanning_Planning]
GO
ALTER TABLE [dbo].[EventPostEvent]  WITH CHECK ADD  CONSTRAINT [FK_EventPostEvent_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[EventPostEvent] CHECK CONSTRAINT [FK_EventPostEvent_Centre]
GO
ALTER TABLE [dbo].[EventPostEvent]  WITH CHECK ADD  CONSTRAINT [FK_EventPostEvent_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventPostEvent] CHECK CONSTRAINT [FK_EventPostEvent_Event]
GO
ALTER TABLE [dbo].[EventPostEvent]  WITH CHECK ADD  CONSTRAINT [FK_EventPostEvent_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EventPostEvent] CHECK CONSTRAINT [FK_EventPostEvent_Organisation]
GO
ALTER TABLE [dbo].[EventPostEvent]  WITH CHECK ADD  CONSTRAINT [FK_EventPostEvent_Postevent] FOREIGN KEY([PostEventId])
REFERENCES [dbo].[PostEvent] ([PostEventId])
GO
ALTER TABLE [dbo].[EventPostEvent] CHECK CONSTRAINT [FK_EventPostEvent_Postevent]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Centre]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_ExpenseHeader] FOREIGN KEY([ExpenseHeaderId])
REFERENCES [dbo].[ExpenseHeader] ([ExpenseHeaderId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_ExpenseHeader]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Organisation]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Personnel]
GO
ALTER TABLE [dbo].[ExpenseHeader]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseHeader_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[ExpenseHeader] CHECK CONSTRAINT [FK_ExpenseHeader_Organisation]
GO
ALTER TABLE [dbo].[ExpenseProject]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseProject_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[ExpenseProject] CHECK CONSTRAINT [FK_ExpenseProject_Centre]
GO
ALTER TABLE [dbo].[ExpenseProject]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseProject_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[ExpenseProject] CHECK CONSTRAINT [FK_ExpenseProject_Organisation]
GO
ALTER TABLE [dbo].[FollowUpHistory]  WITH CHECK ADD  CONSTRAINT [FK_FollowUpHistory_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[FollowUpHistory] CHECK CONSTRAINT [FK_FollowUpHistory_Centre]
GO
ALTER TABLE [dbo].[FollowUpHistory]  WITH CHECK ADD  CONSTRAINT [FK_FollowUpHistory_FollowUp] FOREIGN KEY([FollowUpId])
REFERENCES [dbo].[FollowUp] ([FollowUpId])
GO
ALTER TABLE [dbo].[FollowUpHistory] CHECK CONSTRAINT [FK_FollowUpHistory_FollowUp]
GO
ALTER TABLE [dbo].[FollowUpHistory]  WITH CHECK ADD  CONSTRAINT [FK_FollowUpHistory_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[FollowUpHistory] CHECK CONSTRAINT [FK_FollowUpHistory_Organisation]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_Centre]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_ExpenseHeader] FOREIGN KEY([ExpenseHeaderId])
REFERENCES [dbo].[ExpenseHeader] ([ExpenseHeaderId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_ExpenseHeader]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_Organisation]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_PaymentMode] FOREIGN KEY([PaymentModeId])
REFERENCES [dbo].[PaymentMode] ([PaymentModeId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_PaymentMode]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_Personnel]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([ProjectId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_Project]
GO
ALTER TABLE [dbo].[OtherFee]  WITH CHECK ADD  CONSTRAINT [FK_OtherFee_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([VoucherId])
GO
ALTER TABLE [dbo].[OtherFee] CHECK CONSTRAINT [FK_OtherFee_Voucher]
GO
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Centre]
GO
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Organisation]
GO
ALTER TABLE [dbo].[PostEvent]  WITH CHECK ADD  CONSTRAINT [FK_Postevent_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[PostEvent] CHECK CONSTRAINT [FK_Postevent_Centre]
GO
ALTER TABLE [dbo].[PostEvent]  WITH CHECK ADD  CONSTRAINT [FK_Postevent_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[PostEvent] CHECK CONSTRAINT [FK_Postevent_Organisation]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Centre]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Organisation]
GO
ALTER TABLE [dbo].[RoomAvailable]  WITH CHECK ADD  CONSTRAINT [FK_RoomAvailable_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[RoomAvailable] CHECK CONSTRAINT [FK_RoomAvailable_Batch]
GO
ALTER TABLE [dbo].[RoomAvailable]  WITH CHECK ADD  CONSTRAINT [FK_RoomAvailable_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[RoomAvailable] CHECK CONSTRAINT [FK_RoomAvailable_Centre]
GO
ALTER TABLE [dbo].[RoomAvailable]  WITH CHECK ADD  CONSTRAINT [FK_RoomAvailable_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[RoomAvailable] CHECK CONSTRAINT [FK_RoomAvailable_Organisation]
GO
ALTER TABLE [dbo].[RoomAvailable]  WITH CHECK ADD  CONSTRAINT [FK_RoomAvailable_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[RoomAvailable] CHECK CONSTRAINT [FK_RoomAvailable_Room]
GO
ALTER TABLE [dbo].[Taluka]  WITH CHECK ADD  CONSTRAINT [FK_Taluka_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO
ALTER TABLE [dbo].[Taluka] CHECK CONSTRAINT [FK_Taluka_District]
GO
ALTER TABLE [dbo].[Taluka]  WITH CHECK ADD  CONSTRAINT [FK_Taluka_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Taluka] CHECK CONSTRAINT [FK_Taluka_Organisation]
GO
ALTER TABLE [dbo].[Taluka]  WITH CHECK ADD  CONSTRAINT [FK_Taluka_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[Taluka] CHECK CONSTRAINT [FK_Taluka_State]
GO
ALTER TABLE [dbo].[TrainerAvailable]  WITH CHECK ADD  CONSTRAINT [FK_TrainerAvailable_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[TrainerAvailable] CHECK CONSTRAINT [FK_TrainerAvailable_Batch]
GO
ALTER TABLE [dbo].[TrainerAvailable]  WITH CHECK ADD  CONSTRAINT [FK_TrainerAvailable_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[TrainerAvailable] CHECK CONSTRAINT [FK_TrainerAvailable_Centre]
GO
ALTER TABLE [dbo].[TrainerAvailable]  WITH CHECK ADD  CONSTRAINT [FK_TrainerAvailable_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[TrainerAvailable] CHECK CONSTRAINT [FK_TrainerAvailable_Organisation]
GO
ALTER TABLE [dbo].[TrainerAvailable]  WITH CHECK ADD  CONSTRAINT [FK_TrainerAvailable_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainer] ([TrainerId])
GO
ALTER TABLE [dbo].[TrainerAvailable] CHECK CONSTRAINT [FK_TrainerAvailable_Trainer]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Centre]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Organisation]
GO
/****** Object:  StoredProcedure [dbo].[SearchAdmission]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SearchAdmission]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	  CandidateName,
BatchName,
TotalFee,
PaidAmount,
PendingAmount,
AdmissionDate,
AdmissionId,
CentreName,
CentreId,
SearchField

	  FROM 
		[AdmissionSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END








GO
/****** Object:  StoredProcedure [dbo].[SearchCandidateFee]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SearchCandidateFee]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	  CandidateInstallmentId,
CandidateName,
BatchName,
TotalFee,
PaidAmount,
PendingAmount,
AdmissionDate,
AdmissionId,
CentreName,
CentreId,
	  SearchField

	  FROM 
		[CandidateFeeSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END









GO
/****** Object:  StoredProcedure [dbo].[SearchCandidateInstallment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[SearchCandidateInstallment]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	  CandidateInstallmentId,
CandidateName,
BatchName,
TotalFee,
PaidAmount,
PendingAmount,
AdmissionDate,
AdmissionId,
CentreName,
CentreId,
	  SearchField

	  FROM 
		[CandidateInstallmentSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END








GO
/****** Object:  StoredProcedure [dbo].[SearchCounselling]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SearchCounselling]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	CounsellingId,
      EnquiryId,
      Title,
      FirstName,
      MiddleName,
      LastName,
      CentreId,
      OrganisationId,
      PersonnelId,
      CourseOfferedId,
      PreferTiming,
      Remarks,
      FollowUpDate,
      RemarkByBranchManager,
      SectorId,
      PsychomatricTest,
      ConversionProspect,
      [Close],
      ClosingRemark,
      RemarkByBm,
      IsRegistrationDone,
	  SearchField

	  FROM 
		[CounsellingSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END






GO
/****** Object:  StoredProcedure [dbo].[SearchCourse]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SearchCourse]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT
	   CourseId,
      Name,
      OrganisationId,
      SectorId,
      SchemeId,
      CourseTypeId,
      Description,
      Duration,
	SearchField

	  FROM 
		[CourseSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END


GO
/****** Object:  StoredProcedure [dbo].[SearchCourseInstallment]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SearchCourseInstallment]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	  CourseInstallmentId,
      Name,
      CourseId,
      Fee,
      DownPayment,
      LumpsumAmount,
      OrganisationId,
      NumberOfInstallment,
      CentreId,
      CreatedDate,
	  SearchField

	  FROM 
		[CourseInstallmentSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END







GO
/****** Object:  StoredProcedure [dbo].[SearchEnquiry]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SearchEnquiry]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	EnquiryId,
      Title,
      FirstName,
      MiddleName,
      LastName,
      Mobile,
      AlternateMobile,
      EmailId,
	  DateOfBirth,
      Age,
      Address1,
      Address2,
      Address3,
      Address4,
      TalukaId,
      StateId,
      DistrictId,
      PinCode,
      GuardianName,
      GuardianContactNo,
      OccupationId,
      ReligionId,
      CasteCategoryId,
      Gender,
      EducationalQualificationId,
      YearOfPassOut,
      Marks,
      IntrestedCourseId,
      HowDidYouKnowAboutId,
      PreTrainingStatus,
      EmploymentStatus,
      Promotional,
      EnquiryDate,
      StudentCode,
      EnquiryStatus,
      EmployerName,
      EmployerContactNo,
      EmployerAddress,
      AnnualIncome,
      SchemeId,
      EnquiryTypeId,
      StudentTypeId,
      SectorId,
      BatchTimePreferId,
      AppearingQualification,
      YearOfExperience,
      PlacementNeeded,
      Remarks,
      FollowUpDate,
      PreferredMonthForJoining,
      [Close],
      ClosingRemark,
      ConversionProspect,
      OtherInterestedCourse,
      RemarkByBranchManager,
      IsCounsellingDone,
      IsRegistrationDone,
      IsAdmissionDone,
      CentreId,
      OrganisationId,
	  SearchField

	  FROM 
		[EnquirySearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END





GO
/****** Object:  StoredProcedure [dbo].[SearchFollowUp]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SearchFollowUp]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	FollowUpId,
      FollowUpDateTime,
      MobilizationId,
      EnquiryId,
      CounsellingId,
      RegistrationId,
      Title,
      FirstName,
      MiddleName,
      LastName,
      Remark,
      Closed,
      ReadDateTime,
      CreatedDateTime,
      OrganisationId,
      CentreId,
      Mobile,
      IntrestedCourseId,
      FollowUpType,
      AlternateMobile,
      FollowUpURL,
      [Close],
      ClosingRemark,
	  SearchField

	  FROM 
		[FollowUpSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END




GO
/****** Object:  StoredProcedure [dbo].[SearchMobilization]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SearchMobilization]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

		CentreId,
      CentreName,
      MobilizationId,
      EventName,
      CandidateName,
      Mobile,
      InterestedCourse,
      Qualification,
      CreatedDate,
      FollowUpDate,
      Remark,
      StudentLocation,
      OtherInterestedCourse,
      [Close],
      ClosingRemark,
	  SearchField

	  FROM 
		[MobilizationDataGrid]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END






GO
/****** Object:  StoredProcedure [dbo].[SearchPersonnel]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SearchPersonnel]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	   PersonnelId,
      OrganisationId,
      Title,
      Forenames,
      Surname,
      DOB,
      CountryId,
      Address1,
      Address2,
      Address3,
      Address4,
      Postcode,
      Telephone,
      Mobile,
      NINumber,
      BankAccountNumber,
      BankSortCode,
      BankAccountName,
      BankAddress1,
      BankAddress2,
      BankAddress3,
      BankAddress4,
      BankPostcode,
      BankTelephone,
      Email,
      CurrentEmploymentId,
      CentreId,
	  SearchField

	  FROM 
		[PersonnelSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END







GO
/****** Object:  StoredProcedure [dbo].[SearchRegistration]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SearchRegistration]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	RegistrationId,
      StudentCode,
      EnquiryId,
      CourseId,
      CourseInstallmentId,
      CandidateFeeId,
      CandidateInstallmentId,
      Remarks,
      FollowupDate,
      CentreId,
      OrganisationId,
      IsAdmissionDone,
      RegistrationDate,
	  SearchField

	  FROM 
		[RegistrationSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END







GO
/****** Object:  StoredProcedure [dbo].[SearchTrainer]    Script Date: 18/08/2017 06:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SearchTrainer]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')

	SELECT

	TrainerId,
      Title,
      FirstName,
      MiddleName,
      LastName,
      Address1,
      Address2,
      Address3,
      Address4,
      TalukaId,
      StateId,
      DistrictId,
      PinCode,
      Gender,
      AadharNo,
      Mobile,
      DateOfBirth,
      EmailId,
      Certified,
      CertificationNo,
      SectorId,
      CourseId,
      CentreId,
      OrganisationId,
      PersonnelId,
      CreatedDate,
	   SearchField

	  FROM 
		[TrainerSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END






GO
USE [master]
GO
ALTER DATABASE [ServerNidanERP] SET  READ_WRITE 
GO
