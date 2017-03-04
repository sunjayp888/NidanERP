USE [Nidan_Dev]
GO

/****** Object:  StoredProcedure [dbo].[SearchCommercialAdmission]    Script Date: 04/03/2017 02:45:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SearchCommercialAdmission]
	@SearchKeyword nvarchar(100)
AS
BEGIN
	SET @SearchKeyword = REPLACE(@SearchKeyword, ' ', '%')
		
	SELECT
	  CommercialAdmissionId,
      AdmissionDate,
      OrganisationId,
      CentreId,
      EnquiryId,
      BatchId,
      SectorId,
      CourseId,
      StudentTypeId,
      Salutation,
      FirstName,
      MiddleName,
      LastName,
      Mobile,
      EmailId,
      DateOfBirth,
      Gender,
      CasteCategoryId,
      ReligionId,
      FatherName,
      FatherMobile,
      ResidentialNo,
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
	  SearchField
	FROM 
		[CommercialAdmissionSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END





GO


