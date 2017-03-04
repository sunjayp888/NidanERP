USE [Nidan_Dev]
GO

/****** Object:  View [dbo].[CommercialAdmissionSearchField]    Script Date: 04/03/2017 02:40:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CommercialAdmissionSearchField]
AS 
SELECT 
      C.CommercialAdmissionId,
      C.AdmissionDate,
      C.OrganisationId,
      C.CentreId,
      C.EnquiryId,
      C.BatchId,
      C.SectorId,
      C.CourseId,
      C.StudentTypeId,
      C.Salutation,
      C.FirstName,
      C.MiddleName,
      C.LastName,
      C.Mobile,
      C.EmailId,
      C.DateOfBirth,
      C.Gender,
      C.CasteCategoryId,
      C.ReligionId,
      C.FatherName,
      C.FatherMobile,
      C.ResidentialNo,
      C.Address,
      C.TalukaId,
      C.DistrictId,
      C.StateId,
      C.PinCode,
      C.CommunicationAddress,
      C.CommunicationTalukaId,
      C.CommunicationDistrictId,
      C.CommunicationStateId,
      C.CommunicationPinCode,
      C.QualificationId,
      C.ProfessionalQualification,
      C.TechnicalQualification,
      C.PreTrainingStatus,
      C.YearOfExperience,
      C.EmploymentStatus,
      C.EmployerName,
      C.EmployerContactNo,
      C.EmployerAddress,
      C.AnnualIncome,
	  ISNULL(CE.Name, '')+ISNULL(E.CandidateName, '')+ISNULL(S.Name, '')+ISNULL(CO.Name, '')
	  +ISNULL(C.FirstName, '')+ISNULL(C.LastName, '')+ CONVERT(varchar, C.Mobile )
	  +ISNULL(CC.Caste, '')+ISNULL(R.Name, '')+ISNULL(T.Name, '')+ISNULL(ST.Name, '')
	  +ISNULL(D.Name, '')+ISNULL(Q.Name, '')
	  + ISNULL(CONVERT(varchar,C.AdmissionDate, 101), '') + ISNULL(CONVERT(varchar,C.AdmissionDate, 103), '') 
	  + ISNULL(CONVERT(varchar,C.AdmissionDate, 105), '') + ISNULL(CONVERT(varchar,C.AdmissionDate, 126), '')AS SearchField
	 
FROM 

	   CommercialAdmission C WITH (NOLOCK) left join Centre  CE WITH (NOLOCK)
	   ON C.CentreId=CE.CentreId join Enquiry  E WITH (NOLOCK)
	   ON E.EnquiryId=C.EnquiryId join Sector  S WITH (NOLOCK)
	   ON S.SectorId=C.SectorId join Course  CO WITH (NOLOCK)
	   ON CO.CourseId=C.CourseId join CasteCategory  CC WITH (NOLOCK)
	   ON CC.CasteCategoryId=C.CasteCategoryId join Religion  R WITH (NOLOCK)
	   ON R.ReligionId=C.ReligionId join Taluka  T WITH (NOLOCK)
	   ON T.TalukaId=C.TalukaId join State  ST WITH (NOLOCK)
	   ON ST.StateId=C.StateId join District  D WITH (NOLOCK)
	   ON D.DistrictId=C.DistrictId join Qualification  Q WITH (NOLOCK)
	   ON Q.QualificationId=C.QualificationId


GO


