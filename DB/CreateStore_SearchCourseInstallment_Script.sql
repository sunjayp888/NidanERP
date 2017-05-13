USE [NidanERP]
GO

/****** Object:  StoredProcedure [dbo].[SearchCourseInstallment]    Script Date: 13/05/2017 11:53:36 AM ******/
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