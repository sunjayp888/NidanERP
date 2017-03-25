USE [NidanProd]
GO

/****** Object:  StoredProcedure [dbo].[SearchTrainer]    Script Date: 22/03/2017 04:56:15 PM ******/
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
      Name,
      Gender,
      AadharNo,
      Mobile,
      EmailId,
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


