USE [master]
GO
/****** Object:  Database [Nidan_Dev]    Script Date: 26/02/2017 06:00:07 PM ******/
CREATE DATABASE [Nidan_Dev]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Nidan_Dev', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Nidan_Dev.mdf' , SIZE = 5312KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Nidan_Dev_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Nidan_Dev_log.ldf' , SIZE = 10368KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Nidan_Dev] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Nidan_Dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Nidan_Dev] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Nidan_Dev] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Nidan_Dev] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Nidan_Dev] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Nidan_Dev] SET ARITHABORT OFF 
GO
ALTER DATABASE [Nidan_Dev] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Nidan_Dev] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Nidan_Dev] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Nidan_Dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Nidan_Dev] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Nidan_Dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Nidan_Dev] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Nidan_Dev] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Nidan_Dev] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Nidan_Dev] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Nidan_Dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Nidan_Dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Nidan_Dev] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Nidan_Dev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Nidan_Dev] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Nidan_Dev] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Nidan_Dev] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Nidan_Dev] SET RECOVERY FULL 
GO
ALTER DATABASE [Nidan_Dev] SET  MULTI_USER 
GO
ALTER DATABASE [Nidan_Dev] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Nidan_Dev] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Nidan_Dev] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Nidan_Dev] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Nidan_Dev] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Nidan_Dev', N'ON'
GO
USE [Nidan_Dev]
GO
/****** Object:  User [IIS APPPOOL\Nidan]    Script Date: 26/02/2017 06:00:07 PM ******/
CREATE USER [IIS APPPOOL\Nidan] FOR LOGIN [IIS APPPOOL\Nidan] WITH DEFAULT_SCHEMA=[db_accessadmin]
GO
/****** Object:  Table [dbo].[Absence]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsenceDay]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsencePeriod]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsencePolicy]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsencePolicyEntitlement]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsencePolicyPeriod]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsenceStatus]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AbsenceType]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Admission]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Admission](
	[AdmissionId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryId] [int] NULL,
	[StudentCode] [varchar](1000) NULL,
	[Salutation] [varchar](50) NULL,
	[FirstName] [varchar](500) NULL,
	[MiddleName] [varchar](500) NULL,
	[LastName] [varchar](500) NULL,
	[Mobile] [bigint] NULL,
	[LandlineNo] [bigint] NULL,
	[EmailId] [varchar](500) NULL,
	[DateOfBirth] [date] NULL,
	[YearOfBirth] [int] NULL,
	[Gender] [varchar](100) NULL,
	[FatherName] [varchar](500) NULL,
	[FatherMobile] [bigint] NULL,
	[CasteCategoryId] [int] NULL,
	[ReligionId] [int] NULL,
	[Address] [varchar](max) NULL,
	[TalukaId] [int] NULL,
	[DistrictId] [int] NULL,
	[StateId] [int] NULL,
	[PinCode] [int] NULL,
	[CommunicationAddress] [varchar](max) NULL,
	[CommunicationTalukaId] [int] NULL,
	[CommunicationDistrictId] [int] NULL,
	[CommunicationStateId] [int] NULL,
	[CommunicationPinCode] [int] NULL,
	[DisabilityId] [int] NULL,
	[AadhaarNo] [bigint] NULL,
	[AadhaarVerificationStatus] [varchar](1000) NULL,
	[AlternateIdTypeId] [int] NULL,
	[AlternateIdNumber] [bigint] NULL,
	[NameAsInBank] [varchar](500) NULL,
	[BankAccountNo] [bigint] NULL,
	[IfscCode] [varchar](500) NULL,
	[BankName] [varchar](500) NULL,
	[QualificationId] [int] NULL,
	[ProfessionalQualification] [varchar](500) NULL,
	[TechnicalQualification] [varchar](500) NULL,
	[PreTrainingStatus] [varchar](100) NULL,
	[YearOfExperience] [int] NULL,
	[EmploymentStatus] [varchar](100) NULL,
	[EmployerName] [varchar](500) NULL,
	[EmployerContactNo] [bigint] NULL,
	[EmployerAddress] [varchar](max) NULL,
	[AnnualIncome] [bigint] NULL,
	[SchemeId] [int] NULL,
	[SchemeTypeId] [int] NULL,
	[TrainingType] [varchar](500) NULL,
	[SectorId] [int] NULL,
	[SubSectorId] [int] NULL,
	[WhereDidYouHearAboutTheSchemeId] [int] NULL,
	[ConveyanceAndBoardingPreference] [varchar](500) NULL,
	[CourseId] [int] NULL,
	[CourseFees] [int] NULL,
	[PaymentType] [varchar](100) NULL,
	[DurationOfCourse] [varchar](500) NULL,
	[CentreId] [int] NULL,
	[BatchId] [int] NULL,
	[OrganisationId] [int] NULL,
	[AdmissionDate] [date] NULL,
	[TcName] [varchar](500) NULL,
	[TcId] [varchar](500) NULL,
	[PartnerName] [varchar](500) NULL,
	[TcAddress] [varchar](max) NULL,
	[SdmsCandidateId] [varchar](500) NULL,
 CONSTRAINT [PK_Admission_1] PRIMARY KEY CLUSTERED 
(
	[AdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Alert]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AlternateIdType]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlternateIdType](
	[AlternateIdTypeId] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_AlternateIdType] PRIMARY KEY CLUSTERED 
(
	[AlternateIdTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AreaOfInterest]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsersAlertSchedule]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Batch]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Batch](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[SchemeId] [int] NOT NULL,
	[CentreId] [int] NULL,
	[OrganisationId] [int] NULL,
	[TrainingType] [varchar](500) NULL,
	[SectorId] [int] NOT NULL,
	[SubSector] [varchar](500) NULL,
	[CourseId] [int] NOT NULL,
	[TrainingHrsPerDay] [int] NOT NULL,
	[BatchStartDate] [date] NOT NULL,
	[BatchEndDate] [date] NOT NULL,
	[PreferredAssessmentDate] [date] NOT NULL,
	[PersonnelId] [int] NULL,
	[Remarks] [varchar](1000) NULL,
	[CreatedDate] [date] NULL,
 CONSTRAINT [PK_Batch_1] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BatchTimePrefer]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Building]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[CasteCategory]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Centre]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Centre](
	[CentreId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [varchar](100) NULL,
	[Name] [varchar](500) NULL,
	[OrganisationId] [int] NOT NULL,
	[Place] [varchar](500) NULL,
 CONSTRAINT [PK_Centre] PRIMARY KEY CLUSTERED 
(
	[CentreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Colour]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Company]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[CompanyBuilding]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Counselling]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Counselling](
	[CounsellingId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[CourseOfferedId] [int] NOT NULL,
	[PreferTiming] [varchar](500) NULL,
	[Remarks] [varchar](max) NULL,
	[FollowUpDate] [date] NULL,
	[RemarkByBranchManager] [varchar](max) NULL,
	[Name] [varchar](500) NULL,
	[SectorId] [int] NULL,
	[PsychomatricTest] [varchar](100) NULL,
 CONSTRAINT [PK_Counselling] PRIMARY KEY CLUSTERED 
(
	[CounsellingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Course]    Script Date: 26/02/2017 06:00:07 PM ******/
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
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Disability]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Disability](
	[DisabilityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Disability] PRIMARY KEY CLUSTERED 
(
	[DisabilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[District]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[District](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmergencyContact]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Employment]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[EmploymentDepartment]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[EmploymentTeam]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Enquiry]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Enquiry](
	[EnquiryId] [int] IDENTITY(1,1) NOT NULL,
	[CandidateName] [varchar](500) NOT NULL,
	[ContactNo] [bigint] NOT NULL,
	[EmailId] [varchar](500) NULL,
	[Age] [int] NOT NULL,
	[Address] [varchar](max) NOT NULL,
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
	[Place] [varchar](100) NOT NULL,
	[CounselledBy] [varchar](500) NOT NULL,
	[CourseOfferedId] [int] NOT NULL,
	[PreferTiming] [datetime] NULL,
	[Remarks] [varchar](max) NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[FollowUpDate] [date] NULL,
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
	[EnquiryFollowUpDate] [date] NULL,
	[PlacementNeeded] [varchar](100) NULL,
	[WhyEnquiryClosed] [varchar](max) NULL,
	[RemarkByBm] [varchar](max) NULL,
 CONSTRAINT [PK_Enquiry] PRIMARY KEY CLUSTERED 
(
	[EnquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EnquiryType]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Event]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
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
/****** Object:  Table [dbo].[FollowUp]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
 CONSTRAINT [PK_FollowUp] PRIMARY KEY CLUSTERED 
(
	[FollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Frequency]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Host]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[HowDidYouKnowAbout]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Mobilization]    Script Date: 26/02/2017 06:00:07 PM ******/
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
	[CentreId] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Mobile] [bigint] NOT NULL,
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
 CONSTRAINT [PK_Mobilization] PRIMARY KEY CLUSTERED 
(
	[MobilizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MobilizationType]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Occupation]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Organisation]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Personnel]    Script Date: 26/02/2017 06:00:07 PM ******/
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
	[Telephone] [varchar](15) NOT NULL,
	[Mobile] [varchar](15) NULL,
	[NINumber] [varchar](10) NOT NULL,
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
/****** Object:  Table [dbo].[PersonnelAbsenceEntitlement]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[PublicHoliday]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[PublicHolidayPolicy]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Qualification]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Religion]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[Scheme]    Script Date: 26/02/2017 06:00:07 PM ******/
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
	[SchemeTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Scheme] PRIMARY KEY CLUSTERED 
