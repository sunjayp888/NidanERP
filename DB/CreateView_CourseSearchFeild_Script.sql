USE [NidanProd]
GO

/****** Object:  View [dbo].[CourseSearchField]    Script Date: 06/04/2017 4:33:27 PM ******/
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

ISNULL(C.Name, '')+ISNULL(S.Name, '')+ISNULL(SC.Name, '') AS SearchField
FROM 
	Course C  WITH (NOLOCK) left join Sector S WITH (NOLOCK)
	on C.SectorId = S.SectorId join Scheme SC WITH (NOLOCK)
	on C.SchemeId=SC.SchemeId


GO