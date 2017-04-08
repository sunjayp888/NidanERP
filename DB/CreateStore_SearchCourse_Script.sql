USE [NidanProd]
GO

/****** Object:  StoredProcedure [dbo].[SearchCourse]    Script Date: 06/04/2017 4:40:26 PM ******/
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
		SearchField

	FROM 
		[CourseSearchField]
	WHERE  
		ISNULL(@SearchKeyword, '') = '' 
	OR  
		SearchField Like '%' + @SearchKeyword + '%' 
  END


GO