(
	[SchemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchemeType]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchemeType](
	[SchemeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_SchemeType] PRIMARY KEY CLUSTERED 
(
	[SchemeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sector]    Script Date: 26/02/2017 06:00:07 PM ******/
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
 CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
(
	[SectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Site]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[State]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentType]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[SubSector]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubSector](
	[SubSectorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[SectorId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_SubSector] PRIMARY KEY CLUSTERED 
(
	[SubSectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Taluka]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Taluka](
	[TalukaId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
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
/****** Object:  Table [dbo].[Team]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[WorkingPattern]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  Table [dbo].[WorkingPatternDay]    Script Date: 26/02/2017 06:00:07 PM ******/
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
/****** Object:  View [dbo].[AdmissionSearchField]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[AdmissionSearchField]
AS 
SELECT 
	  A.AdmissionId,
	  A.EnquiryId,
      A.StudentCode,
      A.Salutation,
      A.FirstName,
      A.MiddleName,
      A.LastName,
      A.Mobile,
      A.LandlineNo,
      A.EmailId,
      A.DateOfBirth,
      A.YearOfBirth,
      A.Gender,
      A.FatherName,
      A.FatherMobile,
      A.CasteCategoryId,
      A.ReligionId,
      A.Address,
      A.TalukaId,
      A.DistrictId,
      A.StateId,
      A.PinCode,
      A.CommunicationAddress,
      A.CommunicationTalukaId,
      A.CommunicationDistrictId,
      A.CommunicationStateId,
      A.CommunicationPinCode,
      A.DisabilityId,
      A.AadhaarNo,
      A.AadhaarVerificationStatus,
      A.AlternateIdTypeId,
      A.AlternateIdNumber,
      A.NameAsInBank,
      A.BankAccountNo,
      A.IfscCode,
      A.BankName,
      A.QualificationId,
      A.ProfessionalQualification,
      A.TechnicalQualification,
      A.PreTrainingStatus,
      A.YearOfExperience,
      A.EmploymentStatus,
      A.EmployerName,
      A.EmployerContactNo,
      A.EmployerAddress,
      A.AnnualIncome,
      A.SchemeId,
      A.SchemeTypeId,
      A.TrainingType,
      A.SectorId,
      A.SubSectorId,
      A.WhereDidYouHearAboutTheSchemeId,
      A.ConveyanceAndBoardingPreference,
      A.CourseId,
      A.CourseFees,
      A.PaymentType,
      A.DurationOfCourse,
      A.CentreId,
      A.BatchId,
      A.OrganisationId,
      A.AdmissionDate,
      A.TcName,
      A.TcId,
      A.PartnerName,
      A.TcAddress,
      A.SdmsCandidateId,
	  ISNULL(A.FirstName, '')+ISNULL(A.LastName, '')+ CONVERT(varchar, A.Mobile )+ISNULL(CC.Caste, '')+ISNULL(R.Name, '')
	  +ISNULL(T.Name, '')+ISNULL(D.Name, '')+ISNULL(S.Name, '')+CONVERT(varchar,A.AadhaarNo)+ISNULL(SC.Name, '')+ISNULL(SE.Name, '')
	  +ISNULL(A.WhereDidYouHearAboutTheSchemeId, '')
	  --+ISNULL(CO.Name, '')+ISNULL(C.Name, '')
	  -- + ISNULL(CONVERT(varchar,A.AdmissionDate, 101), '') 
	  -- + ISNULL(CONVERT(varchar,A.AdmissionDate, 103), '') 
	 --  + ISNULL(CONVERT(varchar,A.AdmissionDate, 105), '')
	  --  + ISNULL(CONVERT(varchar,A.AdmissionDate, 126), '')
	    +ISNULL(A.TcName, '')+ISNULL(A.StudentCode, '')AS SearchField
FROM
   
   Admission A WITH (NOLOCK) left join CasteCategory CC WITH (NOLOCK)
   on CC.CasteCategoryId=A.CasteCategoryId join Religion R WITH(NOLOCK)
   ON R.ReligionId=A.ReligionId JOIN Taluka T WITH (NOLOCK)
   ON T.TalukaId=A.TalukaId JOIN District D WITH (NOLOCK)
   ON D.DistrictId=A.DistrictId JOIN State S WITH (NOLOCK)
   ON S.StateId=A.StateId JOIN Qualification Q WITH (NOLOCK)
   ON Q.QualificationId=A.QualificationId JOIN SchemeType ST WITH(NOLOCK)
   ON ST.SchemeTypeId=A.SchemeTypeId JOIN Centre C WITH(NOLOCK)
   ON C.CentreId=A.CentreId JOIN Scheme SC WITH (NOLOCK)
   ON SC.SchemeId=A.SchemeId JOIN Sector SE WITH (NOLOCK)
   ON SE.SectorId=A.SectorId JOIN Course CO WITH (NOLOCK)
   ON CO.CourseId=A.CourseId




GO
/****** Object:  View [dbo].[CounsellingSearchField]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[CounsellingSearchField]
AS 
SELECT 

	   C.CounsellingId,
      C.EnquiryId,
      C.CentreId,
      C.OrganisationId,
      C.PersonnelId,
      C.CourseOfferedId,
      C.PreferTiming,
      C.Remarks,
      C.FollowUpDate,
      C.RemarkByBranchManager,
      C.Name,
      C.SectorId,
      C.PsychomatricTest,
	  ISNULL(C.Name, '')+ISNULL(E.CandidateName, '')+ISNULL(C.PersonnelId, '')+ISNULL(CO.Name, '')+ISNULL(CONVERT(varchar,C.FollowUpDate, 101), '') 
	  + ISNULL(CONVERT(varchar,C.FollowUpDate, 103), '') + ISNULL(CONVERT(varchar,C.FollowUpDate, 105), '') 
	  + ISNULL(CONVERT(varchar,C.FollowUpDate, 126), '')+ISNULL(S.Name, '')+ISNULL(C.PsychomatricTest, '')AS SearchField

	FROM Counselling C WITH (NOLOCK) left join Enquiry E WITH (NOLOCK)
	on C.EnquiryId = E.EnquiryId join Course CO WITH (NOLOCK)
	on C.CourseOfferedId=CO.CourseId join Sector S WITH (NOLOCK)
	on C.SectorId=S.SectorId join Personnel P WITH (NOLOCK)
	ON C.PersonnelId = P.PersonnelId




GO
/****** Object:  View [dbo].[EnquirySearchField]    Script Date: 26/02/2017 06:00:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[EnquirySearchField]
AS 
SELECT 
E.EnquiryId,
E.CandidateName,
E.ContactNo,
E.EmailId,
E.Age,
E.Address,
E.GuardianName,
E.GuardianContactNo,
E.OccupationId,
E.ReligionId,
E.CasteCategoryId,
E.Gender,
E.EducationalQualificationId,
E.YearOFPassOut,
E.Marks,
E.IntrestedCourseId,
E.HowDidYouKnowAboutId,
E.PreTrainingStatus,
E.EmploymentStatus,
E.Promotional,
E.EnquiryDate,
E.Place,
E.CounselledBy,
E.CourseOfferedId,
E.PreferTiming,
E.Remarks,
E.CentreId,
E.OrganisationId,
E.FollowUpDate,
E.EnquiryStatus,
		ISNULL(E.CandidateName, '')+ISNULL(E.CounselledBy, '')+ISNULL(E.EmailId, '')+ISNULL(E.Address, '')+ISNULL(R.Name, '') + ISNULL(CONVERT(varchar,E.EnquiryDate, 101), '') + ISNULL(CONVERT(varchar,E.EnquiryDate, 103), '') + ISNULL(CONVERT(varchar,E.EnquiryDate, 105), '') + ISNULL(CONVERT(varchar,E.EnquiryDate, 126), '') + ISNULL(Q.Name, '') + ISNULL(E.Place, '') + CONVERT(varchar, E.ContactNo ) AS SearchField
FROM 
	Enquiry E  WITH (NOLOCK) left join Religion  R WITH (NOLOCK)
	on E.ReligionId = R.ReligionId join Course C WITH (NOLOCK)
	ON C.CourseId = E.IntrestedCourseId join Qualification Q WITH (NOLOCK)
	ON Q.QualificationId = E.EducationalQualificationId


GO
/****** Object:  View [dbo].[MobilizationSearchField]    Script Date: 26/02/2017 06:00:07 PM ******/
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
		M.Name,
		M.Mobile,
		M.InterestedCourseId,
		M.QualificationId,
		M.CreatedDate,
		M.FollowUpDate,
		M.Remark,
		M.MobilizerStatus,
		p.Forenames[Mobilized By],
		M.StudentLocation,
		M.OtherInterestedCourse,
		M.GeneratedDate,
		ISNULL(M.Name, '')+ISNULL(E.Name, '')+ISNULL(C.Name, '')+ISNULL(p.Forenames, '')+ISNULL(M.StudentLocation, '') + ISNULL(CONVERT(varchar,M.GeneratedDate, 101), '') + ISNULL(CONVERT(varchar,GeneratedDate, 103), '') + ISNULL(CONVERT(varchar,GeneratedDate, 105), '') + ISNULL(CONVERT(varchar,GeneratedDate, 126), '') + ISNULL(StudentLocation, '') + ISNULL(Q.Name, '') + CONVERT(varchar, m.Mobile ) AS SearchField
FROM 
	Mobilization M  WITH (NOLOCK) left join Event E WITH (NOLOCK)
	on M.Eventid = E.EventId join Course C WITH (NOLOCK)
	ON C.CourseId = m.InterestedCourseId join Qualification Q WITH (NOLOCK)
	ON Q.QualificationId = M.QualificationId join Personnel P WITH (NOLOCK)
	on m.PersonnelId=p.PersonnelId




GO
/****** Object:  View [dbo].[PersonnelSearchField]    Script Date: 26/02/2017 06:00:07 PM ******/
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
	
		ISNULL(Surname, '') + ISNULL(Forenames, '') + ISNULL(Surname, '') + ISNULL(CONVERT(varchar,DOB, 101), '') + ISNULL(CONVERT(varchar,DOB, 103), '') + ISNULL(CONVERT(varchar,DOB, 105), '') + ISNULL(CONVERT(varchar,DOB, 126), '') + ISNULL(EMail, '') + ISNULL(Postcode, '') + ISNULL(Mobile, '') AS SearchField
FROM 
	Personnel  WITH (NOLOCK)



GO
/****** Object:  View [dbo].[UserAuthorisationFilter]    Script Date: 26/02/2017 06:00:07 PM ******/
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
SET IDENTITY_INSERT [dbo].[Absence] ON 

INSERT [dbo].[Absence] ([AbsenceId], [OrganisationId], [PersonnelAbsenceEntitlementId], [AbsenceTypeId], [AbsenceStatusId], [AbsenceStatusByUser], [AbsenceStatusDateTimeUtc], [Description], [ReturnToWorkCompleted], [DoctorsNoteSupplied]) VALUES (65, 4, 1, 3, 1, N'baa66088-ea1b-43ab-b8b0-ee75523e613a', CAST(N'2017-01-04 14:49:09.9854584' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Absence] ([AbsenceId], [OrganisationId], [PersonnelAbsenceEntitlementId], [AbsenceTypeId], [AbsenceStatusId], [AbsenceStatusByUser], [AbsenceStatusDateTimeUtc], [Description], [ReturnToWorkCompleted], [DoctorsNoteSupplied]) VALUES (69, 4, 47, 3, 1, N'baa66088-ea1b-43ab-b8b0-ee75523e613a', CAST(N'2017-01-06 10:29:42.0016821' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Absence] ([AbsenceId], [OrganisationId], [PersonnelAbsenceEntitlementId], [AbsenceTypeId], [AbsenceStatusId], [AbsenceStatusByUser], [AbsenceStatusDateTimeUtc], [Description], [ReturnToWorkCompleted], [DoctorsNoteSupplied]) VALUES (71, 4, 43, 3, 1, N'baa66088-ea1b-43ab-b8b0-ee75523e613a', CAST(N'2017-01-06 10:39:14.5242644' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Absence] ([AbsenceId], [OrganisationId], [PersonnelAbsenceEntitlementId], [AbsenceTypeId], [AbsenceStatusId], [AbsenceStatusByUser], [AbsenceStatusDateTimeUtc], [Description], [ReturnToWorkCompleted], [DoctorsNoteSupplied]) VALUES (72, 4, 43, 3, 1, N'baa66088-ea1b-43ab-b8b0-ee75523e613a', CAST(N'2017-01-06 10:41:31.5815424' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Absence] ([AbsenceId], [OrganisationId], [PersonnelAbsenceEntitlementId], [AbsenceTypeId], [AbsenceStatusId], [AbsenceStatusByUser], [AbsenceStatusDateTimeUtc], [Description], [ReturnToWorkCompleted], [DoctorsNoteSupplied]) VALUES (73, 4, 43, 3, 1, N'baa66088-ea1b-43ab-b8b0-ee75523e613a', CAST(N'2017-01-06 10:48:45.7573640' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Absence] ([AbsenceId], [OrganisationId], [PersonnelAbsenceEntitlementId], [AbsenceTypeId], [AbsenceStatusId], [AbsenceStatusByUser], [AbsenceStatusDateTimeUtc], [Description], [ReturnToWorkCompleted], [DoctorsNoteSupplied]) VALUES (75, 4, 45, 2, 1, N'baa66088-ea1b-43ab-b8b0-ee75523e613a', CAST(N'2017-01-06 12:00:05.8472758' AS DateTime2), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Absence] OFF
SET IDENTITY_INSERT [dbo].[AbsenceDay] ON 

INSERT [dbo].[AbsenceDay] ([AbsenceDayId], [OrganisationId], [AbsenceId], [Date], [AM], [PM], [Duration]) VALUES (85, 4, 65, CAST(N'2017-01-04' AS Date), 1, 1, 1)
INSERT [dbo].[AbsenceDay] ([AbsenceDayId], [OrganisationId], [AbsenceId], [Date], [AM], [PM], [Duration]) VALUES (89, 4, 69, CAST(N'2017-04-11' AS Date), 1, 1, 1)
INSERT [dbo].[AbsenceDay] ([AbsenceDayId], [OrganisationId], [AbsenceId], [Date], [AM], [PM], [Duration]) VALUES (91, 4, 71, CAST(N'2017-01-16' AS Date), 1, 1, 1)
INSERT [dbo].[AbsenceDay] ([AbsenceDayId], [OrganisationId], [AbsenceId], [Date], [AM], [PM], [Duration]) VALUES (92, 4, 72, CAST(N'2017-01-09' AS Date), 1, 1, 1)
INSERT [dbo].[AbsenceDay] ([AbsenceDayId], [OrganisationId], [AbsenceId], [Date], [AM], [PM], [Duration]) VALUES (93, 4, 73, CAST(N'2017-01-10' AS Date), 1, 1, 1)
INSERT [dbo].[AbsenceDay] ([AbsenceDayId], [OrganisationId], [AbsenceId], [Date], [AM], [PM], [Duration]) VALUES (95, 4, 75, CAST(N'2017-01-11' AS Date), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[AbsenceDay] OFF
SET IDENTITY_INSERT [dbo].[AbsencePeriod] ON 

INSERT [dbo].[AbsencePeriod] ([AbsencePeriodId], [StartDate], [EndDate], [OrganisationId]) VALUES (18, CAST(N'2016-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-03-31 00:00:00.0000000' AS DateTime2), 4)
INSERT [dbo].[AbsencePeriod] ([AbsencePeriodId], [StartDate], [EndDate], [OrganisationId]) VALUES (19, CAST(N'2017-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2018-03-31 00:00:00.0000000' AS DateTime2), 4)
INSERT [dbo].[AbsencePeriod] ([AbsencePeriodId], [StartDate], [EndDate], [OrganisationId]) VALUES (20, CAST(N'2016-01-01 00:00:00.0000000' AS DateTime2), CAST(N'2016-12-31 00:00:00.0000000' AS DateTime2), 4)
SET IDENTITY_INSERT [dbo].[AbsencePeriod] OFF
SET IDENTITY_INSERT [dbo].[AbsencePolicy] ON 

INSERT [dbo].[AbsencePolicy] ([AbsencePolicyId], [OrganisationId], [Name], [WorkingPatternId]) VALUES (11, 4, N'Permanent', 39)
INSERT [dbo].[AbsencePolicy] ([AbsencePolicyId], [OrganisationId], [Name], [WorkingPatternId]) VALUES (36, 4, N'General', 57)
INSERT [dbo].[AbsencePolicy] ([AbsencePolicyId], [OrganisationId], [Name], [WorkingPatternId]) VALUES (38, 4, N'Test General', 56)
SET IDENTITY_INSERT [dbo].[AbsencePolicy] OFF
SET IDENTITY_INSERT [dbo].[AbsencePolicyEntitlement] ON 

INSERT [dbo].[AbsencePolicyEntitlement] ([AbsencePolicyEntitlementId], [OrganisationId], [AbsenceTypeId], [FrequencyId], [Entitlement], [MaximumCarryForward], [IsUnplanned], [IsPaid], [AbsencePolicyId], [HasEntitlement]) VALUES (16, 4, 3, 1, 10, 4, 1, 1, 11, 1)
INSERT [dbo].[AbsencePolicyEntitlement] ([AbsencePolicyEntitlementId], [OrganisationId], [AbsenceTypeId], [FrequencyId], [Entitlement], [MaximumCarryForward], [IsUnplanned], [IsPaid], [AbsencePolicyId], [HasEntitlement]) VALUES (33, 4, 2, 2, 10, 5, 0, 0, 11, 1)
INSERT [dbo].[AbsencePolicyEntitlement] ([AbsencePolicyEntitlementId], [OrganisationId], [AbsenceTypeId], [FrequencyId], [Entitlement], [MaximumCarryForward], [IsUnplanned], [IsPaid], [AbsencePolicyId], [HasEntitlement]) VALUES (40, 4, 3, 1, 15, 7, 0, 1, 38, 1)
INSERT [dbo].[AbsencePolicyEntitlement] ([AbsencePolicyEntitlementId], [OrganisationId], [AbsenceTypeId], [FrequencyId], [Entitlement], [MaximumCarryForward], [IsUnplanned], [IsPaid], [AbsencePolicyId], [HasEntitlement]) VALUES (46, 4, 3, 1, 12, 8, 1, 1, 36, 1)
INSERT [dbo].[AbsencePolicyEntitlement] ([AbsencePolicyEntitlementId], [OrganisationId], [AbsenceTypeId], [FrequencyId], [Entitlement], [MaximumCarryForward], [IsUnplanned], [IsPaid], [AbsencePolicyId], [HasEntitlement]) VALUES (50, 4, 2, 2, 3, 2, 1, 1, 36, 1)
SET IDENTITY_INSERT [dbo].[AbsencePolicyEntitlement] OFF
SET IDENTITY_INSERT [dbo].[AbsencePolicyPeriod] ON 

INSERT [dbo].[AbsencePolicyPeriod] ([AbsencePolicyPeriodId], [OrganisationId], [AbsencePolicyId], [AbsencePeriodId]) VALUES (15, 4, 11, 20)
INSERT [dbo].[AbsencePolicyPeriod] ([AbsencePolicyPeriodId], [OrganisationId], [AbsencePolicyId], [AbsencePeriodId]) VALUES (29, 4, 38, 18)
INSERT [dbo].[AbsencePolicyPeriod] ([AbsencePolicyPeriodId], [OrganisationId], [AbsencePolicyId], [AbsencePeriodId]) VALUES (44, 4, 36, 18)
INSERT [dbo].[AbsencePolicyPeriod] ([AbsencePolicyPeriodId], [OrganisationId], [AbsencePolicyId], [AbsencePeriodId]) VALUES (45, 4, 36, 19)
SET IDENTITY_INSERT [dbo].[AbsencePolicyPeriod] OFF
SET IDENTITY_INSERT [dbo].[AbsenceStatus] ON 

INSERT [dbo].[AbsenceStatus] ([AbsenceStatusId], [Name]) VALUES (1, N'Unapproved')
INSERT [dbo].[AbsenceStatus] ([AbsenceStatusId], [Name]) VALUES (2, N'Approved')
INSERT [dbo].[AbsenceStatus] ([AbsenceStatusId], [Name]) VALUES (3, N'Declined')
SET IDENTITY_INSERT [dbo].[AbsenceStatus] OFF
SET IDENTITY_INSERT [dbo].[AbsenceType] ON 

INSERT [dbo].[AbsenceType] ([AbsenceTypeId], [OrganisationId], [Name], [ColourId]) VALUES (2, 4, N'Casual', 9)
INSERT [dbo].[AbsenceType] ([AbsenceTypeId], [OrganisationId], [Name], [ColourId]) VALUES (3, 4, N'Earned', 27)
INSERT [dbo].[AbsenceType] ([AbsenceTypeId], [OrganisationId], [Name], [ColourId]) VALUES (4, 4, N'Doctor''s Appointment', 27)
SET IDENTITY_INSERT [dbo].[AbsenceType] OFF
SET IDENTITY_INSERT [dbo].[Admission] ON 

INSERT [dbo].[Admission] ([AdmissionId], [EnquiryId], [StudentCode], [Salutation], [FirstName], [MiddleName], [LastName], [Mobile], [LandlineNo], [EmailId], [DateOfBirth], [YearOfBirth], [Gender], [FatherName], [FatherMobile], [CasteCategoryId], [ReligionId], [Address], [TalukaId], [DistrictId], [StateId], [PinCode], [CommunicationAddress], [CommunicationTalukaId], [CommunicationDistrictId], [CommunicationStateId], [CommunicationPinCode], [DisabilityId], [AadhaarNo], [AadhaarVerificationStatus], [AlternateIdTypeId], [AlternateIdNumber], [NameAsInBank], [BankAccountNo], [IfscCode], [BankName], [QualificationId], [ProfessionalQualification], [TechnicalQualification], [PreTrainingStatus], [YearOfExperience], [EmploymentStatus], [EmployerName], [EmployerContactNo], [EmployerAddress], [AnnualIncome], [SchemeId], [SchemeTypeId], [TrainingType], [SectorId], [SubSectorId], [WhereDidYouHearAboutTheSchemeId], [ConveyanceAndBoardingPreference], [CourseId], [CourseFees], [PaymentType], [DurationOfCourse], [CentreId], [BatchId], [OrganisationId], [AdmissionDate], [TcName], [TcId], [PartnerName], [TcAddress], [SdmsCandidateId]) VALUES (2, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-02-26' AS Date), NULL, NULL, NULL, NULL, 1, 1, NULL, 1, 1, 1, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 1, NULL, 4, CAST(N'2017-02-26' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Admission] ([AdmissionId], [EnquiryId], [StudentCode], [Salutation], [FirstName], [MiddleName], [LastName], [Mobile], [LandlineNo], [EmailId], [DateOfBirth], [YearOfBirth], [Gender], [FatherName], [FatherMobile], [CasteCategoryId], [ReligionId], [Address], [TalukaId], [DistrictId], [StateId], [PinCode], [CommunicationAddress], [CommunicationTalukaId], [CommunicationDistrictId], [CommunicationStateId], [CommunicationPinCode], [DisabilityId], [AadhaarNo], [AadhaarVerificationStatus], [AlternateIdTypeId], [AlternateIdNumber], [NameAsInBank], [BankAccountNo], [IfscCode], [BankName], [QualificationId], [ProfessionalQualification], [TechnicalQualification], [PreTrainingStatus], [YearOfExperience], [EmploymentStatus], [EmployerName], [EmployerContactNo], [EmployerAddress], [AnnualIncome], [SchemeId], [SchemeTypeId], [TrainingType], [SectorId], [SubSectorId], [WhereDidYouHearAboutTheSchemeId], [ConveyanceAndBoardingPreference], [CourseId], [CourseFees], [PaymentType], [DurationOfCourse], [CentreId], [BatchId], [OrganisationId], [AdmissionDate], [TcName], [TcId], [PartnerName], [TcAddress], [SdmsCandidateId]) VALUES (3, 7, NULL, N'Miss', N'Shraddha', N'Vijay', N'Paradkar', 9773606038, NULL, N'paradkarsh24@gmail.com', CAST(N'1993-11-24' AS Date), NULL, N'Female', N'Vijay Paradkar', 9920723597, 5, 1, N'Koperkhairane', 116, 125, 15, NULL, N'Koperkhairane', 1, 1, 15, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, N'Professional', N'Technical', N'Experience', 4, N'Employed', N'Nidan Technologies Pvt Ltd', 7894561234, N'Belapur', 200000, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, NULL, NULL, NULL, 1, NULL, 4, CAST(N'2017-02-26' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Admission] ([AdmissionId], [EnquiryId], [StudentCode], [Salutation], [FirstName], [MiddleName], [LastName], [Mobile], [LandlineNo], [EmailId], [DateOfBirth], [YearOfBirth], [Gender], [FatherName], [FatherMobile], [CasteCategoryId], [ReligionId], [Address], [TalukaId], [DistrictId], [StateId], [PinCode], [CommunicationAddress], [CommunicationTalukaId], [CommunicationDistrictId], [CommunicationStateId], [CommunicationPinCode], [DisabilityId], [AadhaarNo], [AadhaarVerificationStatus], [AlternateIdTypeId], [AlternateIdNumber], [NameAsInBank], [BankAccountNo], [IfscCode], [BankName], [QualificationId], [ProfessionalQualification], [TechnicalQualification], [PreTrainingStatus], [YearOfExperience], [EmploymentStatus], [EmployerName], [EmployerContactNo], [EmployerAddress], [AnnualIncome], [SchemeId], [SchemeTypeId], [TrainingType], [SectorId], [SubSectorId], [WhereDidYouHearAboutTheSchemeId], [ConveyanceAndBoardingPreference], [CourseId], [CourseFees], [PaymentType], [DurationOfCourse], [CentreId], [BatchId], [OrganisationId], [AdmissionDate], [TcName], [TcId], [PartnerName], [TcAddress], [SdmsCandidateId]) VALUES (1002, 7, NULL, N'MR', N'Vijay', N'Nanasaheb', N'Raut', 9322992324, NULL, N'vijayraut33@gmail.com', CAST(N'1993-01-27' AS Date), NULL, N'Male', N'Nanasaheb Raut', 9773606038, 3, 1, N'KALEKAR CHAWL ONGC BLDG,12/13/3 FLR,DHARAVI,  SANT ROHIDAS MARG', 1, 1, 15, 400017, N'KALEKAR CHAWL ONGC BLDG,12/13/3 FLR,DHARAVI,  SANT ROHIDAS MARG', 1, 1, 15, 400017, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, N'Professional', N'Technical', N'Fresher', 0, N'UnEmployed', NULL, NULL, NULL, NULL, 1, 1, NULL, 2, NULL, NULL, NULL, 2, NULL, NULL, NULL, 1, NULL, 4, CAST(N'2017-02-26' AS Date), NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Admission] OFF
SET IDENTITY_INSERT [dbo].[AreaOfInterest] ON 

INSERT [dbo].[AreaOfInterest] ([AreaOfInterestId], [Name], [OrganisationId]) VALUES (0, N'IT', 4)
INSERT [dbo].[AreaOfInterest] ([AreaOfInterestId], [Name], [OrganisationId]) VALUES (1, N'ITEs', 4)
SET IDENTITY_INSERT [dbo].[AreaOfInterest] OFF
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'1', N'SuperAdmin', N'ApplicationRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'2', N'Admin', N'ApplicationRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'3', N'User', N'ApplicationRole')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'01b40bcf-c599-473c-854c-4effa911ec1e', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'032c1e43-fba7-47f8-a477-9a09dcf82daa', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'04ee5f8b-e018-4a17-aac9-b6019cfce276', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'06586b59-4429-436d-9b26-800d880483df', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0a6b1e2b-4ac3-414e-8738-9e5a4d9b6e36', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0b23dc04-f364-45ba-b98e-fb0a49f94656', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0f13e616-a95a-47a5-9995-60cbc592c3d1', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0f7a08f6-1a5a-4319-a5fd-1f2fa719634d', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'10f5e02b-c762-4565-8b2d-0f43aa081319', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'116fc575-4a86-4f13-b997-fe9f32d305ff', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1700c28e-d342-4959-bb7d-641d66f6719f', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1b4ec15b-abff-46c0-a029-df405b2d4ffb', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1ca4f2bf-dd06-44a4-aa09-2b4dcf5b4a79', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1f2db86c-81fb-4eec-9d5f-0c780eafb590', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'20c83ae9-a47c-4122-8b00-2a14e98db983', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'246e2f76-68bd-486a-8468-c54851c87541', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'24b0530f-ce19-4004-be38-a6cc1533e322', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2992313b-87d5-4229-8c3a-6578d222e293', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2ac20a85-a6dc-4429-92e1-15b00f08adb8', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2b9a1443-fa62-4cb0-aec7-b7b5770436ac', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2c4ebd78-f541-440c-b9de-fad81a0f5a98', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2d921f72-b892-4cca-b294-210592790854', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2f4ebcc1-5ff9-4f9f-a31d-66c47aee5894', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2fd4cd69-4a25-46e0-af8c-2ed5d28f610f', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'30ee0b23-785a-4d06-a95d-0f7b6d9ce31f', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'316f648b-6ca2-4d98-9f93-c7afdd943789', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'320ac667-ee90-48a2-9b53-fb9c10ec0d61', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'33bb0ae7-2832-4072-8622-fec97b97b71b', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'368eb845-84c0-4b41-a3fe-4b8e17566012', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'38279ad7-91c8-4968-932c-0a5a05ab495c', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3deed64b-69ec-49fc-905f-a73cd5582f3c', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'40ee8af2-4d7c-4a93-af2b-118d78db6b81', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'424193a9-7b3f-43cc-9806-b27db02c5db4', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'431aa089-ac08-48d6-b671-e1087f85bcb8', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4741b301-d9dc-4c8e-b029-a8688a0a0308', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'475e52f8-bb3b-4834-ae84-17842b12b0c8', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4836e102-343b-4c9f-ba96-00461f4893f3', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'491e3deb-c570-47f1-aec9-b649ac239db2', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4cdce9f5-89a3-4680-a211-1c0913bf58bf', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'50b3a684-5599-4a40-9f0f-47a5c37e8e15', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'52abb99d-d48a-4d32-ba55-bbb89bece8de', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'552b29ce-8872-4f8c-b8dd-747ae4333e4e', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'55a1f00f-cd0b-491f-b04b-57803b0db161', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'58bfb3ef-4949-4d43-8c89-a1409d16a08a', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'594e3477-339d-4749-8b89-58d75936a640', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5f4dd0b1-b3e8-4203-bc10-1df868bf5963', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'648b8178-35bf-4223-8ebf-7649977683c7', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6ef120ae-6041-4314-86ee-884093da1a68', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'777e2ec6-d74a-4707-a1ef-16c96f73248d', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'77ec2e78-ed30-4b70-8fdf-7498a3f85492', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7d585050-8a4a-4d9a-960b-2228875ddc46', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8157d325-2c95-4811-82b3-b215fd49afa4', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'817cad52-7b2e-4bf3-994d-2c9c91e03cd2', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'81c6963a-e6b6-40f2-8ff7-ab17a033d95f', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'87731cf8-8a27-4efe-8f6a-7ed3b2b5f255', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'87856032-423b-4815-aeb8-69b7e4683a22', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88450232-24c3-4cba-919b-7c1c4c64f4f2', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8acf06ba-e517-4b6e-abcb-359024cc2ad9', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9126ad61-924c-4ec3-8eaf-05386fb9a999', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9349a8cd-5b5d-4ab5-80bf-9b178a0629cd', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'995a714c-8876-41ab-8e48-1c79b01d459f', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9b923fee-8028-493a-b54d-666fb6397a75', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9bb0610e-788e-4dfc-93b6-82af4c016765', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a4110fcf-efe0-4055-986e-02e1d2affbc4', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a4a988b5-e38b-4c70-a20b-bed97a8db4cb', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a79cb6fc-1cba-4f32-96f9-e1e4e9890ee5', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'aab6fc24-4f57-4de7-9979-3b790eef72c9', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ae397432-e4aa-44a6-80a6-dc9bea2a251e', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'aed476a0-3739-44a0-bd2f-b11a3287689c', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'afcf5153-7257-4d0e-9095-2d2286c0abbe', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'afdca75e-d51d-410b-abfc-a3fb0b61a198', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b38dc1bf-0691-4edf-9428-7b205a07f80d', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b7ca56c6-e0b1-4d0c-8657-01ee818a377f', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'baa66088-ea1b-43ab-b8b0-ee75523e613a', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bb141879-1336-4a91-8640-506a9d1f71bf', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bbe75a61-9216-4c97-8e5c-2a85ec3b6885', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c1a636b0-4939-4da5-8e76-bbb2c876d82b', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c2971d39-34ae-4165-89dc-78fedde57f81', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c42d62c2-1c0b-4f32-8c0f-c13ab60e3073', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c7ef57ec-c373-49bc-bbcb-f677b6dea483', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ca1a33e0-4455-484c-a494-2d6ee9921359', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ccd7d6ea-f514-40a8-9530-dd7744c5706c', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cd588053-3088-4ad2-b26d-22b0822fe855', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cded29f3-f32f-4143-86b9-30cce601d2db', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ceddcad2-e6ab-4046-a109-11c1c8b5ffa3', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd9ceb6d0-4ade-4d87-ae3c-c829206620d5', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'db5f88be-4018-4647-8776-01e9cd07753a', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e2ae65fc-51f3-4d01-8c0c-063c4abd0da2', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e3482627-f15f-4d8d-b347-572e78e41dae', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ea78e966-60fc-42d3-8fd2-68e826d1c56d', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f1e97402-9a85-4dbc-8919-fcaf4318f7c7', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f6295d2a-12ee-4d1e-ab0b-707716a24d6a', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f72d0864-d72f-468b-940c-1a3deb3b3250', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fa4c3ca7-d0d6-4c09-9dd7-22af3c5d6abe', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fbe3dde3-c2da-4337-9667-0d363bfac5c5', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fdd6d9a2-1d33-4e76-a4e5-7988effb6d81', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fe861a67-db8e-401f-95f3-9142300a7e84', N'1')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'fe861a67-db8e-401f-95f3-9142300a7e84', N'superadmin@itsupportlimited.com', 1, N'AFAer9GyepjQA5csPi4JDIGT/8xTLOOISoUgP76GyRR4ovNixtJPGeYPJ4aoWAXhqw==', N'dac1f363-20c6-4593-a998-f35725ba23c9', NULL, 0, 0, NULL, 0, 0, N'superadmin@itsupportlimited.com', NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'f6295d2a-12ee-4d1e-ab0b-707716a24d6a', N'admin1@itsupportlimited.com', 1, N'AKx/BlAAgt0InFnzYZGtOlEmZLDyeRPCG8dT9pQF6EHLo5tBitvfQVUdE27RXmiZ6Q==', N'62873593-517e-4f95-9f64-0490e940528a', NULL, 0, 0, NULL, 0, 0, N'admin1@itsupportlimited.com', 1, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'24b0530f-ce19-4004-be38-a6cc1533e322', N'admin2@itsupportlimited.com', 1, N'ACVfIh0mQ5oH9pdcSc6ERNl7qQCkqRoD7QRygwJbfypaJn0kgM1gT14pb94Zimmi2g==', N'e34fdba4-6bec-417f-923f-8d95dda829b2', NULL, 0, 0, NULL, 0, 0, N'admin2@itsupportlimited.com', 2, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'afcf5153-7257-4d0e-9095-2d2286c0abbe', N'employee@itsupportlimited.com', 1, N'APKaRU8mmaEDb4qhGZtULX9+BL7P3egWUXbAmBLNaWdRzsJT0CIk+heD4A7sWhp/sw==', N'cf421afd-9c38-457c-a6af-c521e1d602d9', NULL, 0, 0, NULL, 0, 0, N'employee@itsupportlimited.com', NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'81c6963a-e6b6-40f2-8ff7-ab17a033d95f', N'admindevuk@itsupportlimited.com', 1, N'AOZ7eoPANrypxjGJqhmnnz6qbbgFbu4Jv1Wi2hvT+v1R6xkP9cvS5PVG6AdYH3f+ow==', N'87edef0d-7230-4a5e-81a5-00e45867c9b7', NULL, 0, 0, NULL, 0, 0, N'admindevuk@itsupportlimited.com', 3, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'baa66088-ea1b-43ab-b8b0-ee75523e613a', N'admindevmumbai@nidantech.com', 1, N'AAmuYdVcwfyvri+pKSdq1fDhPzxrHGR4xNVNXO2+E6lff1YWbpYHSIPVe4zcEjYSxw==', N'2638a07d-1d70-4d9d-aea8-9fa592ebd519', NULL, 0, 0, NULL, 0, 0, N'admindevmumbai@nidantech.com', 4, 20, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'cded29f3-f32f-4143-86b9-30cce601d2db', N'boss@hr.com', 0, N'AH0vO+3MP8kkdVm8IoZS19v5APLypj2uwHMLSBA1Xg+cxyk8G/S3aCz/fdoJx9199Q==', N'88943326-3aac-4de3-9952-aef532cb151b', NULL, 0, 0, NULL, 1, 0, N'boss@hr.com', 1, 3, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'4836e102-343b-4c9f-ba96-00461f4893f3', N'85e8ad13-c28d-480c-992f-78e1abdbc9d2@hr.com', 0, N'ACW3/ceaJjW9e/oT2nxn9v/Q/FQLglKBTSEXAEf6GtbX831WzerA27uYY/arFe1GPw==', N'cbfb458c-0f8d-4bf8-883d-0e46c5c4426b', NULL, 0, 0, NULL, 1, 0, N'85e8ad13-c28d-480c-992f-78e1abdbc9d2@hr.com', 4, 20, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'd9ceb6d0-4ade-4d87-ae3c-c829206620d5', N'e2010454-1065-45db-954f-38b0e4d09e12@hr.com', 0, N'AFEVc/Uq8gzgcuTE6uu4EgmUacvJ6yn4lMPXbQEPWxYSCbfQXzLozvx1THt0RZZBSw==', N'f0d27bfc-9683-4dfc-8bb4-9a8353c3ebe9', NULL, 0, 0, NULL, 1, 0, N'e2010454-1065-45db-954f-38b0e4d09e12@hr.com', 4, 21, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'88450232-24c3-4cba-919b-7c1c4c64f4f2', N'96a36c2a-8c00-4da8-8477-b5a4ea8af4d3@hr.com', 0, N'ABI2FZg+m6jGqaF+a9PzyRA0NtNQJvQFyrvpnyCqNHNlKgKAo78ZTsmGYtZD2+dU3w==', N'd29730e8-04fe-4e86-be51-b6de06e173db', NULL, 0, 0, NULL, 1, 0, N'96a36c2a-8c00-4da8-8477-b5a4ea8af4d3@hr.com', 4, 22, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'1b4ec15b-abff-46c0-a029-df405b2d4ffb', N'0a283ac7-1216-4157-9ea0-7cc7c58a4287@hr.com', 0, N'AKZSMVcBzYBL5MvtUqrlWLei3szf+J8rSyAiPKbTScYZIkf00qT7mdY+P249RKHj1Q==', N'6bdbe6e5-2109-4d58-b54c-e4212315465d', NULL, 0, 0, NULL, 1, 0, N'0a283ac7-1216-4157-9ea0-7cc7c58a4287@hr.com', 4, 23, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'20c83ae9-a47c-4122-8b00-2a14e98db983', N'ff9c1c49-1fe5-41aa-a5a4-047c29fa7ff5@hr.com', 0, N'AKEElUtJj4jzXaVb5qcqf7xo+Ls2NTE87DPT9idHHzXfB6ugj441QFSUBcRjAALkSA==', N'19e19041-015f-45d4-a2dd-5c486e2b3736', NULL, 0, 0, NULL, 1, 0, N'ff9c1c49-1fe5-41aa-a5a4-047c29fa7ff5@hr.com', 4, 24, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'424193a9-7b3f-43cc-9806-b27db02c5db4', N'sunjayp88456@gmail.com', 0, N'APXT4xqBxCurHbok+PAUdMusq3T/p/xzPmveyktk2rArdB839VKgIYuO5kOlJKYA6g==', N'47e90459-e1cf-41b2-a0b4-e88cc4f5266b', NULL, 0, 0, NULL, 1, 0, N'sunjayp88456@gmail.com', 4, 13, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'9349a8cd-5b5d-4ab5-80bf-9b178a0629cd', N'panontonganm@andersongroup.uk.com', 0, N'AHb5jEpgZ7ShIv8B2Zv4FAn+2lgmWPln9RtYpw1xDx0Ey+DhYZHWQa3O1hbqzeGR9Q==', N'1108f492-9cba-4934-b5fb-652b97e11f90', NULL, 0, 0, NULL, 1, 0, N'panontonganm@andersongroup.uk.com', 1, 13, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'db5f88be-4018-4647-8776-01e9cd07753a', N'johhpaulc@andersongroup.uk.com', 0, N'AM11d49o06Uwshg0Hpv2rCz+vijUPCc/+nnPLyZE/qgvsaSO1NP1i0sAmUvmTVgNSQ==', N'904bfac7-a8bc-48a3-a551-7fa0e779b464', NULL, 0, 0, NULL, 1, 0, N'johhpaulc@andersongroup.uk.com', 1, 14, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'aed476a0-3739-44a0-bd2f-b11a3287689c', N'kristineg@andersongroup.uk.com', 0, N'APhV2jacwogv2QAq3UrhGLgA8lt/2gdQisiXdywnniGP5ghSA2aa09P1mT/WbM39gQ==', N'4fbc5e0a-e3d2-4622-bd35-04a8502f5268', NULL, 0, 0, NULL, 1, 0, N'kristineg@andersongroup.uk.com', 1, 15, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'06586b59-4429-436d-9b26-800d880483df', N'dev@andersongroup.uk.com', 0, N'ACLi7xxARxrTXxaCT/JIfbHKp/JGdpx/UHpyBFUVpbxMADpVgUMJSNBTddvqvG6dQw==', N'e56d3f92-2be7-4ecf-8a0f-f6118507987a', NULL, 0, 0, NULL, 1, 0, N'christophers@andersongroup.uk.com', 1, 16, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'ccd7d6ea-f514-40a8-9530-dd7744c5706c', N'dikshas@andersongroup.uk.com', 0, N'AAmuYdVcwfyvri+pKSdq1fDhPzxrHGR4xNVNXO2+E6lff1YWbpYHSIPVe4zcEjYSxw==', N'2ddce434-0ca5-4fa4-ae56-e49b62937034', NULL, 0, 0, NULL, 1, 0, N'dikshas@andersongroup.uk.com', 4, 16, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'77ec2e78-ed30-4b70-8fdf-7498a3f85492', N'67f677f3-a905-483e-afd9-b3932e14e8ba@hr.com', 0, N'ANwvgwTLoftcqxSF4dsBMzOfwYLOTpMOoP+N57CfGFndV18O5rSsA/Jp32ej2dzakA==', N'c5549f22-06e2-4870-9cb8-65b062d5b575', NULL, 0, 0, NULL, 1, 0, N'67f677f3-a905-483e-afd9-b3932e14e8ba@hr.com', 4, 14, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'817cad52-7b2e-4bf3-994d-2c9c91e03cd2', N'pankajc1@andersongroup.uk.com', 0, N'APIIejJ9I2pM84fLCCOuM+4DUSDGZuFyN42kQq46lSNk+lWVybANRPyNlLCLVyrvUQ==', N'7a1cdaa8-6e74-431e-ba28-63ee84ab43a7', NULL, 0, 0, NULL, 1, 0, N'pankajc1@andersongroup.uk.com', 4, 15, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [OrganisationId], [PersonnelId], [CentreId]) VALUES (N'a4a988b5-e38b-4c70-a20b-bed97a8db4cb', N'1011ed35-5c53-487a-9cb9-e90ed2453c13@hr.com', 0, N'AFI8mkZeZf9zGkVljOewgYDJWV3tFDJ63H/OqR5LGIX6gjoAZbotFUG8V2ATfHlOAA==', N'3d770f21-d1c7-44a7-a221-11e5cbcfa887', NULL, 0, 0, NULL, 1, 0, N'1011ed35-5c53-487a-9cb9-e90ed2453c13@hr.com', 4, 25, NULL)
SET IDENTITY_INSERT [dbo].[Batch] ON 

INSERT [dbo].[Batch] ([BatchId], [SchemeId], [CentreId], [OrganisationId], [TrainingType], [SectorId], [SubSector], [CourseId], [TrainingHrsPerDay], [BatchStartDate], [BatchEndDate], [PreferredAssessmentDate], [PersonnelId], [Remarks], [CreatedDate]) VALUES (1, 3, 1, 4, N'Long Term', 3, N'lololo', 5, 8, CAST(N'2017-02-23' AS Date), CAST(N'2017-02-23' AS Date), CAST(N'2017-02-23' AS Date), 20, N'lololo', CAST(N'0001-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[Batch] OFF
SET IDENTITY_INSERT [dbo].[BatchTimePrefer] ON 

INSERT [dbo].[BatchTimePrefer] ([BatchTimePreferId], [Name], [OrganisationId]) VALUES (1, N'8 am To 12 pm', 4)
INSERT [dbo].[BatchTimePrefer] ([BatchTimePreferId], [Name], [OrganisationId]) VALUES (2, N'12 pm To 4 pm', 4)
INSERT [dbo].[BatchTimePrefer] ([BatchTimePreferId], [Name], [OrganisationId]) VALUES (3, N'4 pm To 8 pm', 4)
SET IDENTITY_INSERT [dbo].[BatchTimePrefer] OFF
SET IDENTITY_INSERT [dbo].[Building] ON 

INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (1, N'PankajBuilding', 4, 12, N'Mumbai')
INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (2, N'PankajBuilding2', 4, 13, N'Mumbai2')
INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (3, N'PankajBuilding3', 4, 12, N'Mumbai3')
INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (4, N'PankajBuilding4', 4, 14, N'TestBuilding')
INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (5, N'test1building', 4, 12, NULL)
INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (7, N't2building', 4, 12, NULL)
INSERT [dbo].[Building] ([BuildingId], [Name], [OrganisationId], [SiteId], [Address1]) VALUES (8, N'test new', 4, 16, NULL)
SET IDENTITY_INSERT [dbo].[Building] OFF
SET IDENTITY_INSERT [dbo].[CasteCategory] ON 

INSERT [dbo].[CasteCategory] ([CasteCategoryId], [Caste], [OrganisationId]) VALUES (1, N'SC', 4)
INSERT [dbo].[CasteCategory] ([CasteCategoryId], [Caste], [OrganisationId]) VALUES (2, N'ST', 4)
INSERT [dbo].[CasteCategory] ([CasteCategoryId], [Caste], [OrganisationId]) VALUES (3, N'OBC', 4)
INSERT [dbo].[CasteCategory] ([CasteCategoryId], [Caste], [OrganisationId]) VALUES (4, N'MBC', 4)
INSERT [dbo].[CasteCategory] ([CasteCategoryId], [Caste], [OrganisationId]) VALUES (5, N'GEN', 4)
SET IDENTITY_INSERT [dbo].[CasteCategory] OFF
SET IDENTITY_INSERT [dbo].[Centre] ON 

INSERT [dbo].[Centre] ([CentreId], [CentreCode], [Name], [OrganisationId], [Place]) VALUES (1, N'thane', N'Nest thane', 4, NULL)
INSERT [dbo].[Centre] ([CentreId], [CentreCode], [Name], [OrganisationId], [Place]) VALUES (2, NULL, N'test', 4, NULL)
INSERT [dbo].[Centre] ([CentreId], [CentreCode], [Name], [OrganisationId], [Place]) VALUES (3, NULL, N'nagpur', 4, NULL)
INSERT [dbo].[Centre] ([CentreId], [CentreCode], [Name], [OrganisationId], [Place]) VALUES (4, NULL, N'jabalpur', 4, NULL)
SET IDENTITY_INSERT [dbo].[Centre] OFF
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (1, N'ff8000')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (2, N'FF0000')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (3, N'808080')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (4, N'708090')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (5, N'000000')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (6, N'FFFFE0')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (7, N'FFD700')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (8, N'FFC0CB')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (9, N'FF69B4')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (10, N'FFA07A')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (11, N'FA8072')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (12, N'DC143C')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (13, N'B22222')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (14, N'FF0000')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (15, N'FF4500')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (16, N'FF8C00')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (17, N'FFA500')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (18, N'DEB887')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (19, N'F4A460')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (20, N'B8860B')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (21, N'CD853F')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (22, N'8B4513')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (23, N'A52A2A')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (24, N'800000')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (25, N'556B2F')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (26, N'6B8E23')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (27, N'32CD32')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (28, N'00FF00')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (29, N'00FF7F')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (30, N'008000')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (31, N'006400')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (32, N'00FFFF')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (33, N'E0FFFF')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (34, N'AFEEEE')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (35, N'40E0D0')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (36, N'20B2AA')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (37, N'008080')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (38, N'B0C4DE')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (39, N'87CEEB')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (40, N'00BFFF')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (41, N'1E90FF')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (42, N'0000FF')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (43, N'000080')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (44, N'DDA0DD')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (45, N'EE82EE')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (46, N'FF00FF')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (47, N'9400D3')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (48, N'800080')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (49, N'4B0082')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (50, N'6A5ACD')
INSERT [dbo].[Colour] ([ColourId], [Hex]) VALUES (51, N'6699CC')
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyId], [Name], [OrganisationId], [ColourId]) VALUES (7, N'ITSK DS', 4, 1)
INSERT [dbo].[Company] ([CompanyId], [Name], [OrganisationId], [ColourId]) VALUES (8, N'PankajCompany', 4, 45)
INSERT [dbo].[Company] ([CompanyId], [Name], [OrganisationId], [ColourId]) VALUES (11, N'Test1', 4, 41)
INSERT [dbo].[Company] ([CompanyId], [Name], [OrganisationId], [ColourId]) VALUES (13, N'test12345', 4, 17)
INSERT [dbo].[Company] ([CompanyId], [Name], [OrganisationId], [ColourId]) VALUES (14, N'Test123454', 4, 48)
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[CompanyBuilding] ON 

INSERT [dbo].[CompanyBuilding] ([CompanyBuildingId], [BuildingId], [CompanyId], [OrganisationId]) VALUES (3, 2, 8, 4)
INSERT [dbo].[CompanyBuilding] ([CompanyBuildingId], [BuildingId], [CompanyId], [OrganisationId]) VALUES (4, 3, 8, 4)
INSERT [dbo].[CompanyBuilding] ([CompanyBuildingId], [BuildingId], [CompanyId], [OrganisationId]) VALUES (10, 5, 8, 4)
INSERT [dbo].[CompanyBuilding] ([CompanyBuildingId], [BuildingId], [CompanyId], [OrganisationId]) VALUES (12, 1, 7, 4)
SET IDENTITY_INSERT [dbo].[CompanyBuilding] OFF
SET IDENTITY_INSERT [dbo].[Counselling] ON 

INSERT [dbo].[Counselling] ([CounsellingId], [EnquiryId], [CentreId], [OrganisationId], [PersonnelId], [CourseOfferedId], [PreferTiming], [Remarks], [FollowUpDate], [RemarkByBranchManager], [Name], [SectorId], [PsychomatricTest]) VALUES (1, 6, 1, 4, 20, 1, NULL, NULL, CAST(N'2017-02-26' AS Date), NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Counselling] OFF
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([CountryId], [Name], [OrganisationId]) VALUES (24, N'PankajCountry', 4)
INSERT [dbo].[Country] ([CountryId], [Name], [OrganisationId]) VALUES (25, N'PankajCountry2', 4)
INSERT [dbo].[Country] ([CountryId], [Name], [OrganisationId]) VALUES (29, N'Test', 4)
SET IDENTITY_INSERT [dbo].[Country] OFF
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (1, N'MIS', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (2, N'.Net', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (3, N'Java', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (4, N'OST', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (5, N'Hardware & Networking', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (6, N'Logistics', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (7, N'Financial Accounting', 4)
INSERT [dbo].[Course] ([CourseId], [Name], [OrganisationId]) VALUES (8, N'Others', 4)
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentId], [Name], [OrganisationId], [ColourId]) VALUES (24, N'IT', 4, 19)
INSERT [dbo].[Department] ([DepartmentId], [Name], [OrganisationId], [ColourId]) VALUES (25, N'Testing', 4, 32)
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[District] ON 

INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (1, N'Anantapur', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (2, N'Chittoor', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (3, N'East Godavari', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (4, N'Guntur', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (5, N'Kadapa', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (6, N'Krishna', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (7, N'Kurnool', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (8, N'Nellore', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (9, N'Prakasam', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (10, N'Srikakulam', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (11, N'Visakhapatnam', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (12, N'Vizianagaram', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (13, N'West Godavari', 4, 1)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (14, N'Tawang', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (15, N'West Kameng', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (16, N'East Kameng', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (17, N'Papum Pare', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (18, N'Kurung Kumey', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (19, N'Kra Daadi', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (20, N'Lower Subansiri', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (21, N'Upper Subansiri', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (22, N'West Siang', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (23, N'East Siang', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (24, N'Central Siang', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (25, N'Upper Siang', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (26, N'Lower Dibang Valley', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (27, N'Upper Dibang Valley', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (28, N'Anjaw', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (29, N'Lohit', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (30, N'Namsai', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (31, N'Changlang', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (32, N'Tirap', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (33, N'Longding', 4, 2)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (34, N'Baksa', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (35, N'Barpeta', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (36, N'Biswanath', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (37, N'Bongaigaon', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (38, N'Cachar', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (39, N'Charaideo', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (40, N'Chirang', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (41, N'Darrang', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (42, N'Dhemaji', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (43, N'Dhubri', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (44, N'Dibrugarh', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (45, N'Goalpara', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (46, N'Golaghat', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (47, N'Hailakandi', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (48, N'Hojai', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (49, N'Jorhat', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (50, N'Kamrup Metropolitan', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (51, N'Kamrup', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (52, N'Karbi Anglong', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (53, N'Karimganj', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (54, N'Kokrajhar', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (55, N'Lakhimpur', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (56, N'Majuli', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (57, N'Morigaon', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (58, N'Nagaon', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (59, N'Nalbari', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (60, N'Dima Hasao', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (61, N'Sivasagar', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (62, N'Sonitpur', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (63, N'South Salmara-Mankachar', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (64, N'Tinsukia', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (65, N'Udalguri', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (66, N'West Karbi Anglong', 4, 3)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (67, N'Araria', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (68, N'Arwal', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (69, N'Aurangabad', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (70, N'Banka', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (71, N'Begusarai', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (72, N'Bhagalpur', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (73, N'Bhojpur', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (74, N'Buxar', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (75, N'Darbhanga', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (76, N'East Champaran', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (77, N'Gaya', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (78, N'Gopalganj', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (79, N'Jamui', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (81, N'Jehanabad', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (83, N'Khagaria', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (84, N'Kishanganj', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (85, N'Kaimur', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (86, N'Katihar', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (87, N'Lakhisarai', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (88, N'Madhubani', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (89, N'Munger', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (90, N'Madhepura', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (91, N'Muzaffarpur', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (92, N'Nalanda', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (93, N'Nawada', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (94, N'Patna', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (95, N'Purnia', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (96, N'Rohtas', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (97, N'Saharsa', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (98, N'Samastipur', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (99, N'Sheohar', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (100, N'Sheikhpura', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (101, N'Saran', 4, 4)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (102, N'Sitamarhi', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (103, N'Supaul', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (104, N'Siwan', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (105, N'Vaishali', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (106, N'West Champaran', 4, 4)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (107, N'Balod', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (108, N'Baloda Bazar', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (109, N'Balrampur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (110, N'Bemetara', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (111, N'	Bijapur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (112, N'Bilaspur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (113, N'Dantewada', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (114, N'Dhamtari', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (115, N'Durg', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (116, N'Gariaband', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (117, N'Jagdalpur (Madhya Bastar)', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (118, N'Janjgir-Champa', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (119, N'Jashpur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (120, N'Kabirdham', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (121, N'Kanker', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (122, N'Kondagaon', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (123, N'Korba', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (124, N'Koriya', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (125, N'Mahasamund', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (126, N'Mungeli', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (127, N'Narayanpur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (128, N'Raigarh', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (129, N'Raipur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (130, N'Rajnandgaon', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (131, N'Sukma', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (132, N'Surajpur', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (133, N'Surguja', 4, 5)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (134, N'North Goa(Panaji)', 4, 6)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (135, N'South Goa(Margao)', 4, 6)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (136, N'Ahmedabad', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (137, N'Amreli', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (138, N'Anand', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (139, N'Aravalli', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (140, N'Banaskantha', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (141, N'Bharuch', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (142, N'Bhavnagar', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (143, N'Botad', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (144, N'Chhota Udaipur', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (145, N'Dahod', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (146, N'Dang', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (147, N'Devbhoomi Dwarka', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (148, N'Gandhinagar', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (149, N'Gir Somnath', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (150, N'Jamnagar', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (151, N'Junagadh', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (152, N'Kutch', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (153, N'Kheda', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (154, N'Mahisagar', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (155, N'Mehsana', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (156, N'Morbi', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (157, N'Narmada', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (158, N'Navsari', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (159, N'Panchmahal', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (160, N'Patan', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (161, N'Porbandar', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (162, N'Rajkot', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (163, N'Sabarkantha', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (164, N'Surat', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (165, N'Surendranagar', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (166, N'Tapi', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (167, N'Vadodara', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (168, N'Valsad', 4, 7)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (169, N'Ambala', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (170, N'Bhiwani', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (171, N'Faridabad', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (172, N'Fatehabad', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (173, N'Gurugram', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (174, N'Hisar', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (175, N'Jhajjar', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (176, N'Jind', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (177, N'Karnal', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (178, N'Kaithal', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (179, N'Kurukshetra', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (180, N'Mahendragarh', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (181, N'Mewat', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (182, N'Panchkula', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (183, N'Palwal', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (184, N'Panipat', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (185, N'Rewari', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (186, N'Rohtak', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (187, N'Sirsa', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (188, N'Sonipat', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (189, N'Yamuna Nagar', 4, 8)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (190, N'Bilaspur', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (191, N'Chamba', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (192, N'Hamirpur', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (193, N'Kangra', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (194, N'Kinnaur', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (195, N'Kullu', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (196, N'Lahaul and Spiti', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (197, N'Mandi', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (198, N'Shimla', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (199, N'Sirmaur', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (200, N'Solan', 4, 9)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (201, N'Una', 4, 9)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (202, N'Doda', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (203, N'Jammu', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (204, N'Kathua', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (205, N'Kishtwar', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (206, N'Poonch', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (207, N'Rajouri', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (208, N'Ramban', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (209, N'Reasi', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (210, N'Samba', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (211, N'Udhampur', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (212, N'Anantnag', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (213, N'Bandipora', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (214, N'Baramulla', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (215, N'Budgam', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (216, N'Ganderbal', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (217, N'Kulgam', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (218, N'Kupwara', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (219, N'Pulwama', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (220, N'Shopian', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (221, N'Srinagar', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (222, N'Kargil', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (223, N'Leh', 4, 10)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (224, N'Garhwa', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (225, N'Palamu', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (226, N'Latehar', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (227, N'Chatra', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (228, N'Hazaribagh', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (229, N'Koderma', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (230, N'Giridih', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (231, N'Ramgarh', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (232, N'Bokaro', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (233, N'Dhanbad', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (234, N'Lohardaga', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (235, N'Gumla', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (236, N'Simdega', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (237, N'Ranchi', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (238, N'Khunti', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (239, N'West Singhbhum', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (240, N'Saraikela Kharsawan', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (241, N'East Singhbhum', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (242, N'Jamtara', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (243, N'Deoghar', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (244, N'Dumka', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (245, N'Pakur', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (246, N'Godda', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (247, N'Sahebganj', 4, 11)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (248, N'Bagalkot', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (249, N'Bengaluru Urban', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (250, N'Bengaluru Rural', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (251, N'Belagavi', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (252, N'Bellary', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (253, N'Bidar', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (254, N'Vijayapura', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (255, N'Chamarajanagar', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (256, N'Chikballapur', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (257, N'Chikkamagaluru', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (258, N'Chitradurga', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (259, N'Dakshina Kannada', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (260, N'Davanagere', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (261, N'Dharwad', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (262, N'Gadag', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (263, N'Kalaburagi', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (264, N'Hassan', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (265, N'Haveri', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (266, N'Kodagu', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (267, N'Kolar', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (268, N'Koppal', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (269, N'Mandya', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (270, N'Mysuru', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (271, N'Raichur', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (272, N'Ramanagara', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (273, N'Shivamogga', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (274, N'Tumakuru', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (275, N'Udupi', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (276, N'Uttara Kannada', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (277, N'Yadgir', 4, 12)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (278, N'Alappuzha', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (279, N'Ernakulam', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (280, N'Idukki', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (281, N'Kannur', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (282, N'Kasaragod', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (283, N'Kollam', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (284, N'Kottayam', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (285, N'Kozhikode', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (286, N'Malappuram', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (287, N'Palakkad', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (288, N'Pathanamthitta', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (289, N'Thiruvananthapuram', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (290, N'Thrissur', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (291, N'Wayanad', 4, 13)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (292, N'Bhopal', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (293, N'Raisen', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (294, N'Rajgarh', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (295, N'Sehore', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (296, N'Vidisha', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (297, N'Morena', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (298, N'Sheopur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (299, N'Bhind', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (300, N'Gwalior', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (301, N'Ashoknagar', 4, 14)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (302, N'Shivpuri', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (303, N'Datia', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (304, N'Guna', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (305, N'Alirajpur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (306, N'Barwani', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (307, N'Burhanpur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (308, N'Dhar', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (309, N'Indore', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (310, N'Jhabua', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (311, N'Khandwa', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (312, N'Khargone', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (313, N'Balaghat', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (314, N'Chhindwara', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (315, N'Jabalpur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (316, N'Katni', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (317, N'Mandla', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (318, N'Dindori', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (319, N'Narsinghpur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (320, N'Seoni', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (321, N'Betul', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (322, N'Harda', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (323, N'Hoshangabad', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (324, N'Rewa', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (325, N'Satna', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (326, N'Sidhi', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (327, N'Singrauli', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (328, N'Chhatarpur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (329, N'Damoh', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (330, N'Panna', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (331, N'Sagar', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (332, N'Tikamgarh', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (333, N'Anuppur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (334, N'Shahdol', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (335, N'Umaria', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (336, N'Agar Malwa', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (337, N'Dewas', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (338, N'Mandsaur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (339, N'Neemuch', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (340, N'Ratlam', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (341, N'Shajapur', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (342, N'Ujjain', 4, 14)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (343, N'Ahmednagar', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (344, N'Akola', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (345, N'Amravati', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (346, N'Aurangabad', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (347, N'Beed', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (348, N'Bhandara', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (349, N'Buldhana', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (350, N'Chandrapur', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (351, N'Dhule', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (352, N'Gadchiroli', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (353, N'Gondia', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (354, N'Hingoli', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (355, N'Jalgaon', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (356, N'Jalna', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (357, N'Kolhapur', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (358, N'Latur', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (359, N'Mumbai City', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (360, N'Mumbai Suburban', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (361, N'Nagpur', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (362, N'Nanded', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (363, N'Nandurbar', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (364, N'Nashik', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (365, N'Osmanabad', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (366, N'Parbhani', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (367, N'Pune', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (368, N'Raigad', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (369, N'Ratnagiri', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (370, N'Sangli', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (371, N'Satara', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (372, N'Sindhudurg', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (373, N'Solapur', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (374, N'Thane', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (375, N'Wardha', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (376, N'Washim', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (377, N'Yavatmal', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (378, N'Palghar', 4, 15)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (379, N'Bishnupur', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (380, N'Thoubal', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (381, N'Imphal East', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (382, N'Imphal West', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (383, N'Senapati', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (384, N'Ukhrul', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (385, N'Chandel', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (386, N'Churachandpur', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (387, N'Tamenglong', 4, 16)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (388, N'Jowai', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (389, N'Khliehriat', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (390, N'Shillong', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (391, N'Nongstoin', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (392, N'Mawkyrwat', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (393, N'Nongpoh', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (394, N'Resubelpara', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (395, N'Williamnagar', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (396, N'Baghmara', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (397, N'Tura', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (398, N'Ampati', 4, 17)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (399, N'Aizawl', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (400, N'Kolasib', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (401, N'Lawngtlai', 4, 18)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (402, N'Lunglei', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (403, N'Mamit', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (404, N'Siaha', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (405, N'Serchhip', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (406, N'Champhai', 4, 18)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (407, N'Dimapur', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (408, N'Kiphire', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (409, N'Kohima', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (410, N'Longleng', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (411, N'Mokokchung', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (412, N'Mon', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (413, N'Peren', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (414, N'Phek', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (415, N'Tuensang', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (416, N'Wokha', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (417, N'Zunheboto', 4, 19)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (418, N'Angul', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (419, N'Boudh', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (420, N'Balangir', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (421, N'Bargarh', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (422, N'Balasore', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (423, N'Bhadrak', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (424, N'Cuttack', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (425, N'Debagarh', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (426, N'Dhenkanal', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (427, N'Ganjam', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (428, N'Gajapati', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (429, N'Jharsuguda', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (430, N'Jajpur', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (431, N'Jagatsinghapur', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (432, N'Khordha', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (433, N'Kendujhar', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (434, N'Kalahandi', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (435, N'Kandhamal', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (436, N'Koraput', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (437, N'Kendrapara', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (438, N'Malkangiri', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (439, N'Mayurbhanj', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (440, N'Nabarangpur', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (441, N'Nuapada', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (442, N'Nayagarh', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (443, N'Puri', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (444, N'Rayagada', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (445, N'Sambalpur', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (446, N'Subarnapur', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (447, N'Sundergarh', 4, 20)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (448, N'Amritsar', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (449, N'Barnala', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (450, N'Bathinda', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (451, N'Faridkot', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (452, N'Fatehgarh Sahib', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (453, N'Firozpur', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (454, N'Fazilka', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (455, N'Gurdaspur', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (456, N'Hoshiarpur', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (457, N'Jalandhar', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (458, N'Kapurthala', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (459, N'Ludhiana', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (460, N'Mansa', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (461, N'Moga', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (462, N'Sri Muktsar Sahib', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (463, N'Pathankot', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (464, N'Patiala', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (465, N'Rupnagar', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (466, N'Sahibzada Ajit Singh Nagar', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (467, N'Sangrur', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (468, N'Shahid Bhagat Singh Nagar', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (469, N'Tarn Taran', 4, 21)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (470, N'Ajmer', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (471, N'Alwar', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (472, N'Banswara', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (473, N'Baran', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (474, N'Barmer', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (475, N'Bharatpur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (476, N'Bhilwara', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (477, N'Bikaner', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (478, N'Bundi', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (479, N'Chittorgarh', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (480, N'Churu', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (481, N'Dausa', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (482, N'Dholpur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (483, N'Dungarpur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (484, N'Hanumangarh', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (485, N'Jaipur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (486, N'Jaisalmer', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (487, N'Jalor', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (488, N'Jhalawar', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (489, N'Jhunjhunu', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (490, N'Jodhpur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (491, N'Karauli', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (492, N'Kota', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (493, N'Nagaur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (494, N'Pali', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (495, N'Pratapgarh', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (496, N'Rajsamand', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (497, N'Sawai Madhopur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (498, N'Sikar', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (499, N'Sirohi', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (500, N'Sri Ganganagar', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (501, N'Tonk', 4, 22)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (502, N'Udaipur', 4, 22)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (503, N'Gangtok', 4, 23)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (504, N'Mangan', 4, 23)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (505, N'Namchi', 4, 23)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (506, N'Geyzing', 4, 23)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (507, N'Ariyalur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (508, N'Karur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (509, N'Nagapattinam', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (510, N'Perambalur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (511, N'Pudukkottai', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (512, N'Thanjavur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (513, N'Tiruchirappalli', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (514, N'Tiruvarur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (515, N'Dharmapuri', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (516, N'Coimbatore', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (517, N'Erode', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (518, N'Krishnagiri', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (519, N'Namakkal', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (520, N'Nilgiris', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (521, N'Salem', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (522, N'Tiruppur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (523, N'Dindigul', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (524, N'Kanyakumari', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (525, N'Madurai', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (526, N'Ramanathapuram', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (527, N'Sivaganga', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (528, N'Theni', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (529, N'Thoothukudi', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (530, N'Tirunelveli', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (531, N'Virudhunagar', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (532, N'Chennai', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (533, N'Cuddalore', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (534, N'Kanchipuram', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (535, N'Tiruvallur', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (536, N'Tiruvannamalai', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (537, N'Vellore', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (538, N'Viluppuram', 4, 24)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (539, N'Dhalai', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (540, N'South Tripura', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (541, N'Gomati', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (542, N'North Tripura', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (543, N'Sipahijala', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (544, N'Khowai', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (545, N'West Tripura', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (546, N'Unakoti', 4, 25)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (547, N'Agra', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (548, N'Firozabad', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (549, N'Mainpuri', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (550, N'Mathura', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (551, N'Aligarh', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (552, N'Etah', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (553, N'Hathras', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (554, N'Kasganj', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (555, N'Allahabaad', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (556, N'Fatehpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (557, N'Kaushambi', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (558, N'Pratapgarh', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (559, N'Azamgarh', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (560, N'Ballia', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (561, N'Mau', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (562, N'Budaun', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (563, N'Bareilly', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (564, N'Pilibhit', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (565, N'Shahjahanpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (566, N'Basti', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (567, N'Sant Kabir Nagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (568, N'Siddharthnagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (569, N'Banda', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (570, N'Chitrakoot', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (571, N'Hamirpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (572, N'Mahoba', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (573, N'Bahraich', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (574, N'Balarampur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (575, N'Gonda', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (576, N'Shravasti', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (577, N'Ambedkar Nagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (578, N'Amethi', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (579, N'Barabanki', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (580, N'Faizabad', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (581, N'Sultanpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (582, N'Deoria', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (583, N'Gorakhpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (584, N'Kushinagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (585, N'Maharajganj', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (586, N'Jalaun', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (587, N'Jhansi', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (588, N'Lalitpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (589, N'Auraiya', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (590, N'Etawah', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (591, N'Farrukhabad', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (592, N'Kannauj', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (593, N'Kanpur Dehat', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (594, N'Kanpur Nagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (595, N'Hardoi', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (596, N'Lakhimpur Kheri', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (597, N'Lucknow', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (598, N'Raebareli', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (599, N'Sitapur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (600, N'Unnao', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (601, N'Bagpat', 4, 26)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (602, N'Bulandshahr', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (603, N'Gautam Buddha Nagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (604, N'Ghaziabad', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (605, N'Hapur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (606, N'Meerut', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (607, N'Mirzapur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (608, N'Sant Ravidas Nagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (609, N'Sonbhadra', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (610, N'Amroha', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (611, N'Bijnor', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (612, N'Moradabad', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (613, N'Rampur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (614, N'Sambhal', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (615, N'Muzaffarnagar', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (616, N'Saharanpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (617, N'Shamli', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (618, N'Chandauli', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (619, N'Ghazipur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (620, N'Jaunpur', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (621, N'Varanasi', 4, 26)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (622, N'Almora', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (623, N'Bageshwar', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (624, N'Chamoli', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (625, N'Champawat', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (626, N'Dehradun', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (627, N'Haridwar', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (628, N'Nainital', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (629, N'Pauri Garhwal', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (630, N'Pithoragarh', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (631, N'Rudraprayag', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (632, N'Tehri Garhwal', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (633, N'Udham Singh Nagar', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (634, N'Uttarkashi', 4, 27)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (635, N'Alipurduar', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (636, N'Bankura', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (637, N'Bardhaman', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (638, N'Birbhum', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (639, N'Cooch Behar', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (640, N'Darjeeling', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (641, N'East Midnapore', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (642, N'Hooghly', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (643, N'Howrah', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (644, N'Jalpaiguri', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (645, N'Kolkata', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (646, N'Kalimpong', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (647, N'Malda', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (648, N'Murshidabad', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (649, N'Nadia', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (650, N'North Parganas', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (651, N'North Dinajpur', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (652, N'Purulia', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (653, N'South Parganas', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (654, N'South Dinajpur', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (655, N'West Midnapore', 4, 28)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (656, N'Adilabad', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (657, N'Bhadradri', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (658, N'Hyderabad', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (659, N'Jagtial', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (660, N'Jangaon', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (661, N'Jayashankar', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (662, N'Jogulamba', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (663, N'Kamareddy', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (664, N'Karimnagar', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (665, N'Khammam', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (666, N'Komaram Bheem', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (667, N'Mahabubabad', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (668, N'Mahabubnagar', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (669, N'Mancherial', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (670, N'Medak', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (671, N'Medchal', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (672, N'Nagarkurnool', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (673, N'Nalgonda', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (674, N'Nirmal', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (675, N'Nizamabad', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (676, N'Peddapalli', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (677, N'Rajanna Sircilla', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (678, N'Ranga Reddy', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (679, N'Sangareddy', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (680, N'Siddipet', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (681, N'Suryapet', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (682, N'Vikarabad', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (683, N'Wanaparthy', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (684, N'Warangal Rural', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (685, N'Warangal Urban', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (686, N'Yadadri', 4, 29)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (687, N'Nicobar', 4, 30)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (688, N'North and Middle Andaman', 4, 30)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (689, N'South Andaman', 4, 30)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (690, N'Chandigarh', 4, 31)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (691, N'Dadra and Nagar Haveli', 4, 32)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (692, N'Daman', 4, 33)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (693, N'Diu', 4, 33)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (694, N'Lakshadweep', 4, 34)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (695, N'Daryaganj', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (696, N'Sadar Bazaar', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (697, N'Saket', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (698, N'Preet Vihar', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (699, N'Shahdara', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (700, N'Palam', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (701, N'Connaught Place', 4, 35)
GO
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (702, N'Kanjhawala', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (703, N'Rajouri Garden', 4, 35)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (704, N'Karaikal', 4, 36)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (705, N'Mahe', 4, 36)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (706, N'Pondicherry', 4, 36)
INSERT [dbo].[District] ([DistrictId], [Name], [OrganisationId], [StateId]) VALUES (707, N'Yanam', 4, 36)
SET IDENTITY_INSERT [dbo].[District] OFF
SET IDENTITY_INSERT [dbo].[EmergencyContact] ON 

INSERT [dbo].[EmergencyContact] ([EmergencyContactId], [OrganisationId], [PersonnelId], [Relationship], [Title], [Forenames], [Surname], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile]) VALUES (2, 4, 20, N'Friend', N'mr', N'sanjay', N'prajapati', 25, N'krishna Paradise1', NULL, NULL, NULL, NULL, N'98213113344', NULL)
SET IDENTITY_INSERT [dbo].[EmergencyContact] OFF
SET IDENTITY_INSERT [dbo].[Employment] ON 

INSERT [dbo].[Employment] ([EmploymentId], [OrganisationId], [PersonnelId], [StartDate], [EndDate], [TerminationDate], [BuildingId], [ReportsToPersonnelId], [JobTitle], [JobDescription], [EndEmploymentReasonId], [WorkingPatternId], [PublicHolidayPolicyId], [AbsencePolicyId], [CompanyId]) VALUES (2, 4, 20, CAST(N'2016-10-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-01-06 00:00:00.0000000' AS DateTime2), NULL, 5, NULL, N'Test', N'Test', NULL, 56, 30, 38, 8)
INSERT [dbo].[Employment] ([EmploymentId], [OrganisationId], [PersonnelId], [StartDate], [EndDate], [TerminationDate], [BuildingId], [ReportsToPersonnelId], [JobTitle], [JobDescription], [EndEmploymentReasonId], [WorkingPatternId], [PublicHolidayPolicyId], [AbsencePolicyId], [CompanyId]) VALUES (6, 4, 21, CAST(N'2016-10-10 00:00:00.0000000' AS DateTime2), NULL, NULL, 5, NULL, N'Test', N'Test', NULL, 57, 30, 36, 8)
SET IDENTITY_INSERT [dbo].[Employment] OFF
SET IDENTITY_INSERT [dbo].[EmploymentDepartment] ON 

INSERT [dbo].[EmploymentDepartment] ([EmploymentDepartmentId], [EmploymentId], [DepartmentId], [OrganisationId]) VALUES (1, 2, 24, 4)
INSERT [dbo].[EmploymentDepartment] ([EmploymentDepartmentId], [EmploymentId], [DepartmentId], [OrganisationId]) VALUES (2, 2, 25, 4)
SET IDENTITY_INSERT [dbo].[EmploymentDepartment] OFF
SET IDENTITY_INSERT [dbo].[EmploymentTeam] ON 

INSERT [dbo].[EmploymentTeam] ([EmploymentTeamId], [EmploymentId], [TeamId], [OrganisationId]) VALUES (1, 2, 1, 4)
SET IDENTITY_INSERT [dbo].[EmploymentTeam] OFF
SET IDENTITY_INSERT [dbo].[Enquiry] ON 

INSERT [dbo].[Enquiry] ([EnquiryId], [CandidateName], [ContactNo], [EmailId], [Age], [Address], [GuardianName], [GuardianContactNo], [OccupationId], [ReligionId], [CasteCategoryId], [Gender], [EducationalQualificationId], [YearOfPassOut], [Marks], [IntrestedCourseId], [HowDidYouKnowAboutId], [PreTrainingStatus], [EmploymentStatus], [Promotional], [EnquiryDate], [Place], [CounselledBy], [CourseOfferedId], [PreferTiming], [Remarks], [CentreId], [OrganisationId], [FollowUpDate], [EnquiryStatus], [EmployerName], [EmployerContactNo], [EmployerAddress], [AnnualIncome], [SchemeId], [EnquiryTypeId], [StudentTypeId], [SectorId], [BatchTimePreferId], [AppearingQualification], [YearOfExperience], [EnquiryFollowUpDate], [PlacementNeeded], [WhyEnquiryClosed], [RemarkByBm]) VALUES (6, N'Test Enquiry', 9773606038, N'vijay@gmail.com', 125636, N'KALEKAR CHAWL ONGC BLDG,12/13/3 FLR,DHARAVI,  SANT ROHIDAS MARG', N'Nanasaheb Raut', 8422992824, 2, 1, 5, N'Female', 7, N'2014-17', N'35%', 2, 5, N'Experience', N'Employed', N'Yes', CAST(N'2017-02-19' AS Date), N'Thane', N'Sanjay', 1, NULL, N'he will call back on 31 Jan', 1, 4, CAST(N'2017-02-19' AS Date), NULL, N'jibe infomatics', N'876546846484', N'Belapur', 100000, 4, 2, 3, 2, 2, N'BE', 2, CAST(N'2017-02-19' AS Date), N'Yes', N'don''t know', N'don''t know')
INSERT [dbo].[Enquiry] ([EnquiryId], [CandidateName], [ContactNo], [EmailId], [Age], [Address], [GuardianName], [GuardianContactNo], [OccupationId], [ReligionId], [CasteCategoryId], [Gender], [EducationalQualificationId], [YearOfPassOut], [Marks], [IntrestedCourseId], [HowDidYouKnowAboutId], [PreTrainingStatus], [EmploymentStatus], [Promotional], [EnquiryDate], [Place], [CounselledBy], [CourseOfferedId], [PreferTiming], [Remarks], [CentreId], [OrganisationId], [FollowUpDate], [EnquiryStatus], [EmployerName], [EmployerContactNo], [EmployerAddress], [AnnualIncome], [SchemeId], [EnquiryTypeId], [StudentTypeId], [SectorId], [BatchTimePreferId], [AppearingQualification], [YearOfExperience], [EnquiryFollowUpDate], [PlacementNeeded], [WhyEnquiryClosed], [RemarkByBm]) VALUES (7, N'Poonam Shete', 5465654546, N'poonamsp@gmail.com', 21, N'KALEKAR CHAWL ONGC BLDG,12/13/3 FLR,DHARAVI,  SANT ROHIDAS MARG', N'Anili Shete', 8422992824, 1, 1, 1, N'Female', 3, NULL, NULL, 1, 1, N'Fresher', N'UnEmployed', N'Yes', CAST(N'2017-02-19' AS Date), N'Thane', N'Sanjay', 1, NULL, NULL, 1, 4, CAST(N'2017-02-19' AS Date), NULL, NULL, NULL, NULL, 0, 1, 1, 1, 1, 1, NULL, 0, CAST(N'2017-02-19' AS Date), N'Yes', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Enquiry] OFF
SET IDENTITY_INSERT [dbo].[EnquiryType] ON 

INSERT [dbo].[EnquiryType] ([EnquiryTypeId], [Name], [OrganisationId]) VALUES (1, N'Walk-In', 4)
INSERT [dbo].[EnquiryType] ([EnquiryTypeId], [Name], [OrganisationId]) VALUES (2, N'Tele-In', 4)
SET IDENTITY_INSERT [dbo].[EnquiryType] OFF
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([EventId], [Name], [CreatedBy], [CreatedDateTime], [ApprovedBy], [OrganisationId], [CentreId]) VALUES (3, N'Seminar', N'Amit Sir', CAST(N'2016-11-05 00:00:00.000' AS DateTime), 1, 4, 1)
INSERT [dbo].[Event] ([EventId], [Name], [CreatedBy], [CreatedDateTime], [ApprovedBy], [OrganisationId], [CentreId]) VALUES (12, N'Test', NULL, CAST(N'2016-11-05 00:00:00.000' AS DateTime), NULL, 4, 1)
SET IDENTITY_INSERT [dbo].[Event] OFF
SET IDENTITY_INSERT [dbo].[FollowUp] ON 

INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2049, CAST(N'2017-02-21 12:26:24.370' AS DateTime), 0, NULL, NULL, NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 12:26:24.370' AS DateTime), 4, 0, N'Ramesh', 9870245680, 5, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2050, CAST(N'2017-02-21 12:26:24.370' AS DateTime), 0, NULL, NULL, NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 12:26:24.370' AS DateTime), 4, 0, N'Suresh', 8702458790, 4, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2051, CAST(N'2017-02-21 12:26:24.370' AS DateTime), 0, NULL, NULL, NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 12:26:24.370' AS DateTime), 4, 0, N'Hema', 7895425481, 3, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2052, CAST(N'2017-02-21 12:26:24.370' AS DateTime), 0, NULL, NULL, NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 12:26:24.370' AS DateTime), 4, 0, N'Reshma', 8715724520, 2, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2053, CAST(N'2017-02-21 12:26:24.370' AS DateTime), 0, NULL, NULL, NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 12:26:24.370' AS DateTime), 4, 0, N'Mahesh', 9714725415, 7, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2054, CAST(N'2017-02-21 12:27:09.093' AS DateTime), 4130, NULL, N'She will decide & let us know', NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 12:27:09.100' AS DateTime), 4, 1, N'FollowTest', 8097537537, 1, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2055, CAST(N'2017-02-21 00:00:00.000' AS DateTime), NULL, NULL, N'he will call back on 31 Jan', NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 13:53:07.000' AS DateTime), 4, 1, N'Test Enquiry', 9773606038, 2, NULL)
INSERT [dbo].[FollowUp] ([FollowUpId], [FollowUpDateTime], [MobilizationId], [EnquiryId], [Remark], [Closed], [ReadDateTime], [CreatedDateTime], [OrganisationId], [CentreId], [Name], [Mobile], [IntrestedCourseId], [FollowUpType]) VALUES (2056, CAST(N'2017-02-20 00:00:00.000' AS DateTime), NULL, NULL, N'She will come tomarrow', NULL, CAST(N'1917-02-19 00:00:00.000' AS DateTime), CAST(N'2017-02-19 13:55:37.000' AS DateTime), 4, 1, N'Poonam Shete', 5465654546, 1, NULL)
SET IDENTITY_INSERT [dbo].[FollowUp] OFF
SET IDENTITY_INSERT [dbo].[Frequency] ON 

INSERT [dbo].[Frequency] ([FrequencyId], [Name], [Periods]) VALUES (1, N'Yearly', 1)
INSERT [dbo].[Frequency] ([FrequencyId], [Name], [Periods]) VALUES (2, N'Quarterly', 2)
SET IDENTITY_INSERT [dbo].[Frequency] OFF
SET IDENTITY_INSERT [dbo].[Host] ON 

INSERT [dbo].[Host] ([HostId], [HostName], [OrganisationId]) VALUES (6, N'nidanserver', 4)
INSERT [dbo].[Host] ([HostId], [HostName], [OrganisationId]) VALUES (10, N'localhost', 4)
SET IDENTITY_INSERT [dbo].[Host] OFF
SET IDENTITY_INSERT [dbo].[HowDidYouKnowAbout] ON 

INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (1, N'Web Site', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (2, N'SMS', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (3, N'Banner', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (4, N'News Paper', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (5, N'Reference', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (6, N'Internet', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (7, N'Pamphlet', 4)
INSERT [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId], [Name], [OrganisationId]) VALUES (8, N'Event', 4)
SET IDENTITY_INSERT [dbo].[HowDidYouKnowAbout] OFF
SET IDENTITY_INSERT [dbo].[Mobilization] ON 

INSERT [dbo].[Mobilization] ([MobilizationId], [EventId], [OrganisationId], [CentreId], [Name], [Mobile], [InterestedCourseId], [QualificationId], [CreatedDate], [FollowUpDate], [Remark], [MobilizerStatus], [StudentLocation], [OtherInterestedCourse], [GeneratedDate], [MobilizationTypeId], [PersonnelId]) VALUES (4125, 3, 4, 1, N'Ramesh', 9870245680, 5, 1, CAST(N'2017-02-19' AS Date), CAST(N'2017-02-22' AS Date), NULL, NULL, N'thane', N'Test', CAST(N'2017-02-19' AS Date), 1, 20)
INSERT [dbo].[Mobilization] ([MobilizationId], [EventId], [OrganisationId], [CentreId], [Name], [Mobile], [InterestedCourseId], [QualificationId], [CreatedDate], [FollowUpDate], [Remark], [MobilizerStatus], [StudentLocation], [OtherInterestedCourse], [GeneratedDate], [MobilizationTypeId], [PersonnelId]) VALUES (4126, 3, 4, 1, N'Suresh', 8702458790, 4, 2, CAST(N'2017-02-19' AS Date), CAST(N'2017-02-21' AS Date), NULL, NULL, N'sion', N'', CAST(N'2017-02-17' AS Date), 1, 20)
INSERT [dbo].[Mobilization] ([MobilizationId], [EventId], [OrganisationId], [CentreId], [Name], [Mobile], [InterestedCourseId], [QualificationId], [CreatedDate], [FollowUpDate], [Remark], [MobilizerStatus], [StudentLocation], [OtherInterestedCourse], [GeneratedDate], [MobilizationTypeId], [PersonnelId]) VALUES (4127, 3, 4, 1, N'Hema', 7895425481, 3, 3, CAST(N'2017-02-19' AS Date), CAST(N'2017-02-21' AS Date), NULL, NULL, N'belapur', N'', CAST(N'2017-02-17' AS Date), 1, 20)
INSERT [dbo].[Mobilization] ([MobilizationId], [EventId], [OrganisationId], [CentreId], [Name], [Mobile], [InterestedCourseId], [QualificationId], [CreatedDate], [FollowUpDate], [Remark], [MobilizerStatus], [StudentLocation], [OtherInterestedCourse], [GeneratedDate], [MobilizationTypeId], [PersonnelId]) VALUES (4128, 3, 4, 1, N'Reshma', 8715724520, 2, 5, CAST(N'2017-02-19' AS Date), CAST(N'2017-02-21' AS Date), NULL, NULL, N'vashi', N'', CAST(N'2017-02-17' AS Date), 1, 20)
INSERT [dbo].[Mobilization] ([MobilizationId], [EventId], [OrganisationId], [CentreId], [Name], [Mobile], [InterestedCourseId], [QualificationId], [CreatedDate], [FollowUpDate], [Remark], [MobilizerStatus], [StudentLocation], [OtherInterestedCourse], [GeneratedDate], [MobilizationTypeId], [PersonnelId]) VALUES (4129, 3, 4, 1, N'Mahesh', 9714725415, 7, 6, CAST(N'2017-02-19' AS Date), CAST(N'2017-02-21' AS Date), NULL, NULL, N'mulund', N'', CAST(N'2017-02-17' AS Date), 1, 20)
INSERT [dbo].[Mobilization] ([MobilizationId], [EventId], [OrganisationId], [CentreId], [Name], [Mobile], [InterestedCourseId], [QualificationId], [CreatedDate], [FollowUpDate], [Remark], [MobilizerStatus], [StudentLocation], [OtherInterestedCourse], [GeneratedDate], [MobilizationTypeId], [PersonnelId]) VALUES (4130, 3, 4, 1, N'FollowTest', 8097537537, 1, 1, CAST(N'2017-02-19' AS Date), CAST(N'2017-02-21' AS Date), N'She will decide & let us know', NULL, N'Belapur', N'Tally', CAST(N'2017-02-19' AS Date), 3, 20)
SET IDENTITY_INSERT [dbo].[Mobilization] OFF
SET IDENTITY_INSERT [dbo].[MobilizationType] ON 

INSERT [dbo].[MobilizationType] ([MobilizationTypeId], [Name], [OrganisationId]) VALUES (1, N'Walk-In', 4)
INSERT [dbo].[MobilizationType] ([MobilizationTypeId], [Name], [OrganisationId]) VALUES (2, N'Telephonic', 4)
INSERT [dbo].[MobilizationType] ([MobilizationTypeId], [Name], [OrganisationId]) VALUES (3, N'Event', 4)
SET IDENTITY_INSERT [dbo].[MobilizationType] OFF
SET IDENTITY_INSERT [dbo].[Occupation] ON 

INSERT [dbo].[Occupation] ([OccupationId], [Name], [OrganisationId]) VALUES (1, N'Business', 4)
INSERT [dbo].[Occupation] ([OccupationId], [Name], [OrganisationId]) VALUES (2, N'Govt-Employee', 4)
INSERT [dbo].[Occupation] ([OccupationId], [Name], [OrganisationId]) VALUES (3, N'Farmer', 4)
INSERT [dbo].[Occupation] ([OccupationId], [Name], [OrganisationId]) VALUES (4, N'Doctor', 4)
INSERT [dbo].[Occupation] ([OccupationId], [Name], [OrganisationId]) VALUES (5, N'Other', 4)
SET IDENTITY_INSERT [dbo].[Occupation] OFF
SET IDENTITY_INSERT [dbo].[Organisation] ON 

INSERT [dbo].[Organisation] ([OrganisationId], [Name]) VALUES (4, N'Dev Mumbai Organisation')
SET IDENTITY_INSERT [dbo].[Organisation] OFF
SET IDENTITY_INSERT [dbo].[Personnel] ON 

INSERT [dbo].[Personnel] ([PersonnelId], [OrganisationId], [Title], [Forenames], [Surname], [DOB], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile], [NINumber], [BankAccountNumber], [BankSortCode], [BankAccountName], [BankAddress1], [BankAddress2], [BankAddress3], [BankAddress4], [BankPostcode], [BankTelephone], [Email], [CurrentEmploymentId], [CentreId]) VALUES (20, 4, N'Mr', N'TEst', N'Test', CAST(N'2017-01-04 00:00:00.0000000' AS DateTime2), 24, N'Address1', NULL, NULL, NULL, N'POST CODE', N'12345678', NULL, N'NZ1234567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'85e8ad13-c28d-480c-992f-78e1abdbc9d2@hr.com', 2, NULL)
INSERT [dbo].[Personnel] ([PersonnelId], [OrganisationId], [Title], [Forenames], [Surname], [DOB], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile], [NINumber], [BankAccountNumber], [BankSortCode], [BankAccountName], [BankAddress1], [BankAddress2], [BankAddress3], [BankAddress4], [BankPostcode], [BankTelephone], [Email], [CurrentEmploymentId], [CentreId]) VALUES (21, 4, N'Mr', N'Abc', N'B', CAST(N'2017-01-05 00:00:00.0000000' AS DateTime2), 0, N'Address1', NULL, NULL, NULL, N'POST CODE', N'12345678', NULL, N'NZ1234567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'e2010454-1065-45db-954f-38b0e4d09e12@hr.com', NULL, NULL)
INSERT [dbo].[Personnel] ([PersonnelId], [OrganisationId], [Title], [Forenames], [Surname], [DOB], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile], [NINumber], [BankAccountNumber], [BankSortCode], [BankAccountName], [BankAddress1], [BankAddress2], [BankAddress3], [BankAddress4], [BankPostcode], [BankTelephone], [Email], [CurrentEmploymentId], [CentreId]) VALUES (22, 4, N'Mr', N'x', N'z', CAST(N'2017-01-12 00:00:00.0000000' AS DateTime2), 0, N'Address1', NULL, NULL, NULL, N'POST CODE', N'12345678', NULL, N'NZ1234567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'96a36c2a-8c00-4da8-8477-b5a4ea8af4d3@hr.com', NULL, NULL)
INSERT [dbo].[Personnel] ([PersonnelId], [OrganisationId], [Title], [Forenames], [Surname], [DOB], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile], [NINumber], [BankAccountNumber], [BankSortCode], [BankAccountName], [BankAddress1], [BankAddress2], [BankAddress3], [BankAddress4], [BankPostcode], [BankTelephone], [Email], [CurrentEmploymentId], [CentreId]) VALUES (23, 4, N'Mr', N'test1', N'B', CAST(N'2017-01-12 00:00:00.0000000' AS DateTime2), 0, N'Address1', NULL, NULL, NULL, N'POST CODE', N'12345678', NULL, N'NZ1234567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'0a283ac7-1216-4157-9ea0-7cc7c58a4287@hr.com', NULL, NULL)
INSERT [dbo].[Personnel] ([PersonnelId], [OrganisationId], [Title], [Forenames], [Surname], [DOB], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile], [NINumber], [BankAccountNumber], [BankSortCode], [BankAccountName], [BankAddress1], [BankAddress2], [BankAddress3], [BankAddress4], [BankPostcode], [BankTelephone], [Email], [CurrentEmploymentId], [CentreId]) VALUES (24, 4, N'Mr', N'Vijay', N'Raut', CAST(N'1993-01-27 00:00:00.0000000' AS DateTime2), 0, N'Address1', NULL, NULL, NULL, N'POST CODE', N'12345678', NULL, N'NZ1234567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ff9c1c49-1fe5-41aa-a5a4-047c29fa7ff5@hr.com', NULL, NULL)
INSERT [dbo].[Personnel] ([PersonnelId], [OrganisationId], [Title], [Forenames], [Surname], [DOB], [CountryId], [Address1], [Address2], [Address3], [Address4], [Postcode], [Telephone], [Mobile], [NINumber], [BankAccountNumber], [BankSortCode], [BankAccountName], [BankAddress1], [BankAddress2], [BankAddress3], [BankAddress4], [BankPostcode], [BankTelephone], [Email], [CurrentEmploymentId], [CentreId]) VALUES (25, 4, N'Mr', N'Nidan', N'B', CAST(N'2017-02-04 00:00:00.0000000' AS DateTime2), 0, N'Address1', NULL, NULL, NULL, N'POST CODE', N'12345678', NULL, N'NZ1234567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1011ed35-5c53-487a-9cb9-e90ed2453c13@hr.com', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Personnel] OFF
SET IDENTITY_INSERT [dbo].[PersonnelAbsenceEntitlement] ON 

INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (1, 4, 20, 29, 3, CAST(N'2016-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-03-31 00:00:00.0000000' AS DateTime2), 15, 0, 1, 14, 7, 1, 2)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (43, 4, 21, 44, 3, CAST(N'2016-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-03-31 00:00:00.0000000' AS DateTime2), 5.5, 0, 3, 2.5, 8, 1, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (44, 4, 21, 44, 2, CAST(N'2016-10-01 00:00:00.0000000' AS DateTime2), CAST(N'2016-12-31 00:00:00.0000000' AS DateTime2), 2.5, 0, 0, 2.5, 2, 2, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (45, 4, 21, 44, 2, CAST(N'2017-01-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-03-31 00:00:00.0000000' AS DateTime2), 3, 0, 1, 2, 2, 2, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (46, 4, 21, 44, NULL, CAST(N'2016-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-03-31 00:00:00.0000000' AS DateTime2), 0, 0, 0, 0, 0, 1, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (47, 4, 21, 45, 3, CAST(N'2017-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2018-03-31 00:00:00.0000000' AS DateTime2), 12, 0, 1, 11, 8, 1, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (48, 4, 21, 45, 2, CAST(N'2017-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-06-30 00:00:00.0000000' AS DateTime2), 3, 0, 0, 3, 2, 2, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (49, 4, 21, 45, 2, CAST(N'2017-07-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-09-30 00:00:00.0000000' AS DateTime2), 3, 0, 0, 3, 2, 2, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (50, 4, 21, 45, 2, CAST(N'2017-10-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-12-31 00:00:00.0000000' AS DateTime2), 3, 0, 0, 3, 2, 2, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (51, 4, 21, 45, 2, CAST(N'2018-01-01 00:00:00.0000000' AS DateTime2), CAST(N'2018-03-31 00:00:00.0000000' AS DateTime2), 3, 0, 0, 3, 2, 2, 6)
INSERT [dbo].[PersonnelAbsenceEntitlement] ([PersonnelAbsenceEntitlementId], [OrganisationId], [PersonnelId], [AbsencePolicyPeriodId], [AbsenceTypeId], [StartDate], [EndDate], [Entitlement], [CarriedOver], [Used], [Remaining], [MaximumCarryForward], [FrequencyId], [EmploymentId]) VALUES (52, 4, 21, 45, NULL, CAST(N'2017-04-01 00:00:00.0000000' AS DateTime2), CAST(N'2018-03-31 00:00:00.0000000' AS DateTime2), 0, 0, 0, 0, 0, 1, 6)
SET IDENTITY_INSERT [dbo].[PersonnelAbsenceEntitlement] OFF
SET IDENTITY_INSERT [dbo].[PublicHoliday] ON 

INSERT [dbo].[PublicHoliday] ([PublicHolidayId], [Name], [Date], [OrganisationId], [PublicHolidayPolicyId]) VALUES (18, N'Guru', CAST(N'2017-02-23 00:00:00.0000000' AS DateTime2), 4, 4)
INSERT [dbo].[PublicHoliday] ([PublicHolidayId], [Name], [Date], [OrganisationId], [PublicHolidayPolicyId]) VALUES (25, N'Test', CAST(N'2016-12-31 00:00:00.0000000' AS DateTime2), 4, 4)
INSERT [dbo].[PublicHoliday] ([PublicHolidayId], [Name], [Date], [OrganisationId], [PublicHolidayPolicyId]) VALUES (42, N'Test1', CAST(N'2016-12-30 00:00:00.0000000' AS DateTime2), 4, 4)
INSERT [dbo].[PublicHoliday] ([PublicHolidayId], [Name], [Date], [OrganisationId], [PublicHolidayPolicyId]) VALUES (51, N'Guru', CAST(N'2017-02-23 00:00:00.0000000' AS DateTime2), 4, 30)
INSERT [dbo].[PublicHoliday] ([PublicHolidayId], [Name], [Date], [OrganisationId], [PublicHolidayPolicyId]) VALUES (52, N'Test', CAST(N'2016-12-31 00:00:00.0000000' AS DateTime2), 4, 30)
INSERT [dbo].[PublicHoliday] ([PublicHolidayId], [Name], [Date], [OrganisationId], [PublicHolidayPolicyId]) VALUES (53, N'Test1', CAST(N'2016-12-30 00:00:00.0000000' AS DateTime2), 4, 30)
SET IDENTITY_INSERT [dbo].[PublicHoliday] OFF
SET IDENTITY_INSERT [dbo].[PublicHolidayPolicy] ON 

INSERT [dbo].[PublicHolidayPolicy] ([PublicHolidayPolicyId], [OrganisationId], [Name]) VALUES (4, 4, N'Test Policy')
INSERT [dbo].[PublicHolidayPolicy] ([PublicHolidayPolicyId], [OrganisationId], [Name]) VALUES (30, 4, N'Test Policy 10:51:30')
SET IDENTITY_INSERT [dbo].[PublicHolidayPolicy] OFF
SET IDENTITY_INSERT [dbo].[Qualification] ON 

INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (1, N'SSC', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (2, N'HSC', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (3, N'Under Graduate', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (4, N'Graduate', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (5, N'Post Graduate', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (6, N'Masters', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (7, N'Diploma', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (9, N'BE', 4)
INSERT [dbo].[Qualification] ([QualificationId], [Name], [OrganisationId]) VALUES (11, N'Others', 4)
SET IDENTITY_INSERT [dbo].[Qualification] OFF
SET IDENTITY_INSERT [dbo].[Religion] ON 

INSERT [dbo].[Religion] ([ReligionId], [Name], [OrganisationId]) VALUES (1, N'Hindu', 4)
INSERT [dbo].[Religion] ([ReligionId], [Name], [OrganisationId]) VALUES (2, N'Muslim', 4)
INSERT [dbo].[Religion] ([ReligionId], [Name], [OrganisationId]) VALUES (3, N'Christen', 4)
INSERT [dbo].[Religion] ([ReligionId], [Name], [OrganisationId]) VALUES (4, N'Sikh', 4)
INSERT [dbo].[Religion] ([ReligionId], [Name], [OrganisationId]) VALUES (5, N'Jain', 4)
INSERT [dbo].[Religion] ([ReligionId], [Name], [OrganisationId]) VALUES (6, N'Buddha', 4)
SET IDENTITY_INSERT [dbo].[Religion] OFF
SET IDENTITY_INSERT [dbo].[Scheme] ON 

INSERT [dbo].[Scheme] ([SchemeId], [Name], [OrganisationId], [SchemeTypeId]) VALUES (1, N'Commercial', 4, 2)
INSERT [dbo].[Scheme] ([SchemeId], [Name], [OrganisationId], [SchemeTypeId]) VALUES (2, N'PMKVY', 4, 1)
INSERT [dbo].[Scheme] ([SchemeId], [Name], [OrganisationId], [SchemeTypeId]) VALUES (3, N'NULM', 4, 1)
INSERT [dbo].[Scheme] ([SchemeId], [Name], [OrganisationId], [SchemeTypeId]) VALUES (4, N'CILT', 4, 1)
INSERT [dbo].[Scheme] ([SchemeId], [Name], [OrganisationId], [SchemeTypeId]) VALUES (5, N'NSDC', 4, 1)
SET IDENTITY_INSERT [dbo].[Scheme] OFF
SET IDENTITY_INSERT [dbo].[SchemeType] ON 

INSERT [dbo].[SchemeType] ([SchemeTypeId], [Name], [OrganisationId]) VALUES (1, N'Commercial', 4)
INSERT [dbo].[SchemeType] ([SchemeTypeId], [Name], [OrganisationId]) VALUES (2, N'Government', 4)
SET IDENTITY_INSERT [dbo].[SchemeType] OFF
SET IDENTITY_INSERT [dbo].[Sector] ON 

INSERT [dbo].[Sector] ([SectorId], [Name], [OrganisationId]) VALUES (1, N'Logistics', 4)
INSERT [dbo].[Sector] ([SectorId], [Name], [OrganisationId]) VALUES (2, N'IT', 4)
INSERT [dbo].[Sector] ([SectorId], [Name], [OrganisationId]) VALUES (3, N'Retail', 4)
INSERT [dbo].[Sector] ([SectorId], [Name], [OrganisationId]) VALUES (4, N'Telecom', 4)
INSERT [dbo].[Sector] ([SectorId], [Name], [OrganisationId]) VALUES (5, N'Apparel,Made ups & Home furnishing', 4)
INSERT [dbo].[Sector] ([SectorId], [Name], [OrganisationId]) VALUES (6, N'Beauty & Wellness', 4)
SET IDENTITY_INSERT [dbo].[Sector] OFF
SET IDENTITY_INSERT [dbo].[Site] ON 

INSERT [dbo].[Site] ([SiteId], [Name], [CountryId], [OrganisationId]) VALUES (12, N'PankajSite', 24, 4)
INSERT [dbo].[Site] ([SiteId], [Name], [CountryId], [OrganisationId]) VALUES (13, N'PankajSite2', 25, 4)
INSERT [dbo].[Site] ([SiteId], [Name], [CountryId], [OrganisationId]) VALUES (14, N'TestSite', 29, 4)
INSERT [dbo].[Site] ([SiteId], [Name], [CountryId], [OrganisationId]) VALUES (16, N'test', 29, 4)
SET IDENTITY_INSERT [dbo].[Site] OFF
SET IDENTITY_INSERT [dbo].[State] ON 

INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (1, N'Andhra Pradesh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (2, N'Arunachal Pradesh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (3, N'Assam', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (4, N'Bihar', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (5, N'Chhattisgarh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (6, N'Goa', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (7, N'Gujarat', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (8, N'Haryana', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (9, N'Himachal Pradesh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (10, N'Jammu and Kashmir', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (11, N'Jharkhand', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (12, N'Karnataka', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (13, N'Kerala', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (14, N'Madhya Pradesh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (15, N'Maharashtra', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (16, N'Manipur', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (17, N'Meghalaya', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (18, N'Mizoram', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (19, N'Nagaland', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (20, N'Odisha', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (21, N'Punjab', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (22, N'Rajasthan', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (23, N'Sikkim', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (24, N'Tamil Nadu', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (25, N'Tripura', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (26, N'Uttar Pradesh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (27, N'Uttarakhand', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (28, N'West Bengal', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (29, N'Telangana', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (30, N'Andaman and Nicobar', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (31, N'Chandigarh', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (32, N'Dadra and Nagar Haveli', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (33, N'Daman and Diu', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (34, N'Lakshadweep', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (35, N'NCT Delhi', 4)
INSERT [dbo].[State] ([StateId], [Name], [OrganisationId]) VALUES (36, N'Puducherry', 4)
SET IDENTITY_INSERT [dbo].[State] OFF
SET IDENTITY_INSERT [dbo].[StudentType] ON 

INSERT [dbo].[StudentType] ([StudentTypeId], [Name], [OrganisationId]) VALUES (1, N'Student', 4)
INSERT [dbo].[StudentType] ([StudentTypeId], [Name], [OrganisationId]) VALUES (2, N'Working Professional', 4)
INSERT [dbo].[StudentType] ([StudentTypeId], [Name], [OrganisationId]) VALUES (3, N'Searching For Job', 4)
SET IDENTITY_INSERT [dbo].[StudentType] OFF
SET IDENTITY_INSERT [dbo].[Taluka] ON 

INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (1, N'Agali', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (2, N'Anantapur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (3, N'Beluguppa', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (4, N'Bukkapatnam', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (5, N'Chilamathur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (6, N'Gandlapenta', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (7, N'Gorantla', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (8, N'Guntakal', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (9, N'Kalyandurg', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (10, N'Kanekal', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (11, N'Kundurpi', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (12, N'Mudigubba', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (13, N'Nambulipulikunta', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (14, N'Pamidi', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (15, N'Peddavadugur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (16, N'Puttaparthi', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (17, N'Rayadurg', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (18, N'Settur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (19, N'Tadimarri', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (20, N'Tanakal', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (21, N'Vidapanakal', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (22, N'Amadagur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (23, N'Atmakur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (24, N'Bommanahal', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (25, N'Bukkaraya Samudram', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (26, N'D Hirehal', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (27, N'Garladinne', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (28, N'Gudibanda', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (29, N'Hindupur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (30, N'Kambadur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (31, N'Kothacheruvu', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (32, N'Lepakshi', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (33, N'Nallacheruvu', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (34, N'Narpala', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (35, N'Parigi', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (36, N'Penukonda', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (37, N'Ramagiri', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (38, N'Roddam', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (39, N'Singanamala', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (40, N'Tadpatri', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (41, N'Uravakonda', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (42, N'Yadiki', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (43, N'Amarapuram', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (44, N'Bathalapalle', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (45, N'Brahmasamudram', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (46, N'Chennekothapalle', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (47, N'Dharmavaram', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (48, N'Gooty', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (49, N'Gummagatta', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (50, N'Kadiri', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (51, N'Kanaganapalle', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (52, N'Kudair', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (53, N'Madakasira', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (54, N'Nallamada', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (55, N'Obuladevaracheruvu', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (56, N'Peddapappur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (57, N'Putlur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (58, N'Raptadu', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (59, N'Rolla', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (60, N'Somandepalle', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (61, N'Talupula', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (62, N'Vajrakarur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (63, N'Yellanur', 1, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (64, N'B Kothakota', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (65, N'Baireddipalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (66, N'Bangarupalem', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (67, N'Buchinaidu Kandriga', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (68, N'Chandragiri', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (69, N'Chinnagottigallu', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (70, N'Chittoor', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (71, N'Chowdepalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (72, N'Gangadhara Nellore', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (73, N'Gangavaram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (74, N'Gudipala', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (75, N'Gudupalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (76, N'Gurramkonda', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (77, N'Irala', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (78, N'K V B Puram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (79, N'Kalakada', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (80, N'Kalikiri', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (81, N'Kambhamvaripalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (82, N'Karvetinagar', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (83, N'Kuppam', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (84, N'Kurabalakota', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (85, N'Madanapalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (86, N'Mulakalacheruvu', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (87, N'Nagalapuram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (88, N'Nagari', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (89, N'Narayanavanam', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (90, N'Nimmanapalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (91, N'Nindra', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (92, N'Pakala', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (93, N'Palamaner', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (94, N'Palasamudram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (95, N'Pedda Thippasamudram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (96, N'Peddamandyam', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (97, N'Peddapanjani', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (98, N'Penumuru', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (99, N'Pichatur', 2, 1, 4)
GO
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (100, N'Pileru', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (101, N'Pulicherla', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (102, N'Punganur', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (103, N'Puthalapattu', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (104, N'Puttur', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (105, N'Ramachandrapuram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (106, N'Ramakuppam', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (107, N'Ramasamudram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (108, N'Renigunta', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (109, N'Rompicherla', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (110, N'Santhipuram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (111, N'Satyavedu', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (112, N'Sodam', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (113, N'Somala', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (114, N'Srikalahasti', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (115, N'Srirangarajapuram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (116, N'Thamballapalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (117, N'Thavanampalle', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (118, N'Thottambedu', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (119, N'Tirupati', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (120, N'Vadamalapeta', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (121, N'Valmikipuram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (122, N'Varadaiahpalem', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (123, N'Vedurukuppam', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (124, N'Venkatagirikota', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (125, N'Vijayapuram', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (126, N'Yadamarri', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (127, N'Yerpedu', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (128, N'Yerravaripalem', 2, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (129, N'Gandepalle', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (130, N'Gangavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (131, N'Gokavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (132, N'Gollaprolu', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (133, N'I Polavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (134, N'Jaggampeta', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (135, N'Kadiam', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (136, N'Kajuluru', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (137, N'Kakinada', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (138, N'Kapileswarapuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (139, N'Karapa', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (140, N'Katrenikona', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (141, N'Kirlampudi', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (142, N'Korukonda', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (143, N'Kotananduru', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (144, N'Kothapalle', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (145, N'Kothapeta', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (146, N'Malikipuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (147, N'Mamidikuduru', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (148, N'Mandapeta', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (149, N'Maredumilli', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (150, N'Mummidivaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (151, N'P Gannavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (152, N'Pamarru', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (153, N'Pedapudi', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (154, N'Peddapuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (155, N'Pithapuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (156, N'Prathipadu', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (157, N'Rajahmundry', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (158, N'Rajahmundry Rural', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (159, N'Rajanagaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (160, N'Rajavommangi', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (161, N'Ramachandrapuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (162, N'Rampachodavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (163, N'Rangampeta', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (164, N'Ravulapalem', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (165, N'Rayavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (166, N'Razole', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (167, N'Rowthulapudi', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (168, N'Sakhinetipalle', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (169, N'Samalkota', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (170, N'Sankhavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (171, N'Seethanagaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (172, N'Thallarevu', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (173, N'Thondangi', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (174, N'Tuni', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (175, N'Uppalaguptam', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (176, N'Y Ramavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (177, N'Yeleswaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (178, N'Addateegala', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (179, N'Ainavilli', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (180, N'Alamuru', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (181, N'Allavaram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (182, N'Amalapuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (183, N'Ambajipeta', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (184, N'Anaparthy', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (185, N'Atreyapuram', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (186, N'Biccavolu', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (187, N'Devipatnam', 3, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (188, N'Amaravathi', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (189, N'Amruthalur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (190, N'Atchampet', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (191, N'Bapatla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (192, N'Bellamkonda', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (193, N'Bhattiprolu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (194, N'Bollapalle', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (195, N'Chebrolu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (196, N'Cherukupalle H O Arumbaka', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (197, N'Dachepalle', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (198, N'Duggirala', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (199, N'Durgi', 4, 1, 4)
GO
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (200, N'Edlapadu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (201, N'Guntur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (202, N'Gurazala', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (203, N'Ipur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (204, N'Kakumanu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (205, N'Karempudi', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (206, N'Karlapalem', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (207, N'Kollipara', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (208, N'Kollur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (209, N'Krosuru', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (210, N'Machavaram', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (211, N'Macherla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (212, N'Mangalagiri', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (213, N'Medikonduru', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (214, N'Muppalla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (215, N'Nadendla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (216, N'Nagaram', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (217, N'Narasaraopet', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (218, N'Nekarikallu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (219, N'Nizampatnam', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (220, N'Nuzendla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (221, N'Pedakakani', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (222, N'Pedakurapadu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (223, N'Pedanandipadu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (224, N'Phirangipuram', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (225, N'Piduguralla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (226, N'Pittalavanipalem', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (227, N'Ponnur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (228, N'Prathipadu', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (229, N'Rajupalem', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (230, N'Rentachintala', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (231, N'Repalle', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (232, N'Rompicherla', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (233, N'Sattenapalle', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (234, N'Savalyapuram H O Kanamarlapudi', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (235, N'Tadepalle', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (236, N'Tadikonda', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (237, N'Tenali', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (238, N'Thullur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (239, N'Tsundur', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (240, N'Vatticherukuru', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (241, N'Veldurthi', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (242, N'Vemuru', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (243, N'Vinukonda', 4, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (244, N'A Konduru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (245, N'Agiripalle', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (246, N'Avanigadda', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (247, N'Bantumilli', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (248, N'Bapulapadu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (249, N'Challapalle', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (250, N'Chandarlapadu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (251, N'Chatrai', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (252, N'G Konduru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (253, N'Gampalagudem', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (254, N'Gannavaram', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (255, N'Ghantasala', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (256, N'Gudivada', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (257, N'Gudlavalleru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (258, N'Guduru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (259, N'Ibrahimpatnam', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (260, N'Jaggayyapeta', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (261, N'Kaikalur', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (262, N'Kalidindi', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (263, N'Kanchikacherla', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (264, N'Kankipadu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (265, N'Koduru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (266, N'Kruthivennu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (267, N'Machilipatnam', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (268, N'Mandavalli', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (269, N'Mopidevi', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (270, N'Movva', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (271, N'Mudinepalle', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (272, N'Musunuru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (273, N'Mylavaram', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (274, N'Nagayalanka', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (275, N'Nandigama', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (276, N'Nandivada', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (277, N'Nuzvid', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (278, N'Pamarru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (279, N'Pamidimukkala', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (280, N'Pedana', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (281, N'Pedaparupudi', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (282, N'Penamaluru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (283, N'Penuganchiprolu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (284, N'Reddigudem', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (285, N'Thotlavalluru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (286, N'Tiruvuru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (287, N'Unguturu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (288, N'Vatsavai', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (289, N'Veerullapadu', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (290, N'Vijayawada', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (291, N'Vissannapet', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (292, N'Vuyyuru', 6, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (293, N'Adoni', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (294, N'Allagadda', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (295, N'Alur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (296, N'Aspari', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (297, N'Atmakur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (298, N'Banaganapalle', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (299, N'Bandi Atmakur', 7, 1, 4)
GO
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (300, N'Bethamcherla', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (301, N'C Belagal', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (302, N'Chagalamarri', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (303, N'Chippagiri', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (304, N'Devanakonda', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (305, N'Dhone', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (306, N'Dornipadu', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (307, N'Gadivemula', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (308, N'Gonegandla', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (309, N'Gospadu', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (310, N'Gudur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (311, N'Halaharvi', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (312, N'Holagunda', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (313, N'Jupadu Bungalow', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (314, N'Kallur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (315, N'Kodumur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (316, N'Koilkuntla', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (317, N'Kolimigundla', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (318, N'Kosigi', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (319, N'Kothapalle', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (320, N'Kowthalam', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (321, N'Krishnagiri', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (322, N'Kurnool', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (323, N'Maddikera', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (324, N'Mahanandi', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (325, N'Mantralayam', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (326, N'Midthur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (327, N'Nandavaram', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (328, N'Nandikotkur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (329, N'Nandyal', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (330, N'Orvakal', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (331, N'Owk', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (332, N'Pagidyala', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (333, N'Pamulapadu', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (334, N'Panyam', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (335, N'Pattikonda', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (336, N'Peapally', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (337, N'Pedda Kadubur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (338, N'Rudravaram', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (339, N'Sanjamala', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (340, N'Sirvel', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (341, N'Srisailam', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (342, N'Tuggali', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (343, N'Uyyalawada', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (344, N'Veldurthi', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (345, N'Velgode', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (346, N'Yemmiganur', 7, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (347, N'Addanki', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (348, N'Ardhaveedu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (349, N'Ballikurava', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (350, N'Bestawaripeta', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (351, N'Chandra Sekhara Puram', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (352, N'Chimakurthi', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (353, N'Chinaganjam', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (354, N'Chirala', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (355, N'Cumbum', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (356, N'Darsi', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (357, N'Donakonda', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (358, N'Dornala', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (359, N'Giddalur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (360, N'Gudluru', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (361, N'Hanumanthuni Padu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (362, N'Inkollu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (363, N'Janakavarampanguluru', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (364, N'Kandukur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (365, N'Kanigiri', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (366, N'Karamchedu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (367, N'Komarolu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (368, N'Konakanamitla', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (369, N'Kondapi', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (370, N'Korisapadu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (371, N'Kotha Patnam', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (372, N'Kurichedu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (373, N'Lingasamudram', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (374, N'Maddipadu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (375, N'Markapur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (376, N'Marripudi', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (377, N'Martur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (378, N'Mundlamuru', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (379, N'Naguluppala Padu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (380, N'Ongole', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (381, N'Pamur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (382, N'Parchur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (383, N'Peda Araveedu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (384, N'Pedacherlo Palle', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (385, N'Podili', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (386, N'Ponnaluru', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (387, N'Pullalacheruvu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (388, N'Racherla', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (389, N'Santhamaguluru', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (390, N'Santhanuthala Padu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (391, N'Singarayakonda', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (392, N'Tangutur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (393, N'Tarlupadu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (394, N'Thallur', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (395, N'Tripuranthakam', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (396, N'Ulavapadu', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (397, N'Veligandla', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (398, N'Vetapalem', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (399, N'Voletivaripalem', 9, 1, 4)
GO
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (400, N'Yeddana Pudi', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (401, N'Yerragondapalem', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (402, N'Zarugumilli', 9, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (403, N'Amadalavalasa', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (404, N'Bhamini', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (405, N'Burja', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (406, N'Etcherla', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (407, N'Ganguvarisigadam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (408, N'Gara', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (409, N'Kanchili', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (410, N'Kaviti', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (411, N'Kotabommali', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (412, N'Kothuru', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (413, N'Lakshminarsupeta', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (414, N'Laveru', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (415, N'Mandasa', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (416, N'Meliaputti', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (417, N'Nandigam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (418, N'Narasannapeta', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (419, N'Palakonda', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (420, N'Palasa', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (421, N'Pathapatnam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (422, N'Polaki', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (423, N'Ponduru', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (424, N'Rajam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (425, N'Ranastalam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (426, N'Regidi Amadalavalasa', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (427, N'Santhabommali', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (428, N'Santhakaviti', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (429, N'Saravakota', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (430, N'Sarubujjili', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (431, N'Seethampeta', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (432, N'Sompeta', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (433, N'Srikakulam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (434, N'Tekkali', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (435, N'Vajrapukothuru', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (436, N'Vangara', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (437, N'Veeraghattam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (438, N'Hiramandalam', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (439, N'Ichchapuram', 10, 1, 4)
INSERT [dbo].[Taluka] ([TalukaId], [Name], [DistrictId], [StateId], [OrganisationId]) VALUES (440, N'Jalumuru', 10, 1, 4)
SET IDENTITY_INSERT [dbo].[Taluka] OFF
SET IDENTITY_INSERT [dbo].[Team] ON 

INSERT [dbo].[Team] ([TeamId], [Name], [OrganisationId], [ColourId]) VALUES (1, N'IT Team', 4, 36)
INSERT [dbo].[Team] ([TeamId], [Name], [OrganisationId], [ColourId]) VALUES (3, N'Testing Team', 4, 20)
SET IDENTITY_INSERT [dbo].[Team] OFF
SET IDENTITY_INSERT [dbo].[WorkingPattern] ON 

INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (28, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (29, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (30, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (31, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (32, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (33, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (34, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (35, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (36, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (37, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (38, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (39, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (40, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (41, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (42, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (43, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (44, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (45, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (46, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (47, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (48, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (49, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (50, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (51, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (52, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (53, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (54, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (55, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (56, 4)
INSERT [dbo].[WorkingPattern] ([WorkingPatternId], [OrganisationId]) VALUES (57, 4)
SET IDENTITY_INSERT [dbo].[WorkingPattern] OFF
SET IDENTITY_INSERT [dbo].[WorkingPatternDay] ON 

INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (274, 33, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (275, 33, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (276, 33, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (277, 33, 4, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (278, 33, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (279, 33, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (280, 33, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (281, 34, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (282, 34, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (283, 34, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (284, 34, 4, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (285, 34, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (286, 34, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (287, 34, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (288, 35, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (289, 35, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (290, 35, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (291, 35, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (292, 35, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (293, 35, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (294, 35, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (295, 36, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (296, 36, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (297, 36, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (298, 36, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (299, 36, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (300, 36, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (301, 36, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (316, 37, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (317, 37, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (318, 37, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (319, 37, 4, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (320, 37, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (321, 37, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (322, 37, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (323, 38, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (324, 38, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (325, 38, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (326, 38, 4, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (327, 38, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (328, 38, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (329, 38, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (337, 39, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (338, 39, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (339, 39, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (340, 39, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (341, 39, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (342, 39, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (343, 39, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (344, 40, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (345, 40, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (346, 40, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (347, 40, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (348, 40, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (349, 40, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (350, 40, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (351, 41, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (352, 41, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (353, 41, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (354, 41, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (355, 41, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (356, 41, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (357, 41, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (358, 42, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (359, 42, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (360, 42, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (361, 42, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (362, 42, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (363, 42, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (364, 42, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (365, 43, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (366, 43, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (367, 43, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (368, 43, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (369, 43, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (370, 43, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (371, 43, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (372, 44, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (373, 44, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (374, 44, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (375, 44, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (376, 44, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (377, 44, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (378, 44, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (379, 45, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (380, 45, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (381, 45, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (382, 45, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (383, 45, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (384, 45, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (385, 45, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (386, 46, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (387, 46, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (388, 46, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (389, 46, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (390, 46, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (391, 46, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (392, 46, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (393, 47, 1, 1, 1)
GO
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (394, 47, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (395, 47, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (396, 47, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (397, 47, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (398, 47, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (399, 47, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (400, 49, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (401, 48, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (402, 49, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (403, 48, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (404, 48, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (405, 49, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (406, 49, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (407, 48, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (408, 48, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (409, 49, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (410, 48, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (411, 49, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (412, 48, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (413, 49, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (414, 50, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (415, 50, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (416, 50, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (417, 50, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (418, 50, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (419, 50, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (420, 50, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (421, 51, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (422, 51, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (423, 51, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (424, 51, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (425, 51, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (426, 51, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (427, 51, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (428, 52, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (429, 52, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (430, 52, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (431, 52, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (432, 52, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (433, 52, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (434, 52, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (442, 53, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (443, 53, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (444, 53, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (445, 53, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (446, 53, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (447, 53, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (448, 53, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (449, 54, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (450, 54, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (451, 54, 3, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (452, 54, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (453, 54, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (454, 54, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (455, 54, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (456, 55, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (457, 55, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (458, 55, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (459, 55, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (460, 55, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (461, 55, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (462, 55, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (463, 56, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (464, 56, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (465, 56, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (466, 56, 4, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (467, 56, 5, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (468, 56, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (469, 56, 0, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (470, 57, 1, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (471, 57, 2, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (472, 57, 3, 1, 1)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (473, 57, 4, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (474, 57, 5, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (475, 57, 6, 0, 0)
INSERT [dbo].[WorkingPatternDay] ([WorkingPatternDayId], [WorkingPatternId], [DayOfWeek], [AM], [PM]) VALUES (476, 57, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[WorkingPatternDay] OFF
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
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_AlternateIdType] FOREIGN KEY([AlternateIdTypeId])
REFERENCES [dbo].[AlternateIdType] ([AlternateIdTypeId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_AlternateIdType]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Batch]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_CasteCategory] FOREIGN KEY([CasteCategoryId])
REFERENCES [dbo].[CasteCategory] ([CasteCategoryId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_CasteCategory]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Centre]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Course]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Disability] FOREIGN KEY([DisabilityId])
REFERENCES [dbo].[Disability] ([DisabilityId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Disability]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_District]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_DistrictCommunication] FOREIGN KEY([CommunicationDistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_DistrictCommunication]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Enquiry]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Organisation]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Qualification] FOREIGN KEY([QualificationId])
REFERENCES [dbo].[Qualification] ([QualificationId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Qualification]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Religion] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[Religion] ([ReligionId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Religion]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Scheme]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_SchemeType] FOREIGN KEY([SchemeTypeId])
REFERENCES [dbo].[SchemeType] ([SchemeTypeId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_SchemeType]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Sector]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_State]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_StateCommunication] FOREIGN KEY([CommunicationStateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_StateCommunication]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_SubSector] FOREIGN KEY([SubSectorId])
REFERENCES [dbo].[SubSector] ([SubSectorId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_SubSector]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Taluka] FOREIGN KEY([TalukaId])
REFERENCES [dbo].[Taluka] ([TalukaId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Taluka]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_TalukaCommunication] FOREIGN KEY([CommunicationTalukaId])
REFERENCES [dbo].[Taluka] ([TalukaId])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_TalukaCommunication]
GO
ALTER TABLE [dbo].[AlternateIdType]  WITH CHECK ADD  CONSTRAINT [FK_AlternateIdType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[AlternateIdType] CHECK CONSTRAINT [FK_AlternateIdType_Organisation]
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
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Organisation]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Personnel]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Scheme]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Sector]
GO
ALTER TABLE [dbo].[BatchTimePrefer]  WITH CHECK ADD  CONSTRAINT [FK_BatchTimePrefer_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[BatchTimePrefer] CHECK CONSTRAINT [FK_BatchTimePrefer_Organisation]
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
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Enquiry]
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
ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Sector]
GO
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Organisation]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Organisation]
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
ALTER TABLE [dbo].[Disability]  WITH CHECK ADD  CONSTRAINT [FK_Disability_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Disability] CHECK CONSTRAINT [FK_Disability_Organisation]
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
ALTER TABLE [dbo].[EmploymentTeam]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentTeam_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EmploymentTeam] CHECK CONSTRAINT [FK_EmploymentTeam_Organisation]
GO
ALTER TABLE [dbo].[EmploymentTeam]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentTeam_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[EmploymentTeam] CHECK CONSTRAINT [FK_EmploymentTeam_Team]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_BatchTimePrefer] FOREIGN KEY([BatchTimePreferId])
REFERENCES [dbo].[BatchTimePrefer] ([BatchTimePreferId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_BatchTimePrefer]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_CasteCategory] FOREIGN KEY([CasteCategoryId])
REFERENCES [dbo].[CasteCategory] ([CasteCategoryId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_CasteCategory]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Centre]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Course] FOREIGN KEY([IntrestedCourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Course]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Course2] FOREIGN KEY([CourseOfferedId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Course2]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Enquiry]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_EnquiryType] FOREIGN KEY([EnquiryTypeId])
REFERENCES [dbo].[EnquiryType] ([EnquiryTypeId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_EnquiryType]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_HowDidYouKnowAbout] FOREIGN KEY([HowDidYouKnowAboutId])
REFERENCES [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_HowDidYouKnowAbout]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Occupation] FOREIGN KEY([OccupationId])
REFERENCES [dbo].[Occupation] ([OccupationId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Occupation]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Organisation]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Qualification] FOREIGN KEY([EducationalQualificationId])
REFERENCES [dbo].[Qualification] ([QualificationId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Qualification]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Religion] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[Religion] ([ReligionId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Religion]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Scheme]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_Sector]
GO
ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_Enquiry_StudentType] FOREIGN KEY([StudentTypeId])
REFERENCES [dbo].[StudentType] ([StudentTypeId])
GO
ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_Enquiry_StudentType]
GO
ALTER TABLE [dbo].[EnquiryType]  WITH CHECK ADD  CONSTRAINT [FK_EnquiryType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EnquiryType] CHECK CONSTRAINT [FK_EnquiryType_Organisation]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Centre]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Organisation]
GO
ALTER TABLE [dbo].[FollowUp]  WITH CHECK ADD  CONSTRAINT [FK_FollowUp_Course] FOREIGN KEY([IntrestedCourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[FollowUp] CHECK CONSTRAINT [FK_FollowUp_Course]
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
ALTER TABLE [dbo].[Host]  WITH CHECK ADD  CONSTRAINT [FK_Host_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Host] CHECK CONSTRAINT [FK_Host_Organisation]
GO
ALTER TABLE [dbo].[HowDidYouKnowAbout]  WITH CHECK ADD  CONSTRAINT [FK_HowDidYouKnowAboutUs_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[HowDidYouKnowAbout] CHECK CONSTRAINT [FK_HowDidYouKnowAboutUs_Organisation]
GO
ALTER TABLE [dbo].[Mobilization]  WITH CHECK ADD  CONSTRAINT [FK_Mobilization_Centre] FOREIGN KEY([InterestedCourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Mobilization] CHECK CONSTRAINT [FK_Mobilization_Centre]
GO
ALTER TABLE [dbo].[Mobilization]  WITH CHECK ADD  CONSTRAINT [FK_Mobilization_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[Mobilization] CHECK CONSTRAINT [FK_Mobilization_Event]
GO
ALTER TABLE [dbo].[Mobilization]  WITH CHECK ADD  CONSTRAINT [FK_Mobilization_Mobilization] FOREIGN KEY([MobilizationId])
REFERENCES [dbo].[Mobilization] ([MobilizationId])
GO
ALTER TABLE [dbo].[Mobilization] CHECK CONSTRAINT [FK_Mobilization_Mobilization]
GO
ALTER TABLE [dbo].[Mobilization]  WITH CHECK ADD  CONSTRAINT [FK_Mobilization_MobilizationType] FOREIGN KEY([MobilizationTypeId])
REFERENCES [dbo].[MobilizationType] ([MobilizationTypeId])
GO
ALTER TABLE [dbo].[Mobilization] CHECK CONSTRAINT [FK_Mobilization_MobilizationType]
GO
ALTER TABLE [dbo].[Mobilization]  WITH CHECK ADD  CONSTRAINT [FK_Mobilization_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Mobilization] CHECK CONSTRAINT [FK_Mobilization_Organisation]
GO
ALTER TABLE [dbo].[Mobilization]  WITH CHECK ADD  CONSTRAINT [FK_Mobilization_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[Mobilization] CHECK CONSTRAINT [FK_Mobilization_Personnel]
GO
ALTER TABLE [dbo].[MobilizationType]  WITH CHECK ADD  CONSTRAINT [FK_MobilizationType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[MobilizationType] CHECK CONSTRAINT [FK_MobilizationType_Organisation]
GO
ALTER TABLE [dbo].[Occupation]  WITH CHECK ADD  CONSTRAINT [FK_Occupation_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Occupation] CHECK CONSTRAINT [FK_Occupation_Organisation]
GO
ALTER TABLE [dbo].[Personnel]  WITH CHECK ADD  CONSTRAINT [FK_Personnel_Employment] FOREIGN KEY([CurrentEmploymentId])
REFERENCES [dbo].[Employment] ([EmploymentId])
GO
ALTER TABLE [dbo].[Personnel] CHECK CONSTRAINT [FK_Personnel_Employment]
GO
ALTER TABLE [dbo].[Personnel]  WITH CHECK ADD  CONSTRAINT [FK_Personnel_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Personnel] CHECK CONSTRAINT [FK_Personnel_Organisation]
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelAbsenceEntitlement_AbsencePolicyPeriod] FOREIGN KEY([AbsencePolicyPeriodId])
REFERENCES [dbo].[AbsencePolicyPeriod] ([AbsencePolicyPeriodId])
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement] CHECK CONSTRAINT [FK_PersonnelAbsenceEntitlement_AbsencePolicyPeriod]
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelAbsenceEntitlement_AbsenceType] FOREIGN KEY([AbsenceTypeId])
REFERENCES [dbo].[AbsenceType] ([AbsenceTypeId])
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement] CHECK CONSTRAINT [FK_PersonnelAbsenceEntitlement_AbsenceType]
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelAbsenceEntitlement_Employment] FOREIGN KEY([EmploymentId])
REFERENCES [dbo].[Employment] ([EmploymentId])
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement] CHECK CONSTRAINT [FK_PersonnelAbsenceEntitlement_Employment]
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelAbsenceEntitlement_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([FrequencyId])
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement] CHECK CONSTRAINT [FK_PersonnelAbsenceEntitlement_Frequency]
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelAbsenceEntitlement_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement] CHECK CONSTRAINT [FK_PersonnelAbsenceEntitlement_Organisation]
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelAbsenceEntitlement_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO
ALTER TABLE [dbo].[PersonnelAbsenceEntitlement] CHECK CONSTRAINT [FK_PersonnelAbsenceEntitlement_Personnel]
GO
ALTER TABLE [dbo].[PublicHoliday]  WITH CHECK ADD  CONSTRAINT [FK_PublicHoliday_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[PublicHoliday] CHECK CONSTRAINT [FK_PublicHoliday_Organisation]
GO
ALTER TABLE [dbo].[PublicHoliday]  WITH CHECK ADD  CONSTRAINT [FK_PublicHoliday_PublicHolidayPolicy] FOREIGN KEY([PublicHolidayPolicyId])
REFERENCES [dbo].[PublicHolidayPolicy] ([PublicHolidayPolicyId])
GO
ALTER TABLE [dbo].[PublicHoliday] CHECK CONSTRAINT [FK_PublicHoliday_PublicHolidayPolicy]
GO
ALTER TABLE [dbo].[PublicHolidayPolicy]  WITH CHECK ADD  CONSTRAINT [FK_PublicHolidayPolicy_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[PublicHolidayPolicy] CHECK CONSTRAINT [FK_PublicHolidayPolicy_Organisation]
GO
ALTER TABLE [dbo].[Qualification]  WITH CHECK ADD  CONSTRAINT [FK_Qualification_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Qualification] CHECK CONSTRAINT [FK_Qualification_Organisation]
GO
ALTER TABLE [dbo].[Religion]  WITH CHECK ADD  CONSTRAINT [FK_Religion_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Religion] CHECK CONSTRAINT [FK_Religion_Organisation]
GO
ALTER TABLE [dbo].[Scheme]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Scheme] CHECK CONSTRAINT [FK_Scheme_Organisation]
GO
ALTER TABLE [dbo].[Scheme]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_SchemeType] FOREIGN KEY([SchemeTypeId])
REFERENCES [dbo].[SchemeType] ([SchemeTypeId])
GO
ALTER TABLE [dbo].[Scheme] CHECK CONSTRAINT [FK_Scheme_SchemeType]
GO
ALTER TABLE [dbo].[SchemeType]  WITH CHECK ADD  CONSTRAINT [FK_SchemeType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[SchemeType] CHECK CONSTRAINT [FK_SchemeType_Organisation]
GO
ALTER TABLE [dbo].[Sector]  WITH CHECK ADD  CONSTRAINT [FK_Sector_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Sector] CHECK CONSTRAINT [FK_Sector_Organisation]
GO
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FK_Site_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FK_Site_Country]
GO
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FK_Site_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FK_Site_Organisation]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_State_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_State_Organisation]
GO
ALTER TABLE [dbo].[StudentType]  WITH CHECK ADD  CONSTRAINT [FK_StudentType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[StudentType] CHECK CONSTRAINT [FK_StudentType_Organisation]
GO
ALTER TABLE [dbo].[SubSector]  WITH CHECK ADD  CONSTRAINT [FK_SubSector_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[SubSector] CHECK CONSTRAINT [FK_SubSector_Organisation]
GO
ALTER TABLE [dbo].[SubSector]  WITH CHECK ADD  CONSTRAINT [FK_SubSector_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO
ALTER TABLE [dbo].[SubSector] CHECK CONSTRAINT [FK_SubSector_Sector]
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
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Colour] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([ColourId])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Colour]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Organisation]
GO
ALTER TABLE [dbo].[WorkingPattern]  WITH CHECK ADD  CONSTRAINT [FK_WorkingPattern_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[WorkingPattern] CHECK CONSTRAINT [FK_WorkingPattern_Organisation]
GO
ALTER TABLE [dbo].[WorkingPatternDay]  WITH CHECK ADD  CONSTRAINT [FK_WorkingPatternDay_WorkingPattern] FOREIGN KEY([WorkingPatternId])
REFERENCES [dbo].[WorkingPattern] ([WorkingPatternId])
GO
ALTER TABLE [dbo].[WorkingPatternDay] CHECK CONSTRAINT [FK_WorkingPatternDay_WorkingPattern]
GO
ALTER TABLE [dbo].[AbsenceDay]  WITH CHECK ADD  CONSTRAINT [CK_AbsenceDay_AMPMDuration] CHECK  ((([AM]=(1) OR [PM]=(1)) AND ([Duration]=(0.5) OR [Duration]=(1))))
GO
ALTER TABLE [dbo].[AbsenceDay] CHECK CONSTRAINT [CK_AbsenceDay_AMPMDuration]
GO
/****** Object:  StoredProcedure [dbo].[SearchAdmission]    Script Date: 26/02/2017 06:00:08 PM ******/
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
	  AdmissionId,
	  EnquiryId,
      StudentCode,
      Salutation,
      FirstName,
      MiddleName,
      LastName,
      Mobile,
      LandlineNo,
      EmailId,
      DateOfBirth,
      YearOfBirth,
      Gender,
      FatherName,
      FatherMobile,
      CasteCategoryId,
      ReligionId,
      Address,
      TalukaId,
      DistrictId,
      StateId,
      PinCode,
      CommunicationAddress,
      CommunicationTalukaId,
      CommunicationDistrictId,
      CommunicationStateId,
      CommunicationPinCode,
      DisabilityId,
      AadhaarNo,
      AadhaarVerificationStatus,
      AlternateIdTypeId,
      AlternateIdNumber,
      NameAsInBank,
      BankAccountNo,
      IfscCode,
      BankName,
      QualificationId,
      ProfessionalQualification,
      TechnicalQualification,
      PreTrainingStatus,
      YearOfExperience,
      EmploymentStatus,
      EmployerName,
      EmployerContactNo,
      EmployerAddress,
      AnnualIncome,
      SchemeId,
      SchemeTypeId,
      TrainingType,
      SectorId,
      SubSectorId,
      WhereDidYouHearAboutTheSchemeId,
      ConveyanceAndBoardingPreference,
      CourseId,
      CourseFees,
      PaymentType,
      DurationOfCourse,
      CentreId,
      BatchId,
      OrganisationId,
      AdmissionDate,
      TcName,
      TcId,
      PartnerName,
      TcAddress,
      SdmsCandidateId,
	  SearchField
	  
	  	FROM 
		[AdmissionSearchField]

		WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END




GO
/****** Object:  StoredProcedure [dbo].[SearchCounselling]    Script Date: 26/02/2017 06:00:08 PM ******/
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
      CentreId,
      OrganisationId,
      PersonnelId,
      CourseOfferedId,
      PreferTiming,
      Remarks,
      FollowUpDate,
      RemarkByBranchManager,
      Name,
      SectorId,
      PsychomatricTest,
	  SearchField

	FROM 
		[CounsellingSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END






GO
/****** Object:  StoredProcedure [dbo].[SearchEnquiry]    Script Date: 26/02/2017 06:00:08 PM ******/
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
	CandidateName,
	ContactNo,
	EmailId,
	Age,
	Address,
	GuardianName,
	GuardianContactNo,
	OccupationId,
	ReligionId,
	CasteCategoryId,
	Gender,
	EducationalQualificationId,
	YearOFPassOut,
	Marks,
	IntrestedCourseId,
	HowDidYouKnowAboutId,
	PreTrainingStatus,
	EmploymentStatus,
	Promotional,
	EnquiryDate,
	Place,
	CounselledBy,
	CourseOfferedId,
	PreferTiming,
	Remarks,
	CentreId,
	OrganisationId,
	FollowUpDate,
	EnquiryStatus,
	SearchField
	FROM 
		[EnquirySearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END



GO
/****** Object:  StoredProcedure [dbo].[SearchMobilization]    Script Date: 26/02/2017 06:00:08 PM ******/
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
		MobilizationId,
		EventId,
		OrganisationId,
		CentreId,
		Name,
		Mobile,
		InterestedCourseId,
		QualificationId,
		CreatedDate,
		FollowUpDate,
		Remark,
		MobilizerStatus,
		[Mobilized By],
		StudentLocation,
		OtherInterestedCourse,
		GeneratedDate,
		SearchField
	FROM 
		[MobilizationSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END



GO
/****** Object:  StoredProcedure [dbo].[SearchPersonnel]    Script Date: 26/02/2017 06:00:08 PM ******/
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
		SearchField
	FROM 
		[PersonnelSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END






GO
USE [master]
GO
ALTER DATABASE [Nidan_Dev] SET  READ_WRITE 
GO
